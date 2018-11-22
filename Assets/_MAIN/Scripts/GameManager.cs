using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Unity.Rendering;
using UnityEngine.SceneManagement;

using UnityRandom = UnityEngine.Random; //AMBIGUOUS ISSUE

namespace Javatale.Prototype 
{
	public sealed class GameManager 
	{
		public static EntityArchetype playerArchetype;
		public static EntityArchetype beeEnemyArchetype;
		public static EntityArchetype playerAttackArchetype;

		public static JavataleSettings settings;

		public static List<float3> entitiesPos;
		public static List<EntryPlayerAnim> entitiesPlayerAnim;
		public static List<EntryProjectileAnim> entitiesProjectileAnim;
		public static List<EntryBeeAnim> entitiesBeeAnim;

		#region Empty List
		public static List<int> emptyPosIndexes;
		public static List<int> emptyProjectileAnimIndexes;
		#endregion

		[RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void Initialize () 
		{
			EntityManager manager = World.Active.GetOrCreateManager<EntityManager>();

			playerArchetype = manager.CreateArchetype(
				typeof(Player),
				typeof(Position),
				typeof(Rotation),
				typeof(MoveDirection),
				typeof(FaceDirection),
				typeof(MoveSpeed),
				typeof(Parent),
				typeof(PlayerInputDirection),
				typeof(PlayerInputAttack)

				// typeof(MeshInstanceRenderer)
			);

			beeEnemyArchetype = manager.CreateArchetype(
				typeof(Bee),
				typeof(Position),
				typeof(Rotation),
				typeof(MoveDirection),
				typeof(FaceDirection),
				typeof(MoveSpeed),
				typeof(Parent),
				typeof(EnemyAI)

				// typeof(MeshInstanceRenderer)
			);

			playerAttackArchetype = manager.CreateArchetype(
				typeof(PlayerAttackSpawnData)
			);

			entitiesPos = new List<float3>();
			entitiesPlayerAnim = new List<EntryPlayerAnim>();
			entitiesProjectileAnim = new List<EntryProjectileAnim>();
			entitiesBeeAnim = new List<EntryBeeAnim>();

			emptyPosIndexes = new List<int>();
			emptyProjectileAnimIndexes = new List<int>();
		}

		public static void NewGame () 
		{
			AddPlayer();
			AddEnemy(settings.maxEnemy);
		}

		[RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType.AfterSceneLoad)]
		public static void InitializeAfterSceneLoad () 
		{
			GameObject settingGO = GameObject.FindGameObjectWithTag("Settings");
			if (settingGO != null) 
			{
				InitializeWithScene();
			}
			else
			{
				SceneManager.sceneLoaded += OnSceneLoaded;
			}
		}

		public static void InitializeWithScene ()
		{
			GameObject settingGO = GameObject.FindGameObjectWithTag("Settings");
			
			if (settingGO != null) 
			{
				settings = settingGO?.GetComponent<JavataleSettings>();

				if (settings != null)
				{
					NewGame();
				}
			}
			else
			{
				SceneManager.sceneLoaded += OnSceneLoaded;
			}
		}

		static void OnSceneLoaded (Scene scene, LoadSceneMode loadSceneMode)
		{
			InitializeWithScene();
		}

		static void AddPlayer ()
		{
			EntityManager manager = World.Active.GetOrCreateManager<EntityManager>();

			Entity playerEntity = manager.CreateEntity(playerArchetype);

			float3 float3Zero = float3.zero;

			manager.SetComponentData(playerEntity, new Player { State = PlayerAnimationState.IDLE_STAND, StartAnimationToggle = 1, EndAnimationToggle = 0 });
			manager.SetComponentData(playerEntity, new PlayerInputDirection { Value = float3Zero });
			manager.SetComponentData(playerEntity, new Position { Value = float3Zero });
			manager.SetComponentData(playerEntity, new Rotation { Value = Quaternion.Euler(settings.worldToCameraRotation) });
			manager.SetComponentData(playerEntity, new MoveDirection { Value = float3Zero });
			manager.SetComponentData(playerEntity, new FaceDirection { Value = float3Zero, dirIndex = 0 });
			manager.SetComponentData(playerEntity, new MoveSpeed { Value = settings.playerMoveSpeed });

			// manager.SetSharedComponentData(playerEntity, 
			// new MeshInstanceRenderer 
			// {
			// 	mesh = settings.playerMesh,
			// 	material = settings.playerMaterial
			// });

			#region POS LIST
			Position playerInitPos = manager.GetComponentData<Position>(playerEntity);

			entitiesPos.Add(playerInitPos.Value); //Add Entity Position to Pos List
			
			int currentPosListIndex = entitiesPos.Count-1; //Get last List Index
			#endregion

			#region ANIM LIST
			FaceDirection playerInitFaceDir = manager.GetComponentData<FaceDirection>(playerEntity);
 			
			//Add Entity Animation State to List (AnimationToggle = 1)
			entitiesPlayerAnim.Add(new EntryPlayerAnim(playerInitFaceDir.dirIndex, playerInitFaceDir.Value, PlayerAnimationState.IDLE_STAND, 1, 0));
			
			int currentAnimListIndex = entitiesPlayerAnim.Count-1; //Get last List Index to Anim List
			#endregion

			//Set last Pos and Anim List Index to parent and its child
			manager.SetComponentData(playerEntity, new Parent { PosIndex = currentPosListIndex, AnimIndex = currentAnimListIndex });

			GameObject playerGO = GameObject.Instantiate(settings.playerChild, playerInitPos.Value, quaternion.identity);
			playerGO.GetComponent<ChildComponent>().PosIndex = currentPosListIndex;
			playerGO.GetComponent<ChildComponent>().AnimIndex = currentAnimListIndex;
		}

		static void AddEnemy (int enemyCount) {
			EntityManager manager = World.Active.GetOrCreateManager<EntityManager>();
			
			NativeArray<Entity> entities = new NativeArray<Entity>(enemyCount, Allocator.Temp);
			manager.CreateEntity(beeEnemyArchetype, entities);

			float3 float3Zero = float3.zero;
			float horBound = settings.horizontalBound;
			float verBound = settings.verticalBound;
			float enemyMoveSpeed = settings.enemyMoveSpeed;
			Vector3 worldToCameraRotation = settings.worldToCameraRotation;

			for (int i=0; i<enemyCount; i++) {
				float xVal = UnityRandom.Range(-horBound, horBound);
				float zVal = UnityRandom.Range(-verBound, verBound);
				float randomIdleTimer = UnityRandom.Range(settings.enemyMinIdleCooldown, settings.enemyMaxIdleCooldown);
				float3 randomMoveDir = new float3 (UnityRandom.Range(-1,2) == 0 ? 1f : -1f, 0f, UnityRandom.Range(-1,2) == 0 ? 1f : -1f);

				manager.SetComponentData(entities[i], new Bee { State = BeeAnimationState.IDLE_FLY, AnimationToggle = 1 });
				manager.SetComponentData(entities[i], new EnemyAI { IdleTimer = randomIdleTimer, PatrolTimer = 0f });
				manager.SetComponentData(entities[i], new Position { Value = new float3(xVal, 0f, zVal) });
				manager.SetComponentData(entities[i], new Rotation { Value = Quaternion.Euler(worldToCameraRotation) });
				manager.SetComponentData(entities[i], new MoveDirection { Value = float3Zero });
				manager.SetComponentData(entities[i], new FaceDirection { Value = randomMoveDir, dirIndex = 1 });
				manager.SetComponentData(entities[i], new MoveSpeed { Value = enemyMoveSpeed });

				// manager.SetSharedComponentData(entities[i], 
				// new MeshInstanceRenderer 
				// {
				// 	mesh = settings.enemyMesh,
				// 	material = settings.enemyMaterial
				// });

				#region POSITION LIST
				Position enemyInitPos = manager.GetComponentData<Position>(entities[i]);
				GameObject enemyGO = GameObject.Instantiate(settings.beeEnemyChild, enemyInitPos.Value, quaternion.identity);

				entitiesPos.Add(enemyInitPos.Value); //Add Entity Position to List

				int currentPosListIndex = entitiesPos.Count-1; //Get last List Index

				//Set last Pos List Index to parent and its child
				// manager.SetComponentData(entities[i], new Parent { PosIndex = currentPosListIndex });
				enemyGO.GetComponent<ChildComponent>().PosIndex = currentPosListIndex;
				#endregion

				#region ANIMATION LIST
				FaceDirection enemyInitDir = manager.GetComponentData<FaceDirection>(entities[i]);

				entitiesBeeAnim.Add(new EntryBeeAnim(enemyInitDir.dirIndex, BeeAnimationState.IDLE_FLY)); //Add Entity Animation State to List

				int currentAnimListIndex = entitiesBeeAnim.Count-1; //Get last List Index to Anim List

				//Set last Anim List Index to parent and its child
				enemyGO.GetComponent<ChildComponent>().AnimIndex = currentAnimListIndex;
				#endregion

				manager.SetComponentData(entities[i], new Parent { PosIndex = currentPosListIndex, AnimIndex = currentAnimListIndex });
			}

			entities.Dispose();
		}

		public void AddToList () {
			
		}
	}
}

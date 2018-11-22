using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Burst;
using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;

namespace Javatale.Prototype
{
	public class PlayerAttackDestroyerSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct Data 
		{
			public readonly int Length;
			public ComponentArray<PlayerAttackComponent> PlayerAttackComponent;
			[ReadOnlyAttribute] public ComponentArray<ChildComponent> ChildComponent;
		}
		[InjectAttribute] private Data data;

		float deltaTime;

		protected override void OnUpdate () {
			deltaTime = Time.deltaTime;

           	List<float3> listPos = GameManager.entitiesPos;
			List<int> emptyPosIndexes = GameManager.emptyPosIndexes;
			List<int> emptyProjectileAnimIndexes = GameManager.emptyProjectileAnimIndexes;
			
			for (int i=0; i<data.Length; i++)
			{
				PlayerAttackComponent playerAttackComponent = data.PlayerAttackComponent[i];
				ChildComponent childComponent = data.ChildComponent[i];
				
				float destroyDuration = playerAttackComponent.duration;
				float destroyTimer = playerAttackComponent.timer;

				if (destroyTimer >= destroyDuration)
				{
					//Add pos index to List of empty pos index
					emptyPosIndexes.Add(childComponent.PosIndex);

					//Add anim index to List of empty anim index
					emptyProjectileAnimIndexes.Add(childComponent.AnimIndex);

					// GameDebug.Log("childComponent.PosIndex "+childComponent.PosIndex+"\n listPos "+listPos[childComponent.PosIndex]);
					GameObjectEntity.Destroy(playerAttackComponent.gameObject);
					UpdateInjectedComponentGroups();

					foreach (int a in emptyPosIndexes) GameDebug.Log(a);
				}
				else
				{
					playerAttackComponent.timer += deltaTime;
				}
			}
		}
	}
}

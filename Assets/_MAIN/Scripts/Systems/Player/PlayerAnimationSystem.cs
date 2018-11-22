using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
// using Unity.Burst;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	// public class AnimationBarrier : BarrierSystem {}

	public class PlayerAnimationSystem : ComponentSystem 
	{
		// [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray AnimationEntities;
			public ComponentDataArray<Player> Player;
			public ComponentDataArray<MoveDirection> MoveDirection;
			[ReadOnlyAttribute] public ComponentDataArray<FaceDirection> FaceDirection;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
		}
		[InjectAttribute] private Data data;

		// Vector3 vector3Zero = Vector3.zero;
		float3 float3Zero = float3.zero;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
            List<EntryPlayerAnim> listAnim = GameManager.entitiesPlayerAnim;
			int maxPlayerAttackIndex = GameManager.settings.maxPlayerAttackIndex;

			for (int i=0; i<data.Length; i++)
			{
				Entity animEntity = data.AnimationEntities[i];
				Player player = data.Player[i];
				MoveDirection moveDir = data.MoveDirection[i];
				FaceDirection faceDir = data.FaceDirection[i];
				Parent parent = data.Parent[i];

				// PlayerAnimationState state = player.State;
				// Vector3 direction = moveDir.Value;
				int animIndex = parent.AnimIndex;
				EntryPlayerAnim entryPlayerAnim = listAnim[animIndex];
				
				int playerStartAnimToggle = player.StartAnimationToggle;
				int playerEndAnimToggle = player.EndAnimationToggle;
				int playerAnimToggleValue = player.AnimationToggleValue;
				// PlayerAnimationState state = player.State;
				int dirIndex = faceDir.dirIndex;
				float3 faceDirValue = faceDir.Value;

#region START ANIMATION
				if (playerStartAnimToggle != 0) 
				{
					switch (playerStartAnimToggle) 
					{
						case 1:
							commandBuffer.AddComponent(animEntity, new AnimationPlayerIdleStand{});
							
							break;
						case 2:
							commandBuffer.AddComponent(animEntity, new AnimationPlayerMoveRun{});
						
							break;
						case 3:
							//
							GameDebug.Log("ADD COMPONENT GET HURT");
						
							break;
						case 21:
							commandBuffer.AddComponent(animEntity, new AnimationPlayerAttack1{});

							moveDir.Value = float3Zero;
							data.MoveDirection[i] = moveDir;

							break;
						case 22:
							commandBuffer.AddComponent(animEntity, new AnimationPlayerAttack2{});

							moveDir.Value = float3Zero;
							data.MoveDirection[i] = moveDir;

							break;
						case 23:
							commandBuffer.AddComponent(animEntity, new AnimationPlayerAttack3{});

							moveDir.Value = float3Zero;
							data.MoveDirection[i] = moveDir;

							break;
					}
                
					//SET LIST
					// int endAnimToggle = listAnim[animIndex].EndAnimationToggle;
					entryPlayerAnim.DirIndex = dirIndex;
					entryPlayerAnim.FaceDirValue = faceDirValue;
					// entryPlayerAnim.State = state;
					// entryPlayerAnim.StartAnimationToggle = 0;

					// listAnim[animIndex] = new EntryPlayerAnim(dirIndex, faceDirValue, state, 0, endAnimToggle);
					listAnim[animIndex] = entryPlayerAnim;	

					//SET TO PLAYER (PARENT)						
					player.StartAnimationToggle = 0;
					data.Player[i] = player;
				}
#endregion 

#region END ANIMATION
				else if (playerEndAnimToggle != 0) 
				{
					switch (playerEndAnimToggle) 
					{
						case 1:				
							if (playerAnimToggleValue == 0)
								commandBuffer.AddComponent(animEntity, new AnimationPlayerIdleStand{});
							
							break;
						case 2:
							commandBuffer.AddComponent(animEntity, new PlayerInputDirection{});
							commandBuffer.AddComponent(animEntity, new PlayerInputAttack{});

							int attackIndex = player.AttackIndex >= maxPlayerAttackIndex ? 0 : player.AttackIndex+1;
							player.AttackIndex = attackIndex;
							player.AnimationToggleValue = 0;
							// data.Player[i] = player;
							
							break;
					}
                
					//SET LIST
					// int startAnimToggle = listAnim[animIndex].StartAnimationToggle;
					// entryPlayerAnim.State = state;
					entryPlayerAnim.EndAnimationToggle = 0;

					// listAnim[animIndex] = new EntryPlayerAnim(dirIndex, faceDirValue, state, startAnimToggle, 0);
					listAnim[animIndex] = entryPlayerAnim;	

					//SET TO PLAYER (PARENT)						
					player.EndAnimationToggle = 0;
					data.Player[i] = player;
				}
#endregion
			}
		}	
	}
}

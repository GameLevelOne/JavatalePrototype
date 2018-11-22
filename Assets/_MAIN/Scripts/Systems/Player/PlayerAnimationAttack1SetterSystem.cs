using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
using Unity.Burst;
// using Unity.Mathematics;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerAnimationAttack1SetterSystem : ComponentSystem 
	{
        [BurstCompileAttribute]
		public struct ParentData
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray AnimationIdleEntities;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
			public ComponentDataArray<Player> Player;
			[ReadOnlyAttribute] public ComponentDataArray<FaceDirection> FaceDirection;
			[ReadOnlyAttribute] public ComponentDataArray<AnimationPlayerAttack1> AnimationPlayerAttack1;
		}
		[InjectAttribute] private ParentData parentData;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
            List<EntryPlayerAnim> listAnim = GameManager.entitiesPlayerAnim;

			for (int i=0; i<parentData.Length; i++) {
				Entity animEntity = parentData.AnimationIdleEntities[i];
				Parent parent = parentData.Parent[i];
				Player player = parentData.Player[i];
				FaceDirection faceDir = parentData.FaceDirection[i];

				commandBuffer.RemoveComponent<AnimationPlayerAttack1>(animEntity);
				commandBuffer.RemoveComponent<PlayerInputDirection>(animEntity);
				commandBuffer.RemoveComponent<PlayerInputAttack>(animEntity);
                
				//SET LIST
				// int dirIndex = faceDir.dirIndex;
				// float3 faceDirValue = faceDir.Value;
				PlayerAnimationState state = PlayerAnimationState.ATTACK_1;
				// int endAnimToggle = listAnim[parent.AnimIndex].EndAnimationToggle;

				EntryPlayerAnim entryPlayerAnim = listAnim[parent.AnimIndex];
				entryPlayerAnim.State = state;
				entryPlayerAnim.StartAnimationToggle = 21;

				// listAnim[parent.AnimIndex] = new EntryPlayerAnim(dirIndex, faceDirValue, state, 21, endAnimToggle);
				listAnim[parent.AnimIndex] = entryPlayerAnim;

				//SET TO PLAYER (PARENT)
				player.AnimationToggleValue = 1;		
				player.State = state;
				parentData.Player[i] = player;
			}
		}
	}
}
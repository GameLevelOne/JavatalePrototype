using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
using Unity.Burst;
// using Unity.Mathematics;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerAnimationIdleSetterSystem : ComponentSystem 
	{
        [BurstCompileAttribute]
		public struct ParentData
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray AnimationIdleEntities;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
			public ComponentDataArray<Player> Player;
			[ReadOnlyAttribute] public ComponentDataArray<FaceDirection> FaceDirection;
			[ReadOnlyAttribute] public ComponentDataArray<AnimationPlayerIdleStand> AnimationPlayerIdle;
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

				commandBuffer.RemoveComponent<AnimationPlayerIdleStand>(animEntity);
                
				//SET LIST
				// int dirIndex = faceDir.dirIndex;
				// float3 faceDirValue = faceDir.Value;
				PlayerAnimationState state = PlayerAnimationState.IDLE_STAND;
				// int endAnimToggle = listAnim[parent.AnimIndex].EndAnimationToggle;

				EntryPlayerAnim entryPlayerAnim = listAnim[parent.AnimIndex];
				entryPlayerAnim.State = state;
				entryPlayerAnim.StartAnimationToggle = 1;

				// listAnim[parent.AnimIndex] = new EntryPlayerAnim(dirIndex, faceDirValue, state, 1, endAnimToggle);
				listAnim[parent.AnimIndex] = entryPlayerAnim;
				// GameDebug.Log("Set : "+listAnim[parent.AnimIndex].State+"\n StartAnimationToggle : "+listAnim[parent.AnimIndex].StartAnimationToggle);

				//SET TO PLAYER (PARENT)	
				player.AttackIndex = 0;	
				player.State = state;
				parentData.Player[i] = player;
			}
		}
	}
}
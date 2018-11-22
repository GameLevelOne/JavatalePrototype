using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
using Unity.Burst;
// using Unity.Mathematics;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerAnimationMoveRunSetterSystem : ComponentSystem 
	{
        [BurstCompileAttribute]
		public struct ParentData
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray AnimationRunEntities;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
			public ComponentDataArray<Player> Player;
			[ReadOnlyAttribute] public ComponentDataArray<FaceDirection> FaceDirection;
			[ReadOnlyAttribute] public ComponentDataArray<AnimationPlayerMoveRun> AnimationPlayerRun;
		}
		[InjectAttribute] private ParentData parentData;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
            List<EntryPlayerAnim> listAnim = GameManager.entitiesPlayerAnim;

			for (int i=0; i<parentData.Length; i++) {
				Entity animEntity = parentData.AnimationRunEntities[i];
				Parent parent = parentData.Parent[i];
				Player player = parentData.Player[i];
				FaceDirection faceDir = parentData.FaceDirection[i];

				commandBuffer.RemoveComponent<AnimationPlayerMoveRun>(animEntity);
                
				//SET LIST
				// int dirIndex = faceDir.dirIndex;
				// float3 faceDirValue = faceDir.Value;
				PlayerAnimationState state = PlayerAnimationState.MOVE_RUN;
				// int endAnimToggle = listAnim[parent.AnimIndex].EndAnimationToggle;

				EntryPlayerAnim entryPlayerAnim = listAnim[parent.AnimIndex];
				entryPlayerAnim.State = state;
				entryPlayerAnim.StartAnimationToggle = 2;

				// listAnim[parent.AnimIndex] = new EntryPlayerAnim(dirIndex, faceDirValue, state, 2, endAnimToggle);
				listAnim[parent.AnimIndex] = entryPlayerAnim;	
				// GameDebug.Log("Set : "+listAnim[parent.AnimIndex].State+"\n StartAnimationToggle : "+listAnim[parent.AnimIndex].StartAnimationToggle);

				//SET TO PLAYER (PARENT)	
				player.State = state;
				parentData.Player[i] = player;
			}
		}
	}
}
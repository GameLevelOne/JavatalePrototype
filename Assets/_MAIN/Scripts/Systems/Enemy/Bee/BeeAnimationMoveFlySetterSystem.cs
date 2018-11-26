using Unity.Collections;
using Unity.Entities;
// using UnityEngine;
using Unity.Burst;

namespace Javatale.Prototype 
{
	public class BeeAnimationMoveFlySetterSystem : ComponentSystem 
	{
        [BurstCompileAttribute]
		public struct ParentData
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray AnimationIdleEntities;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
			[ReadOnlyAttribute] public ComponentDataArray<FaceDirection> FaceDirection;
			public ComponentDataArray<AnimationBeeMoveFly> AnimationBeeIdle;
		}
		[InjectAttribute] private ParentData parentData;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;

			for (int i=0; i<parentData.Length; i++) {
				Entity animEntity = parentData.AnimationIdleEntities[i];
				Parent parent = parentData.Parent[i];
				FaceDirection faceDir = parentData.FaceDirection[i];

				commandBuffer.RemoveComponent<AnimationBeeMoveFly>(animEntity);
				
				// GameManager.entitiesBeeAnim[parent.AnimIndex] = new EntryBeeAnim(faceDir.dirIndex, BeeAnimationState.MOVE_FLY);
			}
		}
	}
}
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Javatale.Prototype 
{
	// public class AnimationBarrier : BarrierSystem {}

	public class BeeAnimationSystem : ComponentSystem 
	{
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray AnimationEntities;
			public ComponentDataArray<EnemyAI> EnemyAI;
			[ReadOnlyAttribute] public ComponentDataArray<MoveDirection> MoveDirection;
		}
		[InjectAttribute] private Data data;

		Vector3 vectorZero = Vector3.zero;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;

			for (int i=0; i<data.Length; i++)
			{
				Entity animEntity = data.AnimationEntities[i];
				MoveDirection moveDir = data.MoveDirection[i];
				EnemyAI enemyAI = data.EnemyAI[i];

				Vector3 direction = moveDir.Value;
				int enemyAnimTogglevalue = enemyAI.AnimationToggle;

				if (enemyAnimTogglevalue > 0) 
				{
					switch (enemyAnimTogglevalue) 
					{
						case 1:
							if (direction != vectorZero)
							{
								commandBuffer.AddComponent(animEntity, new AnimationBeeMoveFly{});
							}
							else 
							{
								commandBuffer.AddComponent(animEntity, new AnimationBeeIdleFly{});
							}
						
							enemyAI.AnimationToggle = 0;
							data.EnemyAI[i] = enemyAI;
							break;
						default:
							enemyAI.AnimationToggle = 0;
							data.EnemyAI[i] = enemyAI;
							break;
					}
				}
			}
		}	
	}
}

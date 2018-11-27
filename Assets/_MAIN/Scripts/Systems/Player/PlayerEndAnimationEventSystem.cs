using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
// using Unity.Burst;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerEndAnimationSystem : ComponentSystem 
	{
		// [BurstCompileAttribute]
		public struct Data
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entities;
			[ReadOnlyAttribute] public ComponentArray<ChildComponent> Child;
			public ComponentArray<PlayerAnimatorComponent> PlayerAnimatorComponent;
			public ComponentArray<EndAnimationChildComponent> EndAnimationChildComponent;
		}
		[InjectAttribute] private Data data;

		// float3 float3Zero = float3.zero;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
			List<Entity> entitiesInGame = GameManager.entitiesInGame;
            
			int maxPlayerAttackIndex = GameManager.settings.maxPlayerAttackIndex;

			for (int i=0; i<data.Length; i++)
			{
				Entity entity = data.Entities[i];
				ChildComponent child = data.Child[i];
                PlayerAnimatorComponent playerAnimatorComponent = data.PlayerAnimatorComponent[i];
                EndAnimationChildComponent endAnimChildComponent = data.EndAnimationChildComponent[i];

				int childEntityIndex = child.EntityIndex;
				int endAnimValue = endAnimChildComponent.Value;

                commandBuffer.RemoveComponent<EndAnimationChildComponent>(entity);
				GameObject.Destroy(endAnimChildComponent);
                UpdateInjectedComponentGroups();

				commandBuffer.AddComponent(entitiesInGame[childEntityIndex], new EndAnimationEvent{ Value = endAnimValue});

                playerAnimatorComponent.isCheckOnEndAnimation = false;
            }
		}	
	}
}

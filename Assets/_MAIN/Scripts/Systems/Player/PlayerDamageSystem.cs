using Unity.Collections;
using Unity.Entities;
using UnityEngine;
// using Unity.Mathematics;
using Unity.Burst;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerDamageSystem : ComponentSystem 
	{
		
        [BurstCompileAttribute]
		public struct Data 
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entities;
			[ReadOnlyAttribute] public ComponentArray<ChildComponent> Child;
            [ReadOnlyAttribute] public ComponentArray<PlayerColliderComponent> PlayerColliderComponent;
            public ComponentArray<DamageChildComponent> DamageChildComponent;
		}
		[InjectAttribute] Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
			List<Entity> entitiesInGame = GameManager.entitiesInGame;

			for (int i=0; i<data.Length; i++) 
			{
				Entity entity = data.Entities[i];
				ChildComponent child = data.Child[i];
				DamageChildComponent damageChildComponent = data.DamageChildComponent[i];
                
				int childEntityIndex = child.EntityIndex;
				float damageValue = damageChildComponent.Value;

				commandBuffer.RemoveComponent<DamageChildComponent>(entity);
				GameObject.Destroy(damageChildComponent);
                UpdateInjectedComponentGroups();

				commandBuffer.AddComponent(entitiesInGame[childEntityIndex], new Damaged{ Value = damageValue});
			}
		}
	}
}

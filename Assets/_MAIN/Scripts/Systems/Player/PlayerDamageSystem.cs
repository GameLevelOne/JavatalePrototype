using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.Burst;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerDamageSystem : ComponentSystem 
	{
        // [BurstCompileAttribute]
		// public struct ParentData 
		// {
		// 	public readonly int Length;
		// 	[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
        //     //
		// }
		// [InjectAttribute] ParentData parentData;
		
        [BurstCompileAttribute]
		public struct ChildData 
		{
			public readonly int Length;
			[ReadOnlyAttribute] public EntityArray Entities;
			[ReadOnlyAttribute] public ComponentArray<ChildComponent> Child;
            public ComponentArray<PlayerColliderComponent> PlayerColliderComponent;
            public ComponentArray<DamageThisChildComponent> DamageThisChildComponent;
		}
		[InjectAttribute] ChildData childData;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;

			// for (int i=0; i<parentData.Length; i++) 
			// {
			// 	Parent parent = parentData.Parent[i];
                
            //     //
			// }

			for (int j=0; j<childData.Length; j++) 
			{
				Entity entity = childData.Entities[j];
				ChildComponent child = childData.Child[j];
                PlayerColliderComponent playerColliderComponent = childData.PlayerColliderComponent[j];
				DamageThisChildComponent damageThisChildComponent = childData.DamageThisChildComponent[j];
                
                int childAnimIndex = child.AnimIndex;
                bool isInitDamaged = playerColliderComponent.isInitDamaged;

				commandBuffer.RemoveComponent<DamageThisChildComponent>(entity);
				GameObject.Destroy(damageThisChildComponent);
                UpdateInjectedComponentGroups();

				GameDebug.Log("PLAYER DAMAGED");

                // if (isInitDamaged)
                // {
                //     //

                //     playerColliderComponent.isInitDamaged = false;
                // }
			}
		}
	}
}

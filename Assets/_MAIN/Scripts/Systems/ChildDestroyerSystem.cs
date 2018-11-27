using Unity.Collections;
using Unity.Entities;
// using Unity.Transforms;
// using Unity.Rendering;
using Unity.Burst;
// using UnityEngine;
using System.Collections.Generic;
// using Unity.Mathematics;

namespace Javatale.Prototype
{
	public class ChildDestroyerSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct Data 
		{
			public readonly int Length;
			[ReadOnlyAttribute] public ComponentArray<DestroyChildComponent> DestroyChildComponent;
			[ReadOnlyAttribute] public ComponentArray<ChildComponent> ChildComponent;
		}
		[InjectAttribute] private Data data;

		protected override void OnUpdate () 
		{
			EntityCommandBuffer commandBuffer = PostUpdateCommands;
			
			List<Entity> listEntities = GameManager.entitiesInGame;
			List<EntryAnimation> listAnim = GameManager.entitiesAnimation;
			
			List<int> emptyEntitiesIndexes = GameManager.emptyEntitiesIndexes;
			List<int> emptyPosIndexes = GameManager.emptyPosIndexes;
			List<int> emptyAnimIndexes = GameManager.emptyAnimIndexes;

            for (int i=0; i<data.Length; i++)
			{
				DestroyChildComponent destroyChildComponent = data.DestroyChildComponent[i];
				ChildComponent childComponent = data.ChildComponent[i];

				int childEntityIndex = childComponent.EntityIndex;
				int childPosIndex = childComponent.PosIndex;
				int childAnimIndex = childComponent.AnimIndex;
				
                //Add pos index to List of empty entity index
                emptyEntitiesIndexes.Add(childEntityIndex);
				
                //Add pos index to List of empty pos index
                emptyPosIndexes.Add(childPosIndex);

                //Add anim index to List of empty anim index
                emptyAnimIndexes.Add(childAnimIndex);


				commandBuffer.DestroyEntity(listEntities[childEntityIndex]);

                GameObjectEntity.Destroy(destroyChildComponent.gameObject);
                UpdateInjectedComponentGroups();
				
                EntryAnimation entryAnim = listAnim[childAnimIndex];
                entryAnim.EndAnimationToggle = 1;

                listAnim[childAnimIndex] = entryAnim;

				GameDebug.Log(childEntityIndex);
			}
        }
    }
}
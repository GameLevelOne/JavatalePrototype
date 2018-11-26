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
			[ReadOnlyAttribute] public ComponentArray<DestroyThisChildComponent> DestroyThisComponent;
			[ReadOnlyAttribute] public ComponentArray<ChildComponent> ChildComponent;
		}
		[InjectAttribute] private Data data;

		protected override void OnUpdate () 
		{
			List<EntryAnimation> listAnim = GameManager.entitiesAnimation;
			List<int> emptyPosIndexes = GameManager.emptyPosIndexes;
			List<int> emptyAnimIndexes = GameManager.emptyAnimIndexes;

            for (int i=0; i<data.Length; i++)
			{
				DestroyThisChildComponent destroyThisComponent = data.DestroyThisComponent[i];
				ChildComponent childComponent = data.ChildComponent[i];

				int childPosIndex = childComponent.PosIndex;
				int childAnimIndex = childComponent.AnimIndex;
				
                //Add pos index to List of empty pos index
                emptyPosIndexes.Add(childPosIndex);

                //Add anim index to List of empty anim index
                emptyAnimIndexes.Add(childAnimIndex);

                GameObjectEntity.Destroy(destroyThisComponent.gameObject);
                UpdateInjectedComponentGroups();
				
                EntryAnimation entryAnim = listAnim[childAnimIndex];
                entryAnim.EndAnimationToggle = 1;

                listAnim[childAnimIndex] = entryAnim;
			}
        }
    }
}
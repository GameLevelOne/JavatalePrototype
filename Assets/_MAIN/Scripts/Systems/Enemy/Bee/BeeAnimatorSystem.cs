using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Burst;
using Unity.Mathematics;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class BeeAnimatorSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct ChildData
		{
			public readonly int Length;
			[ReadOnlyAttribute] public ComponentArray<ChildComponent> Child;
			[ReadOnlyAttribute] public ComponentArray<BeeAnimatorComponent> Animator;
		}
		[InjectAttribute] private ChildData childData;

		protected override void OnUpdate () 
		{
            List<EntryBeeAnim> listAnim = GameManager.entitiesBeeAnim;

			for (int i=0; i<childData.Length; i++)
			{
				ChildComponent child = childData.Child[i];
				BeeAnimatorComponent anim = childData.Animator[i];

				int currentDirIndex = anim.currentDirIndex;
				BeeAnimationState currentState = anim.currentState;

				int dirIndex = listAnim[child.AnimIndex].DirIndex;
				BeeAnimationState state = listAnim[child.AnimIndex].State;

				if (state != currentState)
				{
                    anim.animator.Play(state.ToString());

					anim.currentState = state;
				}
				
				if (dirIndex != currentDirIndex) 
				{
                    float2 facingValues = float2.zero;

					switch (dirIndex) 
					{
						case 1: //DOWN
                            facingValues = new float2 (0f, -1f);
                            break;
						case 2: //LEFT
                            facingValues = new float2 (-1f, 0f);
                            break;
						case 3: //UP
                            facingValues = new float2 (0f, 1f);
                            break;
						case 4: //RIGHT
                            facingValues = new float2 (1f, 0f);
                            break;
					}

					anim.animator.SetFloat(Constants.AnimatorParameter.Float.FACE_X, facingValues.x);
                    anim.animator.SetFloat(Constants.AnimatorParameter.Float.FACE_Y, facingValues.y);
                    
					anim.currentDirIndex = dirIndex;
				}
			}
		}
	}
}
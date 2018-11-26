using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Burst;
using Unity.Mathematics;
using System.Collections.Generic;

namespace Javatale.Prototype 
{
	public class PlayerAnimatorSystem : ComponentSystem 
	{
		[BurstCompileAttribute]
		public struct ParentData
		{
			public readonly int Length;
			[ReadOnlyAttribute] public ComponentDataArray<Parent> Parent;
			public ComponentDataArray<Player> Player;
		}
		[InjectAttribute] private ParentData parentData;

		[BurstCompileAttribute]
		public struct ChildData
		{
			public readonly int Length;
			[ReadOnlyAttribute] public ComponentArray<ChildComponent> Child;
			[ReadOnlyAttribute] public ComponentArray<PlayerAnimatorComponent> Animator;
		}
		[InjectAttribute] private ChildData childData;
		
		protected override void OnUpdate () 
		{
			List<EntryAnimation> listAnim = GameManager.entitiesAnimation;
            // List<EntryPlayerAnim> listAnim = GameManager.entitiesPlayerAnim;
			List<EntryPlayerAnimState> listPlayerAnimState = GameManager.entitiesPlayerAnimState;

			for (int i=0; i<parentData.Length; i++)
			{
				Parent parent = parentData.Parent[i];
				Player player = parentData.Player[i];

				int parentIndex = parent.AnimIndex;
				EntryAnimation entryAnim = listAnim[parentIndex];

				int endAnimToggle = entryAnim.EndAnimationToggle;
				int currentEndAnimToggle = player.EndAnimationToggle;

				if (endAnimToggle != currentEndAnimToggle) 
				{
					player.EndAnimationToggle = endAnimToggle;
					parentData.Player[i] = player;
				}
			}

			string faceX = Constants.AnimatorParameter.Float.FACE_X;
			string faceY = Constants.AnimatorParameter.Float.FACE_Y;

			for (int j=0; j<childData.Length; j++)
			{
				ChildComponent child = childData.Child[j];
				PlayerAnimatorComponent anim = childData.Animator[j];

				int childIndex = child.AnimIndex;
				EntryAnimation entryAnim = listAnim[childIndex];
				float3 faceDirValue = entryAnim.FaceDirValue;

				int childAnimStateIndex = child.AnimStateIndex;
				EntryPlayerAnimState entryPlayerAnimState = listPlayerAnimState[childAnimStateIndex];
				PlayerAnimationState state = entryPlayerAnimState.State;

#region PLAY & STOP ANIMATION
				int playerStartAnimToggle = entryAnim.StartAnimationToggle;

				if (playerStartAnimToggle != 0)
				{
                    anim.animator.Play(state.ToString());

					anim.currentState = state;
					entryAnim.StartAnimationToggle = 0;
					listAnim[childIndex] = entryAnim;
				}
				else 
				{
					if (anim.isCheckOnEndAnimation) {
						entryAnim.EndAnimationToggle = 1;
						listAnim[childIndex] = entryAnim;

						anim.isCheckOnEndAnimation = false;
					}

					if (anim.isCheckOnEndSpecificAnimation) {
						entryAnim.EndAnimationToggle = 2;
						listAnim[childIndex] = entryAnim;

						anim.isCheckOnEndSpecificAnimation = false;
					}
				}
#endregion
				
#region DIRECTION
				int dirIndex = entryAnim.DirIndex;
				int currentDirIndex = anim.currentDirIndex;
				
				if (dirIndex != currentDirIndex) 
				{
					anim.animator.SetFloat(faceX, faceDirValue.x);
                    anim.animator.SetFloat(faceY, faceDirValue.z);
                    
					anim.currentDirIndex = dirIndex;
					anim.currentFaceDirValue = faceDirValue;
				}
#endregion
			}
		}
	}
}
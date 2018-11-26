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
            List<EntryPlayerAnim> listAnim = GameManager.entitiesPlayerAnim;

			for (int i=0; i<parentData.Length; i++)
			{
				Parent parent = parentData.Parent[i];
				Player player = parentData.Player[i];

				int parentIndex = parent.AnimIndex;
				EntryPlayerAnim entryPlayerAnim = listAnim[parentIndex];

				int endAnimToggle = entryPlayerAnim.EndAnimationToggle;
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
				EntryPlayerAnim entryPlayerAnim = listAnim[childIndex];

				float3 faceDirValue = entryPlayerAnim.FaceDirValue;
				PlayerAnimationState state = entryPlayerAnim.State;

#region PLAY & STOP ANIMATION
				// PlayerAnimationState currentState = anim.currentState;
				int playerStartAnimToggle = entryPlayerAnim.StartAnimationToggle;
				// int currentPlayerStartAnimToggle = anim.currentPlayerStartAnimToggle;

				if (playerStartAnimToggle != 0)
				{
                    anim.animator.Play(state.ToString());

					anim.currentState = state;
					// anim.currentPlayerStartAnimToggle = playerStartAnimToggle;
					entryPlayerAnim.StartAnimationToggle = 0;
					listAnim[childIndex] = entryPlayerAnim;
				}
				else 
				{
					// int startAnimToggle = listAnim[childIndex].StartAnimationToggle;

					if (anim.isCheckOnEndAnimation) {
						entryPlayerAnim.EndAnimationToggle = 1;
						// listAnim[childIndex] = new EntryPlayerAnim(dirIndex, faceDirValue, state, startAnimToggle, 1);
						listAnim[childIndex] = entryPlayerAnim;

						anim.isCheckOnEndAnimation = false;
					}

					if (anim.isCheckOnEndSpecificAnimation) {
						entryPlayerAnim.EndAnimationToggle = 2;
						// listAnim[childIndex] = new EntryPlayerAnim(dirIndex, faceDirValue, state, startAnimToggle, 2);
						listAnim[childIndex] = entryPlayerAnim;

						anim.isCheckOnEndSpecificAnimation = false;
					}
				}
#endregion
				
#region DIRECTION
				int dirIndex = entryPlayerAnim.DirIndex;
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
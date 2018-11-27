// using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
// using Unity.Transforms;
using Unity.Mathematics;

namespace Javatale.Prototype
{
	public class PlayerAnimatorComponent : MonoBehaviour 
	{
		public GameObjectEntity entityGO;
		public Animator animator;
		public AnimationEvent animationEvent;

		[HeaderAttribute("Current")]
		public int currentDirIndex;
		// public int currentPlayerStartAnimToggle;
		public PlayerAnimationState currentState;
		public float3 currentFaceDirValue;
		
		[SpaceAttribute(10f)]
		public bool isFinishAnyAnimation = true;
		public bool isFinishAttackAnimation = true;
		public bool isCheckOnStartAnimation = false;
		public bool isCheckOnSpawnSomethingOnAnimation = false;
		public bool isCheckOnEndSpecificAnimation = false;
		public bool isCheckOnEndAnimation = false;

		void OnEnable ()
		{
			animationEvent.OnStartAnimation += OnStartAnimation;
			animationEvent.OnSpawnSomethingOnAnimation += OnSpawnSomethingOnAnimation;
			animationEvent.OnEndSpecificAnimation += OnEndSpecificAnimation;
			animationEvent.OnEndAnimation += OnEndAnimation;
		}

		void OnDisable ()
		{
			animationEvent.OnStartAnimation -= OnStartAnimation;
			animationEvent.OnSpawnSomethingOnAnimation -= OnSpawnSomethingOnAnimation;
			animationEvent.OnEndSpecificAnimation -= OnEndSpecificAnimation;
			animationEvent.OnEndAnimation -= OnEndAnimation;
		}

		void OnStartAnimation ()
		{
			isCheckOnStartAnimation = true;
		}

		void OnSpawnSomethingOnAnimation ()
		{
			isCheckOnSpawnSomethingOnAnimation = true;
		}

		void OnEndSpecificAnimation ()
		{
			isCheckOnEndSpecificAnimation = true;
		}

		void OnEndAnimation ()
		{
			if (!isCheckOnEndAnimation) 
			{
				isCheckOnEndAnimation = true;
				
				gameObject.AddComponent<EndAnimationChildComponent>().Value = 0;
				entityGO.enabled = false;
				entityGO.enabled = true;
			}
		}
	}
}

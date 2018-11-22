// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

namespace Javatale.Prototype
{
	public class PlayerAttackComponent : MonoBehaviour 
	{
		public AnimationEvent animationEvent;

		[HeaderAttribute("Current")]
		public bool isInitDestroyed = false;
		public bool isDestroyed = false;
		
		void OnEnable ()
		{
			animationEvent.OnEndAnimation += OnEndAnimation;
		}

		void OnDisable ()
		{
			animationEvent.OnEndAnimation -= OnEndAnimation;
		}

		void OnEndAnimation ()
		{
			isInitDestroyed = true;
		}
	}
}
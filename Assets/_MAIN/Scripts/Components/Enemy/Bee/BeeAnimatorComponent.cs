using System;
using Unity.Entities;
using UnityEngine;
// using Unity.Transforms;

namespace Javatale.Prototype
{
	public class BeeAnimatorComponent : MonoBehaviour {
		public Animator animator;

		[HeaderAttribute("Current")]
		public int currentDirIndex;
		public BeeAnimationState currentState;
	}
}

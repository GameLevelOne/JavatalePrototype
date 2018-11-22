using Unity.Entities;
using UnityEngine;


namespace Javatale.Prototype 
{
	//PLAYER
	public struct Player : IComponentData
	{
		public PlayerAnimationState State;

		/// <summary>
		/// <para>Values: <br /></para>
		/// <para>-2 Reset PlayerAttackComponent<br /></para>
		/// <para>-1 Reset PlayerInputComponent<br /></para>
		/// <para>0 OFF<br /></para>
		/// <para>1 Idle Stand<br /></para>
		/// <para>2 Idle Run<br /></para>
		/// <para>21 Attack 1<br /></para>
		/// <para>22 Attack 2<br /></para>
		/// </summary>
		public int StartAnimationToggle;
		public int EndAnimationToggle;
		
		public int AttackIndex;
	}
	public class PlayerComponent : ComponentDataWrapper<Player> {}

	//PROJECTILE
	public struct Projectile : IComponentData
	{
		// public int StartAnimationToggle;
		// public int EndAnimationToggle;
		//
	}
	public class ProjectileComponent : ComponentDataWrapper<Projectile> {}
    
	//BEE
	public struct Bee : IComponentData
	{
		public BeeAnimationState State;
		public int AnimationToggle;
	}
	public class BeeComponent : ComponentDataWrapper<Bee> {}
}

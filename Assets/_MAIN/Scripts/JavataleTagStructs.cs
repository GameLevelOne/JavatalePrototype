using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Mathematics;

namespace Javatale.Prototype 
{
#region ==========TAG==========

	// PLAYER
	public struct Player : IComponentData
	{
		public int AnimStateIndex;

		[HeaderAttribute("Current")]
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
		public int AnimationToggleValue;
		
		/// <summary>
		/// <para>Values: <br /></para>
		/// <para>0 Attack 1<br /></para>
		/// <para>1 Attack 2<br /></para>
		/// <para>2 Attack 3<br /></para>
		/// </summary>
		public int AttackIndex;
	}
	// public class PlayerComponent : ComponentDataWrapper<Player> {}

	// PROJECTILE
	public struct Projectile : IComponentData
	{
		//
	}
	// public class ProjectileComponent : ComponentDataWrapper<Projectile> {}

	// BEE
	public struct Bee : IComponentData
	{
		public int AnimStateIndex;

		[HeaderAttribute("Current")]
		public BeeAnimationState State;
		public int StartAnimationToggle;
		public int EndAnimationToggle;
		public int AnimationToggleValue;
		
		public float IdleTimer;
		public float PatrolTimer;
	}
	// public class BeeComponent : ComponentDataWrapper<Bee> {}

#endregion

#region ANIMATION EVENT

	// START
	public struct StartAnimationEvent : IComponentData
	{
		public int Value;
	}

	// SPECIFIC EVENT
	public struct SpecificAnimationEvent : IComponentData
	{
		public int Value;
	}

	// END
	public struct EndAnimationEvent : IComponentData
	{
		public int Value;
	}

#endregion

#region ==========LIST==========

	// ANIMATION LIST
	public struct EntryAnimation 
    {
        public int DirIndex; //USELESS
        public float3 FaceDirValue;
        public int StartAnimationToggle;
        public int EndAnimationToggle;

        public EntryAnimation (int dirIndex, float3 faceDirValue, int startAnimToggle, int endAnimToggle)
        {
            DirIndex = dirIndex;
            FaceDirValue = faceDirValue;
            StartAnimationToggle = startAnimToggle;
            EndAnimationToggle = endAnimToggle;
        }
    }

#endregion
	
#region ==========OTHER==========

	// ATTACK
	public struct PlayerAttackSpawnData : IComponentData
	{
		public Position pos;
		public Rotation rot;
		public MoveDirection moveDir;
		public FaceDirection faceDir;
        public Parent parent;
        public Projectile projectile;
        public int attackIndex;
	}

	// DAMAGED
	public struct Damaged : IComponentData
	{
		public float Value;
	}

	// DESTROYED
	public struct Destroyed : IComponentData
	{
		//
	}

#endregion

}

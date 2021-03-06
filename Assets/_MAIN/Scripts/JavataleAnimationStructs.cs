﻿using Unity.Entities;

namespace Javatale.Prototype
{
	#region Player
	//IDLE STAND
	public struct AnimationPlayerIdleStand : IComponentData {}
	public class PlayerAnimationIdleComponent : ComponentDataWrapper<AnimationPlayerIdleStand> {}

	//MOVE RUN
	public struct AnimationPlayerMoveRun : IComponentData {}
	public class PlayerAnimationMoveRunComponent : ComponentDataWrapper<AnimationPlayerMoveRun> {}

	//ATTACK 1
	public struct AnimationPlayerAttack1 : IComponentData {}
	public class PlayerAnimationAttack1Component : ComponentDataWrapper<AnimationPlayerAttack1> {}

	//ATTACK 2
	public struct AnimationPlayerAttack2 : IComponentData {}
	public class PlayerAnimationAttack2Component : ComponentDataWrapper<AnimationPlayerAttack2> {}

	//ATTACK 3
	public struct AnimationPlayerAttack3 : IComponentData {}
	public class PlayerAnimationAttack3Component : ComponentDataWrapper<AnimationPlayerAttack3> {}
	#endregion
	
	#region Bee
	//IDLE FLY
	public struct AnimationBeeIdleFly : IComponentData {}
	public class BeeAnimationIdleComponent : ComponentDataWrapper<AnimationBeeIdleFly> {}

	//MOVE FLY
	public struct AnimationBeeMoveFly : IComponentData {}
	public class BeeAnimationMoveFlyComponent : ComponentDataWrapper<AnimationBeeMoveFly> {}
	#endregion
}

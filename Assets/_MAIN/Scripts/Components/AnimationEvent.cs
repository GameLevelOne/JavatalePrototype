using UnityEngine;

public class AnimationEvent : MonoBehaviour {
	public delegate void AnimationControl();
	public event AnimationControl OnStartAnimation;
	public event AnimationControl OnSpawnSomethingOnAnimation;
	public event AnimationControl OnEndSpecificAnimation;
	public event AnimationControl OnEndAnimation;

	public Animator animator;

	// bool isAnimating = false;

	void StartAnimation () 
	{
		if (OnStartAnimation != null) 
		{
			OnStartAnimation();
		}
	} 

	void SpawnSomethingOnAnimation ()
	{
		if (OnSpawnSomethingOnAnimation != null)
		{
			OnSpawnSomethingOnAnimation();
		}
	}

	void EndSpecificAnimation ()
	{
		if (OnEndSpecificAnimation != null)
		{
			OnEndSpecificAnimation();
		}
	}

	void EndAnimation ()
	{
		if (OnEndAnimation != null)
		{
			OnEndAnimation();
		}
	}
}

using UnityEngine;

namespace Javatale.Prototype
{
	public class PlayerColliderComponent : MonoBehaviour 
	{
		public ColliderEvent colliderEvent;

		[HeaderAttribute("Current")]
		public bool isInitDamaged;
		public float damageValue;

		void OnEnable ()
		{
			colliderEvent.OnDamageEvent += OnDamageEvent;
		}

		void OnDisable ()
		{
			colliderEvent.OnDamageEvent -= OnDamageEvent;
		}

		void OnDamageEvent (float damageValue)
		{
			isInitDamaged = true;
			this.damageValue = damageValue;
		}
	}
}

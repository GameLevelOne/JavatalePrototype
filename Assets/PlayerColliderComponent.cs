using UnityEngine;
using Unity.Entities;

namespace Javatale.Prototype
{
	public class PlayerColliderComponent : MonoBehaviour 
	{
		public ColliderEvent colliderEvent;
		public GameObjectEntity entityGO;

		[HeaderAttribute("Current")]
		public bool isInitDamaged;
		// public float damageValue;

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
			// isInitDamaged = true;
			// this.damageValue = damageValue;
			// DamageThisChildComponent damageThisChildComponent = new DamageThisChildComponent();
			// damageThisChildComponent.damageValue = damageValue;
			
			gameObject.AddComponent<DamageThisChildComponent>().damageValue = damageValue;
			entityGO.enabled = false;
			entityGO.enabled = true;
		}
	}
}

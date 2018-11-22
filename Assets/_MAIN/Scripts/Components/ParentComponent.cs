using System;
using Unity.Entities;
// using UnityEngine;

namespace Javatale.Prototype
{
	[SerializableAttribute]
	public struct Parent : IComponentData 
	{
		public int PosIndex;
		public int AnimIndex;
	}

	public class ParentComponent : ComponentDataWrapper<Parent> {}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
	public class FloorType: MonoBehaviour {

		public enum floorType
		{
			metal = 0,
			normal = 1,
			wet = 2
		};

		public floorType Type;
	}
}

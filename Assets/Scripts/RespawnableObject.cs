using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace WipeOut
{
	public class RespawnableObject : MonoBehaviour
	{
		public Vector3 localPos;
		public Quaternion rot;

		void Start()
		{
			localPos = transform.localPosition;
			rot = transform.rotation;
		}
	}
}
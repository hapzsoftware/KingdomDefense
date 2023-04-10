using System;
using System.Collections.Generic;
using UnityEngine;

namespace Anima2D
{
	public class PoseManager : MonoBehaviour
	{
		[HideInInspector, SerializeField]
		private List<Pose> m_Poses;
	}
}

using System;
using UnityEngine;

namespace Anima2D
{
	public class SpriteMesh : ScriptableObject
	{
		public const int api_version = 4;

		[HideInInspector, SerializeField]
		private int m_ApiVersion;

		[SerializeField]
		private Sprite m_Sprite;

		[SerializeField]
		private Mesh m_SharedMesh;

		public Sprite sprite
		{
			get
			{
				return this.m_Sprite;
			}
		}

		public Mesh sharedMesh
		{
			get
			{
				return this.m_SharedMesh;
			}
		}
	}
}

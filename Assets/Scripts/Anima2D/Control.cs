using System;
using UnityEngine;

namespace Anima2D
{
	public class Control : MonoBehaviour
	{
		[SerializeField]
		private Transform m_BoneTransform;

		private Bone2D m_CachedBone;

		public Color color
		{
			get
			{
				if (this.m_CachedBone)
				{
					Color color = this.m_CachedBone.color;
					color.a = 1f;
					return color;
				}
				return Color.white;
			}
		}

		public Bone2D bone
		{
			get
			{
				if (this.m_CachedBone && this.m_BoneTransform != this.m_CachedBone.transform)
				{
					this.m_CachedBone = null;
				}
				if (!this.m_CachedBone && this.m_BoneTransform)
				{
					this.m_CachedBone = this.m_BoneTransform.GetComponent<Bone2D>();
				}
				return this.m_CachedBone;
			}
			set
			{
				this.m_BoneTransform = value.transform;
			}
		}

		private void Start()
		{
		}

		private void LateUpdate()
		{
			if (this.bone)
			{
				base.transform.position = this.bone.transform.position;
				base.transform.rotation = this.bone.transform.rotation;
			}
		}
	}
}

using System;
using UnityEngine;

namespace Anima2D
{
	[ExecuteInEditMode, RequireComponent(typeof(SpriteMeshInstance))]
	public class SpriteMeshAnimation : MonoBehaviour
	{
		[SerializeField]
		private float m_Frame;

		[SerializeField]
		private SpriteMesh[] m_Frames;

		private int m_OldFrame;

		private SpriteMeshInstance m_SpriteMeshInstance;

		public SpriteMesh[] frames
		{
			get
			{
				return this.m_Frames;
			}
			set
			{
				this.m_Frames = value;
			}
		}

		public SpriteMeshInstance cachedSpriteMeshInstance
		{
			get
			{
				if (!this.m_SpriteMeshInstance)
				{
					this.m_SpriteMeshInstance = base.GetComponent<SpriteMeshInstance>();
				}
				return this.m_SpriteMeshInstance;
			}
		}

		public int frame
		{
			get
			{
				return (int)this.m_Frame;
			}
			set
			{
				this.m_Frame = (float)value;
			}
		}

		private void LateUpdate()
		{
			if (this.m_OldFrame != this.frame && this.m_Frames != null && this.m_Frames.Length > 0 && this.m_Frames.Length > this.frame && this.cachedSpriteMeshInstance)
			{
				this.m_OldFrame = this.frame;
				this.cachedSpriteMeshInstance.spriteMesh = this.m_Frames[this.frame];
			}
		}
	}
}

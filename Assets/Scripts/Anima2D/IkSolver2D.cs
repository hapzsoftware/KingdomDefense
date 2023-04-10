using System;
using System.Collections.Generic;
using UnityEngine;

namespace Anima2D
{
	[Serializable]
	public abstract class IkSolver2D
	{
		[Serializable]
		public class SolverPose
		{
			[SerializeField]
			private Transform m_BoneTransform;

			private Bone2D m_CachedBone;

			public Vector3 solverPosition = Vector3.zero;

			public Quaternion solverRotation = Quaternion.identity;

			public Quaternion defaultLocalRotation = Quaternion.identity;

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
					this.m_CachedBone = value;
					this.m_BoneTransform = null;
					if (value)
					{
						this.m_BoneTransform = this.m_CachedBone.transform;
					}
				}
			}

			public void StoreDefaultPose()
			{
				this.defaultLocalRotation = this.bone.transform.localRotation;
			}

			public void RestoreDefaultPose()
			{
				if (this.bone)
				{
					this.bone.transform.localRotation = this.defaultLocalRotation;
				}
			}
		}

		[SerializeField]
		private Transform m_RootBoneTransform;

		[SerializeField]
		private List<IkSolver2D.SolverPose> m_SolverPoses = new List<IkSolver2D.SolverPose>();

		[SerializeField]
		private float m_Weight = 1f;

		[SerializeField]
		private bool m_RestoreDefaultPose = true;

		private Bone2D m_CachedRootBone;

		public Vector3 targetPosition;

		public Bone2D rootBone
		{
			get
			{
				if (this.m_CachedRootBone && this.m_RootBoneTransform != this.m_CachedRootBone.transform)
				{
					this.m_CachedRootBone = null;
				}
				if (!this.m_CachedRootBone && this.m_RootBoneTransform)
				{
					this.m_CachedRootBone = this.m_RootBoneTransform.GetComponent<Bone2D>();
				}
				return this.m_CachedRootBone;
			}
			private set
			{
				this.m_CachedRootBone = value;
				this.m_RootBoneTransform = null;
				if (value)
				{
					this.m_RootBoneTransform = value.transform;
				}
			}
		}

		public List<IkSolver2D.SolverPose> solverPoses
		{
			get
			{
				return this.m_SolverPoses;
			}
		}

		public float weight
		{
			get
			{
				return this.m_Weight;
			}
			set
			{
				this.m_Weight = Mathf.Clamp01(value);
			}
		}

		public bool restoreDefaultPose
		{
			get
			{
				return this.m_RestoreDefaultPose;
			}
			set
			{
				this.m_RestoreDefaultPose = value;
			}
		}

		public void Initialize(Bone2D _rootBone, int numChilds)
		{
			this.rootBone = _rootBone;
			Bone2D bone2D = this.rootBone;
			this.solverPoses.Clear();
			for (int i = 0; i < numChilds; i++)
			{
				if (bone2D)
				{
					IkSolver2D.SolverPose solverPose = new IkSolver2D.SolverPose();
					solverPose.bone = bone2D;
					this.solverPoses.Add(solverPose);
					bone2D = bone2D.child;
				}
			}
			this.StoreDefaultPoses();
		}

		public void Update()
		{
			if (this.weight > 0f)
			{
				if (this.restoreDefaultPose)
				{
					this.RestoreDefaultPoses();
				}
				this.DoSolverUpdate();
				this.UpdateBones();
			}
		}

		public void StoreDefaultPoses()
		{
			for (int i = 0; i < this.solverPoses.Count; i++)
			{
				IkSolver2D.SolverPose solverPose = this.solverPoses[i];
				if (solverPose != null)
				{
					solverPose.StoreDefaultPose();
				}
			}
		}

		public void RestoreDefaultPoses()
		{
			for (int i = 0; i < this.solverPoses.Count; i++)
			{
				IkSolver2D.SolverPose solverPose = this.solverPoses[i];
				if (solverPose != null)
				{
					solverPose.RestoreDefaultPose();
				}
			}
		}

		private void UpdateBones()
		{
			for (int i = 0; i < this.solverPoses.Count; i++)
			{
				IkSolver2D.SolverPose solverPose = this.solverPoses[i];
				if (solverPose != null && solverPose.bone)
				{
					if (this.weight == 1f)
					{
						solverPose.bone.transform.localRotation = solverPose.solverRotation;
					}
					else
					{
						solverPose.bone.transform.localRotation = Quaternion.Slerp(solverPose.bone.transform.localRotation, solverPose.solverRotation, this.weight);
					}
				}
			}
		}

		protected abstract void DoSolverUpdate();
	}
}

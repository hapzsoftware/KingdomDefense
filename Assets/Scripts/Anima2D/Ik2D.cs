using System;
using UnityEngine;

namespace Anima2D
{
	public abstract class Ik2D : MonoBehaviour
	{
		[SerializeField]
		private bool m_Record;

		[SerializeField]
		private Transform m_TargetTransform;

		[SerializeField]
		private int m_NumBones;

		[SerializeField]
		private float m_Weight = 1f;

		[SerializeField]
		private bool m_RestoreDefaultPose = true;

		[SerializeField]
		private bool m_OrientChild = true;

		private Bone2D m_CachedTarget;

		public IkSolver2D solver
		{
			get
			{
				return this.GetSolver();
			}
		}

		public bool record
		{
			get
			{
				return this.m_Record;
			}
		}

		public Bone2D target
		{
			get
			{
				if (this.m_CachedTarget && this.m_TargetTransform != this.m_CachedTarget.transform)
				{
					this.m_CachedTarget = null;
				}
				if (!this.m_CachedTarget && this.m_TargetTransform)
				{
					this.m_CachedTarget = this.m_TargetTransform.GetComponent<Bone2D>();
				}
				return this.m_CachedTarget;
			}
			set
			{
				this.m_CachedTarget = value;
				this.m_TargetTransform = value.transform;
				this.InitializeSolver();
			}
		}

		public int numBones
		{
			get
			{
				return this.ValidateNumBones(this.m_NumBones);
			}
			set
			{
				int num = this.ValidateNumBones(value);
				if (num != this.m_NumBones)
				{
					this.m_NumBones = num;
					this.InitializeSolver();
				}
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
				this.m_Weight = value;
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

		public bool orientChild
		{
			get
			{
				return this.m_OrientChild;
			}
			set
			{
				this.m_OrientChild = value;
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.matrix = base.transform.localToWorldMatrix;
			if (base.enabled && this.target && this.numBones > 0)
			{
				Gizmos.DrawIcon(base.transform.position, "ikGoal");
			}
			else
			{
				Gizmos.DrawIcon(base.transform.position, "ikGoalDisabled");
			}
		}

		private void OnValidate()
		{
			this.Validate();
		}

		private void Start()
		{
			this.OnStart();
		}

		private void Update()
		{
			this.SetAttachedIK(this);
			this.OnUpdate();
		}

		private void LateUpdate()
		{
			this.OnLateUpdate();
			this.UpdateIK();
		}

		private void SetAttachedIK(Ik2D ik2D)
		{
			for (int i = 0; i < this.solver.solverPoses.Count; i++)
			{
				IkSolver2D.SolverPose solverPose = this.solver.solverPoses[i];
				if (solverPose.bone)
				{
					solverPose.bone.attachedIK = ik2D;
				}
			}
		}

		public void UpdateIK()
		{
			this.OnIkUpdate();
			this.solver.Update();
			if (this.orientChild && this.target.child)
			{
				this.target.child.transform.rotation = base.transform.rotation;
			}
		}

		protected virtual void OnIkUpdate()
		{
			this.solver.weight = this.weight;
			this.solver.targetPosition = base.transform.position;
			this.solver.restoreDefaultPose = this.restoreDefaultPose;
		}

		private void InitializeSolver()
		{
			Bone2D chainBoneByIndex = Bone2D.GetChainBoneByIndex(this.target, this.numBones - 1);
			this.SetAttachedIK(null);
			this.solver.Initialize(chainBoneByIndex, this.numBones);
		}

		protected virtual int ValidateNumBones(int numBones)
		{
			return numBones;
		}

		protected virtual void Validate()
		{
		}

		protected virtual void OnStart()
		{
		}

		protected virtual void OnUpdate()
		{
		}

		protected virtual void OnLateUpdate()
		{
		}

		protected abstract IkSolver2D GetSolver();
	}
}

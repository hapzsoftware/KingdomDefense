using Anima2D;
using System;
using UnityEngine;

namespace UnityChan
{
	[RequireComponent(typeof(Bone2D))]
	public class SpringBone : MonoBehaviour
	{
		public float radius = 0.05f;

		public bool isUseEachBoneForceSettings;

		public float stiffnessForce = 0.01f;

		public float dragForce = 0.4f;

		public Vector3 springForce = new Vector3(0f, -0.0001f, 0f);

		public SpringCollider[] colliders;

		public float threshold = 0.01f;

		public bool isAnimated;

		private float springLength;

		private Quaternion localRotation;

		private Transform trs;

		private Vector3 currTipPos;

		private Vector3 prevTipPos;

		private Transform org;

		private SpringManager managerRef;

		private Bone2D m_Bone;

		private void Awake()
		{
			this.m_Bone = base.GetComponent<Bone2D>();
			this.trs = base.transform;
			this.localRotation = base.transform.localRotation;
			this.managerRef = this.GetParentSpringManager(base.transform);
		}

		private SpringManager GetParentSpringManager(Transform t)
		{
			SpringManager component = t.GetComponent<SpringManager>();
			if (component != null)
			{
				return component;
			}
			if (t.parent != null)
			{
				return this.GetParentSpringManager(t.parent);
			}
			return null;
		}

		private void Start()
		{
			this.springLength = Vector3.Distance(this.trs.position, this.m_Bone.endPosition);
			this.currTipPos = this.m_Bone.endPosition;
			this.prevTipPos = this.m_Bone.endPosition;
		}

		public void UpdateSpring()
		{
			this.org = this.trs;
			if (!this.isAnimated)
			{
				this.trs.localRotation = Quaternion.identity * this.localRotation;
			}
			float d = Time.deltaTime * Time.deltaTime;
			Vector3 a = this.trs.rotation * (Vector3.right * this.stiffnessForce) / d;
			a += (this.prevTipPos - this.currTipPos) * this.dragForce / d;
			a += this.springForce / d;
			Vector3 vector = this.currTipPos;
			this.currTipPos = this.currTipPos - this.prevTipPos + this.currTipPos + a * d;
			this.currTipPos = (this.currTipPos - this.trs.position).normalized * this.springLength + this.trs.position;
			for (int i = 0; i < this.colliders.Length; i++)
			{
				if (Vector3.Distance(this.currTipPos, this.colliders[i].transform.position) <= this.radius + this.colliders[i].radius)
				{
					Vector3 normalized = (this.currTipPos - this.colliders[i].transform.position).normalized;
					this.currTipPos = this.colliders[i].transform.position + normalized * (this.radius + this.colliders[i].radius);
					this.currTipPos = (this.currTipPos - this.trs.position).normalized * this.springLength + this.trs.position;
				}
			}
			this.prevTipPos = vector;
			Vector3 fromDirection = this.trs.TransformDirection(Vector3.right);
			Quaternion lhs = Quaternion.FromToRotation(fromDirection, this.currTipPos - this.trs.position);
			Quaternion b = lhs * this.trs.rotation;
			this.trs.rotation = Quaternion.Lerp(this.org.rotation, b, this.managerRef.dynamicRatio);
		}
	}
}

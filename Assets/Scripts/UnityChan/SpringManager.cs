using System;
using UnityEngine;

namespace UnityChan
{
	public class SpringManager : MonoBehaviour
	{
		public float dynamicRatio = 1f;

		public float stiffnessForce;

		public AnimationCurve stiffnessCurve;

		public float dragForce;

		public AnimationCurve dragCurve;

		public SpringBone[] springBones;

		private void Start()
		{
			this.UpdateParameters();
		}

		private void LateUpdate()
		{
			if (this.dynamicRatio != 0f)
			{
				for (int i = 0; i < this.springBones.Length; i++)
				{
					if (this.dynamicRatio > this.springBones[i].threshold && this.springBones[i])
					{
						this.springBones[i].UpdateSpring();
					}
				}
			}
		}

		private void UpdateParameters()
		{
			this.UpdateParameter("stiffnessForce", this.stiffnessForce, this.stiffnessCurve);
			this.UpdateParameter("dragForce", this.dragForce, this.dragCurve);
		}

		private void UpdateParameter(string fieldName, float baseValue, AnimationCurve curve)
		{
		}
	}
}

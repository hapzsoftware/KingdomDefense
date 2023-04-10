using System;
using UnityEngine;

public class ObjPos : MonoBehaviour
{
	private Transform m_tranform;

	private Transform m_tranChild;

	private Vector3 vPosChild;

	private void Start()
	{
		this.m_tranform = base.transform;
		int childCount = this.m_tranform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			this.m_tranChild = this.m_tranform.GetChild(i);
			this.vPosChild = this.m_tranChild.position;
			this.vPosChild.z = this.vPosChild.y;
			this.m_tranChild.position = this.vPosChild;
		}
	}
}

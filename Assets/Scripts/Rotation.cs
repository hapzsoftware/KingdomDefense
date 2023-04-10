using System;
using UnityEngine;

public class Rotation : MonoBehaviour
{
	public Transform m_trannRotation;

	public float fSpeed;

	public bool bRightToLeft;

	private Vector3 vEuler;

	private void Update()
	{
		if (this.bRightToLeft)
		{
			this.vEuler.z = this.vEuler.z + Time.deltaTime * this.fSpeed;
		}
		else
		{
			this.vEuler.z = this.vEuler.z - Time.deltaTime * this.fSpeed;
		}
		this.m_trannRotation.eulerAngles = this.vEuler;
	}
}

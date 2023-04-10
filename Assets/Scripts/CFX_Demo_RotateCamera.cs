using System;
using UnityEngine;

public class CFX_Demo_RotateCamera : MonoBehaviour
{
	public static bool rotating = true;

	public float speed = 30f;

	public Transform rotationCenter;

	private void Update()
	{
		if (CFX_Demo_RotateCamera.rotating)
		{
			base.transform.RotateAround(this.rotationCenter.position, Vector3.up, this.speed * Time.deltaTime);
		}
	}
}

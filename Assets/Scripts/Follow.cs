using System;
using UnityEngine;

[ExecuteInEditMode]
public class Follow : MonoBehaviour
{
	public Transform target;

	public Vector3 offset;

	private void LateUpdate()
	{
		if (this.target)
		{
			base.transform.position = this.target.position + this.offset;
		}
	}
}

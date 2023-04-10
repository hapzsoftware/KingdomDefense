using System;
using UnityEngine;

public class CFX3_Demo_Translate : MonoBehaviour
{
	public float speed = 30f;

	public Vector3 rotation = Vector3.forward;

	public Vector3 axis = Vector3.forward;

	public bool gravity;

	private Vector3 dir;

	private void Start()
	{
		this.dir = new Vector3(UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f));
		this.dir.Scale(this.rotation);
		base.transform.localEulerAngles = this.dir;
	}

	private void Update()
	{
		base.transform.Translate(this.axis * this.speed * Time.deltaTime, Space.Self);
	}
}

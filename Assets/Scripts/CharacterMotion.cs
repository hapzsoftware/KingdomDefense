using System;
using UnityEngine;

public class CharacterMotion : MonoBehaviour
{
	private Animator animator;

	private void Start()
	{
		this.animator = base.GetComponent<Animator>();
	}

	private void Update()
	{
		float axis = UnityEngine.Input.GetAxis("Horizontal");
		Vector3 localEulerAngles = base.transform.localEulerAngles;
		if (axis < 0f)
		{
			localEulerAngles.y = 180f;
		}
		else if (axis > 0f)
		{
			localEulerAngles.y = 0f;
		}
		base.transform.localRotation = Quaternion.Euler(localEulerAngles);
		this.animator.SetFloat("Forward", Mathf.Abs(axis));
	}
}

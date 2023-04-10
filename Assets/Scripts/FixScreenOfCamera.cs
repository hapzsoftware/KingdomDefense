using System;
using UnityEngine;

public class FixScreenOfCamera : MonoBehaviour
{
	private void Start()
	{
		float num = 0.5625f;
		float num2 = (float)Screen.height / (float)Screen.width;
		float num3 = num / num2;
		Camera component = base.GetComponent<Camera>();
		Rect rect = component.rect;
		rect.width = 1f;
		rect.height = num3;
		rect.x = 0f;
		rect.y = (1f - num3) / 2f;
		component.rect = rect;
	}
}

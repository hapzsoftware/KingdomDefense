using System;
using UnityEngine;

public class CFX_AutodestructWhenNoChildren : MonoBehaviour
{
	private void Update()
	{
		if (base.transform.childCount == 0)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}

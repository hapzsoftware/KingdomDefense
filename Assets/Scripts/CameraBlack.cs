using System;
using UnityEngine;

public class CameraBlack : MonoBehaviour
{
	public static CameraBlack Instance;

	private void Awake()
	{
		if (CameraBlack.Instance == null)
		{
			CameraBlack.Instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}
}

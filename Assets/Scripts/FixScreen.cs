using System;
using UnityEngine;

public class FixScreen : MonoBehaviour
{
	private Camera m_MainCamera;

	private float _defaulWidth;

	private void Awake()
	{
		this.m_MainCamera = Camera.main;
		this._defaulWidth = this.m_MainCamera.orthographicSize * 1.77777779f;
		this.m_MainCamera.orthographicSize = this._defaulWidth / this.m_MainCamera.aspect;
	}
}

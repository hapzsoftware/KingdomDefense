using System;
using UnityEngine;
using UnityEngine.UI;

public class CircleTime : MonoBehaviour
{
	private Image m_Image;

	public float fTimeLife = 3f;

	private float fCountTime;

	private void Start()
	{
		this.m_Image = base.GetComponent<Image>();
		this.fCountTime = 0f;
	}

	private void Update()
	{
		this.Fill();
	}

	private void Fill()
	{
		if (this.fCountTime > 0f)
		{
			this.fCountTime -= Time.deltaTime;
		}
		this.m_Image.fillAmount = this.fCountTime / this.fTimeLife;
	}

	public void StartCount()
	{
		this.fCountTime = this.fTimeLife;
	}

	public bool IsReady()
	{
		return this.fCountTime <= 0f;
	}
}

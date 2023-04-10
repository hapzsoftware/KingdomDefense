using System;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class CFX_LightFlicker : MonoBehaviour
{
	public bool loop;

	public float smoothFactor = 1f;

	public float addIntensity = 1f;

	private float minIntensity;

	private float maxIntensity;

	private float baseIntensity;

	private void Awake()
	{
		this.baseIntensity = base.GetComponent<Light>().intensity;
	}

	private void OnEnable()
	{
		this.minIntensity = this.baseIntensity;
		this.maxIntensity = this.minIntensity + this.addIntensity;
	}

	private void Update()
	{
		base.GetComponent<Light>().intensity = Mathf.Lerp(this.minIntensity, this.maxIntensity, Mathf.PerlinNoise(Time.time * this.smoothFactor, 0f));
	}
}

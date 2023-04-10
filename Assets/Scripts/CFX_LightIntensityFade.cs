using System;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class CFX_LightIntensityFade : MonoBehaviour
{
	public float duration = 1f;

	public float delay;

	public float finalIntensity;

	private float baseIntensity;

	public bool autodestruct;

	private float p_lifetime;

	private float p_delay;

	private void Start()
	{
		this.baseIntensity = base.GetComponent<Light>().intensity;
	}

	private void OnEnable()
	{
		this.p_lifetime = 0f;
		this.p_delay = this.delay;
		if (this.delay > 0f)
		{
			base.GetComponent<Light>().enabled = false;
		}
	}

	private void Update()
	{
		if (this.p_delay > 0f)
		{
			this.p_delay -= Time.deltaTime;
			if (this.p_delay <= 0f)
			{
				base.GetComponent<Light>().enabled = true;
			}
			return;
		}
		if (this.p_lifetime / this.duration < 1f)
		{
			base.GetComponent<Light>().intensity = Mathf.Lerp(this.baseIntensity, this.finalIntensity, this.p_lifetime / this.duration);
			this.p_lifetime += Time.deltaTime;
		}
		else if (this.autodestruct)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}

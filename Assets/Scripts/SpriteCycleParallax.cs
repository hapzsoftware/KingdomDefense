using System;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(SpriteCycle))]
public class SpriteCycleParallax : MonoBehaviour
{
	public Transform target;

	public Vector2 factor;

	private SpriteCycle spriteCicle;

	private void Awake()
	{
		this.spriteCicle = base.GetComponent<SpriteCycle>();
	}

	private void Start()
	{
		if (!this.target && Camera.main)
		{
			this.target = Camera.main.transform;
		}
	}

	private void Update()
	{
		if (this.target && this.spriteCicle)
		{
			this.spriteCicle.position = this.target.position.x * this.factor.x;
			Vector3 localPosition = base.transform.localPosition;
			localPosition.y = this.target.position.y * this.factor.y;
			base.transform.localPosition = localPosition;
		}
	}
}

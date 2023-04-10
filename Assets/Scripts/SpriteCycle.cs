using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpriteCycle : MonoBehaviour
{
	public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

	[Range(0f, 1f)]
	public float offset;

	private float totalWidth = 1f;

	private float mPosition;

	public float position
	{
		get
		{
			return this.mPosition;
		}
		set
		{
			float x = base.transform.localScale.x;
			this.mPosition = value;
			if (x > 0f)
			{
				this.mPosition /= x;
			}
			Vector3 zero = Vector3.zero;
			this.totalWidth = 0f;
			for (int i = 0; i < this.spriteRenderers.Count; i++)
			{
				SpriteRenderer spriteRenderer = this.spriteRenderers[i];
				if (spriteRenderer && spriteRenderer.sprite)
				{
					spriteRenderer.transform.localPosition = zero;
					zero.x += spriteRenderer.sprite.bounds.size.x;
					this.totalWidth += spriteRenderer.sprite.bounds.size.x;
				}
			}
			float d = this.mPosition % this.totalWidth;
			for (int j = 0; j < this.spriteRenderers.Count; j++)
			{
				SpriteRenderer spriteRenderer2 = this.spriteRenderers[j];
				if (spriteRenderer2 && spriteRenderer2.sprite)
				{
					Vector3 localPosition = spriteRenderer2.transform.localPosition + Vector3.right * d;
					if (localPosition.x <= -spriteRenderer2.sprite.bounds.size.x)
					{
						localPosition.x += this.totalWidth;
					}
					else if (localPosition.x > this.totalWidth)
					{
						localPosition.x -= this.totalWidth;
					}
					localPosition.x -= this.offset * this.totalWidth;
					spriteRenderer2.transform.localPosition = localPosition;
				}
			}
		}
	}

	private void Awake()
	{
		this.position = 0f;
	}

	private void OnValidate()
	{
		this.position = 0f;
	}
}

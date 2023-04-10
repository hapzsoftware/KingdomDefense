using System;
using UnityEngine;

public class CircleLight : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer m_spriteRenderer;

	private float _a;

	private void Update()
	{
		this._a = this.m_spriteRenderer.color.a;
		if (this._a > 0f)
		{
			this._a -= Time.deltaTime * 0.25f;
			this.m_spriteRenderer.color = new Color(this.m_spriteRenderer.color.r, this.m_spriteRenderer.color.g, this.m_spriteRenderer.color.b, this._a);
		}
	}
}

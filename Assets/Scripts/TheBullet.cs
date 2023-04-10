using System;
using UnityEngine;

public class TheBullet : MonoBehaviour
{
	public Transform m_transform;

	public float fSpeed;

	private Vector2 vCurrentPos;

	private Vector2 vTargetPos;

	private void Awake()
	{
		this.m_transform = base.transform;
	}

	private void Update()
	{
		this.vCurrentPos = this.m_transform.position;
		this.vCurrentPos = Vector2.MoveTowards(this.vCurrentPos, this.vTargetPos, Time.deltaTime * this.fSpeed);
		if (this.vCurrentPos == this.vTargetPos)
		{
			base.gameObject.SetActive(false);
		}
		this.m_transform.position = this.vCurrentPos;
	}

	public void Shot(Vector2 _pos)
	{
		this.vTargetPos = _pos;
	}
}

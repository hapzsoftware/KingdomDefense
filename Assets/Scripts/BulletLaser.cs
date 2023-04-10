using System;
using UnityEngine;

public class BulletLaser : MonoBehaviour
{
	public LineRenderer m_lineRenderer;

	private Transform m_tranEnemy;

	private Vector2 vStarPos;

	private bool bAllowShot;

	private Vector2 vEnemyPos;

	private void Start()
	{
		this.vStarPos = base.transform.position;
	}

	public void Shot(Transform _enemy)
	{
		this.m_tranEnemy = _enemy;
		this.bAllowShot = true;
		this.m_lineRenderer.positionCount = 2;
		this.m_lineRenderer.gameObject.SetActive(true);
	}

	private void DrawLine(Vector3 _from, Vector3 _to)
	{
		_from.z = (_to.z = -10f);
		this.m_lineRenderer.SetPosition(0, _from);
		this.m_lineRenderer.SetPosition(1, _to);
	}

	public void StopShot()
	{
		this.m_lineRenderer.positionCount = 0;
		this.m_tranEnemy = null;
	}

	private void Update()
	{
		if (!this.bAllowShot)
		{
			return;
		}
		if (this.m_tranEnemy)
		{
			this.vEnemyPos = this.m_tranEnemy.position;
			this.m_lineRenderer.positionCount = 2;
			this.DrawLine(this.vStarPos, this.vEnemyPos);
		}
		else
		{
			this.bAllowShot = false;
			this.StopShot();
		}
	}
}

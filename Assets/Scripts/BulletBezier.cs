using System;
using UnityEngine;

public class BulletBezier : MonoBehaviour
{
	protected Transform m_tranform;

	public Tower m_tower;

	protected Enemy m_enemy;

	private Vector2 vStartPos;

	public float fSpeed;

	private bool bShot;

	public float fHigh;

	public bool bIsBulletPosion;

	private Vector2 vCurrentPos;

	private Vector2 vEnemyPos;

	private float _time;

	private void Awake()
	{
		this.m_tranform = base.transform;
	}

	private void Update()
	{
		if (!this.bShot)
		{
			return;
		}
		this._time += Time.deltaTime * this.fSpeed;
		if (this.m_enemy.gameObject.activeInHierarchy && this.m_enemy.eStatus == Enemy.STATUS.Moving)
		{
			this.vEnemyPos = this.m_enemy.m_tranBodyPoint.position;
		}
		if (this._time <= 1f)
		{
			this.vCurrentPos = Bezier.GetBezier(this.vStartPos, this.vEnemyPos, this._time, this.fHigh);
			this.Rotation(this.m_tranform.position, this.vCurrentPos);
			this.m_tranform.position = this.vCurrentPos;
		}
		else
		{
			this.bShot = false;
			if (this.bIsBulletPosion)
			{
				if (!base.IsInvoking("Destroy"))
				{
					base.Invoke("Destroy", 0.3f);
				}
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}
	}

	public void Shot(Vector2 _startpos, Enemy _enemy, Tower _tower)
	{
		this._time = 0f;
		this.m_tower = _tower;
		this.m_enemy = _enemy;
		this.vStartPos = _startpos;
		this.vCurrentPos = _startpos;
		if (_enemy)
		{
			this.vEnemyPos = this.m_enemy.m_tranBodyPoint.position;
		}
		this.bShot = true;
	}

	public virtual void HitEnemy()
	{
		if (this.m_enemy)
		{
			this.m_enemy.ReduceHP(this.m_tower.m_myTowerData.GetDamage());
		}
	}

	public virtual void Rotation(Vector2 _f, Vector2 _t)
	{
	}

	private void Destroy()
	{
		base.gameObject.SetActive(false);
	}

	private void OnDisable()
	{
		this.m_tranform.eulerAngles = new Vector3(0f, 0f, 90f);
	}
}

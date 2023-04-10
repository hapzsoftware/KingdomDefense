using System;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
	protected Transform m_tranform;

	private Tower m_tower;

	private Enemy m_enemy;

	public float fSpeed;

	private bool bShot;

	private Vector2 vCurrentPos;

	private Vector2 vEnemyPos;

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
		if (this.m_enemy.gameObject.activeInHierarchy)
		{
			this.vEnemyPos = this.m_enemy.m_tranBodyPoint.position;
		}
		this.vCurrentPos = Vector2.MoveTowards(this.vCurrentPos, this.vEnemyPos, this.fSpeed * Time.deltaTime);
		this.m_tranform.position = this.vCurrentPos;
		if (this.vCurrentPos == this.vEnemyPos)
		{
			this.HitEnemy();
		}
	}

	public void Shot(Vector2 _startpos, Enemy _enemy, Tower _tower)
	{
		this.m_tower = _tower;
		this.m_enemy = _enemy;
		this.vCurrentPos = _startpos;
		this.vEnemyPos = this.m_enemy.CurrentPos();
		this.bShot = true;
	}

	private void HitEnemy()
	{
		if (this.m_enemy)
		{
			this.m_enemy.ReduceHP(this.m_tower.m_myTowerData.GetDamage());
		}
		base.gameObject.SetActive(false);
	}
}

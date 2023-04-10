using System;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
	private Transform m_transform;

	private Transform m_TranRender;

	private Enemy m_enemy;

	public float fSpeed;

	private TheRoad m_theRoad;

	private int iIndexTheRoad;

	private Vector3 vCurrentPos;

	private Vector3 vTargetPos;

	private Vector2 vSoldierPos;

	private void Awake()
	{
		this.m_enemy = base.GetComponent<Enemy>();
		this.m_transform = base.transform;
		this.m_TranRender = this.m_transform.GetChild(0);
	}

	public void Init()
	{
		this.iIndexTheRoad = 0;
		this.fSpeed = this.m_enemy.m_enemyData.fMoveSpeed;
		this.vTargetPos = this.m_theRoad.GetPos(this.iIndexTheRoad);
		this.m_transform.position = this.vTargetPos;
	}

	private void Update()
	{
		if (TheGameStatusManager.CURRENT_STATUS != TheGameStatusManager.GAME_STATUS.Playing)
		{
			return;
		}
		if (this.m_enemy.isStatus(Enemy.STATUS.Moving))
		{
			this.Move();
		}
	}

	public void SetRoad(TheRoad _road)
	{
		if (_road)
		{
			this.m_theRoad = _road;
		}
		else
		{
			this.m_theRoad = TheLevel.Instance.LIST_THE_ROAD[0];
		}
		this.m_transform.position = this.m_theRoad.LIST_POS[0];
	}

	private void Move()
	{
		this.vCurrentPos = this.m_transform.position;
		this.vCurrentPos = Vector2.MoveTowards(this.vCurrentPos, this.vTargetPos, Time.deltaTime * this.fSpeed);
		if (this.vCurrentPos == this.vTargetPos)
		{
			if (this.iIndexTheRoad < this.m_theRoad.iTotalPos - 1)
			{
				this.iIndexTheRoad++;
				this.vTargetPos = this.m_theRoad.GetPos(this.iIndexTheRoad);
				this.Rotation();
			}
			else
			{
				this.m_enemy.SetStatus(Enemy.STATUS.CompleteMission);
			}
		}
		this.vCurrentPos.z = this.vCurrentPos.y;
		this.m_transform.position = this.vCurrentPos;
	}

	private void MoveToSoldier()
	{
		this.vCurrentPos = this.m_transform.position;
		this.vCurrentPos = Vector2.MoveTowards(this.vCurrentPos, this.vTargetPos, Time.deltaTime * this.fSpeed);
		if ((Vector2)this.vCurrentPos == this.vSoldierPos)
		{
			this.iIndexTheRoad++;
		}
		this.vCurrentPos.z = this.vCurrentPos.y;
		this.m_transform.position = this.vCurrentPos;
	}

	public void MoveToSoldier(Vector2 _pos)
	{
		this.vSoldierPos = _pos;
	}

	private void Rotation()
	{
		if (this.vCurrentPos.x > this.vTargetPos.x)
		{
			this.m_TranRender.eulerAngles = new Vector3(0f, 180f, 0f);
		}
		else
		{
			this.m_TranRender.eulerAngles = new Vector3(0f, 0f, 0f);
		}
	}

	public void Rotation(float _yAngle)
	{
		this.m_TranRender.eulerAngles = new Vector3(0f, _yAngle, 0f);
	}
}

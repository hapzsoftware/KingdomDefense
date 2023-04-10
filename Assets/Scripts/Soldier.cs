using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Soldier : Tower
{
	public enum STATUS
	{
		Idiel,
		Moving,
		Attacking,
		Die
	}

	private sealed class _IeAddHp_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal float __speedToAddHp___0;

		internal int _i___1;

		internal Soldier _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _IeAddHp_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this.__speedToAddHp___0 = 1f;
				this._i___1 = 0;
				break;
			case 1u:
				this._i___1++;
				break;
			default:
				return false;
			}
			if (this._i___1 < 5)
			{
				if (this._this.m_myTowerData.iHP < this._this.m_myTowerData.iMaxHp)
				{
					this._this.m_myTowerData.iHP += 30;
				}
				else
				{
					this._this.m_myTowerData.iHP = this._this.m_myTowerData.iMaxHp;
				}
				this._this.ShowHpBar(this._this.m_tranHp, (float)this._this.m_myTowerData.iHP, (float)this._this.m_myTowerData.iMaxHp);
				this._current = new WaitForSeconds(this.__speedToAddHp___0);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			this._PC = -1;
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	public Soldier.STATUS eStatus;

	public AudioClip m_auSoundDie;

	public float fTimelife = 30f;

	[Space(20f)]
	public Transform m_tranRender;

	public Transform m_tranHp;

	public Transform m_tranOfBullet;

	private Vector3 vDetalPos;

	public Vector2 vOldPos;

	[Space(20f)]
	public Animator m_animator;

	public AnimationClip m_aniMove;

	public AnimationClip m_aniAttack;

	public AnimationClip m_aniIdie;

	public AnimationClip m_aniDie;

	private GameObject objBloodEff;

	private GameObject objBloodMark;

	private Vector3 vHpBar = new Vector3(1f, 0.9f, 0f);

	private Vector3 vTargetPos;

	private Vector3 vRight = new Vector3(0f, 180f, 0f);

	private Vector3 vLeft = Vector3.zero;

	public override void GetDataTowerConfig()
	{
		this.m_myTowerData = new MyTowerData();
		if (this.eTower == TheEnumManager.TOWER.soldier)
		{
			this.m_myTowerData = TheDataManager.Instance.GetTowerData(TheEnumManager.TOWER.tower_soldier, this.eTowerLevel).Clone();
		}
		else
		{
			this.m_myTowerData = TheDataManager.Instance.GetTowerData(this.eTower, this.eTowerLevel).Clone();
		}
		UpgradeData upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.Reinforcement_MoreTimelife30Percent);
		UpgradeData upgradeData2 = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.Reinforcement_MoreTimelife50Percent);
		float num = 0f;
		float num2 = 0f;
		if (upgradeData.isActived)
		{
			num = this.fTimelife * upgradeData.GetFatorUp();
		}
		if (upgradeData2.isActived)
		{
			num2 = this.fTimelife * upgradeData2.GetFatorUp();
		}
		this.fTimelife = this.fTimelife + num + num2;
		base.Invoke("Die", this.fTimelife);
	}

	private void SetStatus(Soldier.STATUS _status)
	{
		this.eStatus = _status;
		switch (this.eStatus)
		{
		case Soldier.STATUS.Idiel:
			this.m_animator.Play(this.m_aniIdie.name);
			break;
		case Soldier.STATUS.Moving:
			this.m_animator.Play(this.m_aniMove.name);
			break;
		case Soldier.STATUS.Attacking:
			this.m_animator.Play(this.m_aniAttack.name);
			break;
		case Soldier.STATUS.Die:
			this.Die();
			break;
		}
	}

	public override void Init()
	{
		this.vOldPos = this.GetCurrentPos();
		if (this.eTower == TheEnumManager.TOWER.soldier)
		{
			this.vDetalPos = new Vector2(UnityEngine.Random.Range(0.2f, 0.8f), UnityEngine.Random.Range(-0.2f, 0.2f));
			this.SetStatus(Soldier.STATUS.Moving);
			this.vTargetPos = this.vOldPos;
		}
		else
		{
			this.vOldPos = this.GetCurrentPos();
			this.vTargetPos = this.GetCurrentPos();
			this.vDetalPos = new Vector2(0.5f, 0.15f);
			this.SetStatus(Soldier.STATUS.Idiel);
		}
		this.m_tranHp.localScale = this.vHpBar;
	}

	private void Update()
	{
		if (this.eStatus == Soldier.STATUS.Die)
		{
			return;
		}
		this.MoveToEnemy(this.CURRENT_ENEMY);
	}

	public override void Attack(Enemy _enemy)
	{
		if (this.eStatus == Soldier.STATUS.Die)
		{
			return;
		}
		if (_enemy.m_enemyData.bIsAirForece)
		{
			return;
		}
		this.SetStatus(Soldier.STATUS.Attacking);
		if (Vector2.Distance(_enemy.CurrentPos(), this.GetCurrentPos()) < 1f)
		{
			TheSound.Instance.PlayerSoundSoldierAttack();
			_enemy.SetSoldierAttack(this);
			_enemy.ReduceHP(this.m_myTowerData.GetDamage());
		}
	}

	public override void FindCurrentEnemy()
	{
		TheEnumManager.TOWER eTower = this.eTower;
		if (eTower == TheEnumManager.TOWER.soldier)
		{
			this.CURRENT_ENEMY = TheObjPoolingManager.Instance.FindNearestEnemy(this.vCurrentPos, this.m_myTowerData.fRange, TheEnumManager.ENEMY_KIND.Infantry);
		}
	}

	public void ReduceHP(int _damage)
	{
		this.objBloodEff = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.BloodOfEnemy).GetItem();
		if (this.objBloodEff)
		{
			this.objBloodEff.transform.position = this.vCurrentPos + new Vector3(0f, 0.3f, 0f);
			this.objBloodEff.SetActive(true);
		}
		this.m_myTowerData.iHP -= _damage;
		this.ShowHpBar(this.m_tranHp, (float)this.m_myTowerData.iHP, (float)this.m_myTowerData.iMaxHp);
		if (this.m_myTowerData.iHP <= 0)
		{
			this.objBloodMark = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.BloodMark).GetItem();
			if (this.objBloodMark)
			{
				this.objBloodMark.transform.position = this.vCurrentPos;
				this.objBloodMark.SetActive(true);
			}
			this.SetStatus(Soldier.STATUS.Die);
		}
	}

	private void ShowHpBar(Transform _bar, float _currentHp, float _maxHp)
	{
		if (!_bar)
		{
			return;
		}
		this.vHpBar.x = _currentHp / _maxHp;
		if (this.vHpBar.x < 0f)
		{
			this.vHpBar.x = 0f;
		}
		_bar.localScale = this.vHpBar;
	}

	private void MoveToEnemy(Enemy _enemy)
	{
		if (_enemy && _enemy.CurrentPos().x < 20f)
		{
			this.vTargetPos = _enemy.transform.position + this.vDetalPos;
			if (this.vCurrentPos == this.vTargetPos)
			{
				this.SetStatus(Soldier.STATUS.Attacking);
			}
			else
			{
				this.SetStatus(Soldier.STATUS.Moving);
			}
		}
		else
		{
			if (this.eStatus == Soldier.STATUS.Attacking)
			{
				this.SetStatus(Soldier.STATUS.Moving);
				this.vTargetPos = this.vOldPos;
				this.CURRENT_ENEMY = null;
			}
			if (this.vCurrentPos == this.vTargetPos)
			{
				this.SetStatus(Soldier.STATUS.Idiel);
			}
		}
		this.MoveTo(this.vTargetPos);
	}

	private void SkillAddMoreHP(Vector2 _pos)
	{
		float num = Vector2.Distance(_pos, this.vCurrentPos);
		if (num < 2f)
		{
			base.StartCoroutine(this.IeAddHp());
		}
	}

	private IEnumerator IeAddHp()
	{
		Soldier._IeAddHp_c__Iterator0 _IeAddHp_c__Iterator = new Soldier._IeAddHp_c__Iterator0();
		_IeAddHp_c__Iterator._this = this;
		return _IeAddHp_c__Iterator;
	}

	private void MoveTo(Vector2 _target)
	{
		if (_target.x > this.vCurrentPos.x)
		{
			this.m_tranRender.eulerAngles = this.vRight;
		}
		else
		{
			this.m_tranRender.eulerAngles = this.vLeft;
		}
		this.vCurrentPos = this.m_tranform.position;
		this.vCurrentPos = Vector2.MoveTowards(this.vCurrentPos, _target, Time.deltaTime * this.m_myTowerData.fMoveSpeed);
		this.vCurrentPos.z = this.vCurrentPos.y;
		this.m_tranform.position = this.vCurrentPos;
	}

	public void SetMove(Vector2 _pos)
	{
		this.vTargetPos = _pos;
		this.vOldPos = _pos;
		this.SetStatus(Soldier.STATUS.Moving);
	}

	public Vector2 GetCurrentPos()
	{
		if (this.m_tranform)
		{
			return this.m_tranform.position;
		}
		return Vector2.one * 1000f;
	}

	private void Rotation(float angle)
	{
		this.m_tranRender.transform.eulerAngles = new Vector3(0f, angle, 0f);
	}

	private void Die()
	{
		this.objBloodEff = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.BloodOfEnemy).GetItem();
		if (this.objBloodEff)
		{
			this.objBloodEff.transform.position = this.vCurrentPos + new Vector3(0f, 0.3f, 0f);
			this.objBloodEff.SetActive(true);
		}
		TheSound.Instance.PlaySound(this.m_auSoundDie);
		base.CancelInvoke();
		UnityEngine.Object.Destroy(base.gameObject);
	}
}

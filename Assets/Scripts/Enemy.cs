using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public enum STATUS
	{
		Init,
		Moving,
		Attacking,
		Die,
		CompleteMission
	}

	private sealed class _IeHitPondOfPoison_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _i___1;

		internal Enemy _this;

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

		public _IeHitPondOfPoison_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._i___1 = 0;
				break;
			case 1u:
				this._this.ReduceHP(5);
				this._i___1++;
				break;
			default:
				return false;
			}
			if (this._i___1 < 10)
			{
				this._current = new WaitForSeconds(1f);
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

	public TheEnumManager.ENEMY eEnemyID;

	public Enemy.STATUS eStatus;

	public AudioClip m_auEnemyDie;

	private EnemyMove m_EnemyMove;

	private Transform m_tranform;

	public Transform m_tranBodyPoint;

	public Transform tranHPBar;

	private Soldier CURRENT_SOLDIER_TARGET;

	private EnemyAnimation m_enemyAnimation;

	public EnemyData m_enemyData;

	private GameObject objBloodEff;

	private GameObject objBloodMark;

	private bool bAllowReduceHp;

	private Vector3 vHpBar = new Vector3(1f, 0.9f, 0f);

	private bool bIsFreeze;

	private void Awake()
	{
		this.m_tranform = base.transform;
		this.m_EnemyMove = base.GetComponent<EnemyMove>();
		this.m_enemyAnimation = base.GetComponent<EnemyAnimation>();
	}

	private void Start()
	{
		this.SetStatus(Enemy.STATUS.Init);
	}

	public void SetStatus(Enemy.STATUS _status)
	{
		this.eStatus = _status;
		switch (this.eStatus)
		{
		case Enemy.STATUS.Init:
			this.m_enemyData = TheDataManager.Instance.GetEnemyData(this.eEnemyID.ToString().ToLower()).Clone();
			this.m_enemyData.ConfigEnemyDataWithDifficuft();
			this.m_enemyData.ConfigEnemyDataWithUpgradeSystem();
			this.m_enemyData.ConfigEnemyDataWithLevel();
			this.vHpBar.x = 1f;
			this.tranHPBar.localScale = this.vHpBar;
			this.m_EnemyMove.Init();
			TheObjPoolingManager.Instance.AddToEnemylistGameplay(this);
			TheEventManager.PostEnemyEvent(this, TheEventManager.EnemyEventID.OnEnemyIsBorn);
			this.SetStatus(Enemy.STATUS.Moving);
			base.Invoke("SetAllowReduceHP", 1f);
			break;
		case Enemy.STATUS.Moving:
			if (this.m_enemyData.bBoss)
			{
				this.m_enemyAnimation.Play(EnemyAnimation.ANI.Move, 0.5f);
			}
			else
			{
				this.m_enemyAnimation.Play(EnemyAnimation.ANI.Move, 1f);
			}
			break;
		case Enemy.STATUS.Attacking:
			this.m_EnemyMove.Rotation(0f);
			this.m_enemyAnimation.Play(EnemyAnimation.ANI.Attack, 1f);
			break;
		case Enemy.STATUS.Die:
		{
			this.m_enemyData.iHP = 0;
			TheLevel.Instance.iOriginalCoin += this.m_enemyData.iCoin;
			TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
			if (this.m_enemyData.bBoss)
			{
				this.objBloodMark = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Explosion_of_mine).GetItem();
				if (this.objBloodMark)
				{
					this.objBloodMark.transform.position = this.m_tranform.position;
					this.objBloodMark.SetActive(true);
				}
			}
			else
			{
				this.objBloodMark = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.BloodMark).GetItem();
				if (this.objBloodMark)
				{
					this.objBloodMark.transform.position = this.m_tranform.position;
					this.objBloodMark.SetActive(true);
				}
			}
			if (this.m_auEnemyDie)
			{
				TheSound.Instance.PlaySound(this.m_auEnemyDie);
			}
			GameObject item = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.EffEnemyText).GetItem();
			if (item)
			{
				item.transform.position = this.CurrentPos();
				item.SetActive(true);
			}
			if (this.m_enemyData.bIsInfantry)
			{
				item = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.BloodMark).GetItem();
				if (item)
				{
					item.transform.position = this.CurrentPos();
					item.SetActive(true);
				}
			}
			TheEventManager.PostEnemyEvent(this, TheEventManager.EnemyEventID.OnEnemyIsDetroyedOnRoad);
			this.Die();
			break;
		}
		case Enemy.STATUS.CompleteMission:
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.heart);
			TheLevel.Instance.iCurrentHeart--;
			TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
			TheEventManager.PostEnemyEvent(this, TheEventManager.EnemyEventID.OnEnemyCompletedRoad);
			this.Die();
			break;
		}
	}

	public bool isStatus(Enemy.STATUS _status)
	{
		return this.eStatus == _status;
	}

	private void Die()
	{
		this.bAllowReduceHp = false;
		base.transform.position = Vector2.one * 1000f;
		TheObjPoolingManager.Instance.RemoveEnemyFormListGameplay(this);
		base.gameObject.SetActive(false);
	}

	private void SetAllowReduceHP()
	{
		this.bAllowReduceHp = true;
	}

	public Vector2 CurrentPos()
	{
		return this.m_tranform.position;
	}

	public Vector2 BulletPos()
	{
		return this.m_tranBodyPoint.position;
	}

	public void ReduceHP(int _damage)
	{
		if (!this.bAllowReduceHp)
		{
			return;
		}
		if (this.m_enemyData.iHP > 0)
		{
			if (!this.m_tranBodyPoint)
			{
				return;
			}
			this.objBloodEff = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.BloodOfEnemy).GetItem();
			if (this.objBloodEff)
			{
				this.objBloodEff.transform.position = this.m_tranBodyPoint.position;
				this.objBloodEff.SetActive(true);
			}
			this.m_enemyData.iHP -= _damage;
			this.ShowHpBar(this.tranHPBar, (float)this.m_enemyData.iHP, (float)this.m_enemyData.iMaxHp);
			if (this.m_enemyData.iHP <= 0)
			{
				this.SetStatus(Enemy.STATUS.Die);
			}
		}
	}

	public void SetSoldierAttack(Soldier _soldier)
	{
		this.CURRENT_SOLDIER_TARGET = _soldier;
		if (!base.IsInvoking("AttackSoldier"))
		{
			base.InvokeRepeating("AttackSoldier", 0.05f, this.m_enemyData.fShootingSpeed);
		}
	}

	private void AttackSoldier()
	{
		if (Vector2.Distance(this.m_tranform.position, this.CURRENT_SOLDIER_TARGET.GetCurrentPos()) < this.m_enemyData.fRange)
		{
			if (!this.bIsFreeze)
			{
				this.SetStatus(Enemy.STATUS.Attacking);
				if (this.CURRENT_SOLDIER_TARGET)
				{
					this.CURRENT_SOLDIER_TARGET.ReduceHP(this.m_enemyData.iDamage);
				}
			}
			if (this.CURRENT_SOLDIER_TARGET.GetCurrentPos().x > this.CurrentPos().x)
			{
				this.m_EnemyMove.Rotation(0f);
			}
			else
			{
				this.m_EnemyMove.Rotation(180f);
			}
		}
		else if (this.eStatus != Enemy.STATUS.Moving)
		{
			this.SetStatus(Enemy.STATUS.Moving);
			base.CancelInvoke("AttackSoldier");
		}
	}

	private void ShowHpBar(Transform _bar, float _currentHp, float _maxHp)
	{
		this.vHpBar.x = _currentHp / _maxHp;
		if (this.vHpBar.x < 0f)
		{
			this.vHpBar.x = 0f;
		}
		_bar.localScale = this.vHpBar;
	}

	private void PlayerFreezeSkill(Vector2 _pos)
	{
		if (Vector2.Distance(this.CurrentPos(), _pos) <= 3f)
		{
			this.bIsFreeze = true;
			this.m_enemyAnimation.Stop();
			this.m_EnemyMove.fSpeed = 0f;
			UpgradeData upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.Skill_MoreTimeForFreeze);
			float num = upgradeData.GetValueDefaul();
			if (upgradeData.isActived)
			{
				num *= 1f + upgradeData.GetFatorUp();
			}
			base.Invoke("ExitFreezingMode", num);
			GameObject item = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.FreezeEff).GetItem();
			if (item)
			{
				item.transform.position = this.m_tranBodyPoint.position - new Vector3(0f, 0f, 0.01f);
				if (this.m_enemyData.bBoss)
				{
					item.transform.localScale = Vector3.one * 2f;
				}
				else
				{
					item.transform.localScale = Vector3.one;
				}
				item.GetComponent<TimeLife>().fTimelife = num;
				item.SetActive(true);
			}
		}
	}

	private void ExitFreezingMode()
	{
		this.bIsFreeze = false;
		this.m_EnemyMove.fSpeed = this.m_enemyData.fMoveSpeed;
		this.SetStatus(Enemy.STATUS.Moving);
	}

	private void PlayerMineOnRoadSkill(Vector2 _pos)
	{
		float num = Vector2.Distance(this.CurrentPos(), _pos);
		UpgradeData upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.Skill_MoreRangeForFireFromSky);
		float num2 = upgradeData.GetValueDefaul();
		if (upgradeData.isActived)
		{
			num2 *= 1f + upgradeData.GetFatorUp();
		}
		if (num < num2)
		{
			this.ReduceHP(100);
		}
	}

	private void PlayerBoomFromSkySkill(Vector2 _pos)
	{
		float num = Vector2.Distance(this.CurrentPos(), _pos);
		UpgradeData upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.Skill_MoreRangeForFireFromSky);
		float num2 = upgradeData.GetValueDefaul();
		if (upgradeData.isActived)
		{
			num2 *= 1f + upgradeData.GetFatorUp();
		}
		if (num < num2)
		{
			this.ReduceHP(100);
		}
	}

	public void HitPondOfPoison()
	{
		base.StartCoroutine(this.IeHitPondOfPoison());
	}

	private IEnumerator IeHitPondOfPoison()
	{
		Enemy._IeHitPondOfPoison_c__Iterator0 _IeHitPondOfPoison_c__Iterator = new Enemy._IeHitPondOfPoison_c__Iterator0();
		_IeHitPondOfPoison_c__Iterator._this = this;
		return _IeHitPondOfPoison_c__Iterator;
	}

	private void HitRocket(Vector2 _pos, int _damage)
	{
		float num = Vector2.Distance(this.m_tranBodyPoint.position, _pos);
		if (num < 0.65f)
		{
			this.ReduceHP(_damage);
		}
	}

	private void OnEnable()
	{
		TheSkillManager.OnUsedSkillFreeze += new TheSkillManager.UsedSkill(this.PlayerFreezeSkill);
		TheSkillManager.OnUsedSkillMineOnRoad += new TheSkillManager.UsedSkill(this.PlayerMineOnRoadSkill);
		TheSkillManager.OnUserSkillBoomFromSky += new TheSkillManager.UsedSkill(this.PlayerBoomFromSkySkill);
		TheEventManager.OnRocketHit += new TheEventManager.EventOfWeapon(this.HitRocket);
	}

	private void OnDisable()
	{
		TheSkillManager.OnUsedSkillFreeze -= new TheSkillManager.UsedSkill(this.PlayerFreezeSkill);
		TheSkillManager.OnUsedSkillMineOnRoad -= new TheSkillManager.UsedSkill(this.PlayerMineOnRoadSkill);
		TheSkillManager.OnUserSkillBoomFromSky -= new TheSkillManager.UsedSkill(this.PlayerBoomFromSkySkill);
		TheEventManager.OnRocketHit -= new TheEventManager.EventOfWeapon(this.HitRocket);
		TheObjPoolingManager.Instance.RemoveEnemyFormListGameplay(this);
	}
}

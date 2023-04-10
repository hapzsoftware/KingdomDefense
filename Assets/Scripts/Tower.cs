using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
	public TheEnumManager.TOWER eTower;

	public TheEnumManager.TOWER_LEVEL eTowerLevel;

	[Space(20f)]
	protected Transform m_tranform;

	protected Vector3 vCurrentPos;

	public MyTowerData m_myTowerData;

	[HideInInspector, Space(20f)]
	public Enemy CURRENT_ENEMY;

	private Enemy NearestEnemy;

	private float fDistanceOfNearestEnemy;

	private void Awake()
	{
		this.GetDataTowerConfig();
		this.m_tranform = base.transform;
		this.vCurrentPos = base.transform.position;
		this.vCurrentPos.z = this.vCurrentPos.y;
		base.transform.position = this.vCurrentPos;
	}

	private void Start()
	{
		this.Init();
		this.ShowCircle();
		base.InvokeRepeating("FindCurrentEnemy", 0f, 0.5f);
		base.InvokeRepeating("Attack", 0.5f, this.m_myTowerData.fAttackSpeed);
		this.SetTowerRender((int)this.eTowerLevel);
	}

	private void OnDisable()
	{
		this.DestroyTower();
	}

	public virtual void Init()
	{
	}

	public virtual void GetDataTowerConfig()
	{
		this.m_myTowerData = new MyTowerData();
		this.m_myTowerData = TheDataManager.Instance.GetTowerData(this.eTower, this.eTowerLevel).Clone();
		this.m_myTowerData.eTowerLevel = this.eTowerLevel;
	}

	public virtual void ShowCircle()
	{
	}

	private void Attack()
	{
		if (this.CURRENT_ENEMY)
		{
			this.Attack(this.CURRENT_ENEMY);
		}
	}

	public virtual void Attack(Enemy _enemy)
	{
	}

	public virtual void FindCurrentEnemy()
	{
		this.CURRENT_ENEMY = TheObjPoolingManager.Instance.FindNearestEnemy(this.vCurrentPos, this.m_myTowerData.fRange, TheEnumManager.ENEMY_KIND.All);
	}

	public void UpgradeTower()
	{
		if (TheLevel.Instance.iOriginalCoin >= this.m_myTowerData.iPriceToUpgrade)
		{
			TheLevel.Instance.iOriginalCoin -= this.m_myTowerData.iPriceToUpgrade;
			TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
			if (this.eTowerLevel != TheEnumManager.TOWER_LEVEL.level_4)
			{
				this.eTowerLevel++;
				this.SetTowerRender((int)this.eTowerLevel);
				this.GetDataTowerConfig();
				this.ShowCircle();
				TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.tower_upgrade);
			}
		}
		else
		{
			UnityEngine.Debug.Log("NOT ENOUGHT COIN");
			Note.ShowPopupNote(Note.NOTE.NotEnoughtCoin);
		}
		if (this.eTower == TheEnumManager.TOWER.tower_soldier)
		{
			base.GetComponent<TowerSoldier>().UpgradeSoldier();
		}
	}

	public void SellTower()
	{
		TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);
		TheLevel.Instance.iOriginalCoin += this.m_myTowerData.iValueOfSell;
		TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
		this.DestroyTower();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public virtual void SetTowerRender(int _level)
	{
	}

	public virtual void DestroyTower()
	{
	}
}

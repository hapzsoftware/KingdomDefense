using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerSoldier : Tower
{
	public Soldier[] LIST_SOLDIER = new Soldier[3];

	private Vector2 _targetPos;

	[Space(20f)]
	public List<Sprite> LIST_TOWER_SPRITE;

	public SpriteRenderer sprTowerRender;

	public Soldier[] LIST_PREFAB_SOLDIER;

	public override void Init()
	{
		float num = 3f;
		GameObject[] array = GameObject.FindGameObjectsWithTag("isRoad");
		for (int i = 0; i < array.Length; i++)
		{
			if (Vector2.Distance(this.vCurrentPos, array[i].transform.position) < num)
			{
				this._targetPos = array[i].transform.position;
			}
		}
		base.InvokeRepeating("BornSoldier", 2f, (float)this.m_myTowerData.iTimeOfTranning);
	}

	public override void GetDataTowerConfig()
	{
		this.m_myTowerData = new MyTowerData();
		this.m_myTowerData = TheDataManager.Instance.GetTowerData(this.eTower, this.eTowerLevel).Clone();
		this.m_myTowerData.eTowerLevel = this.eTowerLevel;
		this.DestroyTower();
		this.LIST_SOLDIER = new Soldier[this.m_myTowerData.iNumberOfSoldier];
	}

	public override void DestroyTower()
	{
		for (int i = 0; i < this.LIST_SOLDIER.Length; i++)
		{
			if (this.LIST_SOLDIER[i])
			{
				UnityEngine.Object.Destroy(this.LIST_SOLDIER[i].gameObject);
			}
		}
	}

	public void UpgradeSoldier()
	{
		base.CancelInvoke("BornSoldier");
		base.InvokeRepeating("BornSoldier", 2f, (float)this.m_myTowerData.iTimeOfTranning);
		for (int i = 0; i < this.LIST_SOLDIER.Length; i++)
		{
			if (this.LIST_SOLDIER[i] && this.LIST_SOLDIER[i].eTowerLevel != TheEnumManager.TOWER_LEVEL.level_4)
			{
				this.LIST_SOLDIER[i].eTowerLevel++;
				this.LIST_SOLDIER[i].GetDataTowerConfig();
			}
		}
	}

	public void BornSoldier()
	{
		for (int i = 0; i < this.LIST_SOLDIER.Length; i++)
		{
			if (this.LIST_SOLDIER[i] == null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.LIST_PREFAB_SOLDIER[(int)this.eTowerLevel].gameObject);
				gameObject.transform.position = this.vCurrentPos;
				this.LIST_SOLDIER[i] = gameObject.GetComponent<Soldier>();
				this.LIST_SOLDIER[i].vOldPos = this._targetPos + new Vector2(UnityEngine.Random.Range(-0.8f, 0.8f), UnityEngine.Random.Range(-0.8f, 0.8f));
				this.LIST_SOLDIER[i].GetDataTowerConfig();
			}
		}
	}

	public override void SetTowerRender(int _level)
	{
		this.sprTowerRender.sprite = this.LIST_TOWER_SPRITE[_level];
	}
}

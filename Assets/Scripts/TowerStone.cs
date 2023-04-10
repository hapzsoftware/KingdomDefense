using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerStone : Tower
{
	public GameObject _bullet;

	public Transform m_StartBullet;

	public GameObject m_objCricleRange;

	[Space(20f)]
	public List<GameObject> LIST_TOWER_LEVEL;

	public override void ShowCircle()
	{
		this.m_objCricleRange.transform.localScale = Vector3.one * this.m_myTowerData.fRange;
		this.m_objCricleRange.SetActive(false);
		this.m_objCricleRange.SetActive(true);
	}

	public override void Attack(Enemy _enemy)
	{
		if (!MainCode_Gameplay.Instance.IsSafetyRange(_enemy.CurrentPos()))
		{
			return;
		}
		this._bullet = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Bullet_TowerStone).GetItem();
		if (this._bullet)
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.attack_cannonner);
			this._bullet.GetComponent<BulletBezier>().Shot(this.m_StartBullet.position, _enemy, this);
			this._bullet.transform.position = this.m_StartBullet.position;
			this._bullet.SetActive(true);
		}
	}

	public override void SetTowerRender(int _level)
	{
		int count = this.LIST_TOWER_LEVEL.Count;
		for (int i = 0; i < count; i++)
		{
			if (i == _level)
			{
				this.LIST_TOWER_LEVEL[i].SetActive(true);
			}
			else
			{
				this.LIST_TOWER_LEVEL[i].SetActive(false);
			}
		}
	}
}

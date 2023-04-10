using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerPoision : Tower
{
	private GameObject _bullet;

	public GameObject m_objCricleRange;

	[SerializeField]
	private Transform m_tranBulletPos;

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
		this._bullet = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Bullet_Posion).GetItem();
		if (this._bullet)
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.attack_poison);
			this._bullet.GetComponent<BulletBezier>().Shot(this.m_tranBulletPos.position, _enemy, this);
			this._bullet.transform.position = this.m_tranBulletPos.position;
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

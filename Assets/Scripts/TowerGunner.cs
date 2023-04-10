using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerGunner : Tower
{
	private GameObject _bullet;

	public GameObject m_objCricleRange;

	[Space(20f)]
	public List<Sprite> LIST_TOWER_SPRITE;

	public SpriteRenderer sprTowerRender;

	public GameObject objEffMagicCircle;

	public GameObject objFire_0;

	public GameObject objFire_45;

	public GameObject objFire_90;

	public GameObject objFire_135;

	public GameObject objFire_180;

	private float fAngle;

	public override void Init()
	{
		this.objEffMagicCircle.SetActive(false);
	}

	public override void ShowCircle()
	{
		this.m_objCricleRange.transform.localScale = Vector3.one * this.m_myTowerData.fRange;
		this.m_objCricleRange.SetActive(false);
		this.m_objCricleRange.SetActive(true);
	}

	public override void Attack(Enemy _enemy)
	{
		TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.attack_gunmen);
		_enemy.ReduceHP(this.m_myTowerData.GetDamage());
		this.ShowFire(_enemy.CurrentPos());
	}

	public override void SetTowerRender(int _level)
	{
		this.sprTowerRender.sprite = this.LIST_TOWER_SPRITE[_level];
		if (_level == 3)
		{
			this.objEffMagicCircle.SetActive(true);
		}
	}

	private void ShowFire(Vector2 _posOfEnemy)
	{
		if (_posOfEnemy.x <= this.vCurrentPos.x)
		{
			if (_posOfEnemy.y > this.vCurrentPos.y)
			{
				this.objFire_180.SetActive(true);
			}
			else if (this.vCurrentPos.x - _posOfEnemy.x > this.vCurrentPos.y - _posOfEnemy.y)
			{
				this.objFire_135.SetActive(true);
			}
			else
			{
				this.objFire_90.SetActive(true);
			}
		}
		else if (_posOfEnemy.y > this.vCurrentPos.y)
		{
			this.objFire_0.SetActive(true);
		}
		else if (_posOfEnemy.x - this.vCurrentPos.x > _posOfEnemy.y - this.vCurrentPos.y)
		{
			this.objFire_45.SetActive(true);
		}
		else
		{
			this.objFire_90.SetActive(true);
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TowerArcher : Tower
{
	private sealed class _Shot_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal Enemy _enemy;

		internal int _i___1;

		internal TowerArcher _this;

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

		public _Shot_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				if (this._this.eTowerLevel == TheEnumManager.TOWER_LEVEL.level_1 || this._this.eTowerLevel == TheEnumManager.TOWER_LEVEL.level_2)
				{
					this._this._bullet = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Bullet_TowerArcher).GetItem();
					if (this._this._bullet)
					{
						TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.attack_archer);
						this._this._bullet.GetComponent<BulletArcher>().Shot(this._this.m_StartBullet1.position, this._enemy, this._this);
						this._this._bullet.transform.position = this._this.m_StartBullet1.position;
						this._this._bullet.transform.eulerAngles = new Vector3(0f, 0f, 90f);
						this._this._bullet.SetActive(true);
					}
					goto IL_2F3;
				}
				this._i___1 = 0;
				break;
			case 1u:
				this._i___1++;
				break;
			default:
				return false;
			}
			if (this._i___1 < 3)
			{
				this._this._bullet = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Bullet_TowerArcher).GetItem();
				if (this._this._bullet)
				{
					TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.attack_archer);
					if (this._i___1 == 0)
					{
						this._this._bullet.transform.position = this._this.m_StartBullet1.position;
						this._this._bullet.GetComponent<BulletArcher>().Shot(this._this.m_StartBullet1.position, this._enemy, this._this);
					}
					else if (this._i___1 == 1)
					{
						this._this._bullet.transform.position = this._this.m_StartBullet2.position;
						this._this._bullet.GetComponent<BulletArcher>().Shot(this._this.m_StartBullet2.position, this._enemy, this._this);
					}
					else if (this._i___1 == 2)
					{
						this._this._bullet.transform.position = this._this.m_StartBullet3.position;
						this._this._bullet.GetComponent<BulletArcher>().Shot(this._this.m_StartBullet3.position, this._enemy, this._this);
					}
					this._this._bullet.SetActive(true);
				}
				this._current = new WaitForSeconds(0.2f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			IL_2F3:
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

	private GameObject _bullet;

	public Transform m_StartBullet1;

	public Transform m_StartBullet2;

	public Transform m_StartBullet3;

	[Space(20f)]
	public SpriteRenderer m_ArcherRender;

	public Sprite m_sprArcher_normal;

	public Sprite m_sprArcher_attack;

	public GameObject m_objCricleRange;

	[Space(20f)]
	public List<Sprite> LIST_TOWER_SPRITE;

	public SpriteRenderer sprTowerRender;

	public GameObject objEffMagicCircle;

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
		if (!MainCode_Gameplay.Instance.IsSafetyRange(_enemy.CurrentPos()))
		{
			return;
		}
		base.StartCoroutine(this.Shot(_enemy));
	}

	public IEnumerator Shot(Enemy _enemy)
	{
		TowerArcher._Shot_c__Iterator0 _Shot_c__Iterator = new TowerArcher._Shot_c__Iterator0();
		_Shot_c__Iterator._enemy = _enemy;
		_Shot_c__Iterator._this = this;
		return _Shot_c__Iterator;
	}

	public override void SetTowerRender(int _level)
	{
		this.sprTowerRender.sprite = this.LIST_TOWER_SPRITE[_level];
		if (_level == 3)
		{
			this.objEffMagicCircle.SetActive(true);
		}
	}
}

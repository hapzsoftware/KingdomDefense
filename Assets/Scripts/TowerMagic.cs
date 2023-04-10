using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TowerMagic : Tower
{
	private sealed class _AnimationWitch_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal float _angle;

		internal TowerMagic _this;

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

		public _AnimationWitch_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._this.m_WitchSpriterRenderer.sprite = this._this.m_spriteWitch_attack;
				this._current = new WaitForSecondsRealtime(0.3f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.m_WitchSpriterRenderer.sprite = this._this.m_spriteWitch_normal;
				this._this.m_WitchSpriterRenderer.transform.eulerAngles = new Vector3(0f, this._angle, 0f);
				this._PC = -1;
				break;
			}
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

	public SpriteRenderer m_WitchSpriterRenderer;

	public Sprite m_spriteWitch_normal;

	public Sprite m_spriteWitch_attack;

	public GameObject m_objCricleRange;

	public GameObject objEffMagicCircle;

	[Space(20f)]
	public Transform m_tranStartBullet;

	private GameObject _bullet;

	[Space(20f)]
	public List<Sprite> LIST_TOWER_SPRITE;

	public SpriteRenderer sprTowerRender;

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
		if (_enemy != null)
		{
			if (!MainCode_Gameplay.Instance.IsSafetyRange(_enemy.CurrentPos()))
			{
				return;
			}
			if (_enemy.CurrentPos().x > this.vCurrentPos.x)
			{
				base.StartCoroutine(this.AnimationWitch(180f));
			}
			else
			{
				base.StartCoroutine(this.AnimationWitch(0f));
			}
			this._bullet = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Bullet_TowerMagic).GetItem();
			if (this._bullet)
			{
				TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.attack_magic);
				this._bullet.GetComponent<BulletMove>().Shot(this.m_tranStartBullet.position, _enemy, this);
				this._bullet.transform.position = this.m_tranStartBullet.position;
				this._bullet.SetActive(true);
			}
		}
	}

	public IEnumerator AnimationWitch(float _angle)
	{
		TowerMagic._AnimationWitch_c__Iterator0 _AnimationWitch_c__Iterator = new TowerMagic._AnimationWitch_c__Iterator0();
		_AnimationWitch_c__Iterator._angle = _angle;
		_AnimationWitch_c__Iterator._this = this;
		return _AnimationWitch_c__Iterator;
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

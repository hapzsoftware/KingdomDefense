using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TowerElectric : Tower
{
	private sealed class _ShowThunder_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal Vector3 _pos;

		internal TowerElectric _this;

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

		public _ShowThunder_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._this.m_objThunder.transform.position = this._pos + new Vector3(0f, 0f, -0.02f);
				switch (this._this.eTowerLevel)
				{
				case TheEnumManager.TOWER_LEVEL.level_1:
					this._this.m_objThunder.transform.localScale = Vector3.one * 2f;
					break;
				case TheEnumManager.TOWER_LEVEL.level_2:
					this._this.m_objThunder.transform.localScale = Vector3.one * 2.3f;
					break;
				case TheEnumManager.TOWER_LEVEL.level_3:
					this._this.m_objThunder.transform.localScale = Vector3.one * 2.6f;
					break;
				case TheEnumManager.TOWER_LEVEL.level_4:
					this._this.m_objThunder.transform.localScale = Vector3.one * 3f;
					break;
				}
				this._this.m_objThunder.SetActive(true);
				this._current = new WaitForSeconds(0.2f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.m_objThunder.SetActive(false);
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

	private GameObject _bullet;

	public GameObject m_objCricleRange;

	public GameObject m_objThunder;

	[SerializeField]
	private Transform m_tranBulletPos;

	private TheElectric m_MainTheElectric1;

	private TheElectric m_MainTheElectric2;

	[Space(20f)]
	public List<GameObject> LIST_TOWER_LEVEL;

	public List<TheElectric> LIST_ELECTRIC_LEVEL;

	public override void ShowCircle()
	{
		this.m_objCricleRange.transform.localScale = Vector3.one * this.m_myTowerData.fRange;
		this.m_objCricleRange.SetActive(false);
		this.m_objCricleRange.SetActive(true);
	}

	public override void Attack(Enemy _enemy)
	{
		TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.attack_thunder);
		if (this.m_MainTheElectric1)
		{
			this.m_MainTheElectric1.ShowElectric(_enemy.m_tranBodyPoint.position);
		}
		if (this.m_MainTheElectric2)
		{
			this.m_MainTheElectric2.ShowElectric(_enemy.m_tranBodyPoint.position);
		}
		base.StartCoroutine(this.ShowThunder(_enemy.m_tranBodyPoint.position));
		_enemy.ReduceHP(this.m_myTowerData.GetDamage());
	}

	private IEnumerator ShowThunder(Vector3 _pos)
	{
		TowerElectric._ShowThunder_c__Iterator0 _ShowThunder_c__Iterator = new TowerElectric._ShowThunder_c__Iterator0();
		_ShowThunder_c__Iterator._pos = _pos;
		_ShowThunder_c__Iterator._this = this;
		return _ShowThunder_c__Iterator;
	}

	public override void SetTowerRender(int _level)
	{
		int count = this.LIST_TOWER_LEVEL.Count;
		for (int i = 0; i < count; i++)
		{
			if (i == _level)
			{
				this.LIST_TOWER_LEVEL[i].SetActive(true);
				if (i == 3)
				{
					this.m_MainTheElectric1 = this.LIST_ELECTRIC_LEVEL[3];
					this.m_MainTheElectric2 = this.LIST_ELECTRIC_LEVEL[4];
				}
				else
				{
					this.m_MainTheElectric1 = this.LIST_ELECTRIC_LEVEL[i];
					this.m_MainTheElectric2 = null;
				}
			}
			else
			{
				this.LIST_TOWER_LEVEL[i].SetActive(false);
			}
		}
	}
}

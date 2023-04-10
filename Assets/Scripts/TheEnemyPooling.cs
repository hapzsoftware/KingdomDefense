using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TheEnemyPooling : MonoBehaviour
{
	[Serializable]
	public struct KIND_OF_ENEMY
	{
		public TheEnumManager.ENEMY eEnemy;

		public List<Enemy> LIST_ENEMY;

		public void Init(int _number)
		{
			this.LIST_ENEMY = new List<Enemy>();
			for (int i = 0; i < _number; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(TheObjPoolingManager.Instance.GetEnemy(this.eEnemy).objEnemy, Vector2.one * 1000f, Quaternion.identity);
				gameObject.SetActive(false);
				this.LIST_ENEMY.Add(gameObject.GetComponent<Enemy>());
			}
		}

		public Enemy GetEnemy()
		{
			int count = this.LIST_ENEMY.Count;
			for (int i = 0; i < count; i++)
			{
				if (!this.LIST_ENEMY[i].gameObject.activeInHierarchy)
				{
					return this.LIST_ENEMY[i];
				}
			}
			return null;
		}
	}

	private sealed class _Init_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int __total___0;

		internal TheEnemyPooling _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		private static Func<bool> __f__am_cache0;

		private static Func<bool> __f__am_cache1;

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

		public _Init_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitUntil(() => TheObjPoolingManager.Instance);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._current = new WaitUntil(() => TheLevel.Instance);
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			case 2u:
				this.__total___0 = TheLevel.Instance.LIST_CONFIG_ENEMY_FOR_LEVEL.Count;
				for (int i = 0; i < this.__total___0; i++)
				{
					TheEnemyPooling.KIND_OF_ENEMY item = default(TheEnemyPooling.KIND_OF_ENEMY);
					item.eEnemy = TheLevel.Instance.LIST_CONFIG_ENEMY_FOR_LEVEL[i];
					item.Init(10);
					this._this.LIST_ENEMY_POOL.Add(item);
				}
				this._this.iTotalKindOfEnemy = this._this.LIST_ENEMY_POOL.Count;
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

		private static bool __m__0()
		{
			return TheObjPoolingManager.Instance;
		}

		private static bool __m__1()
		{
			return TheLevel.Instance;
		}
	}

	public static TheEnemyPooling Instance;

	private int iTotalKindOfEnemy;

	public List<TheEnemyPooling.KIND_OF_ENEMY> LIST_ENEMY_POOL;

	private Enemy _temp;

	private TheEnemyPooling.KIND_OF_ENEMY _kindOfEnemy;

	private int iIndex;

	public TheEnemyPooling.KIND_OF_ENEMY GetKindOfEnemy(TheEnumManager.ENEMY _enemy)
	{
		int count = this.LIST_ENEMY_POOL.Count;
		for (int i = 0; i < count; i++)
		{
			if (_enemy == this.LIST_ENEMY_POOL[i].eEnemy)
			{
				return this.LIST_ENEMY_POOL[i];
			}
		}
		return this.LIST_ENEMY_POOL[0];
	}

	private void Awake()
	{
		if (TheEnemyPooling.Instance == null)
		{
			TheEnemyPooling.Instance = this;
		}
	}

	private void Start()
	{
		base.StartCoroutine(this.Init());
	}

	private IEnumerator Init()
	{
		TheEnemyPooling._Init_c__Iterator0 _Init_c__Iterator = new TheEnemyPooling._Init_c__Iterator0();
		_Init_c__Iterator._this = this;
		return _Init_c__Iterator;
	}

	public Enemy GetEnemy(TheEnumManager.ENEMY _enemy)
	{
		for (int i = 0; i < this.iTotalKindOfEnemy; i++)
		{
			if (this.LIST_ENEMY_POOL[i].eEnemy == _enemy)
			{
				this.iIndex = i;
				this._kindOfEnemy = this.LIST_ENEMY_POOL[i];
				this._temp = this.LIST_ENEMY_POOL[i].GetEnemy();
				if (this._temp)
				{
					return this._temp;
				}
			}
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(TheObjPoolingManager.Instance.GetEnemy(_enemy).objEnemy, Vector2.one * 1000f, Quaternion.identity);
		this._kindOfEnemy.LIST_ENEMY.Add(gameObject.GetComponent<Enemy>());
		this.LIST_ENEMY_POOL[this.iIndex] = this._kindOfEnemy;
		return gameObject.GetComponent<Enemy>();
	}
}

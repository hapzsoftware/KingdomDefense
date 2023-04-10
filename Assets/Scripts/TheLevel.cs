using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TheLevel : MonoBehaviour
{
	private sealed class _BornEnemy_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int NumberOfGroup;

		internal float _delaytime;

		internal int _totalEnemyOfOneGroup;

		internal int _j___1;

		internal int _i___2;

		internal TheEnumManager.ENEMY _eEnemy;

		internal Enemy __enemy___3;

		internal TheRoad _road;

		internal TheLevel _this;

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

		public _BornEnemy_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				if (this.NumberOfGroup <= 0)
				{
					this.NumberOfGroup = 1;
				}
				if (TheGameStatusManager.CurrentStatus(TheGameStatusManager.GAME_STATUS.Playing))
				{
					this._current = new WaitForSeconds(this._delaytime);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				goto IL_202;
			case 1u:
				this._this.iTotalNumberEnemyOfWave += this._totalEnemyOfOneGroup * this.NumberOfGroup;
				TheEventManager.PostEvent(TheEventManager.EventID.START_WAVE);
				this._j___1 = 0;
				goto IL_176;
			case 2u:
				this._i___2++;
				break;
			case 3u:
				this._j___1++;
				goto IL_176;
			default:
				return false;
			}
			IL_133:
			if (this._i___2 >= this._totalEnemyOfOneGroup)
			{
				this._current = new WaitForSeconds(5f);
				if (!this._disposing)
				{
					this._PC = 3;
				}
				return true;
			}
			this.__enemy___3 = TheEnemyPooling.Instance.GetEnemy(this._eEnemy);
			this.__enemy___3.gameObject.SetActive(true);
			this.__enemy___3.GetComponent<EnemyMove>().SetRoad(this._road);
			this.__enemy___3.GetComponent<Enemy>().SetStatus(Enemy.STATUS.Init);
			this._current = new WaitForSeconds(UnityEngine.Random.Range(0.5f, 2f));
			if (!this._disposing)
			{
				this._PC = 2;
			}
			return true;
			IL_176:
			if (this._j___1 < this.NumberOfGroup)
			{
				this._i___2 = 0;
				goto IL_133;
			}
			if (this._this.OBJ_BOSS != null && this._this.iCurrentWave == this._this.iMAX_WAVE_CONFIG)
			{
                _this.iTotalNumberEnemyOfWave++;
                if (this._this.OBJ_BOSS)
				{
					GameObject gameObject = Instantiate(this._this.OBJ_BOSS);
					gameObject.GetComponent<EnemyMove>().SetRoad(this._road);
				}        
				else
				{
					return false;
				}
            }

			IL_202:
			this._this.bIsStartingWave = false;
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

	public static TheLevel Instance;

	[Header("*** Config ***")]
	public TheEnumManager.KIND_OF_MAPS eKindOfMap;

	public int iOriginalHeart = 10;

	public int iOriginalCoin = 220;

	public int iMAX_WAVE_CONFIG;

	public int iBasePriceForTowerPos = 25;

	[Header("Enemies and Boss in level"), Space(20f)]
	public GameObject OBJ_BOSS;

	public List<TheEnumManager.ENEMY> LIST_CONFIG_ENEMY_FOR_LEVEL;

	[Header("==============================="), Space(20f)]
	public Transform GROUP_ROADS;

	public int iCurrentHeart;

	public int iCurrentWave = 1;

	public List<TheRoad> LIST_THE_ROAD;

	[Header("Starting mark in level"), Space(20f)]
	public Transform GROUP_STARTING_MARK;

	public List<TheStartingWaveMark> LIST_STARTING_MARK;

	private bool bIsStartingWave;

	private int iNUM_OF_ROAD_OF_THIS_WAVE;

	private int iTOTAL_GROUP_OF_EARCH_ONE_KIND_OF_ENEMY;

	private int iTOTAL_ENEMY_IN_ONE_GROUP;

	public List<TheRoad> LIST_ROAD_OF_THIS_WAVE;

	private TheEnumManager.ENEMY ENEMY_OF_THIS_WAVE;

	public int iTotalNumberEnemyOfWave;

	private void Awake()
	{
		if (TheLevel.Instance == null)
		{
			TheLevel.Instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		this.LIST_THE_ROAD = new List<TheRoad>();
		this.LIST_STARTING_MARK = new List<TheStartingWaveMark>();
		int childCount = this.GROUP_ROADS.childCount;
		for (int i = 0; i < childCount; i++)
		{
			this.LIST_THE_ROAD.Add(this.GROUP_ROADS.GetChild(i).GetComponent<TheRoad>());
		}
		for (int j = 0; j < this.GROUP_STARTING_MARK.childCount; j++)
		{
			this.LIST_STARTING_MARK.Add(this.GROUP_STARTING_MARK.GetChild(j).GetComponent<TheStartingWaveMark>());
			this.LIST_STARTING_MARK[j].iIndex = j;
			this.LIST_STARTING_MARK[j].Init();
		}
	}

	private void Start()
	{
		this.iCurrentHeart = this.iOriginalHeart;
		this.iOriginalCoin = this.CalculationCoinForLevel();
		TheEventManager.PostEvent(TheEventManager.EventID.START_WAVE);
		TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
		TheEventManager.EventGameStart(0);
		TheGameStatusManager.SetGameStatus(TheGameStatusManager.GAME_STATUS.Playing);
		this.LIST_ROAD_OF_THIS_WAVE = new List<TheRoad>();
		base.Invoke("ConfigWave", 1f);
	}

	private int CalculationCoinForLevel()
	{
		return this.iMAX_WAVE_CONFIG * this.iBasePriceForTowerPos;
	}

	public void ConfigWave()
	{
		if(iCurrentWave <= iMAX_WAVE_CONFIG)
		{
            iCurrentWave++;
            print("BBBBB" + iCurrentWave);
        }


		if (this.iCurrentWave <= 5)
		{
			this.iNUM_OF_ROAD_OF_THIS_WAVE = 1;
			this.iTOTAL_GROUP_OF_EARCH_ONE_KIND_OF_ENEMY = 4;
			this.iTOTAL_ENEMY_IN_ONE_GROUP = 5;
		}
		else if (this.iCurrentWave > 5 && this.iCurrentWave <= 10)
		{
			this.iNUM_OF_ROAD_OF_THIS_WAVE = 2;
			this.iTOTAL_GROUP_OF_EARCH_ONE_KIND_OF_ENEMY = 4;
			this.iTOTAL_ENEMY_IN_ONE_GROUP = 5;
		}
		else if (this.iCurrentWave > 10 && this.iCurrentWave <= 20)
		{
			this.iNUM_OF_ROAD_OF_THIS_WAVE = 3;
			this.iTOTAL_GROUP_OF_EARCH_ONE_KIND_OF_ENEMY = 4;
			this.iTOTAL_ENEMY_IN_ONE_GROUP = 4;
		}
		else
		{
			this.iNUM_OF_ROAD_OF_THIS_WAVE = 4;
			this.iTOTAL_GROUP_OF_EARCH_ONE_KIND_OF_ENEMY = 4;
			this.iTOTAL_ENEMY_IN_ONE_GROUP = 6;
		}
		this.LIST_ROAD_OF_THIS_WAVE.Clear();
		for (int i = 0; i < this.iNUM_OF_ROAD_OF_THIS_WAVE; i++)
		{
			this.LIST_ROAD_OF_THIS_WAVE.Add(TheLevel.Instance.LIST_THE_ROAD[UnityEngine.Random.Range(0, TheLevel.Instance.LIST_THE_ROAD.Count)]);
		}
		for (int j = 0; j < this.LIST_ROAD_OF_THIS_WAVE.Count; j++)
		{
			this.FindNeasestWaveMark(this.LIST_ROAD_OF_THIS_WAVE[j].LIST_POS[0]).gameObject.SetActive(true);
			this.FindNeasestWaveMark(this.LIST_ROAD_OF_THIS_WAVE[j].LIST_POS[0]).StartCount();
		}
	}

	private void NewWaveOnRoad()
	{
		if (TheLevel.Instance == null)
		{
			return;
		}
		if (this.bIsStartingWave)
		{
			return;
		}
		this.bIsStartingWave = true;
		if (this.iCurrentWave > this.iMAX_WAVE_CONFIG)
		{
			return;
		}
		if (this.iCurrentWave == this.iMAX_WAVE_CONFIG)
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.battle_last_wave);
			if (UIGameplay.Instance)
			{
				UIGameplay.Instance.ShowLastWaveText();
			}
		}
		else
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.battle_new_wave);
		}
		TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
		for (int i = 0; i < this.iNUM_OF_ROAD_OF_THIS_WAVE; i++)
		{
			this.ENEMY_OF_THIS_WAVE = this.LIST_CONFIG_ENEMY_FOR_LEVEL[UnityEngine.Random.Range(0, this.LIST_CONFIG_ENEMY_FOR_LEVEL.Count)];
			base.StartCoroutine(this.BornEnemy(this.iTOTAL_GROUP_OF_EARCH_ONE_KIND_OF_ENEMY, this.iTOTAL_ENEMY_IN_ONE_GROUP, this.ENEMY_OF_THIS_WAVE, this.LIST_ROAD_OF_THIS_WAVE[i], 0.5f));
		}
	}

	private IEnumerator BornEnemy(int NumberOfGroup, int _totalEnemyOfOneGroup, TheEnumManager.ENEMY _eEnemy, TheRoad _road, float _delaytime)
	{
		TheLevel._BornEnemy_c__Iterator0 _BornEnemy_c__Iterator = new TheLevel._BornEnemy_c__Iterator0();
		_BornEnemy_c__Iterator.NumberOfGroup = NumberOfGroup;
		_BornEnemy_c__Iterator._delaytime = _delaytime;
		_BornEnemy_c__Iterator._totalEnemyOfOneGroup = _totalEnemyOfOneGroup;
		_BornEnemy_c__Iterator._eEnemy = _eEnemy;
		_BornEnemy_c__Iterator._road = _road;
		_BornEnemy_c__Iterator._this = this;
		return _BornEnemy_c__Iterator;
	}

	public GameObject GetKindOfEnemy()
	{
		int index = UnityEngine.Random.Range(0, this.LIST_CONFIG_ENEMY_FOR_LEVEL.Count);
		return TheObjPoolingManager.Instance.GetEnemy(this.LIST_CONFIG_ENEMY_FOR_LEVEL[index]).objEnemy;
	}

	private void CheckNewWave(Enemy _en)
	{
		this.iTotalNumberEnemyOfWave--;
		if (this.iTotalNumberEnemyOfWave == 0)
		{
			this.ConfigWave();
		}
	}

	public void StartNewWave()
	{
		base.Invoke("NewWaveOnRoad", 0.5f);
	}

	public void HideAllRoadMark()
	{
		for (int i = 0; i < this.GROUP_STARTING_MARK.childCount; i++)
		{
			this.LIST_STARTING_MARK[i].gameObject.SetActive(false);
		}
	}

	private void OnEnable()
	{
		TheEventManager.RegisterGetEnemyEvent(TheEventManager.EnemyEventID.OnEnemyCompletedRoad, new TheEventManager.ACTION_ENEMY(this.CheckNewWave));
		TheEventManager.RegisterGetEnemyEvent(TheEventManager.EnemyEventID.OnEnemyIsDetroyedOnRoad, new TheEventManager.ACTION_ENEMY(this.CheckNewWave));
	}

	private void OnDisable()
	{
		TheLevel.Instance = null;
		base.StopAllCoroutines();
		TheEventManager.RemoveEnemyEvent(TheEventManager.EnemyEventID.OnEnemyCompletedRoad, new TheEventManager.ACTION_ENEMY(this.CheckNewWave));
		TheEventManager.RemoveEnemyEvent(TheEventManager.EnemyEventID.OnEnemyIsDetroyedOnRoad, new TheEventManager.ACTION_ENEMY(this.CheckNewWave));
	}

	private void OnDestroy()
	{
		base.StopAllCoroutines();
	}

	private TheStartingWaveMark FindNeasestWaveMark(Vector2 _fromPos)
	{
		float num = 5f;
		int count = this.LIST_STARTING_MARK.Count;
		TheStartingWaveMark result = new();
		for (int i = 0; i < count; i++)
		{
			if (Vector2.Distance(_fromPos, this.LIST_STARTING_MARK[i].transform.position) < num)
			{
				num = Vector2.Distance(_fromPos, this.LIST_STARTING_MARK[i].transform.position);
				result = this.LIST_STARTING_MARK[i];
			}
		}

		return result;
	}
}
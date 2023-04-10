using System;
using System.Collections.Generic;
using UnityEngine;

public class TheObjPoolingManager : MonoBehaviour
{
	[Serializable]
	public struct TOWER_POOLING
	{
		public TheEnumManager.TOWER eTower;

		public GameObject objTower;
	}

	[Serializable]
	public struct ENEMY
	{
		public string strNameEnemy;

		public TheEnumManager.ENEMY eEnemy;

		public GameObject objEnemy;
	}

	[Serializable]
	public struct OBJ_POOLING
	{
		public TheEnumManager.ITEMS_POOLING eItem;

		public GameObject objPrefab;

		public int iNumberOfArray;

		public GameObject[] Array;

		public void Init()
		{
			if (this.iNumberOfArray == 0)
			{
				return;
			}
			this.Array = new GameObject[this.iNumberOfArray];
			for (int i = 0; i < this.iNumberOfArray; i++)
			{
				this.Array[i] = UnityEngine.Object.Instantiate<GameObject>(this.objPrefab);
				this.Array[i].transform.SetParent(TheObjPoolingManager.Instance.transform);
				this.Array[i].SetActive(false);
			}
		}

		public GameObject GetItem()
		{
			if (this.iNumberOfArray == 0)
			{
				return null;
			}
			for (int i = 0; i < this.iNumberOfArray; i++)
			{
				if (!this.Array[i].activeInHierarchy)
				{
					return this.Array[i];
				}
			}
			return null;
		}
	}

	public static TheObjPoolingManager Instance;

	[Header("Towers")]
	public List<TheObjPoolingManager.TOWER_POOLING> LIST_TOWER;

	[Header("Enemies"), Space(20f)]
	public List<TheObjPoolingManager.ENEMY> LIST_ENEMY;

	[Header("Object Polling"), Space(20f)]
	public List<TheObjPoolingManager.OBJ_POOLING> LIST_OBJ_POOLING;

	[Header("OBJ PREFAB SKILL"), Space(20f)]
	public GameObject objSkill_MineOfRoad;

	public GameObject objSkill_BoomFromSky;

	public GameObject objSkill_PondOfPoison;

	public GameObject objSkill_Reinforcements_2Mans;

	public GameObject objSkill_Reinforcements_3Mans;

	[Space(10f)]
	public List<Enemy> LIST_ENEMY_IN_GAMEPLAY;

	[Header("KIND OF TOWER-POS"), Space(20f)]
	public GameObject objTowerPos_OfGlassMaps;

	public GameObject objTowerPos_OfSnowMaps;

	[Header("OTHER"), Space(20f)]
	public Sprite sprCallWave_Skull;

	public Sprite sprCallWave_Empty;

	public GameObject objCoinGift;

	private void Awake()
	{
		if (TheObjPoolingManager.Instance == null)
		{
			TheObjPoolingManager.Instance = this;
		}
	}

	private void Start()
	{
		int count = this.LIST_OBJ_POOLING.Count;
		TheObjPoolingManager.OBJ_POOLING value = default(TheObjPoolingManager.OBJ_POOLING);
		for (int i = 0; i < count; i++)
		{
			value = this.LIST_OBJ_POOLING[i];
			value.Init();
			this.LIST_OBJ_POOLING[i] = value;
		}
	}

	public TheObjPoolingManager.TOWER_POOLING GetTower(TheEnumManager.TOWER eTower)
	{
		int count = this.LIST_TOWER.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.LIST_TOWER[i].eTower == eTower)
			{
				return this.LIST_TOWER[i];
			}
		}
		return this.LIST_TOWER[0];
	}

	public TheObjPoolingManager.ENEMY GetEnemy(TheEnumManager.ENEMY eEnemy)
	{
		return this.LIST_ENEMY[(int)eEnemy];
	}

	public TheObjPoolingManager.OBJ_POOLING GetObj(TheEnumManager.ITEMS_POOLING eItem)
	{
		return this.LIST_OBJ_POOLING[(int)eItem];
	}

	public void AddToEnemylistGameplay(Enemy _enemy)
	{
		this.LIST_ENEMY_IN_GAMEPLAY.Add(_enemy);
	}

	public void RemoveEnemyFormListGameplay(Enemy _enemy)
	{
		this.LIST_ENEMY_IN_GAMEPLAY.Remove(_enemy);
	}

	public Enemy FindNearestEnemy(Vector2 _fromPos, float _range, TheEnumManager.ENEMY_KIND eKindOfEnemy = TheEnumManager.ENEMY_KIND.All)
	{
		Enemy result = null;
		float num = _range;
		int count = this.LIST_ENEMY_IN_GAMEPLAY.Count;
		for (int i = 0; i < count; i++)
		{
			if (Vector2.Distance(_fromPos, this.LIST_ENEMY_IN_GAMEPLAY[i].CurrentPos()) < num)
			{
				if (eKindOfEnemy != TheEnumManager.ENEMY_KIND.All)
				{
					if (eKindOfEnemy != TheEnumManager.ENEMY_KIND.Airforce)
					{
						if (eKindOfEnemy == TheEnumManager.ENEMY_KIND.Infantry)
						{
							if (this.LIST_ENEMY_IN_GAMEPLAY[i].m_enemyData.bIsInfantry)
							{
								num = Vector2.Distance(_fromPos, this.LIST_ENEMY_IN_GAMEPLAY[i].CurrentPos());
								result = this.LIST_ENEMY_IN_GAMEPLAY[i];
							}
						}
					}
					else if (this.LIST_ENEMY_IN_GAMEPLAY[i].m_enemyData.bIsAirForece)
					{
						num = Vector2.Distance(_fromPos, this.LIST_ENEMY_IN_GAMEPLAY[i].CurrentPos());
						result = this.LIST_ENEMY_IN_GAMEPLAY[i];
					}
				}
				else
				{
					num = Vector2.Distance(_fromPos, this.LIST_ENEMY_IN_GAMEPLAY[i].CurrentPos());
					result = this.LIST_ENEMY_IN_GAMEPLAY[i];
				}
			}
		}
		return result;
	}

	public GameObject GetTowerPosObj(TheEnumManager.KIND_OF_MAPS eKindOfMaps)
	{
		if (eKindOfMaps == TheEnumManager.KIND_OF_MAPS.GlassMap)
		{
			return this.objTowerPos_OfGlassMaps;
		}
		if (eKindOfMaps == TheEnumManager.KIND_OF_MAPS.RedLand)
		{
			return this.objTowerPos_OfGlassMaps;
		}
		if (eKindOfMaps != TheEnumManager.KIND_OF_MAPS.SnowLand)
		{
			return this.objTowerPos_OfGlassMaps;
		}
		return this.objTowerPos_OfSnowMaps;
	}

	[ContextMenu("Auto Update List enemy")]
	public void UpdateListEnemy()
	{
		int num = Enum.GetNames(typeof(TheEnumManager.ENEMY)).Length;
		this.LIST_ENEMY.Clear();
		for (int i = 0; i < num; i++)
		{
			TheObjPoolingManager.ENEMY item = default(TheObjPoolingManager.ENEMY);
			item.eEnemy = (TheEnumManager.ENEMY)i;
			item.strNameEnemy = item.eEnemy.ToString();
			if (!item.strNameEnemy.Contains("boss"))
			{
				GameObject objEnemy = Resources.Load<GameObject>("ENEMY-PREFAB/" + item.strNameEnemy);
				item.objEnemy = objEnemy;
				this.LIST_ENEMY.Add(item);
			}
		}
		UnityEngine.Debug.Log("====UPDATE ENEMY: DONE=====");
	}

	[ContextMenu("Check Error of PrefabLevel")]
	public void CheckErrorOfPrefabLevel()
	{
		UnityEngine.Debug.Log("CHECKING...");
		for (int i = 1; i < 1000; i++)
		{
			GameObject gameObject = Resources.Load<GameObject>("Levels/LEVEL_" + i);
			if (!gameObject)
			{
				UnityEngine.Debug.Log("CHECK " + i + ": DONE");
				break;
			}
			int childCount = gameObject.transform.childCount;
			int num = 0;
			int num2 = 0;
			for (int j = 0; j < childCount; j++)
			{
				if (gameObject.transform.GetChild(j).gameObject.name == "ROADS")
				{
					num = gameObject.transform.GetChild(j).childCount;
				}
				if (gameObject.transform.GetChild(j).gameObject.name == "MARK_GROUP")
				{
					num2 = gameObject.transform.GetChild(j).childCount;
				}
			}
			if (num != num2)
			{
				UnityEngine.Debug.Log("ERROR: Road != MarkRoak: LEVEL" + i);
			}
		}
	}

	[ContextMenu("Set enemy For All levels")]
	public void SetEnemyForAllLevel()
	{
		UnityEngine.Debug.Log("SET LEVEL...");
		TheLevel theLevel = new TheLevel();
		GameObject gameObject = new GameObject();
		for (int i = 1; i < 1000; i++)
		{
			gameObject = Resources.Load<GameObject>("Levels/LEVEL_" + i);
			int max = Enum.GetNames(typeof(TheEnumManager.ENEMY)).Length;
			if (!gameObject)
			{
				break;
			}
			theLevel = gameObject.GetComponent<TheLevel>();
			while (true)
			{
				IL_57:
				theLevel.LIST_CONFIG_ENEMY_FOR_LEVEL.Clear();
				for (int j = 0; j < 5; j++)
				{
					int num = UnityEngine.Random.Range(0, max);
					TheEnumManager.ENEMY eNEMY = (TheEnumManager.ENEMY)num;
					string text = eNEMY.ToString();
					if (text.Contains("boss"))
					{
						goto IL_57;
					}
					theLevel.LIST_CONFIG_ENEMY_FOR_LEVEL.Add((TheEnumManager.ENEMY)num);
				}
				break;
			}
		}
		UnityEngine.Debug.Log(" DONE");
	}

	[ContextMenu("Check -TowerPos- in Level")]
	public void CheckTowerPosGroup()
	{
		for (int i = 1; i < 1000; i++)
		{
			GameObject gameObject = Resources.Load<GameObject>("Levels/LEVEL_" + i);
			if (!gameObject)
			{
				break;
			}
			if (!gameObject.transform.Find("TowerPos"))
			{
				UnityEngine.Debug.Log("NOT TOWERPOS AT LEVEL: " + i);
			}
		}
		UnityEngine.Debug.Log(" DONE");
	}

	[ContextMenu("Check all enemy có đã được sử dụng hay không?")]
	public void CheckAllEnemyToUse()
	{
		int num = Enum.GetNames(typeof(TheEnumManager.ENEMY)).Length;
		List<string> list = new List<string>();
		for (int i = 0; i < num; i++)
		{
			List<string> arg_2F_0 = list;
			TheEnumManager.ENEMY eNEMY = (TheEnumManager.ENEMY)i;
			arg_2F_0.Add(eNEMY.ToString());
		}
		for (int j = 1; j < 1000; j++)
		{
			GameObject gameObject = Resources.Load<GameObject>("Levels/LEVEL_" + j);
			if (!gameObject)
			{
				break;
			}
			for (int k = 0; k < gameObject.GetComponent<TheLevel>().LIST_CONFIG_ENEMY_FOR_LEVEL.Count; k++)
			{
				if (this.CheckStringInList(list, gameObject.GetComponent<TheLevel>().LIST_CONFIG_ENEMY_FOR_LEVEL[k].ToString()))
				{
					list.Remove(gameObject.GetComponent<TheLevel>().LIST_CONFIG_ENEMY_FOR_LEVEL[k].ToString());
				}
			}
		}
		for (int l = 0; l < list.Count; l++)
		{
			UnityEngine.Debug.Log(" LIST_ENEMY_WAS_DESIGN: " + list[l]);
		}
	}

	private bool CheckStringInList(List<string> LIST, string _id)
	{
		for (int i = 0; i < LIST.Count; i++)
		{
			if (LIST[i] == _id)
			{
				return true;
			}
		}
		return false;
	}
}

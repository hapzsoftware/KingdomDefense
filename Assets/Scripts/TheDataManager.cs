using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

public class TheDataManager : MonoBehaviour
{
	[Serializable]
	public struct ICON_TOWER
	{
		public TheEnumManager.TOWER eTower;

		public Sprite sprIcon;
	}

	public static TheDataManager Instance;

	private string PATH_OF_PLAYER_DATA_XML;

	private bool TESING_MODE;

	public static int RANDOM_NUMBER;

	[Space(20f)]
	public TextAsset TOWER_CONFIG_CSV_FILE;

	public List<MyTowerData> TOWER_CONFIG;

	public List<TheDataManager.ICON_TOWER> LIST_ICON_TOWER;

	[Space(20f)]
	public TextAsset ENEMY_CONFIG_CSV_FILE;

	public List<EnemyData> ENEMY_CONFIG;

	[Space(20f)]
	public TextAsset SHOP_CONFIG_CSV_FILE;

	public List<ShopData> SHOP_CONFIG;

	[Space(20f)]
	public TextAsset UPGRADE_CONFIG_CSV;

	public List<UpgradeData> UPGRADE_CONFIG;

	public static ThePlayerData THE_PLAYER_DATA;

	[Header("CONFIG TIPS"), Space(30f)]
	public List<string> LIST_TIPS;

	private void Awake()
	{
		if (TheDataManager.Instance == null)
		{
			TheDataManager.Instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		TheDataManager.RANDOM_NUMBER = UnityEngine.Random.Range(100000, 200000);
		this.PATH_OF_PLAYER_DATA_XML = Application.persistentDataPath + "/PlayerData.xml";
	}

	private void Start()
	{
		this.ReadFileCSV_EnemyConfig();
		this.ReadFileCSV_ShopConfig();
		TheDataManager.THE_PLAYER_DATA = new ThePlayerData();
		if (!File.Exists(this.PATH_OF_PLAYER_DATA_XML))
		{
			this.SerialzerPlayerData();
		}
		else
		{
			TheDataManager.THE_PLAYER_DATA = this.DeserialzerPlayerData();
		}
		this.ReadFileCSV_UpgradeConfig();
		this.ReadFileCSV_TowerConfig();
	}

	public void ReadFileCSV_TowerConfig()
	{
		this.TOWER_CONFIG = new List<MyTowerData>();
		string[] array = this.TOWER_CONFIG_CSV_FILE.text.Split(new char[]
		{
			'\n'
		});
		string[][] array2 = new string[array.Length][];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = array[i].Split(new char[]
			{
				','
			});
		}
		for (int j = 1; j < array.Length - 1; j++)
		{
			if (array2[j][0] != string.Empty)
			{
				MyTowerData myTowerData = new MyTowerData();
				myTowerData.strID = array2[j][0];
				if (array2[j][1] != string.Empty)
				{
					myTowerData.strName = array2[j][1];
				}
				if (array2[j][2] != string.Empty)
				{
					myTowerData.strContent = array2[j][2];
				}
				if (array2[j][4] != string.Empty)
				{
					myTowerData.iDamage = int.Parse(array2[j][4]);
				}
				if (array2[j][5] != string.Empty)
				{
					myTowerData.fAttackSpeed = float.Parse(array2[j][5]);
				}
				if (array2[j][6] != string.Empty)
				{
					myTowerData.fRange = float.Parse(array2[j][6]);
				}
				if (array2[j][7] != string.Empty)
				{
					myTowerData.iTimeOfTranning = int.Parse(array2[j][7]);
				}
				if (array2[j][8] != string.Empty)
				{
					myTowerData.iHP = int.Parse(array2[j][8]);
					myTowerData.iMaxHp = myTowerData.iHP;
				}
				if (array2[j][9].ToLower() == "yes")
				{
					myTowerData.bCanKillInfantry = true;
				}
				if (array2[j][10].ToLower() == "yes")
				{
					myTowerData.bCanKillAirForce = true;
				}
				if (array2[j][11] != string.Empty)
				{
					myTowerData.iPriceToBuy = int.Parse(array2[j][11]);
				}
				if (array2[j][12] != string.Empty)
				{
					myTowerData.iPriceToUpgrade = int.Parse(array2[j][12]);
				}
				if (array2[j][13] != string.Empty)
				{
					myTowerData.iValueOfSell = int.Parse(array2[j][13]);
				}
				if (array2[j][14] != string.Empty)
				{
					myTowerData.iLevelToUnlock = int.Parse(array2[j][14]);
				}
				if (array2[j][15] != string.Empty)
				{
					myTowerData.fMoveSpeed = float.Parse(array2[j][15]);
				}
				if (array2[j][16] != string.Empty)
				{
					myTowerData.iPriceGemToUnlock = int.Parse(array2[j][16]);
				}
				if (array2[j][17] != string.Empty)
				{
					myTowerData.iBullet = int.Parse(array2[j][17]);
				}
				myTowerData.Init();
				this.TOWER_CONFIG.Add(myTowerData);
			}
		}
		UnityEngine.Debug.Log("TOTAL TOWER: " + this.TOWER_CONFIG.Count);
	}

	public MyTowerData GetTowerData(string _id)
	{
		int count = this.TOWER_CONFIG.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.TOWER_CONFIG[i].strID.Equals(_id))
			{
				return this.TOWER_CONFIG[i];
			}
		}
		return null;
	}

	public MyTowerData GetTowerData(TheEnumManager.TOWER eTower, TheEnumManager.TOWER_LEVEL eTowerLevel)
	{
		string id = eTower.ToString() + "_" + eTowerLevel.ToString();
		MyTowerData towerData = this.GetTowerData(id);
		towerData.eTower = eTower;
		towerData.eTowerLevel = eTowerLevel;
		return towerData;
	}

	public void SetUnlockTower()
	{
		int currentLevelOfPlayer = TheDataManager.THE_PLAYER_DATA.GetCurrentLevelOfPlayer();
		MyTowerData myTowerData = new MyTowerData();
		List<TheEnumManager.TOWER> list = new List<TheEnumManager.TOWER>();
		list.Add(TheEnumManager.TOWER.tower_archer);
		list.Add(TheEnumManager.TOWER.tower_cannonner);
		list.Add(TheEnumManager.TOWER.tower_gunmen);
		list.Add(TheEnumManager.TOWER.tower_magic);
		list.Add(TheEnumManager.TOWER.tower_poison);
		list.Add(TheEnumManager.TOWER.tower_rocket_laucher);
		list.Add(TheEnumManager.TOWER.tower_thunder);
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			myTowerData = this.GetTowerData(list[i], TheEnumManager.TOWER_LEVEL.level_1);
			if (currentLevelOfPlayer >= myTowerData.iLevelToUnlock)
			{
				TheDataManager.THE_PLAYER_DATA.SetUnlockTower(list[i], true);
			}
		}
		this.SerialzerPlayerData();
	}

	public TheDataManager.ICON_TOWER GetIconTower(TheEnumManager.TOWER eTower)
	{
		int count = this.LIST_ICON_TOWER.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.LIST_ICON_TOWER[i].eTower == eTower)
			{
				return this.LIST_ICON_TOWER[i];
			}
		}
		return this.LIST_ICON_TOWER[0];
	}

	public void ReadFileCSV_EnemyConfig()
	{
		this.ENEMY_CONFIG = new List<EnemyData>();
		string[] array = this.ENEMY_CONFIG_CSV_FILE.text.Split(new char[]
		{
			'\n'
		});
		string[][] array2 = new string[array.Length][];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = array[i].Split(new char[]
			{
				','
			});
		}
		for (int j = 1; j < array.Length - 1; j++)
		{
			if (array2[j][0] != string.Empty)
			{
				EnemyData enemyData = new EnemyData();
				enemyData.strID = array2[j][0];
				if (array2[j][1] != string.Empty)
				{
					enemyData.strName = array2[j][1];
				}
				if (array2[j][2] != string.Empty)
				{
					enemyData.strContent = array2[j][2];
				}
				if (array2[j][3] != string.Empty)
				{
					enemyData.iDamage = int.Parse(array2[j][3]);
				}
				if (array2[j][4] != string.Empty)
				{
					enemyData.fShootingSpeed = float.Parse(array2[j][4]);
				}
				if (array2[j][5] != string.Empty)
				{
					enemyData.fMoveSpeed = float.Parse(array2[j][5]);
				}
				if (array2[j][6] != string.Empty)
				{
					enemyData.fRange = float.Parse(array2[j][6]);
				}
				if (array2[j][7] != string.Empty)
				{
					enemyData.iHP = int.Parse(array2[j][7]);
					enemyData.iMaxHp = enemyData.iHP;
				}
				if (array2[j][8].ToLower() == "yes")
				{
					enemyData.bIsInfantry = true;
				}
				if (array2[j][9].ToLower() == "yes")
				{
					enemyData.bIsAirForece = true;
				}
				if (array2[j][10].ToLower() == "yes")
				{
					enemyData.bBoss = true;
				}
				if (array2[j][11] != string.Empty)
				{
					enemyData.iCoin = int.Parse(array2[j][11]);
				}
				this.ENEMY_CONFIG.Add(enemyData);
			}
		}
		UnityEngine.Debug.Log("TOTAL TOWER: " + this.ENEMY_CONFIG.Count);
	}

	public EnemyData GetEnemyData(string _id)
	{
		int count = this.ENEMY_CONFIG.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.ENEMY_CONFIG[i].strID.Equals(_id))
			{
				return this.ENEMY_CONFIG[i];
			}
		}
		return null;
	}

	public void ReadFileCSV_ShopConfig()
	{
		this.SHOP_CONFIG = new List<ShopData>();
		string[] array = this.SHOP_CONFIG_CSV_FILE.text.Split(new char[]
		{
			'\n'
		});
		string[][] array2 = new string[array.Length][];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = array[i].Split(new char[]
			{
				','
			});
		}
		for (int j = 1; j < array.Length - 1; j++)
		{
			if (array2[j][0] != string.Empty)
			{
				ShopData shopData = new ShopData();
				if (array2[j][0] != string.Empty)
				{
					shopData.strId = array2[j][0];
				}
				if (array2[j][1] != string.Empty)
				{
					shopData.strPackName = array2[j][1];
				}
				if (array2[j][2] != string.Empty)
				{
					shopData.iGemValueToAdd = int.Parse(array2[j][2]);
				}
				if (array2[j][3] != string.Empty)
				{
					shopData.iCoinValueToAdd = int.Parse(array2[j][3]);
				}
				if (array2[j][4] != string.Empty)
				{
					shopData.iValueToAdd = int.Parse(array2[j][4]);
				}
				if (array2[j][5] != string.Empty)
				{
					shopData.iPriceGem = int.Parse(array2[j][5]);
				}
				if (array2[j][6] != string.Empty)
				{
					shopData.fPriceDollar = float.Parse(array2[j][6]);
				}
				if (array2[j][7] != string.Empty)
				{
					shopData.strIdKeyStore = array2[j][7];
				}
				if (array2[j][8].ToLower() == "iap")
				{
					shopData.eKindOfShop = TheEnumManager.KIND_OF_SHOP.Iap;
				}
				else if (array2[j][8].ToLower() == "shop coin")
				{
					shopData.eKindOfShop = TheEnumManager.KIND_OF_SHOP.ShopCoin;
				}
				else if (array2[j][8].ToLower() == "shop skill")
				{
					shopData.eKindOfShop = TheEnumManager.KIND_OF_SHOP.ShopSkill;
				}
				if (array2[j][9] != string.Empty)
				{
					shopData.strContent = array2[j][9];
				}
				this.SHOP_CONFIG.Add(shopData);
			}
		}
		UnityEngine.Debug.Log("TOTAL SHOP: " + this.SHOP_CONFIG.Count);
	}

	public ShopData GetShopData(string _id)
	{
		int count = this.SHOP_CONFIG.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.SHOP_CONFIG[i].strId == _id)
			{
				return this.SHOP_CONFIG[i];
			}
		}
		return this.SHOP_CONFIG[0];
	}

	public List<ShopData> ListShopData(TheEnumManager.KIND_OF_SHOP eKindOfShop)
	{
		List<ShopData> list = new List<ShopData>();
		foreach (ShopData current in this.SHOP_CONFIG)
		{
			if (current.eKindOfShop == eKindOfShop)
			{
				list.Add(current);
			}
		}
		return list;
	}

	public void ReadFileCSV_UpgradeConfig()
	{
		this.UPGRADE_CONFIG = new List<UpgradeData>();
		string[] array = this.UPGRADE_CONFIG_CSV.text.Split(new char[]
		{
			'\n'
		});
		string[][] array2 = new string[array.Length][];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = array[i].Split(new char[]
			{
				','
			});
		}
		for (int j = 1; j < array.Length - 1; j++)
		{
			if (array2[j][0] != string.Empty)
			{
				UpgradeData upgradeData = new UpgradeData();
				upgradeData.strId = array2[j][0];
				if (array2[j][1] != string.Empty)
				{
					upgradeData.strName = array2[j][1];
				}
				if (array2[j][2] != string.Empty)
				{
					upgradeData.strContent = array2[j][2];
				}
				if (array2[j][4] != string.Empty)
				{
					upgradeData.fFactorUp = float.Parse(array2[j][4]);
				}
				if (array2[j][5] != string.Empty)
				{
					upgradeData.fFactorDown = float.Parse(array2[j][5]);
				}
				if (array2[j][6] != string.Empty)
				{
					upgradeData.fValueBeAfterActived = float.Parse(array2[j][6]);
				}
				if (array2[j][7] != string.Empty)
				{
					upgradeData.fValueDefaul = float.Parse(array2[j][7]);
				}
				if (array2[j][8] != string.Empty)
				{
					upgradeData.iPrice_star_white = int.Parse(array2[j][8]);
				}
				if (array2[j][9] != string.Empty)
				{
					upgradeData.iPrice_star_blue = int.Parse(array2[j][9]);
				}
				if (array2[j][10] != string.Empty)
				{
					upgradeData.iPrice_star_yellow = int.Parse(array2[j][10]);
				}
				upgradeData.Init();
				this.UPGRADE_CONFIG.Add(upgradeData);
			}
		}
		UnityEngine.Debug.Log("TOTAL TOWER: " + this.TOWER_CONFIG.Count);
	}

	public UpgradeData GetUpgradeData(TheEnumManager.UPGRADE eUpgrade)
	{
		int count = this.UPGRADE_CONFIG.Count;
		string text = eUpgrade.ToString();
		for (int i = 0; i < count; i++)
		{
			if (text.Equals(this.UPGRADE_CONFIG[i].strId))
			{
				return this.UPGRADE_CONFIG[i];
			}
		}
		return null;
	}

	public int GetTotalStarWasUsed(TheEnumManager.STAR eStarKind)
	{
		int num = 0;
		int count = this.UPGRADE_CONFIG.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.UPGRADE_CONFIG[i].isActived && this.UPGRADE_CONFIG[i].GetStarKind() == eStarKind)
			{
				num += this.UPGRADE_CONFIG[i].GetPriceStar();
			}
		}
		return num;
	}

	public void SerialzerPlayerData()
	{
		ThePlayerData thePlayerData = new ThePlayerData();
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(ThePlayerData));
		StreamWriter streamWriter = new StreamWriter(this.PATH_OF_PLAYER_DATA_XML);
		xmlSerializer.Serialize(streamWriter.BaseStream, TheDataManager.THE_PLAYER_DATA);
		streamWriter.Close();
		MonoBehaviour.print("SERIALZER: SAVE DONE!!!!");
		string text = File.ReadAllText(this.PATH_OF_PLAYER_DATA_XML);
		text = TheEncryptionManager.EncryptData(text);
		byte[] bytes = Encoding.ASCII.GetBytes(text);
		File.WriteAllBytes(this.PATH_OF_PLAYER_DATA_XML, bytes);
	}

	public ThePlayerData DeserialzerPlayerData()
	{
		if (File.Exists(this.PATH_OF_PLAYER_DATA_XML))
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(ThePlayerData));
			StringReader stringReader = new StringReader(TheEncryptionManager.DecryptData(File.ReadAllText(this.PATH_OF_PLAYER_DATA_XML)));
			ThePlayerData result = (ThePlayerData)xmlSerializer.Deserialize(stringReader);
			stringReader.Close();
			UnityEngine.Debug.Log("SERIALZER: GET PLAYER DATA DONE!");
			return result;
		}
		UnityEngine.Debug.Log("SERIALZER: NO FILE EXITS");
		return null;
	}

	public string GetRandomTips()
	{
		int index = UnityEngine.Random.Range(0, this.LIST_TIPS.Count);
		return this.GetTips(index);
	}

	public string GetTips(int _index)
	{
		if (_index >= this.LIST_TIPS.Count)
		{
			_index = this.LIST_TIPS.Count - 1;
		}
		return this.LIST_TIPS[_index];
	}

	public void ResetGame()
	{
		if (File.Exists(this.PATH_OF_PLAYER_DATA_XML))
		{
			File.Delete(this.PATH_OF_PLAYER_DATA_XML);
			TheDataManager.THE_PLAYER_DATA = new ThePlayerData();
			this.SerialzerPlayerData();
			this.ReadFileCSV_EnemyConfig();
			this.ReadFileCSV_ShopConfig();
			this.ReadFileCSV_UpgradeConfig();
			this.ReadFileCSV_TowerConfig();
			TheUIManager.Instance.LoadScene(TheEnumManager.SCENE.Menu, false);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ShopTowerAndWeapon : MonoBehaviour
{
	[Serializable]
	public struct TRACK_TOWER
	{
		public TheEnumManager.TOWER eTower;

		public MyTowerData m_MyTower;

		public Text txtTitle;

		public Text txtContent;

		public Button buButtonBuy;

		public Button buShowInfo;

		public GameObject objReady;

		public void Init()
		{
			this.m_MyTower = TheDataManager.Instance.GetTowerData(this.eTower, TheEnumManager.TOWER_LEVEL.level_1);
			this.txtTitle.text = this.m_MyTower.strName;
			this.txtContent.text = "UNLOCK AT LEVEL " + this.m_MyTower.iLevelToUnlock;
			if (!TheDataManager.THE_PLAYER_DATA.CheckUnlockTower(this.eTower))
			{
				this.objReady.SetActive(false);
			}
			else
			{
				this.objReady.SetActive(true);
			}
		}

		public void ShowBoardToBuy()
		{
			if (TheDataManager.THE_PLAYER_DATA.CheckUnlockTower(this.m_MyTower.eTower))
			{
				Note.ShowPopupNote(Note.NOTE.TowerIsReady);
				return;
			}
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);
			ShopTowerAndWeapon.Instance.objScreenBuy.SetActive(true);
			ShopTowerAndWeapon.Instance.MY_TOWER_DATA = this.m_MyTower;
			ShopTowerAndWeapon.Instance.buBuyTowerWithGem.GetComponentInChildren<Text>().text = this.m_MyTower.iPriceGemToUnlock.ToString();
			ShopTowerAndWeapon.Instance.buBuyTowerWithIap.GetComponentInChildren<Text>().text = TheDataManager.Instance.GetShopData(this.m_MyTower.eTower.ToString()).fPriceDollar.ToString() + " USD";
		}

		public void ShowInfo()
		{
			string content = string.Concat(new object[]
			{
				this.m_MyTower.strName,
				"\n______________________ \n",
				this.m_MyTower.strContent,
				"\n* DAMAGE: ",
				this.m_MyTower.GetDamage(),
				" \n* RELOAD: ",
				this.m_MyTower.fAttackSpeed,
				"s \n* RANGE: ",
				this.m_MyTower.fRange,
				"m \n______________________________ \nPRICE: ",
				this.m_MyTower.iPriceGemToUnlock,
				" GEMS \nOR FREE FROM LEVEL ",
				this.m_MyTower.iLevelToUnlock,
				"."
			});
			Note.ShowPopupNote(content);
		}
	}

	private sealed class _Start_c__AnonStorey0
	{
		internal ShopTowerAndWeapon.TRACK_TOWER _temp;

		internal void __m__0()
		{
			this._temp.ShowBoardToBuy();
		}

		internal void __m__1()
		{
			this._temp.ShowInfo();
		}
	}

	private static ShopTowerAndWeapon Instance;

	public GameObject objScreenBuy;

	public Button buBuyTowerWithGem;

	public Button buBuyTowerWithIap;

	public Button buClose;

	public MyTowerData MY_TOWER_DATA;

	public List<ShopTowerAndWeapon.TRACK_TOWER> LIST_TOWER;

	private void Awake()
	{
		if (ShopTowerAndWeapon.Instance == null)
		{
			ShopTowerAndWeapon.Instance = this;
		}
	}

	private void Start()
	{
		this.buClose.onClick.AddListener(delegate
		{
			this.CloseButtonBuytTower();
		});
		this.buBuyTowerWithIap.onClick.AddListener(delegate
		{
			this.BuyTowerWithIap();
		});
		this.buBuyTowerWithGem.onClick.AddListener(delegate
		{
			this.BuyTowerWithGem();
		});
		for (int i = 0; i < this.LIST_TOWER.Count; i++)
		{
			ShopTowerAndWeapon.TRACK_TOWER _temp = default(ShopTowerAndWeapon.TRACK_TOWER);
			_temp = this.LIST_TOWER[i];
			_temp.Init();
			this.LIST_TOWER[i] = _temp;
			if (_temp.buButtonBuy)
			{
				_temp.buButtonBuy.onClick.AddListener(delegate
				{
					_temp.ShowBoardToBuy();
				});
			}
			if (_temp.buShowInfo)
			{
				_temp.buShowInfo.onClick.AddListener(delegate
				{
					_temp.ShowInfo();
				});
			}
		}
	}

	private void CloseButtonBuytTower()
	{
		TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_back);
		this.objScreenBuy.SetActive(false);
	}

	private void BuyTowerWithGem()
	{
		if (TheDataManager.THE_PLAYER_DATA.iPlayerGem >= this.MY_TOWER_DATA.iPriceGemToUnlock)
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);
			TheDataManager.THE_PLAYER_DATA.iPlayerGem -= this.MY_TOWER_DATA.iPriceGemToUnlock;
			TheDataManager.THE_PLAYER_DATA.SetUnlockTower(this.MY_TOWER_DATA.eTower, true);
			TheDataManager.Instance.SerialzerPlayerData();
			TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
			for (int i = 0; i < this.LIST_TOWER.Count; i++)
			{
				this.LIST_TOWER[i].Init();
			}
		}
		else
		{
			Note.ShowPopupNote(Note.NOTE.NotEnoughtGem);
		}
		this.objScreenBuy.SetActive(false);
	}

	private void BuyTowerWithIap()
	{
		InAppPurchaseManager.Instance.BuyTower(this.MY_TOWER_DATA.eTower.ToString());
	}

	private void ShowInfo()
	{
		for (int i = 0; i < this.LIST_TOWER.Count; i++)
		{
			this.LIST_TOWER[i].Init();
		}
		this.objScreenBuy.SetActive(false);
	}

	private void OnEnable()
	{
		this.ShowInfo();
		TheEventManager.RegisterEvent(TheEventManager.EventID.UNLOCK_TOWER, new TheEventManager.ACTION(this.ShowInfo));
	}

	private void OnDisable()
	{
		TheEventManager.RemoveEvent(TheEventManager.EventID.UNLOCK_TOWER, new TheEventManager.ACTION(this.ShowInfo));
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class TheButtonTower : MonoBehaviour
{
	[Serializable]
	public struct CONFIG_BUTTON
	{
		public Button buButtonBuyTower;

		public TheEnumManager.TOWER eTowerKind;

		public MyTowerData _myTowerData;

		public void Init()
		{
			if (!TheDataManager.THE_PLAYER_DATA.CheckUnlockTower(this.eTowerKind))
			{
				this.buButtonBuyTower.image.color = Color.gray;
				this.buButtonBuyTower.GetComponentInChildren<Text>().text = string.Empty;
				this.buButtonBuyTower.transform.GetChild(1).GetComponent<Image>().color = Color.white;
			}
			else
			{
				this.buButtonBuyTower.image.color = Color.white;
				this.buButtonBuyTower.GetComponentInChildren<Text>().text = this._myTowerData.iPriceToBuy.ToString();
				this.buButtonBuyTower.transform.GetChild(1).GetComponent<Image>().color = Color.white * 0f;
			}
		}

		public void BuyTower()
		{
			if (!TheDataManager.THE_PLAYER_DATA.CheckUnlockTower(this.eTowerKind))
			{
				TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_back);
				return;
			}
			if (TheLevel.Instance.iOriginalCoin >= this._myTowerData.iPriceToBuy)
			{
				TheLevel.Instance.iOriginalCoin -= this._myTowerData.iPriceToBuy;
				TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
				GameObject gameObject = TheObjPoolingManager.Instance.GetTower(this.eTowerKind).objTower;
				gameObject = UnityEngine.Object.Instantiate<GameObject>(gameObject, TheButtonTower.OBJ_BUILD_POS.transform.position, Quaternion.identity);
				GameObject item = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Smock).GetItem();
				if (item)
				{
					item.transform.position = gameObject.transform.position;
					item.SetActive(true);
				}
				UnityEngine.Object.Destroy(TheButtonTower.OBJ_BUILD_POS.gameObject);
				TheButtonTower.Instance.ShowBoard(TheButtonTower.Board.HideAll);
				TheButtonTower.BACKGROUND_COLLIDER.SetActive(true);
				TheButtonTower.ARROW.transform.position = Vector2.one * 1000f;
				TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.tower_build);
			}
			else
			{
				UnityEngine.Debug.Log("CANT BUY THIS TOWER");
				Note.ShowPopupNote(Note.NOTE.NotEnoughtCoin);
			}
		}

		public void CheckEnoughtCoinToBuy()
		{
			if (TheLevel.Instance.iOriginalCoin >= TheDataManager.Instance.GetTowerData(this.eTowerKind, TheEnumManager.TOWER_LEVEL.level_1).iPriceToBuy)
			{
				this.buButtonBuyTower.image.color = Color.white;
			}
			else
			{
				this.buButtonBuyTower.image.color = Color.gray;
			}
		}
	}

	public enum Properties
	{
		Upgrade,
		Sell
	}

	public enum Board
	{
		BuyTower,
		UpgradeOfSell,
		HideAll
	}

	private sealed class _Start_c__AnonStorey0
	{
		internal TheButtonTower.CONFIG_BUTTON _temp;

		internal void __m__0()
		{
			this._temp.BuyTower();
		}
	}

	public static TheButtonTower Instance;

	public List<TheButtonTower.CONFIG_BUTTON> LIST_BUTTON_BUY_TOWER;

	public GameObject AREA_NOT_ALLOW_PLAYER_INPUT;

	[Space(20f)]
	public Button buUpgradeTower;

	[Space(20f)]
	public Button buSellTower;

	public GameObject objBoardBuyTower;

	public GameObject objBoardUpradeTower;

	public GameObject objBackgroundColliderForUI;

	private static GameObject BACKGROUND_COLLIDER;

	private static GameObject BOARD_BUY_TOWER;

	private static GameObject ARROW;

	private static Tower SELECTED_TOWER;

	private static GameObject OBJ_BUILD_POS;

	public TheButtonTower.CONFIG_BUTTON GetButtonConfig(TheEnumManager.TOWER _towerKind)
	{
		int count = this.LIST_BUTTON_BUY_TOWER.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.LIST_BUTTON_BUY_TOWER[i].eTowerKind == _towerKind)
			{
				return this.LIST_BUTTON_BUY_TOWER[i];
			}
		}
		return this.LIST_BUTTON_BUY_TOWER[0];
	}

	private void Awake()
	{
		if (TheButtonTower.Instance == null)
		{
			TheButtonTower.Instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Start()
	{
		TheButtonTower.BOARD_BUY_TOWER = this.objBoardBuyTower;
		TheButtonTower.ARROW = MainCode_Gameplay.Instance.m_BoardMark;
		TheButtonTower.BACKGROUND_COLLIDER = this.objBackgroundColliderForUI;
		TheDataManager.Instance.SetUnlockTower();
		for (int i = 0; i < this.LIST_BUTTON_BUY_TOWER.Count; i++)
		{
			TheButtonTower.CONFIG_BUTTON _temp = default(TheButtonTower.CONFIG_BUTTON);
			_temp = this.LIST_BUTTON_BUY_TOWER[i];
			_temp._myTowerData = TheDataManager.Instance.GetTowerData(_temp.eTowerKind, TheEnumManager.TOWER_LEVEL.level_1);
			_temp.Init();
			_temp.buButtonBuyTower.onClick.AddListener(delegate
			{
				_temp.BuyTower();
			});
			this.LIST_BUTTON_BUY_TOWER[i] = _temp;
		}
		this.buUpgradeTower.onClick.AddListener(delegate
		{
			this.UpgradeTower(TheButtonTower.Properties.Upgrade);
		});
		this.buSellTower.onClick.AddListener(delegate
		{
			this.UpgradeTower(TheButtonTower.Properties.Sell);
		});
		this.ShowBoard(TheButtonTower.Board.HideAll);
		this.AREA_NOT_ALLOW_PLAYER_INPUT.SetActive(false);
	}

	public void ShowBoard(TheButtonTower.Board _board)
	{
		if (_board != TheButtonTower.Board.BuyTower)
		{
			if (_board != TheButtonTower.Board.UpgradeOfSell)
			{
				if (_board == TheButtonTower.Board.HideAll)
				{
					this.AREA_NOT_ALLOW_PLAYER_INPUT.SetActive(false);
					this.objBoardBuyTower.SetActive(false);
					this.objBoardUpradeTower.SetActive(false);
					this.objBackgroundColliderForUI.gameObject.SetActive(true);
					MainCode_Gameplay.Instance.m_BoardMark.transform.position = Vector2.one * 1000f;
				}
			}
			else
			{
				this.AREA_NOT_ALLOW_PLAYER_INPUT.SetActive(true);
				this.objBoardBuyTower.SetActive(false);
				this.objBoardUpradeTower.SetActive(true);
				this.objBackgroundColliderForUI.gameObject.SetActive(false);
				if (TheButtonTower.SELECTED_TOWER.eTowerLevel == TheEnumManager.TOWER_LEVEL.level_4)
				{
					this.buUpgradeTower.image.color = Color.gray;
				}
				else
				{
					this.buUpgradeTower.image.color = Color.white;
				}
				this.buUpgradeTower.GetComponentInChildren<Text>().text = TheButtonTower.SELECTED_TOWER.m_myTowerData.iPriceToUpgrade.ToString();
				this.buSellTower.GetComponentInChildren<Text>().text = TheButtonTower.SELECTED_TOWER.m_myTowerData.iValueOfSell.ToString();
			}
		}
		else
		{
			this.AREA_NOT_ALLOW_PLAYER_INPUT.SetActive(true);
			this.objBoardBuyTower.SetActive(true);
			this.objBoardUpradeTower.SetActive(false);
			this.objBackgroundColliderForUI.gameObject.SetActive(false);
		}
	}

	public bool IsShowing()
	{
		return this.objBoardBuyTower.activeInHierarchy || this.objBoardUpradeTower.activeInHierarchy;
	}

	public void SetSelectedTower(Tower _tower)
	{
		TheButtonTower.SELECTED_TOWER = _tower;
	}

	public void SetTowerPosToBuild(GameObject _objBuildPos)
	{
		TheButtonTower.OBJ_BUILD_POS = _objBuildPos;
	}

	private void UpgradeTower(TheButtonTower.Properties _properties)
	{
		if (_properties != TheButtonTower.Properties.Upgrade)
		{
			if (_properties == TheButtonTower.Properties.Sell)
			{
				UnityEngine.Object.Instantiate<GameObject>(TheObjPoolingManager.Instance.GetTowerPosObj(TheLevel.Instance.eKindOfMap), TheButtonTower.SELECTED_TOWER.transform.position, Quaternion.identity);
				TheButtonTower.SELECTED_TOWER.SellTower();
				TheButtonTower.Instance.ShowBoard(TheButtonTower.Board.HideAll);
				MainCode_Gameplay.Instance.m_boardInfoTower.Hide();
			}
		}
		else
		{
			if (TheButtonTower.SELECTED_TOWER.eTowerLevel != TheEnumManager.TOWER_LEVEL.level_4)
			{
				GameObject item = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Smock).GetItem();
				if (item)
				{
					item.transform.position = TheButtonTower.SELECTED_TOWER.transform.position;
					item.SetActive(true);
				}
				TheButtonTower.SELECTED_TOWER.UpgradeTower();
				this.buUpgradeTower.GetComponentInChildren<Text>().text = TheButtonTower.SELECTED_TOWER.m_myTowerData.iPriceToUpgrade.ToString();
			}
			else
			{
				MonoBehaviour.print("MAX LEVEL");
			}
			if (TheButtonTower.SELECTED_TOWER.eTowerLevel == TheEnumManager.TOWER_LEVEL.level_4)
			{
				this.buUpgradeTower.image.color = Color.gray;
			}
			else
			{
				this.buUpgradeTower.image.color = Color.white;
			}
			MainCode_Gameplay.Instance.m_boardInfoTower.ShowContent(TheButtonTower.SELECTED_TOWER.eTower, TheButtonTower.SELECTED_TOWER.m_myTowerData);
		}
	}

	private void OnEnable()
	{
	}
}

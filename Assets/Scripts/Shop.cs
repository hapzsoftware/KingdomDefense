using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
	public enum PANEL
	{
		Gems,
		PowerUps,
		Coins,
		Towers
	}

	[Serializable]
	public struct CONFIG_PANEL
	{
		public Shop.PANEL ePanel;

		public Button buButton;

		public GameObject objPanel;
	}

	public Button buFreeGem;

	public Button buBack;

	public GameObject objBoardCoin;

	public List<Shop.CONFIG_PANEL> LIST_PANEL;

	public static Shop.PANEL CHOOSE_PANEL;

	private static UnityAction __f__am_cache0;

	private static UnityAction __f__am_cache1;

	private void Start()
	{
		this.buBack.onClick.AddListener(delegate
		{
			TheUIManager.HidePopup(ThePopupManager.POP_UP.Shop);
		});
		this.buFreeGem.onClick.AddListener(delegate
		{
			TheAdsManager.Instance.WatchRewardedVideo(TheAdsManager.REWARED_VIDEO.FreeGem);
		});
		this.LIST_PANEL[0].buButton.onClick.AddListener(delegate
		{
			this.CallPanel(Shop.PANEL.Gems);
		});
		this.LIST_PANEL[1].buButton.onClick.AddListener(delegate
		{
			this.CallPanel(Shop.PANEL.PowerUps);
		});
		this.LIST_PANEL[2].buButton.onClick.AddListener(delegate
		{
			this.CallPanel(Shop.PANEL.Towers);
		});
		this.LIST_PANEL[3].buButton.onClick.AddListener(delegate
		{
			this.CallPanel(Shop.PANEL.Coins);
		});
	}

	public void CallPanel(Shop.PANEL _panel)
	{
		TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);
		for (int i = 0; i < this.LIST_PANEL.Count; i++)
		{
			if (_panel == this.LIST_PANEL[i].ePanel)
			{
				this.LIST_PANEL[i].buButton.image.color = Color.white;
				this.LIST_PANEL[i].objPanel.SetActive(true);
			}
			else
			{
				this.LIST_PANEL[i].buButton.image.color = Color.gray;
				this.LIST_PANEL[i].objPanel.SetActive(false);
			}
		}
		if (_panel == Shop.PANEL.Coins)
		{
			this.buFreeGem.gameObject.SetActive(false);
			this.objBoardCoin.SetActive(true);
		}
		else
		{
			this.buFreeGem.gameObject.SetActive(true);
			this.objBoardCoin.SetActive(false);
		}
	}

	private void OnEnable()
	{
		TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
		this.CallPanel(Shop.CHOOSE_PANEL);
		if (TheUIManager.isLoadingScene(TheEnumManager.SCENE.Gameplay))
		{
			this.LIST_PANEL[3].buButton.gameObject.SetActive(true);
		}
		else
		{
			this.LIST_PANEL[3].buButton.gameObject.SetActive(false);
		}
	}
}

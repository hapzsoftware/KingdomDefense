using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
	public enum NOTE
	{
		NotEnoughtCoin,
		NotEnoughtGem,
		WatchAdsToGetFreeGem,
		GetFreeGem,
		Need3StarToUnlock,
		RewardedVideo,
		ResetGame,
		TowerIsReady,
		AddContent
	}

	private static Note.NOTE eNote;

	public Button buBack;

	public Button buOk;

	public Text txtContent;

	private static string m_strContent;

	private static UnityAction __f__am_cache0;

	private void Start()
	{
		this.buBack.onClick.AddListener(delegate
		{
			TheUIManager.HidePopup(ThePopupManager.POP_UP.Note);
		});
		this.buOk.onClick.AddListener(delegate
		{
			this.ButtonOk();
		});
	}

	private void TextContent()
	{
		switch (Note.eNote)
		{
		case Note.NOTE.NotEnoughtCoin:
			this.txtContent.text = "Woop! Not enghout coin! \n Do you wan't more?";
			break;
		case Note.NOTE.NotEnoughtGem:
			this.txtContent.text = "Woop! Not enghout gem! \n Do you wan't more?";
			break;
		case Note.NOTE.WatchAdsToGetFreeGem:
			this.txtContent.text = "Watch Ads to get +" + TheDataManager.THE_PLAYER_DATA.iGemFormWatchingAds + " GEMS now!";
			break;
		case Note.NOTE.GetFreeGem:
			this.txtContent.text = "Perfect! You get " + TheDataManager.THE_PLAYER_DATA.iGemFormWatchingAds + " GEMS!";
			break;
		case Note.NOTE.Need3StarToUnlock:
			this.txtContent.text = "Need 3 stars to unlock !";
			break;
		case Note.NOTE.RewardedVideo:
			this.txtContent.text = "Congratulations, you got the gift!";
			break;
		case Note.NOTE.ResetGame:
			this.txtContent.text = "You will lose all data. \n Are you sure?";
			break;
		case Note.NOTE.TowerIsReady:
			this.txtContent.text = "This Tower is ready to use!";
			break;
		case Note.NOTE.AddContent:
			this.txtContent.text = Note.m_strContent;
			break;
		}
	}

	public static void ShowPopupNote(Note.NOTE _note)
	{
		Note.eNote = _note;
		TheUIManager.ShowPopup(ThePopupManager.POP_UP.Note);
	}

	public static void ShowPopupNote(string _content)
	{
		Note.eNote = Note.NOTE.AddContent;
		Note.m_strContent = _content;
		TheUIManager.ShowPopup(ThePopupManager.POP_UP.Note);
	}

	private void ButtonOk()
	{
		switch (Note.eNote)
		{
		case Note.NOTE.NotEnoughtCoin:
			TheUIManager.ShowPopup(Shop.PANEL.Coins);
			break;
		case Note.NOTE.NotEnoughtGem:
			TheUIManager.ShowPopup(Shop.PANEL.Gems);
			break;
		case Note.NOTE.WatchAdsToGetFreeGem:
			TheAdsManager.Instance.WatchRewardedVideo(TheAdsManager.REWARED_VIDEO.FreeGem);
			break;
		case Note.NOTE.RewardedVideo:
			TheUIManager.HidePopup(ThePopupManager.POP_UP.Note);
			TheUIManager.HidePopup(ThePopupManager.POP_UP.RewardedVideo);
			break;
		case Note.NOTE.ResetGame:
			TheDataManager.Instance.ResetGame();
			break;
		}
		TheUIManager.HidePopup(ThePopupManager.POP_UP.Note);
	}

	private void OnDisable()
	{
		if (Note.eNote == Note.NOTE.GetFreeGem)
		{
			TheDataManager.THE_PLAYER_DATA.iPlayerGem += TheDataManager.THE_PLAYER_DATA.iGemFormWatchingAds;
			TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
			TheDataManager.Instance.SerialzerPlayerData();
		}
	}

	private void OnEnable()
	{
		this.TextContent();
	}
}

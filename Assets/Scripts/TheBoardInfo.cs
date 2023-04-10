using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TheBoardInfo : MonoBehaviour
{
	public Text txtValue;

	public TheEnumManager.BOARD_INFO eBoardInfo;

	private Button buButton;

	private static UnityAction __f__am_cache0;

	private static UnityAction __f__am_cache1;

	private void Awake()
	{
		this.txtValue = base.GetComponentInChildren<Text>();
		if (this.eBoardInfo == TheEnumManager.BOARD_INFO.GemBoard)
		{
			this.buButton = base.GetComponent<Button>();
			this.buButton.onClick.AddListener(delegate
			{
				TheUIManager.ShowPopup(Shop.PANEL.Gems);
			});
		}
		if (this.eBoardInfo == TheEnumManager.BOARD_INFO.CoinBoard)
		{
			this.buButton = base.GetComponent<Button>();
			this.buButton.onClick.AddListener(delegate
			{
				TheUIManager.ShowPopup(Shop.PANEL.Coins);
			});
		}
	}

	private void UpdateBoard()
	{
		switch (this.eBoardInfo)
		{
		case TheEnumManager.BOARD_INFO.GemBoard:
			this.txtValue.text = TheDataManager.THE_PLAYER_DATA.iPlayerGem.ToString();
			break;
		case TheEnumManager.BOARD_INFO.CoinBoard:
			if (TheLevel.Instance)
			{
				this.txtValue.text = TheLevel.Instance.iOriginalCoin.ToString();
			}
			break;
		case TheEnumManager.BOARD_INFO.WaveBoard:
			if (TheLevel.Instance)
			{
				this.txtValue.text = string.Concat(new object[]
				{
					"WAVE:    ",
					TheLevel.Instance.iCurrentWave.ToString(),
					"/",
					TheLevel.Instance.iMAX_WAVE_CONFIG
				});
			}
			break;
		case TheEnumManager.BOARD_INFO.HeartBoard:
			if (TheLevel.Instance && TheLevel.Instance.iCurrentHeart >= 0)
			{
				this.txtValue.text = TheLevel.Instance.iCurrentHeart.ToString();
			}
			break;
		case TheEnumManager.BOARD_INFO.StarToUpgradeBoard_Normal:
		{
					string text = TheDataManager.Instance.GetTotalStarWasUsed(TheEnumManager.STAR.white).ToString(); /*+ "/" + TheDataManager.THE_PLAYER_DATA.GetTotalStar(TheEnumManager.DIFFICUFT.Normal).ToString();*/
			this.txtValue.text = text;
			break;
		}
		case TheEnumManager.BOARD_INFO.StarToUpgradeBoard_Hard:
		{
			string text2 = TheDataManager.Instance.GetTotalStarWasUsed(TheEnumManager.STAR.blue).ToString(); /*+ "/" + TheDataManager.THE_PLAYER_DATA.GetTotalStar(TheEnumManager.DIFFICUFT.Hard).ToString();*/
			this.txtValue.text = text2;
			break;
		}
		case TheEnumManager.BOARD_INFO.StarToUpgradeBoard_Nightmate:
		{
			string text3 = TheDataManager.Instance.GetTotalStarWasUsed(TheEnumManager.STAR.yellow).ToString(); /*+ "/" + TheDataManager.THE_PLAYER_DATA.GetTotalStar(TheEnumManager.DIFFICUFT.Nightmate).ToString();*/
			this.txtValue.text = text3;
			break;
		}
		//case TheEnumManager.BOARD_INFO.TotalStarBoard_Normal:
		//	this.txtValue.text = TheDataManager.THE_PLAYER_DATA.GetTotalStar(TheEnumManager.DIFFICUFT.Normal).ToString();
		//	break;
		//case TheEnumManager.BOARD_INFO.TotalStarBoard_Hard:
		//	this.txtValue.text = TheDataManager.THE_PLAYER_DATA.GetTotalStar(TheEnumManager.DIFFICUFT.Hard).ToString();
		//	break;
		//case TheEnumManager.BOARD_INFO.TotalStarBoard_Nightmate:
		//	this.txtValue.text = TheDataManager.THE_PLAYER_DATA.GetTotalStar(TheEnumManager.DIFFICUFT.Nightmate).ToString();
		//	break;
		}
	}

	private void OnEnable()
	{
		this.UpdateBoard();
		TheEventManager.RegisterEvent(TheEventManager.EventID.UPDATE_BOARD_INFO, new TheEventManager.ACTION(this.UpdateBoard));
	}

	private void OnDisable()
	{
		TheEventManager.RemoveEvent(TheEventManager.EventID.UPDATE_BOARD_INFO, new TheEventManager.ACTION(this.UpdateBoard));
	}
}

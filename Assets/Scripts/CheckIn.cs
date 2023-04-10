using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CheckIn : MonoBehaviour
{
	public Button buBack;

	public List<Button> LIST_BUTTON_WEEK;

	public List<CheckIn_ButtonDay> LIST_BUTTON_DAY;

	private int iIndexOfWeek;

	private int iIndexOfDay;

	public int iNumberOfGiftsReceived;

	public Text txtTextResuft;

	private int iCurrentDayOfYear;

	private int iCurrentYear;

	private static UnityAction __f__am_cache0;

	private void Start()
	{
		this.buBack.onClick.AddListener(delegate
		{
			ThePopupManager.Instance.Hide(ThePopupManager.POP_UP.CheckIn);
		});
		this.LIST_BUTTON_WEEK[0].onClick.AddListener(delegate
		{
			this.ButtonWeek(1);
		});
		this.LIST_BUTTON_WEEK[1].onClick.AddListener(delegate
		{
			this.ButtonWeek(2);
		});
		this.LIST_BUTTON_WEEK[2].onClick.AddListener(delegate
		{
			this.ButtonWeek(3);
		});
		this.LIST_BUTTON_WEEK[3].onClick.AddListener(delegate
		{
			this.ButtonWeek(4);
		});
		this.LIST_BUTTON_WEEK[4].onClick.AddListener(delegate
		{
			this.ButtonWeek(5);
		});
	}

	private void OnEnable()
	{
		this.ButtonWeek((int)((float)this.iNumberOfGiftsReceived * 1f / 7f) + 1);
		this.ShowRedNote();
	}

	private void ButtonWeek(int _index)
	{
		TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);
		this.iIndexOfWeek = _index;
		int count = this.LIST_BUTTON_DAY.Count;
		for (int i = 0; i < count; i++)
		{
			int iIndex = i + (_index - 1) * 7;
			this.LIST_BUTTON_DAY[i].iIndex = iIndex;
			this.LIST_BUTTON_DAY[i].Init();
		}
		for (int j = 0; j < this.LIST_BUTTON_WEEK.Count; j++)
		{
			if (j == _index - 1)
			{
				this.LIST_BUTTON_WEEK[j].image.color = Color.white;
			}
			else
			{
				this.LIST_BUTTON_WEEK[j].image.color = Color.gray;
			}
		}
	}

	private void ShowRedNote()
	{
		this.iNumberOfGiftsReceived = TheDataManager.THE_PLAYER_DATA.iNumberOfGiftsReceived;
		int num = (int)((float)this.iNumberOfGiftsReceived * 1f / 7f);
		for (int i = 0; i < this.LIST_BUTTON_WEEK.Count; i++)
		{
			if (i == num)
			{
				this.LIST_BUTTON_WEEK[i].transform.GetChild(1).gameObject.SetActive(true);
			}
			else
			{
				this.LIST_BUTTON_WEEK[i].transform.GetChild(1).gameObject.SetActive(false);
			}
		}
	}

	public DateTime GetToday()
	{
		return DateTime.Today;
	}

	public bool CheckToShowCheckInPopup()
	{
		this.iNumberOfGiftsReceived = TheDataManager.THE_PLAYER_DATA.iNumberOfGiftsReceived;
		this.iCurrentYear = TheDataManager.THE_PLAYER_DATA.iCurrentYear;
		this.iCurrentDayOfYear = TheDataManager.THE_PLAYER_DATA.iCurrentDayOfYear;
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"TIME HE THONG: ",
			this.GetToday().Year,
			"/DayOfYear: ",
			this.GetToday().DayOfYear
		}));
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"CURRENT: ",
			this.iCurrentYear,
			"/DayOfYear: ",
			this.iCurrentDayOfYear
		}));
		return this.GetToday().Year > this.iCurrentYear || this.GetToday().DayOfYear > this.iCurrentDayOfYear;
	}

	public void GetGiftCheckIn(int _indexOfGift)
	{
		this.iCurrentYear = TheDataManager.THE_PLAYER_DATA.iCurrentYear;
		this.iCurrentDayOfYear = TheDataManager.THE_PLAYER_DATA.iCurrentDayOfYear;
		UnityEngine.Debug.Log("_indexOfGift - iNumberOfGiftsReceived: " + _indexOfGift.ToString() + "/" + this.iNumberOfGiftsReceived.ToString());
		if (_indexOfGift - this.iNumberOfGiftsReceived != 0)
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_can_not);
			UnityEngine.Debug.Log("Chưa dến ngày!");
			return;
		}
		if (this.GetToday().Year > this.iCurrentYear || this.GetToday().DayOfYear > this.iCurrentDayOfYear)
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);
			TheDataManager.THE_PLAYER_DATA.iNumberOfGiftsReceived = this.iNumberOfGiftsReceived;
			TheDataManager.THE_PLAYER_DATA.iCurrentYear = this.GetToday().Year;
			TheDataManager.THE_PLAYER_DATA.iCurrentDayOfYear = this.GetToday().DayOfYear;
			TheDataManager.Instance.SerialzerPlayerData();
			TheCheckInGiftManager.Instance.ReturnGift((TheCheckInGiftManager.Gift)this.iNumberOfGiftsReceived);
			this.ShowTextAnimation("+" + TheCheckInGiftManager.Instance.GetGiftValue((TheCheckInGiftManager.Gift)this.iNumberOfGiftsReceived).ToString());
			this.iNumberOfGiftsReceived++;
		}
	}

	public void ShowTextAnimation(string _text)
	{
		this.txtTextResuft.text = _text;
		this.txtTextResuft.gameObject.SetActive(false);
		this.txtTextResuft.gameObject.SetActive(true);
	}
}

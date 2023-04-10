using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainCode_LevelSelection : MonoBehaviour
{
	public static MainCode_LevelSelection Instance;

	public Transform GroupButtonLevel;

	public ScrollRect m_ScrollRectLevel;

	[Space(20f)]
	public Button buBack;

	public Button buSetting;

	public Button buAcheivenment;

	public Button buLeaderboard;

	public Button buRewardedVideo;

	[Space(20f)]
	public Button buShop;

	public Button buUpgrade;

	public Button buHeroRoom;

	public Button buBook;

	[Space(20f)]
	public Text Normal;
	public Text Hard;
	public Text NightMare;


	[Space(20f)]
	public List<Sprite> LIST_SPRITE_OF_BUTTON_LEVEL_NORMAL;

	public List<Sprite> LIST_SPRITE_OF_BUTTON_LEVEL_HARD;

	public List<Sprite> LIST_SPRITE_OF_BUTTON_LEVEL_NIGHTMATE;

	[Space(20f)]
	public List<Sprite> LIST_SPRITE_ICON_MAP;

	public GameObject objBoardYouAreHere;

	private static UnityAction __f__am_cache0;

	private static UnityAction __f__am_cache1;

	private static UnityAction __f__am_cache2;

	private static UnityAction __f__am_cache3;

	private static UnityAction __f__am_cache4;

	private static UnityAction __f__am_cache5;

	private void Awake()
	{
		//ThePopupManager.Instance.SetCameraForPopupCanvas(Camera.main);
		if (MainCode_LevelSelection.Instance == null)
		{
			MainCode_LevelSelection.Instance = this;
		}
	}

	private void Start()
	{
		this.m_ScrollRectLevel.verticalNormalizedPosition = 1f;
		this.m_ScrollRectLevel.horizontalNormalizedPosition = 0f;

		this.Normal.text = TheDataManager.THE_PLAYER_DATA.GetTotalStar(TheEnumManager.DIFFICUFT.Normal).ToString();
		Hard.text = TheDataManager.THE_PLAYER_DATA.GetTotalStar(TheEnumManager.DIFFICUFT.Hard).ToString();
		NightMare.text = TheDataManager.THE_PLAYER_DATA.GetTotalStar(TheEnumManager.DIFFICUFT.Nightmate).ToString();


        this.buBack.onClick.AddListener(delegate
		{
			TheUIManager.Instance.LoadScene(TheEnumManager.SCENE.Menu, true);
		});
		this.buSetting.onClick.AddListener(delegate
		{
			TheUIManager.ShowPopup(ThePopupManager.POP_UP.Setting);
		});
		this.buShop.onClick.AddListener(delegate
		{
			TheUIManager.ShowPopup(Shop.PANEL.PowerUps);
		});
		this.buUpgrade.onClick.AddListener(delegate
		{
			TheUIManager.Instance.LoadScene(TheEnumManager.SCENE.Upgrade, false);
		});
		this.buBook.onClick.AddListener(delegate
		{
			TheUIManager.Instance.LoadScene(TheEnumManager.SCENE.Encyclopedia, false);
		});
		this.buRewardedVideo.onClick.AddListener(delegate
		{
			TheUIManager.ShowPopup(ThePopupManager.POP_UP.RewardedVideo);
		});
		TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
		this.Init();
	}

	private void Init()
	{
		int childCount = this.GroupButtonLevel.childCount;
		for (int i = 0; i < childCount; i++)
		{
			ButtonLevel component = this.GroupButtonLevel.GetChild(i).GetComponent<ButtonLevel>();
			int star = TheDataManager.THE_PLAYER_DATA.GetStar(i, TheEnumManager.DIFFICUFT.Normal);
			int star2 = TheDataManager.THE_PLAYER_DATA.GetStar(i, TheEnumManager.DIFFICUFT.Hard);
			int star3 = TheDataManager.THE_PLAYER_DATA.GetStar(i, TheEnumManager.DIFFICUFT.Nightmate);
			if (i == 0)
			{
				if (star < 0)
				{
					component.Init(i, star, TheEnumManager.DIFFICUFT.Normal, false);
					this.objBoardYouAreHere.transform.GetChild(0).GetComponentInChildren<Text>().text = (i + 1).ToString();
					this.objBoardYouAreHere.GetComponent<RectTransform>().transform.position = component.GetComponent<RectTransform>().transform.position + new Vector3(0f, 0.3f, 0f);
				}
				else if (star3 > 0)
				{
					component.Init(i, star3, TheEnumManager.DIFFICUFT.Nightmate, false);
				}
				else if (star2 > 0)
				{
					component.Init(i, star2, TheEnumManager.DIFFICUFT.Hard, false);
				}
				else if (star > 0)
				{
					component.Init(i, star, TheEnumManager.DIFFICUFT.Normal, false);
				}
			}
			else if (star > 0)
			{
				if (star3 > 0)
				{
					component.Init(i, star3, TheEnumManager.DIFFICUFT.Nightmate, false);
				}
				else if (star2 > 0)
				{
					component.Init(i, star2, TheEnumManager.DIFFICUFT.Hard, false);
				}
				else if (star > 0)
				{
					component.Init(i, star, TheEnumManager.DIFFICUFT.Normal, false);
				}
			}
			else if (star == 0 || TheDataManager.THE_PLAYER_DATA.GetStar(i - 1, TheEnumManager.DIFFICUFT.Normal) > 0)
			{
				component.Init(i, star, TheEnumManager.DIFFICUFT.Normal, false);
				this.objBoardYouAreHere.transform.GetChild(0).GetComponentInChildren<Text>().text = (i + 1).ToString();
				this.objBoardYouAreHere.transform.position = component.transform.position + new Vector3(0f, 0.55f, 0f);
			}
			else
			{
				component.Hide();
			}
		}
	}

	[ContextMenu("Show text number for level button")]
	public void ShowNumberOfLevelButton()
	{
		for (int i = 0; i < this.GroupButtonLevel.childCount; i++)
		{
			Text componentInChildren = this.GroupButtonLevel.GetChild(i).GetComponentInChildren<Text>();
			componentInChildren.text = (i + 1).ToString();
			this.GroupButtonLevel.GetChild(i).gameObject.name = (i + 1).ToString();
		}
	}

	public Sprite GetSpriteIconMap(int _level)
	{
		if (_level < this.LIST_SPRITE_ICON_MAP.Count)
		{
			return this.LIST_SPRITE_ICON_MAP[_level];
		}
		return null;
	}
}

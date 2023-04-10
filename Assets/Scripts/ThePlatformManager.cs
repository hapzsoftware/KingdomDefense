using System;
using System.Collections.Generic;
using UnityEngine;

public class ThePlatformManager : MonoBehaviour
{
	public enum MODE
	{
		Testting,
		Release
	}

	public enum GAME
	{
		Android,
		iOS
	}

	[Serializable]
	public struct PLATFORM
	{
		public string strName;

		public ThePlatformManager.GAME eGame;

		[Space(10f)]
		public string strAdmobAppId;

		public string strAdmobId_Banner;

		public string strAdmobId_Institial;

		public string strAdmobId_Rewarded;

		public string strTestDeviceAdmod;

		[Space(10f)]
		public string strUnityAdsID;

		[Space(10f)]
		public string LinkLikeMe;

		public string LinkMoreGame;

		public string LinkFacebook;

		[Space(10f)]
		public string strEmailContact;

		public string strEmailBugReport;
	}

	public static ThePlatformManager Instance;

	public ThePlatformManager.GAME CHOOSING_PLATFORM;

	public ThePlatformManager.MODE CHOOSING_MODE;

	public int TOTAL_LEVEL_IN_GAME;

	[Header("Co hieu luc khi CHOOSING_MODE = TESTTING")]
	public int LEVEL_TESTTING;

	public bool NO_IAP;

	public static ThePlatformManager.MODE eMODE;

	public static int iLevelTesting;

	public static bool bNoIAP;

	[Header("***Config platform"), Space(20f)]
	public List<ThePlatformManager.PLATFORM> LIST_PLATFORM;

	public static ThePlatformManager.PLATFORM MAIN_PLATFORM;

	public void Awake()
	{
		if (ThePlatformManager.Instance == null)
		{
			ThePlatformManager.Instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		for (int i = 0; i < this.LIST_PLATFORM.Count; i++)
		{
			if (this.CHOOSING_PLATFORM == this.LIST_PLATFORM[i].eGame)
			{
				ThePlatformManager.MAIN_PLATFORM = this.LIST_PLATFORM[i];
				break;
			}
		}
		ThePlatformManager.eMODE = this.CHOOSING_MODE;
		ThePlatformManager.iLevelTesting = this.LEVEL_TESTTING;
		ThePlatformManager.bNoIAP = this.NO_IAP;
	}

	private void Start()
	{
		base.Invoke("Init", 2f);
		this.CountTotalLevel();
	}

	private void Init()
	{
		ThePlatformManager.MODE cHOOSING_MODE = this.CHOOSING_MODE;
		if (cHOOSING_MODE != ThePlatformManager.MODE.Testting)
		{
			if (cHOOSING_MODE == ThePlatformManager.MODE.Release)
			{
				TheDataManager.THE_PLAYER_DATA.TESTING_MODE = false;
			}
		}
		else
		{
			TheDataManager.THE_PLAYER_DATA.TESTING_MODE = true;
		}
	}

	private void CountTotalLevel()
	{
		for (int i = 1; i < 1000; i++)
		{
			if (!Resources.Load<GameObject>("Levels/LEVEL_" + i))
			{
				break;
			}
			this.TOTAL_LEVEL_IN_GAME = i;
		}
	}
}

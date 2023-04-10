using System;
using UnityEngine;

public class TheDataAnalyticsManager : MonoBehaviour
{
	public static TheDataAnalyticsManager Instance;

	public TheEnumManager.LEVEL_DIFFICIFT CURRENT_LEVEL_DIFFICUFT_NORMAL;

	public TheEnumManager.LEVEL_DIFFICIFT CURRENT_LEVEL_DIFFICUFT_HARD;

	public TheEnumManager.LEVEL_DIFFICIFT CURRENT_LEVEL_DIFFICUFT_NIGHTMATE;

	[Header("TIMES PLAYED---------------------"), Space(20f)]
	private int iNumberOfTimePlayed_Total;

	[Header("TIMES PLAYED---------------------"), Space(20f)]
	public int iNumberOfTimePlayed_ModeNormal;

	public int iNumberOfTimePlayed_ModeHard;

	public int iNumberOfTimePlayed_ModeNightmate;

	[Header("TIMES OF WINNING---------------------"), Space(20f)]
	private int iNumberOfWin_Total;

	private int iNumberOfWin_ModeNormal;

	private int iNumberOfWin_ModeHard;

	private int iNumberOfWin_ModeNightmate;

	[Header("TIMES WINNING AND DEFEAT---------------------"), Space(20f)]
	public int iNumberOfWinning_1star_ModeNormal;

	public int iNumberOfWinning_2star_ModeNormal;

	public int iNumberOfWinning_3star_ModeNormal;

	[Space(10f)]
	public int iNumberOfWinning_1star_ModeHard;

	public int iNumberOfWinning_2star_ModeHard;

	public int iNumberOfWinning_3star_ModeHard;

	[Space(10f)]
	public int iNumberOfWinning_1star_ModeNightmate;

	public int iNumberOfWinning_2star_ModeNightmate;

	public int iNumberOfWinning_3star_ModeNightmate;

	[Header("TIMES OF DEFEAT---------------------"), Space(20f)]
	private int iNumberOfDefeat_Total;

	public int iNumberOfDefeat_ModeNormal;

	public int iNumberOfDefeat_ModeHard;

	public int iNumberOfDefeat_ModeNightmate;

	[Header("RATION OF WINNINER---------------------"), Space(20f)]
	public float fRatioOfVictory_Total;

	public float fRatioOfVictory_ModeNormal;

	public float fRatioOfVictory_ModeHard;

	public float fRatioOfVictory_ModeNightmate;

	[Space(20f)]
	public float fRatioOfVictory_1star_ModeNormal;

	public float fRatioOfVictory_2star_ModeNormal;

	public float fRatioOfVictory_3star_ModeNormal;

	[Space(20f)]
	public float fRatioOfVictory_1star_ModeHard;

	public float fRatioOfVictory_2star_ModeHard;

	public float fRatioOfVictory_3star_ModeHard;

	[Space(20f)]
	public float fRatioOfVictory_1star_ModeNightmate;

	public float fRatioOfVictory_2star_ModeNightmate;

	public float fRatioOfVictory_3star_ModeNightmate;

	[Header("RATION OF DEFEAT---------------------"), Space(20f)]
	public float fRatioOfDefeat_Total;

	public float fRatioOfDefeat_ModeNormal;

	public float fRatioOfDefeat_ModeHard;

	public float fRatioOfDefeat_ModeNightmate;

	private void Awake()
	{
		if (TheDataAnalyticsManager.Instance == null)
		{
			TheDataAnalyticsManager.Instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		UnityEngine.Object.DontDestroyOnLoad(this);
	}

	private void Start()
	{
		base.Invoke("Init", 2f);
	}

	private void Init()
	{
		this.Calculation();
	}

	private void GetDataFromStorage()
	{
		this.iNumberOfTimePlayed_ModeNormal = TheDataManager.THE_PLAYER_DATA.iNumberOfTimePlayed_ModeNormal;
		this.iNumberOfTimePlayed_ModeHard = TheDataManager.THE_PLAYER_DATA.iNumberOfTimePlayed_ModeHard;
		this.iNumberOfTimePlayed_ModeNightmate = TheDataManager.THE_PLAYER_DATA.iNumberOfTimePlayed_ModeNightmate;
		this.iNumberOfWinning_1star_ModeNormal = TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_1star_ModeNormal;
		this.iNumberOfWinning_2star_ModeNormal = TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_2star_ModeNormal;
		this.iNumberOfWinning_3star_ModeNormal = TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_3star_ModeNormal;
		this.iNumberOfWinning_1star_ModeHard = TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_1star_ModeHard;
		this.iNumberOfWinning_2star_ModeHard = TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_2star_ModeHard;
		this.iNumberOfWinning_3star_ModeHard = TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_3star_ModeHard;
		this.iNumberOfWinning_1star_ModeNightmate = TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_1star_ModeNightmate;
		this.iNumberOfWinning_2star_ModeNightmate = TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_2star_ModeNightmate;
		this.iNumberOfWinning_3star_ModeNightmate = TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_3star_ModeNightmate;
		this.CURRENT_LEVEL_DIFFICUFT_NORMAL = TheDataManager.THE_PLAYER_DATA.CURRENT_LEVEL_DIFFICUFT_MODE_NORMAL;
		this.CURRENT_LEVEL_DIFFICUFT_HARD = TheDataManager.THE_PLAYER_DATA.CURRENT_LEVEL_DIFFICUFT_MODE_HARD;
		this.CURRENT_LEVEL_DIFFICUFT_NIGHTMATE = TheDataManager.THE_PLAYER_DATA.CURRENT_LEVEL_DIFFICUFT_MODE_NIGHTMATE;
	}

	public void Calculation()
	{
		this.GetDataFromStorage();
		this.iNumberOfWin_ModeNormal = this.iNumberOfWinning_1star_ModeNormal + this.iNumberOfWinning_2star_ModeNormal + this.iNumberOfWinning_3star_ModeNormal;
		this.iNumberOfWin_ModeHard = this.iNumberOfWinning_1star_ModeHard + this.iNumberOfWinning_2star_ModeHard + this.iNumberOfWinning_3star_ModeHard;
		this.iNumberOfWin_ModeNightmate = this.iNumberOfWinning_1star_ModeNightmate + this.iNumberOfWinning_2star_ModeNightmate + this.iNumberOfWinning_3star_ModeNightmate;
		this.iNumberOfTimePlayed_Total = this.iNumberOfTimePlayed_ModeNormal + this.iNumberOfTimePlayed_ModeHard + this.iNumberOfTimePlayed_ModeNightmate;
		this.iNumberOfWin_Total = this.iNumberOfWin_ModeNormal + this.iNumberOfWin_ModeHard + this.iNumberOfWin_ModeNightmate;
		this.iNumberOfDefeat_Total = this.iNumberOfDefeat_ModeNormal + this.iNumberOfDefeat_ModeHard + this.iNumberOfDefeat_ModeNightmate;
		this.fRatioOfVictory_1star_ModeNormal = (float)this.iNumberOfWinning_1star_ModeNormal * 1f / (float)this.iNumberOfTimePlayed_Total;
		this.fRatioOfVictory_2star_ModeNormal = (float)this.iNumberOfWinning_2star_ModeNormal * 1f / (float)this.iNumberOfTimePlayed_Total;
		this.fRatioOfVictory_3star_ModeNormal = (float)this.iNumberOfWinning_3star_ModeNormal * 1f / (float)this.iNumberOfTimePlayed_Total;
		this.fRatioOfVictory_1star_ModeHard = (float)this.iNumberOfWinning_1star_ModeHard * 1f / (float)this.iNumberOfTimePlayed_Total;
		this.fRatioOfVictory_2star_ModeHard = (float)this.iNumberOfWinning_2star_ModeHard * 1f / (float)this.iNumberOfTimePlayed_Total;
		this.fRatioOfVictory_3star_ModeHard = (float)this.iNumberOfWinning_3star_ModeHard * 1f / (float)this.iNumberOfTimePlayed_Total;
		this.fRatioOfVictory_1star_ModeNightmate = (float)this.iNumberOfWinning_1star_ModeNightmate * 1f / (float)this.iNumberOfTimePlayed_Total;
		this.fRatioOfVictory_2star_ModeNightmate = (float)this.iNumberOfWinning_2star_ModeNightmate * 1f / (float)this.iNumberOfTimePlayed_Total;
		this.fRatioOfVictory_3star_ModeNightmate = (float)this.iNumberOfWinning_3star_ModeNightmate * 1f / (float)this.iNumberOfTimePlayed_Total;
		this.fRatioOfVictory_Total = (float)this.iNumberOfWin_Total * 1f / (float)this.iNumberOfTimePlayed_Total;
		this.fRatioOfVictory_ModeNormal = (float)this.iNumberOfWin_ModeNormal * 1f / (float)this.iNumberOfWin_ModeNormal;
		this.fRatioOfVictory_ModeHard = (float)this.iNumberOfWin_ModeHard * 1f / (float)this.iNumberOfWin_ModeHard;
		this.fRatioOfVictory_ModeNightmate = (float)this.iNumberOfWin_ModeNightmate * 1f / (float)this.iNumberOfWin_ModeNightmate;
		this.fRatioOfDefeat_Total = (float)this.iNumberOfDefeat_Total * 1f / (float)this.iNumberOfTimePlayed_Total;
		this.fRatioOfDefeat_ModeNormal = (float)this.iNumberOfDefeat_ModeNormal * 1f / (float)this.iNumberOfWin_ModeNormal;
		this.fRatioOfDefeat_ModeHard = (float)this.iNumberOfDefeat_ModeHard * 1f / (float)this.iNumberOfWin_ModeHard;
		this.fRatioOfDefeat_ModeNightmate = (float)this.iNumberOfDefeat_ModeNightmate * 1f / (float)this.iNumberOfWin_ModeNightmate;
		this.AutoBalance();
	}

	private void AutoBalance()
	{
		if (this.fRatioOfVictory_ModeNormal > 0.5f)
		{
			if (this.CURRENT_LEVEL_DIFFICUFT_NORMAL != TheEnumManager.LEVEL_DIFFICIFT.Level_10)
			{
				this.CURRENT_LEVEL_DIFFICUFT_NORMAL++;
			}
		}
		else if (this.fRatioOfVictory_ModeNormal < 0.25f && this.CURRENT_LEVEL_DIFFICUFT_NORMAL != TheEnumManager.LEVEL_DIFFICIFT.Level_1)
		{
			this.CURRENT_LEVEL_DIFFICUFT_NORMAL--;
		}
		if (this.fRatioOfVictory_ModeHard > 0.4f)
		{
			if (this.CURRENT_LEVEL_DIFFICUFT_HARD != TheEnumManager.LEVEL_DIFFICIFT.Level_10)
			{
				this.CURRENT_LEVEL_DIFFICUFT_HARD++;
			}
		}
		else if (this.fRatioOfVictory_ModeHard < 0.2f && this.CURRENT_LEVEL_DIFFICUFT_HARD != TheEnumManager.LEVEL_DIFFICIFT.Level_1)
		{
			this.CURRENT_LEVEL_DIFFICUFT_HARD--;
		}
		if (this.fRatioOfVictory_ModeNightmate > 0.3f)
		{
			if (this.CURRENT_LEVEL_DIFFICUFT_NIGHTMATE != TheEnumManager.LEVEL_DIFFICIFT.Level_10)
			{
				this.CURRENT_LEVEL_DIFFICUFT_NIGHTMATE++;
			}
		}
		else if (this.fRatioOfVictory_ModeNightmate < 0.2f && this.CURRENT_LEVEL_DIFFICUFT_NIGHTMATE != TheEnumManager.LEVEL_DIFFICIFT.Level_1)
		{
			this.CURRENT_LEVEL_DIFFICUFT_NIGHTMATE--;
		}
		TheDataManager.THE_PLAYER_DATA.CURRENT_LEVEL_DIFFICUFT_MODE_NORMAL = this.CURRENT_LEVEL_DIFFICUFT_NORMAL;
		TheDataManager.THE_PLAYER_DATA.CURRENT_LEVEL_DIFFICUFT_MODE_HARD = this.CURRENT_LEVEL_DIFFICUFT_HARD;
		TheDataManager.THE_PLAYER_DATA.CURRENT_LEVEL_DIFFICUFT_MODE_NIGHTMATE = this.CURRENT_LEVEL_DIFFICUFT_NIGHTMATE;
		TheDataManager.Instance.SerialzerPlayerData();
	}

	private void GameWinning(int _star)
	{
		TheEnumManager.DIFFICUFT gAME_DIFFICUFT = TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT;
		if (gAME_DIFFICUFT != TheEnumManager.DIFFICUFT.Normal)
		{
			if (gAME_DIFFICUFT != TheEnumManager.DIFFICUFT.Hard)
			{
				if (gAME_DIFFICUFT == TheEnumManager.DIFFICUFT.Nightmate)
				{
					TheDataManager.THE_PLAYER_DATA.iNumberOfTimePlayed_ModeNightmate++;
				}
			}
			else
			{
				TheDataManager.THE_PLAYER_DATA.iNumberOfTimePlayed_ModeHard++;
			}
		}
		else
		{
			TheDataManager.THE_PLAYER_DATA.iNumberOfTimePlayed_ModeNormal++;
		}
		TheEnumManager.DIFFICUFT gAME_DIFFICUFT2 = TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT;
		if (gAME_DIFFICUFT2 != TheEnumManager.DIFFICUFT.Normal)
		{
			if (gAME_DIFFICUFT2 != TheEnumManager.DIFFICUFT.Hard)
			{
				if (gAME_DIFFICUFT2 == TheEnumManager.DIFFICUFT.Nightmate)
				{
					if (_star == 1)
					{
						TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_1star_ModeNightmate++;
					}
					else if (_star == 2)
					{
						TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_2star_ModeNightmate++;
					}
					else if (_star == 3)
					{
						TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_3star_ModeNightmate++;
					}
				}
			}
			else if (_star == 1)
			{
				TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_1star_ModeHard++;
			}
			else if (_star == 2)
			{
				TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_2star_ModeHard++;
			}
			else if (_star == 3)
			{
				TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_3star_ModeHard++;
			}
		}
		else if (_star == 1)
		{
			TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_1star_ModeNormal++;
		}
		else if (_star == 2)
		{
			TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_2star_ModeNormal++;
		}
		else if (_star == 3)
		{
			TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_3star_ModeNormal++;
		}
		TheDataManager.Instance.SerialzerPlayerData();
		this.Calculation();
	}

	private void GameDefeat(int _star)
	{
		TheEnumManager.DIFFICUFT gAME_DIFFICUFT = TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT;
		if (gAME_DIFFICUFT != TheEnumManager.DIFFICUFT.Normal)
		{
			if (gAME_DIFFICUFT != TheEnumManager.DIFFICUFT.Hard)
			{
				if (gAME_DIFFICUFT == TheEnumManager.DIFFICUFT.Nightmate)
				{
					TheDataManager.THE_PLAYER_DATA.iNumberOfTimePlayed_ModeNightmate++;
				}
			}
			else
			{
				TheDataManager.THE_PLAYER_DATA.iNumberOfTimePlayed_ModeHard++;
			}
		}
		else
		{
			TheDataManager.THE_PLAYER_DATA.iNumberOfTimePlayed_ModeNormal++;
		}
		TheEnumManager.DIFFICUFT gAME_DIFFICUFT2 = TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT;
		if (gAME_DIFFICUFT2 != TheEnumManager.DIFFICUFT.Normal)
		{
			if (gAME_DIFFICUFT2 != TheEnumManager.DIFFICUFT.Hard)
			{
				if (gAME_DIFFICUFT2 == TheEnumManager.DIFFICUFT.Nightmate)
				{
					TheDataManager.THE_PLAYER_DATA.iNumberOfDefeat_ModeNightmate++;
				}
			}
			else
			{
				TheDataManager.THE_PLAYER_DATA.iNumberOfDefeat_ModeHard++;
			}
		}
		else
		{
			TheDataManager.THE_PLAYER_DATA.iNumberOfDefeat_ModeNormal++;
		}
		TheDataManager.Instance.SerialzerPlayerData();
		this.Calculation();
	}

	private void GameStart(int _star)
	{
	}

	private void OnEnable()
	{
		TheEventManager.OnGameWinning += new TheEventManager.EVENT_OF_GAMESTATUS(this.GameWinning);
		TheEventManager.OnGameDefeat += new TheEventManager.EVENT_OF_GAMESTATUS(this.GameDefeat);
		TheEventManager.OnGameStart += new TheEventManager.EVENT_OF_GAMESTATUS(this.GameStart);
	}

	private void OnDisable()
	{
		TheEventManager.OnGameWinning -= new TheEventManager.EVENT_OF_GAMESTATUS(this.GameWinning);
		TheEventManager.OnGameDefeat -= new TheEventManager.EVENT_OF_GAMESTATUS(this.GameDefeat);
		TheEventManager.OnGameStart -= new TheEventManager.EVENT_OF_GAMESTATUS(this.GameStart);
	}
}

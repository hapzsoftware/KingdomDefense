//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TheGooglePlayServicesManager : MonoBehaviour
{
	private sealed class _UnlockArchievement_c__AnonStorey0
	{
		internal string _idArchivement;

		internal void __m__0(bool success)
		{
			if (success)
			{
				TheDataManager.THE_PLAYER_DATA.SetActiveOfArchivement(this._idArchivement, true);
				TheDataManager.Instance.SerialzerPlayerData();
			}
		}
	}

	public static TheGooglePlayServicesManager Instance;

	public bool bSignedIn;

	private static Action<bool> __f__am_cache0;

	private void Awake()
	{
		if (TheGooglePlayServicesManager.Instance == null)
		{
			TheGooglePlayServicesManager.Instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Start()
	{
		this.InitGooglePlayerService();
	}

	private void InitGooglePlayerService()
	{
		if (!this.bSignedIn)
		{
			//PlayGamesClientConfiguration configuration = new PlayGamesClientConfiguration.Builder().Build();
			//PlayGamesPlatform.InitializeInstance(configuration);
			//PlayGamesPlatform.Activate();
			//PlayGamesPlatform.Instance.SignOut();
			this.SignIn();
		}
	}

	private void SignIn()
	{
		Social.localUser.Authenticate(delegate(bool success)
		{
			if (success)
			{
				this.bSignedIn = true;
			}
		});
	}

	public string GetPlayerName()
	{
		if (this.bSignedIn)
		{
			return Social.localUser.userName;
		}
		return string.Empty;
	}

	public string GetPlayerId()
	{
		if (this.bSignedIn)
		{
			return Social.localUser.id;
		}
		return string.Empty;
	}

	public Texture2D GetPlayerIcon()
	{
		if (this.bSignedIn)
		{
			return Social.localUser.image;
		}
		return null;
	}

	public void UnlockArchievement(string _idArchivement)
	{
		if (!TheDataManager.THE_PLAYER_DATA.GetActiveOfArchivement(_idArchivement))
		{
			Social.ReportProgress(_idArchivement, 100.0, delegate(bool success)
			{
				if (success)
				{
					TheDataManager.THE_PLAYER_DATA.SetActiveOfArchivement(_idArchivement, true);
					TheDataManager.Instance.SerialzerPlayerData();
				}
			});
		}
	}

	public void ShowArchivementUI()
	{
		Social.ShowAchievementsUI();
	}

	public void ShowLeadboardUI()
	{
		Social.ShowLeaderboardUI();
	}

	public void PostHighScoreToLeadboard()
	{
		Social.ReportScore((long)TheDataManager.THE_PLAYER_DATA.iPlayerScore, "CggIh56L8GEQAhAD", delegate(bool success)
		{
		});
	}

	private void GameWinning(int _star)
	{
		UnityEngine.Debug.Log("POST SCORE TO GOOGLE PLAY SERVICE");
		int num = 0;
		int num2 = 0;
		TheEnumManager.DIFFICUFT gAME_DIFFICUFT = TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT;
		if (gAME_DIFFICUFT != TheEnumManager.DIFFICUFT.Normal)
		{
			if (gAME_DIFFICUFT != TheEnumManager.DIFFICUFT.Hard)
			{
				if (gAME_DIFFICUFT == TheEnumManager.DIFFICUFT.Nightmate)
				{
					num = 3;
				}
			}
			else
			{
				num = 2;
			}
		}
		else
		{
			num = 1;
		}
		if (_star == 3)
		{
			num2 = 1500;
		}
		else if (_star == 2)
		{
			num2 = 1000;
		}
		else if (_star == 1)
		{
			num2 = 500;
		}
		TheDataManager.THE_PLAYER_DATA.AddPlayerScore(num * num2);
		TheDataManager.Instance.SerialzerPlayerData();
		this.PostHighScoreToLeadboard();
	}

	private void GameStart(int _i)
	{
		TheGooglePlayServicesManager.Instance.UnlockArchievement("CggIh56L8GEQAhAA");
	}

	private void OnEnable()
	{
		TheEventManager.OnGameWinning += new TheEventManager.EVENT_OF_GAMESTATUS(this.GameWinning);
		TheEventManager.OnGameStart += new TheEventManager.EVENT_OF_GAMESTATUS(this.GameStart);
	}

	private void OnDisable()
	{
		TheEventManager.OnGameWinning -= new TheEventManager.EVENT_OF_GAMESTATUS(this.GameWinning);
		TheEventManager.OnGameStart -= new TheEventManager.EVENT_OF_GAMESTATUS(this.GameStart);
	}
}

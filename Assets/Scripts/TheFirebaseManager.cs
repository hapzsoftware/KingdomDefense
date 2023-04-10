//using Firebase;
//using Firebase.Analytics;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class TheFirebaseManager : MonoBehaviour
{
	public enum USER_PROPERTIES
	{
		BUY_COIN,
		BUY_IAP,
		BUY_ITEMS,
		CLICK_BUTTON,
		GAMEOVER,
		GAMEOVER_RATE,
		START_LEVEL,
		WATCH_ADS_GET_FREE_GEM,
		CHECK_IN,
		WINNING,
		WINNING_RATE
	}

	public static TheFirebaseManager Instance;

	//private static Action<Task<DependencyStatus>> __f__am_cache0;

	private void Awake()
	{
		if (TheFirebaseManager.Instance == null)
		{
			TheFirebaseManager.Instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Start()
	{
		this.Init();
	}

	private void Init()
	{
        /*
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(delegate(Task<DependencyStatus> task)
		{
			DependencyStatus result = task.Result;
			if (result != DependencyStatus.Available)
			{
				UnityEngine.Debug.LogError(string.Format("Could not resolve all Firebase dependencies: {0}", result));
			}
		});
		*/
	}

	public void PostEvent_StartLevel()
	{
        /*
		int parameterValue = TheDataManager.THE_PLAYER_DATA.iCurrentLevel + 1;
		FirebaseAnalytics.LogEvent(TheFirebaseManager.USER_PROPERTIES.START_LEVEL.ToString(), "total", parameterValue);
		TheEnumManager.DIFFICUFT gAME_DIFFICUFT = TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT;
		if (gAME_DIFFICUFT != TheEnumManager.DIFFICUFT.Normal)
		{
			if (gAME_DIFFICUFT != TheEnumManager.DIFFICUFT.Hard)
			{
				if (gAME_DIFFICUFT == TheEnumManager.DIFFICUFT.Nightmate)
				{
					FirebaseAnalytics.LogEvent(TheFirebaseManager.USER_PROPERTIES.START_LEVEL.ToString(), "nightmare_mode", parameterValue);
				}
			}
			else
			{
				FirebaseAnalytics.LogEvent(TheFirebaseManager.USER_PROPERTIES.START_LEVEL.ToString(), "hard_mode", parameterValue);
			}
		}
		else
		{
			FirebaseAnalytics.LogEvent(TheFirebaseManager.USER_PROPERTIES.START_LEVEL.ToString(), "normal_mode", parameterValue);
		}
		*/
	}

	public void PostEvent_Winning()
	{
        /*
		int parameterValue = TheDataManager.THE_PLAYER_DATA.iCurrentLevel + 1;
		FirebaseAnalytics.LogEvent(TheFirebaseManager.USER_PROPERTIES.WINNING.ToString(), "total", parameterValue);
		TheEnumManager.DIFFICUFT gAME_DIFFICUFT = TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT;
		if (gAME_DIFFICUFT != TheEnumManager.DIFFICUFT.Normal)
		{
			if (gAME_DIFFICUFT != TheEnumManager.DIFFICUFT.Hard)
			{
				if (gAME_DIFFICUFT == TheEnumManager.DIFFICUFT.Nightmate)
				{
					FirebaseAnalytics.LogEvent(TheFirebaseManager.USER_PROPERTIES.WINNING.ToString(), "nightmare_mode", parameterValue);
				}
			}
			else
			{
				FirebaseAnalytics.LogEvent(TheFirebaseManager.USER_PROPERTIES.WINNING.ToString(), "hard_mode", parameterValue);
			}
		}
		else
		{
			FirebaseAnalytics.LogEvent(TheFirebaseManager.USER_PROPERTIES.WINNING.ToString(), "normal_mode", parameterValue);
		}
		*/
	}

	public void PostEvent_Gameover()
	{
        /*
		int parameterValue = TheDataManager.THE_PLAYER_DATA.iCurrentLevel + 1;
		FirebaseAnalytics.LogEvent(TheFirebaseManager.USER_PROPERTIES.GAMEOVER.ToString(), "total", parameterValue);
		TheEnumManager.DIFFICUFT gAME_DIFFICUFT = TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT;
		if (gAME_DIFFICUFT != TheEnumManager.DIFFICUFT.Normal)
		{
			if (gAME_DIFFICUFT != TheEnumManager.DIFFICUFT.Hard)
			{
				if (gAME_DIFFICUFT == TheEnumManager.DIFFICUFT.Nightmate)
				{
					FirebaseAnalytics.LogEvent(TheFirebaseManager.USER_PROPERTIES.GAMEOVER.ToString(), "nightmare_mode", parameterValue);
				}
			}
			else
			{
				FirebaseAnalytics.LogEvent(TheFirebaseManager.USER_PROPERTIES.GAMEOVER.ToString(), "hard_mode", parameterValue);
			}
		}
		else
		{
			FirebaseAnalytics.LogEvent(TheFirebaseManager.USER_PROPERTIES.GAMEOVER.ToString(), "normal_mode", parameterValue);
		}
		*/
	}

	public void PostEvent_BuyIAP(string _ItemName)
	{
		//FirebaseAnalytics.LogEvent(TheFirebaseManager.USER_PROPERTIES.BUY_IAP.ToString(), "iap", _ItemName);
	}

	public void PostEvent_BuyItemInShop(string _ItemName)
	{
	//FirebaseAnalytics.LogEvent(TheFirebaseManager.USER_PROPERTIES.BUY_ITEMS.ToString(), "item", _ItemName);
	}

	public void PostEvent_BuyCoin(string _ItemName)
	{
	//FirebaseAnalytics.LogEvent(TheFirebaseManager.USER_PROPERTIES.BUY_COIN.ToString(), "coin", _ItemName);
	}

	public void PostEvent_ClickButton(string _buttonName)
	{
	//FirebaseAnalytics.LogEvent(TheFirebaseManager.USER_PROPERTIES.CLICK_BUTTON.ToString(), "button", _buttonName);
	}

	public void PostEvent_WatchAdsToGetFreeGem(string _nameAds)
	{
	//FirebaseAnalytics.LogEvent(TheFirebaseManager.USER_PROPERTIES.WATCH_ADS_GET_FREE_GEM.ToString(), "freegem", _nameAds);
	}

	public void PostEvent_CheckIn(string _nameAds)
	{
	//	FirebaseAnalytics.LogEvent(TheFirebaseManager.USER_PROPERTIES.CHECK_IN.ToString(), "checkin", _nameAds);
	}

	private void GameWinning(int _star)
	{
		this.PostEvent_Winning();
	}

	private void GameDefeat(int _star)
	{
		this.PostEvent_Gameover();
	}

	private void GameStart(int _star)
	{
		this.PostEvent_StartLevel();
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

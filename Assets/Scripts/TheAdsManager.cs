using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Advertisements;

public class TheAdsManager : MonoBehaviour
{
	public enum REWARED_VIDEO
	{
		Null,
		FreeGem,
		GetGift_Pack1,
		GetGift_Pack2,
		GetGift_Pack3
	}

	private sealed class _ReRequetsInterstitital_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal float _time;

		internal TheAdsManager _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _ReRequetsInterstitital_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitForSecondsRealtime(this._time);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				if (!this._this.isReadyInterstitial_Admob())
				{
					this._this.RequestInterstitial();
				}
				else
				{
					this._this.StopCoroutine(this._this.ReRequetsInterstitital(60f));
				}
				this._PC = -1;
				break;
			}
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	private sealed class _IEShowInterstitial_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal float _delay;

		internal TheAdsManager _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _IEShowInterstitial_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitForSecondsRealtime(this._delay);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.ShowInterstitial();
				this._PC = -1;
				break;
			}
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	private sealed class _ReRequetsRewardVideo_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal float _time;

		internal TheAdsManager _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _ReRequetsRewardVideo_c__Iterator2()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitForSecondsRealtime(this._time);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				if (!this._this.isReadyRewardedVideo_Admob())
				{
					this._this.RequestRewardedVideo();
				}
				else
				{
					this._this.StopCoroutine(this._this.ReRequetsRewardVideo(60f));
				}
				this._PC = -1;
				break;
			}
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	private sealed class _IeVideoAdsSeccefull_c__Iterator3 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal TheAdsManager.REWARED_VIDEO eRewared;

		internal TheAdsManager _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _IeVideoAdsSeccefull_c__Iterator3()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitForSeconds(1f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				switch (this.eRewared)
				{
				case TheAdsManager.REWARED_VIDEO.FreeGem:
					this._this.eRewaredVideo = TheAdsManager.REWARED_VIDEO.Null;
					Note.ShowPopupNote(Note.NOTE.GetFreeGem);
					break;
				case TheAdsManager.REWARED_VIDEO.GetGift_Pack1:
					this._this.eRewaredVideo = TheAdsManager.REWARED_VIDEO.Null;
					this._this.GotGift(TheAdsManager.REWARED_VIDEO.GetGift_Pack1);
					break;
				case TheAdsManager.REWARED_VIDEO.GetGift_Pack2:
					this._this.eRewaredVideo = TheAdsManager.REWARED_VIDEO.Null;
					this._this.GotGift(TheAdsManager.REWARED_VIDEO.GetGift_Pack2);
					break;
				case TheAdsManager.REWARED_VIDEO.GetGift_Pack3:
					this._this.eRewaredVideo = TheAdsManager.REWARED_VIDEO.Null;
					this._this.GotGift(TheAdsManager.REWARED_VIDEO.GetGift_Pack3);
					break;
				}
				this._PC = -1;
				break;
			}
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	public static TheAdsManager Instance;

	public bool bShowAdmobAds;

	public bool bShowUnityAds;

	public bool bTestAdsAdmob;

	private string strTestDeviceAdmob;

	private string strAdmobAppID;

	private string strAdmobBannderID;

	private string strAdmobInterstitialID;

	private string strAdmobRewardedVideoID;

	private string strUnityAdsID = string.Empty;

	private BannerView m_BannerView;

	private InterstitialAd m_InterstitialAd;

	private RewardedAd m_RewardedVideoAdmob;

	private bool bIsInit;

	private bool isShowingBannger;

	private TheAdsManager.REWARED_VIDEO eRewaredVideo;

	public TheAdsManager.REWARED_VIDEO CURRENT_GIFT = TheAdsManager.REWARED_VIDEO.GetGift_Pack1;

	private void Awake()
	{
		if (TheAdsManager.Instance == null)
		{
			TheAdsManager.Instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		UnityEngine.Object.DontDestroyOnLoad(this);
	}

	private void Start()
	{
		base.Invoke("Init", 1f);
	}

	public void Init()
	{
		if (!this.bIsInit)
		{
			this.bIsInit = true;
			if (this.bShowAdmobAds)
			{
				this.strTestDeviceAdmob = ThePlatformManager.MAIN_PLATFORM.strTestDeviceAdmod;
				this.strAdmobAppID = ThePlatformManager.MAIN_PLATFORM.strAdmobAppId;
				this.strAdmobBannderID = ThePlatformManager.MAIN_PLATFORM.strAdmobId_Banner;
				this.strAdmobInterstitialID = ThePlatformManager.MAIN_PLATFORM.strAdmobId_Institial;
				this.strAdmobRewardedVideoID = ThePlatformManager.MAIN_PLATFORM.strAdmobId_Rewarded;
				MobileAds.Initialize(init => { });
				this.RequestBanner();
				this.RequestInterstitial();
				this.RequestRewardedVideo();
				this.m_RewardedVideoAdmob.OnUserEarnedReward += new EventHandler<Reward>(this.HandleRewardBasedVideoRewarded);
			}
			if (this.bShowUnityAds)
			{
				this.strUnityAdsID = ThePlatformManager.MAIN_PLATFORM.strUnityAdsID;
				Advertisement.Initialize(this.strUnityAdsID);
			}
		}
	}

	private bool isReadyInterstitial_Admob()
	{
		return this.m_InterstitialAd.IsLoaded();
	}

	private bool isReadyRewardedVideo_Admob()
	{
		return this.bShowAdmobAds && this.m_RewardedVideoAdmob.IsLoaded();
	}

	public void RequestBanner()
	{
		this.m_BannerView = new BannerView(this.strAdmobBannderID, AdSize.Banner, AdPosition.Bottom);
		if (this.bTestAdsAdmob)
		{
			AdRequest request = new AdRequest.Builder().Build();
			this.m_BannerView.LoadAd(request);
		}
		else
		{
			AdRequest request2 = new AdRequest.Builder().Build();
			this.m_BannerView.LoadAd(request2);
		}
		this.HideBanner();
	}

	public void ShowBanner()
	{
		if (!this.bShowAdmobAds)
		{
			return;
		}
		if (!this.isShowingBannger)
		{
			this.m_BannerView.Show();
			this.isShowingBannger = true;
		}
	}

	public void HideBanner()
	{
		this.m_BannerView.Hide();
		this.isShowingBannger = false;
	}

	private void RequestInterstitial()
	{
		this.m_InterstitialAd = new InterstitialAd(this.strAdmobInterstitialID);
		if (this.bTestAdsAdmob)
		{
			AdRequest request = new AdRequest.Builder().Build();
			this.m_InterstitialAd.LoadAd(request);
		}
		else
		{
			AdRequest request2 = new AdRequest.Builder().Build();
			this.m_InterstitialAd.LoadAd(request2);
		}
		base.StartCoroutine(this.ReRequetsInterstitital(60f));
	}

	private IEnumerator ReRequetsInterstitital(float _time)
	{
		TheAdsManager._ReRequetsInterstitital_c__Iterator0 _ReRequetsInterstitital_c__Iterator = new TheAdsManager._ReRequetsInterstitital_c__Iterator0();
		_ReRequetsInterstitital_c__Iterator._time = _time;
		_ReRequetsInterstitital_c__Iterator._this = this;
		return _ReRequetsInterstitital_c__Iterator;
	}

	private void ShowInterstitial()
	{
		if (!this.bShowAdmobAds)
		{
			return;
		}
		if (this.m_InterstitialAd.IsLoaded())
		{
			this.m_InterstitialAd.Show();
		}
	}

	private void ShowInterstitiall(float _timeDelay)
	{
		base.StartCoroutine(this.IEShowInterstitial(_timeDelay));
	}

	private IEnumerator IEShowInterstitial(float _delay)
	{
		TheAdsManager._IEShowInterstitial_c__Iterator1 _IEShowInterstitial_c__Iterator = new TheAdsManager._IEShowInterstitial_c__Iterator1();
		_IEShowInterstitial_c__Iterator._delay = _delay;
		_IEShowInterstitial_c__Iterator._this = this;
		return _IEShowInterstitial_c__Iterator;
	}

	private void RequestRewardedVideo()
	{
		m_RewardedVideoAdmob = new(strAdmobRewardedVideoID);

        if (this.bTestAdsAdmob)
		{
			AdRequest request = new AdRequest.Builder().Build();
			this.m_RewardedVideoAdmob.LoadAd(request);
		}
		else
		{
			AdRequest request2 = new AdRequest.Builder().Build();
			this.m_RewardedVideoAdmob.LoadAd(request2);
		}
		base.StartCoroutine(this.ReRequetsRewardVideo(60f));
	}

	private IEnumerator ReRequetsRewardVideo(float _time)
	{
        TheAdsManager._ReRequetsRewardVideo_c__Iterator2 _ReRequetsRewardVideo_c__Iterator = new()
        {
            _time = _time,
            _this = this
        };
        return _ReRequetsRewardVideo_c__Iterator;
	}

	private void ShowRewardedVideo_Admob(TheAdsManager.REWARED_VIDEO eRewared)
	{
		this.eRewaredVideo = eRewared;
		this.m_RewardedVideoAdmob.Show();
	}

	private void HandleRewardBasedVideoRewarded(object sender, Reward e)
	{
		base.StartCoroutine(this.IeVideoAdsSeccefull(this.eRewaredVideo));
	}

	private void ShowUnityAds()
	{
		if (!this.bShowUnityAds)
		{
			return;
		}
		Advertisement.Show();
	}
	
	private bool isReady_UnityAds()
	{
		return Advertisement.IsReady();
	}
	
	private void ShowRewardedVideo_Unity(TheAdsManager.REWARED_VIDEO eRewared)
	{
		if (!this.bShowUnityAds)
		{
			return;
		}
		this.eRewaredVideo = eRewared;
		Advertisement.Show("rewardedVideo", new ShowOptions
		{
			resultCallback = new Action<ShowResult>(this.HandleShowResult)
		});
	}
	
	private void HandleShowResult(ShowResult result)
	{
		if (result == ShowResult.Finished)
		{
			base.StartCoroutine(this.IeVideoAdsSeccefull(this.eRewaredVideo));
		}
		else if (result == ShowResult.Skipped)
		{
			UnityEngine.Debug.LogWarning("Video was skipped - Do NOT reward the player");
		}
		else if (result == ShowResult.Failed)
		{
			UnityEngine.Debug.LogError("Video failed to show");
		}
	}

	public void ShowFullAds()
	{
		if (this.isReadyInterstitial_Admob())
		{
			this.ShowInterstitial();
			this.RequestInterstitial();
			return;
		}
		if (this.isReady_UnityAds())
		{
			this.ShowUnityAds();
			return;
		}
	}

	public bool isReadyRewardedVideoAd()
	{
		return this.isReadyRewardedVideo_Admob(); //|| this.isReady_UnityAds();
	}

	public void WatchRewardedVideo(TheAdsManager.REWARED_VIDEO eRewared)
	{
		if (this.isReadyRewardedVideo_Admob())
		{
			this.ShowRewardedVideo_Admob(eRewared);
			this.RequestRewardedVideo();
			if (eRewared == TheAdsManager.REWARED_VIDEO.FreeGem)
			{
				TheFirebaseManager.Instance.PostEvent_WatchAdsToGetFreeGem("Admob");
			}
			else
			{
				TheFirebaseManager.Instance.PostEvent_CheckIn("Admob");
			}
			return;
		}
		if (this.isReady_UnityAds())
		{
			this.ShowRewardedVideo_Unity(eRewared);
			if (eRewared == TheAdsManager.REWARED_VIDEO.FreeGem)
			{
				TheFirebaseManager.Instance.PostEvent_WatchAdsToGetFreeGem("UnityAds");
			}
			else
			{
				TheFirebaseManager.Instance.PostEvent_CheckIn("UnityAds");
			}
			return;
		}
	}

	private void GotGift(TheAdsManager.REWARED_VIDEO eRewared)
	{
		if (eRewared == TheAdsManager.REWARED_VIDEO.GetGift_Pack1)
		{
			TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(TheEnumManager.SKILL.skill_freeze, TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(TheEnumManager.SKILL.skill_freeze) + 1);
			TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(TheEnumManager.SKILL.skill_fire_of_lord, TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(TheEnumManager.SKILL.skill_fire_of_lord) + 1);
			this.CURRENT_GIFT = TheAdsManager.REWARED_VIDEO.GetGift_Pack2;
		}
		else if (eRewared == TheAdsManager.REWARED_VIDEO.GetGift_Pack2)
		{
			TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(TheEnumManager.SKILL.skill_freeze, TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(TheEnumManager.SKILL.skill_freeze) + 1);
			TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(TheEnumManager.SKILL.skill_fire_of_lord, TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(TheEnumManager.SKILL.skill_fire_of_lord) + 1);
			TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(TheEnumManager.SKILL.skill_guardian, TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(TheEnumManager.SKILL.skill_guardian) + 1);
			this.CURRENT_GIFT = TheAdsManager.REWARED_VIDEO.GetGift_Pack3;
		}
		else if (eRewared == TheAdsManager.REWARED_VIDEO.GetGift_Pack3)
		{
			TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(TheEnumManager.SKILL.skill_freeze, TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(TheEnumManager.SKILL.skill_freeze) + 1);
			TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(TheEnumManager.SKILL.skill_fire_of_lord, TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(TheEnumManager.SKILL.skill_fire_of_lord) + 1);
			TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(TheEnumManager.SKILL.skill_guardian, TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(TheEnumManager.SKILL.skill_guardian) + 1);
			TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(TheEnumManager.SKILL.skill_poison, TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(TheEnumManager.SKILL.skill_poison) + 1);
			this.CURRENT_GIFT = TheAdsManager.REWARED_VIDEO.Null;
		}
		UnityEngine.Debug.Log("DONE: " + eRewared);
		TheDataManager.Instance.SerialzerPlayerData();
		Note.ShowPopupNote(Note.NOTE.RewardedVideo);
		TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
	}

	private IEnumerator IeVideoAdsSeccefull(TheAdsManager.REWARED_VIDEO eRewared)
	{
		TheAdsManager._IeVideoAdsSeccefull_c__Iterator3 _IeVideoAdsSeccefull_c__Iterator = new TheAdsManager._IeVideoAdsSeccefull_c__Iterator3();
		_IeVideoAdsSeccefull_c__Iterator.eRewared = eRewared;
		_IeVideoAdsSeccefull_c__Iterator._this = this;
		return _IeVideoAdsSeccefull_c__Iterator;
	}
}

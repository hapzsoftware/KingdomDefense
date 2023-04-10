using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RewardedVideo : MonoBehaviour
{
	public Button buBack;

	public Button buWatchAds_track1;

	public Button buWatchAds_track2;

	public Button buWatchAds_track3;

	private static UnityAction __f__am_cache0;

	private void Start()
	{
		this.buBack.onClick.AddListener(delegate
		{
			TheUIManager.HidePopup(ThePopupManager.POP_UP.RewardedVideo);
		});
		this.buWatchAds_track1.onClick.AddListener(delegate
		{
			this.GetGift(TheAdsManager.REWARED_VIDEO.GetGift_Pack1);
		});
		this.buWatchAds_track2.onClick.AddListener(delegate
		{
			this.GetGift(TheAdsManager.REWARED_VIDEO.GetGift_Pack2);
		});
		this.buWatchAds_track3.onClick.AddListener(delegate
		{
			this.GetGift(TheAdsManager.REWARED_VIDEO.GetGift_Pack3);
		});
	}

	private void GetGift(TheAdsManager.REWARED_VIDEO eRewared)
	{
		if (eRewared != TheAdsManager.Instance.CURRENT_GIFT)
		{
			return;
		}
		TheAdsManager.Instance.WatchRewardedVideo(eRewared);
	}

	private void SetReadyAds()
	{
		if (TheAdsManager.Instance.CURRENT_GIFT == TheAdsManager.REWARED_VIDEO.GetGift_Pack1 && TheAdsManager.Instance.isReadyRewardedVideoAd())
		{
			this.buWatchAds_track1.image.color = Color.white;
			this.buWatchAds_track2.image.color = Color.gray;
			this.buWatchAds_track3.image.color = Color.gray;
		}
		else if (TheAdsManager.Instance.CURRENT_GIFT == TheAdsManager.REWARED_VIDEO.GetGift_Pack2 && TheAdsManager.Instance.isReadyRewardedVideoAd())
		{
			this.buWatchAds_track1.image.color = Color.gray;
			this.buWatchAds_track2.image.color = Color.white;
			this.buWatchAds_track3.image.color = Color.gray;
		}
		else if (TheAdsManager.Instance.CURRENT_GIFT == TheAdsManager.REWARED_VIDEO.GetGift_Pack3 && TheAdsManager.Instance.isReadyRewardedVideoAd())
		{
			this.buWatchAds_track1.image.color = Color.gray;
			this.buWatchAds_track2.image.color = Color.gray;
			this.buWatchAds_track3.image.color = Color.white;
		}
		else
		{
			this.buWatchAds_track1.image.color = Color.gray;
			this.buWatchAds_track2.image.color = Color.gray;
			this.buWatchAds_track3.image.color = Color.gray;
		}
	}

	private void OnEnable()
	{
		this.SetReadyAds();
	}
}

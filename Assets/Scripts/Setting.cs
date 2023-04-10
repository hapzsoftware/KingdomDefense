using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
	public Button buMusic;

	public Button buSound;

	public Button buInfo;

	public Button buBugReport;

	public Button buFacebook;

	public Button buMenu;

	public Button buReplay;

	public Button buShop;

	public Button buLevelSelection;

	public Button buTutorial;

	public Button buRateUs;

	public Button buBack;

	private static UnityAction __f__am_cache0;

	private static UnityAction __f__am_cache1;

	private static UnityAction __f__am_cache2;

	private static UnityAction __f__am_cache3;

	private static UnityAction __f__am_cache4;

	private static UnityAction __f__am_cache5;

	private static UnityAction __f__am_cache6;

	private static UnityAction __f__am_cache7;

	private static UnityAction __f__am_cache8;

	private void Start()
	{
		this.buMusic.onClick.AddListener(delegate
		{
			this.ButtonMusic();
		});
		this.buSound.onClick.AddListener(delegate
		{
			this.ButtonSound();
		});
		this.buInfo.onClick.AddListener(delegate
		{
			TheUIManager.ShowPopup(ThePopupManager.POP_UP.AboutUs);
		});
		this.buBugReport.onClick.AddListener(delegate
		{
			TheUIManager.ReportEmail();
		});
		this.buFacebook.onClick.AddListener(delegate
		{
			TheUIManager.LoadLink(ThePlatformManager.MAIN_PLATFORM.LinkFacebook);
		});
		this.buMenu.onClick.AddListener(delegate
		{
			TheUIManager.Instance.LoadScene(TheEnumManager.SCENE.Menu, false);
		});
		this.buShop.onClick.AddListener(delegate
		{
			TheUIManager.ShowPopup(Shop.PANEL.PowerUps);
		});
		this.buReplay.onClick.AddListener(delegate
		{
			this.ButtonReplay();
		});
		this.buLevelSelection.onClick.AddListener(delegate
		{
			TheUIManager.Instance.LoadScene(TheEnumManager.SCENE.LevelSelection, false);
		});
		this.buTutorial.onClick.AddListener(delegate
		{
			TheUIManager.ShowPopup(ThePopupManager.POP_UP.Tutorial);
		});
		this.buRateUs.onClick.AddListener(delegate
		{
			TheUIManager.LoadLink(ThePlatformManager.MAIN_PLATFORM.LinkLikeMe);
		});
		this.buBack.onClick.AddListener(delegate
		{
			TheUIManager.HidePopup(ThePopupManager.POP_UP.Setting);
		});
	}

	private void ButtonMusic()
	{
		TheMusic.Instance.Mute = !TheMusic.Instance.Mute;
		this.SetStatusAudioUI();
	}

	private void ButtonSound()
	{
		TheSound.Instance.Mute = !TheSound.Instance.Mute;
		this.SetStatusAudioUI();
	}

	private void SetStatusAudioUI()
	{
		if (TheMusic.Instance.Mute)
		{
			this.buMusic.GetComponentInChildren<Text>().text = "MUSIC OFF";
			this.buMusic.image.color = Color.gray;
		}
		else
		{
			this.buMusic.GetComponentInChildren<Text>().text = "MUSIC ON";
			this.buMusic.image.color = Color.white;
		}
		if (TheSound.Instance.Mute)
		{
			this.buSound.GetComponentInChildren<Text>().text = "SOUND OFF";
			this.buSound.image.color = Color.gray;
		}
		else
		{
			this.buSound.GetComponentInChildren<Text>().text = "SOUND ON";
			this.buSound.image.color = Color.white;
		}
	}

	private void ButtonReplay()
	{
		if (TheUIManager.isLoadingScene(TheEnumManager.SCENE.Gameplay))
		{
			TheUIManager.Instance.LoadScene(TheEnumManager.SCENE.Gameplay, false);
		}
	}

	private void OnEnable()
	{
		this.SetStatusAudioUI();
		if (!TheUIManager.isLoadingScene(TheEnumManager.SCENE.Gameplay))
		{
			this.buReplay.image.color = Color.gray;
			this.buReplay.transform.GetChild(0).GetComponent<Text>().color = new Vector4(1f, 1f, 1f, 0.8f);
		}
		else
		{
			this.buReplay.image.color = Color.white;
			this.buReplay.transform.GetChild(0).GetComponent<Text>().color = Color.white;
		}
	}

	private void OnDisable()
	{
		TheAdsManager.Instance.HideBanner();
	}
}

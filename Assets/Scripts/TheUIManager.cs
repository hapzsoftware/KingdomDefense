using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheUIManager : MonoBehaviour
{
	private sealed class _IeLoadScene_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal TheEnumManager.SCENE eScene;

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

		public _IeLoadScene_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitForSecondsRealtime(0.5f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				SceneManager.LoadScene(this.eScene.ToString());
				this._current = new WaitForSecondsRealtime(0.01f);
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			case 2u:
				TheUIManager.HideAllPopup();
				this._current = new WaitForSecondsRealtime(0.05f);
				if (!this._disposing)
				{
					this._PC = 3;
				}
				return true;
			case 3u:
				Loading.Instance.PlayLoading(false);
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

	public static TheUIManager Instance;

	private void Awake()
	{
		if (TheUIManager.Instance == null)
		{
			TheUIManager.Instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
        Application.targetFrameRate = 60;
	}

	public void LoadScene(TheEnumManager.SCENE eScene, bool _isback = false)
	{
		if (_isback)
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_back);
		}
		else
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);
		}
		Loading.Instance.PlayLoading(true);
		Time.timeScale = 1f;
		base.StartCoroutine(this.IeLoadScene(eScene));
	}

	private IEnumerator IeLoadScene(TheEnumManager.SCENE eScene)
	{
		TheUIManager._IeLoadScene_c__Iterator0 _IeLoadScene_c__Iterator = new TheUIManager._IeLoadScene_c__Iterator0();
		_IeLoadScene_c__Iterator.eScene = eScene;
		return _IeLoadScene_c__Iterator;
	}

	public static bool isLoadingScene(TheEnumManager.SCENE eScene)
	{
		return SceneManager.GetActiveScene().name == eScene.ToString();
	}

	public static void LoadLink(string _link)
	{
		if (_link == ThePlatformManager.MAIN_PLATFORM.LinkFacebook)
		{
			TheFirebaseManager.Instance.PostEvent_ClickButton("facebook");
		}
		else if (_link == ThePlatformManager.MAIN_PLATFORM.LinkLikeMe)
		{
			TheFirebaseManager.Instance.PostEvent_ClickButton("like_us");
		}
		else if (_link == ThePlatformManager.MAIN_PLATFORM.LinkMoreGame)
		{
			TheFirebaseManager.Instance.PostEvent_ClickButton("more_game");
		}
		Application.OpenURL(_link);
	}

	public static void OpenLeaderboardUI()
	{
		TheFirebaseManager.Instance.PostEvent_ClickButton("leaderboard");
		TheGooglePlayServicesManager.Instance.ShowLeadboardUI();
	}

	public static void OpenAchievementUI()
	{
		TheFirebaseManager.Instance.PostEvent_ClickButton("Archivement");
		TheGooglePlayServicesManager.Instance.ShowArchivementUI();
	}

	public static void ReportEmail()
	{
		TheFirebaseManager.Instance.PostEvent_ClickButton("report_email");
		string strEmailBugReport = ThePlatformManager.MAIN_PLATFORM.strEmailBugReport;
		string text = "[TowerKing2018] Bug Report";
		string text2 = "Hi! \n Continue your email...";
		Application.OpenURL(string.Concat(new string[]
		{
			"mailto:",
			strEmailBugReport,
			"?subject=",
			text,
			"&body=",
			text2
		}));
	}

	public static void Contact()
	{
		TheFirebaseManager.Instance.PostEvent_ClickButton("contact_email");
		string strEmailBugReport = ThePlatformManager.MAIN_PLATFORM.strEmailBugReport;
		string text = "[TowerKing2018] Contact:";
		string text2 = "Hi! \n Continue your email...";
		Application.OpenURL(string.Concat(new string[]
		{
			"mailto:",
			strEmailBugReport,
			"?subject=",
			text,
			"&body=",
			text2
		}));
	}

	public static void ShowPopup(ThePopupManager.POP_UP ePopup)
	{
		ThePopupManager.Instance.Show(ePopup);
		TheEventManager.PostEvent(TheEventManager.EventID.OPEN_UI_POPUP);
	}

	public static void ShowPopup(Shop.PANEL _panel)
	{
		Shop.CHOOSE_PANEL = _panel;
		TheUIManager.ShowPopup(ThePopupManager.POP_UP.Shop);
	}

	private void ShowVictoryPopup()
	{
		TheUIManager.ShowPopup(ThePopupManager.POP_UP.Victory);
	}

	private void ShowGameoverDefeat()
	{
		TheUIManager.ShowPopup(ThePopupManager.POP_UP.Gameover);
	}

	public void VictoryPopup()
	{
		base.Invoke("ShowVictoryPopup", 2f);
	}

	public void DefeatPopup()
	{
		base.Invoke("ShowGameoverDefeat", 2f);
	}

	public static void HidePopup(ThePopupManager.POP_UP ePopup)
	{
		ThePopupManager.Instance.Hide(ePopup);
		TheEventManager.PostEvent(TheEventManager.EventID.CLOSE_UI_POPUP);
	}

	public static void HideAllPopup()
	{
		ThePopupManager.Instance.HideAll();
	}
}

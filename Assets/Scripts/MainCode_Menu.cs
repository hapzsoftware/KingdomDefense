using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainCode_Menu : MonoBehaviour
{
	private sealed class _CheckIn_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal object _current;

		internal bool _disposing;

		internal int _PC;

		private static Func<bool> __f__am_cache0;

		private static Func<bool> __f__am_cache1;

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

		public _CheckIn_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitUntil(() => ThePopupManager.Instance != null);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._current = new WaitUntil(() => TheCheckInGiftManager.Instance != null);
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			case 2u:
				this._current = new WaitForSecondsRealtime(1f);
				if (!this._disposing)
				{
					this._PC = 3;
				}
				return true;
			case 3u:
				if (ThePopupManager.Instance.GetPopUp(ThePopupManager.POP_UP.CheckIn).GetComponent<CheckIn>().CheckToShowCheckInPopup() && TheDataManager.THE_PLAYER_DATA.iNumberOfGiftsReceived < TheCheckInGiftManager.Instance.LIST_GIFT.Count)
				{
					TheUIManager.ShowPopup(ThePopupManager.POP_UP.CheckIn);
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

		private static bool __m__0()
		{
			return ThePopupManager.Instance != null;
		}

		private static bool __m__1()
		{
			return TheCheckInGiftManager.Instance != null;
		}
	}

	private sealed class _ShowTextPlayerName_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal MainCode_Menu _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		private static Func<bool> __f__am_cache0;

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

		public _ShowTextPlayerName_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				if (ThePlatformManager.Instance.CHOOSING_MODE == ThePlatformManager.MODE.Testting)
				{
					//this._this.txtPlayerName.text = "TESTING MODE: ";
				}
				else
				{
					//this._this.txtPlayerName.text = string.Empty;
				}
				this._current = new WaitUntil(() => TheGooglePlayServicesManager.Instance.bSignedIn);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
			{
				//Text expr_A6 = this._this.txtPlayerName;
				//expr_A6.text = expr_A6.text + "Hi, " + TheGooglePlayServicesManager.Instance.GetPlayerName();
				this._PC = -1;
				break;
			}
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

		private static bool __m__0()
		{
			return TheGooglePlayServicesManager.Instance.bSignedIn;
		}
	}

	public static bool START_GAME;

	

	public Button buQuit;

	public Button buLike;

	public Button buFacebook;

	public Button buSetting;

	public Button buMoreGame;

	public Button buStart;

	

	public string[] GRAPHISC;

	private static UnityAction __f__am_cache0;

	private static UnityAction __f__am_cache1;

	private static UnityAction __f__am_cache2;

	private static UnityAction __f__am_cache3;

	private static UnityAction __f__am_cache4;

	private static UnityAction __f__am_cache5;

	private static UnityAction __f__am_cache6;

	private static UnityAction __f__am_cache7;

	private void Awake()
	{
		Application.targetFrameRate = 60;
		Screen.sleepTimeout = -1;
	}

	private void Start()
	{
		//ThePopupManager.Instance.SetCameraForPopupCanvas(Camera.main);
		this.buQuit.onClick.AddListener(delegate
		{
			TheUIManager.ShowPopup(ThePopupManager.POP_UP.Quit);
		});
		this.buLike.onClick.AddListener(delegate
		{
			TheUIManager.LoadLink(ThePlatformManager.MAIN_PLATFORM.LinkLikeMe);
		});
		this.buFacebook.onClick.AddListener(delegate
		{
			TheUIManager.LoadLink(ThePlatformManager.MAIN_PLATFORM.LinkFacebook);
		});
		this.buSetting.onClick.AddListener(delegate
		{
			TheUIManager.ShowPopup(ThePopupManager.POP_UP.Setting);
		});
		this.buMoreGame.onClick.AddListener(delegate
		{
			TheUIManager.LoadLink(ThePlatformManager.MAIN_PLATFORM.LinkMoreGame);
		});
		this.buStart.onClick.AddListener(delegate
		{
			TheUIManager.Instance.LoadScene(TheEnumManager.SCENE.LevelSelection, false);
		});
		
		Loading.Instance.PlayLoading(false);
		base.StartCoroutine(this.CheckIn());
		//base.StartCoroutine(this.ShowTextPlayerName());
		
	}

	private IEnumerator CheckIn()
	{
		return new MainCode_Menu._CheckIn_c__Iterator0();
	}

	private IEnumerator ShowTextPlayerName()
	{
		MainCode_Menu._ShowTextPlayerName_c__Iterator1 _ShowTextPlayerName_c__Iterator = new MainCode_Menu._ShowTextPlayerName_c__Iterator1();
		_ShowTextPlayerName_c__Iterator._this = this;
		return _ShowTextPlayerName_c__Iterator;
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyUp(KeyCode.Escape))
		{
			TheUIManager.ShowPopup(ThePopupManager.POP_UP.Quit);
		}
	}

	private void OnEnable()
	{
	}

	[ContextMenu("Reset all Player data")]
	public void DoSomething()
	{
		PlayerPrefs.DeleteAll();
		UnityEngine.Debug.Log("DONE");
	}
}

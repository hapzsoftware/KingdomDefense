using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ThePopupManager : MonoBehaviour
{
	public enum POP_UP
	{
		Setting,
		Note,
		Gameover,
		Victory,
		Shop,
		Quit,
		AboutUs,
		Rate,
		CheckIn,
		Ready,
		RewardedVideo,
		Tutorial
	}

	[Serializable]
	public struct POP_UP_ELEMENT
	{
		public ThePopupManager.POP_UP ePopup;

		public GameObject objScreen;
	}

	private sealed class _IeShow_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal float _timeDelay;

		internal ThePopupManager.POP_UP epopup;

		internal ThePopupManager _this;

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

		public _IeShow_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitForSecondsRealtime(this._timeDelay);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.Show(this.epopup);
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

	public static ThePopupManager Instance;

	public Canvas m_CanvasOfPopup;

	public Camera m_BlackCamera;

	public List<ThePopupManager.POP_UP_ELEMENT> LIST_POPUP;

	private bool bHadBlackCamera;

	public void Show(ThePopupManager.POP_UP epopup)
	{
		if (epopup == ThePopupManager.POP_UP.Gameover)
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_defeat);
		}
		if (epopup != ThePopupManager.POP_UP.Victory)
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);
		}
		this.LIST_POPUP[(int)epopup].objScreen.SetActive(true);
		this.LIST_POPUP[(int)epopup].objScreen.transform.SetAsLastSibling();
	}

	public void Show(ThePopupManager.POP_UP epopup, float _timeDelay)
	{
		TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);
		base.StartCoroutine(this.IeShow(epopup, _timeDelay));
	}

	private IEnumerator IeShow(ThePopupManager.POP_UP epopup, float _timeDelay)
	{
		ThePopupManager._IeShow_c__Iterator0 _IeShow_c__Iterator = new ThePopupManager._IeShow_c__Iterator0();
		_IeShow_c__Iterator._timeDelay = _timeDelay;
		_IeShow_c__Iterator.epopup = epopup;
		_IeShow_c__Iterator._this = this;
		return _IeShow_c__Iterator;
	}

	public GameObject GetPopUp(ThePopupManager.POP_UP epopup)
	{
		return this.LIST_POPUP[(int)epopup].objScreen;
	}

	public void Hide(ThePopupManager.POP_UP epopup)
	{
		TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_back);
		this.LIST_POPUP[(int)epopup].objScreen.SetActive(false);
	}

	public void HideAll()
	{
		int count = this.LIST_POPUP.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.LIST_POPUP[i].objScreen.activeInHierarchy)
			{
				this.LIST_POPUP[i].objScreen.SetActive(false);
			}
		}
	}

	public bool IsShowing()
	{
		int count = this.LIST_POPUP.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.LIST_POPUP[i].objScreen.activeInHierarchy)
			{
				return true;
			}
		}
		return false;
	}

	private void Awake()
	{
		if (ThePopupManager.Instance == null)
		{
			ThePopupManager.Instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void SetCameraForPopupCanvas(Camera _cam)
	{
		this.m_CanvasOfPopup.renderMode = RenderMode.ScreenSpaceCamera;
		this.m_CanvasOfPopup.worldCamera = _cam;
		this.m_CanvasOfPopup.sortingOrder = 200;
		float num = (float)Screen.width;
		float num2 = (float)Screen.height;
		float num3 = num / num2;
		if (num3 > 1.77777779f)
		{
			float num4 = num2 * 16f / 9f / num;
			_cam.rect = new Rect((1f - num4) / 2f, 0f, num4, 1f);
			this.CreateBackgroundCamera();
		}
		else if (num3 < 1.77777779f)
		{
			float num4 = num * 9f / 16f / num2;
			_cam.rect = new Rect(0f, (1f - num4) / 2f, 1f, num4);
			this.CreateBackgroundCamera();
		}
		else
		{
			_cam.rect = new Rect(0f, 0f, 1f, 1f);
		}
	}

	private void CreateBackgroundCamera()
	{
		if (!this.bHadBlackCamera)
		{
			this.bHadBlackCamera = true;
			UnityEngine.Object.Instantiate<Camera>(this.m_BlackCamera);
		}
	}
}

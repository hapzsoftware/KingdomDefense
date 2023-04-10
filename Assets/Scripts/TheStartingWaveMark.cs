using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TheStartingWaveMark : MonoBehaviour
{
	private sealed class _SetButtonSpriteForCallWave_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal TheStartingWaveMark _this;

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

		public _SetButtonSpriteForCallWave_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitUntil(() => TheObjPoolingManager.Instance != null);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = TheObjPoolingManager.Instance.sprCallWave_Skull;
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
			return TheObjPoolingManager.Instance != null;
		}
	}

	public TextMesh m_textMesh;

	private float fCountTime;

	private float fTime = 11f;

	public bool bAutoCount;

	public int iIndex;

	private void Start()
	{
		this.m_textMesh.text = string.Empty;
		this.m_textMesh.fontSize = 130;
	}

	public void Init()
	{
		this.fCountTime = this.fTime;
		base.gameObject.SetActive(false);
	}

	private void Update()
	{
		if (this.bAutoCount)
		{
			this.fCountTime -= Time.deltaTime;
			if (this.fCountTime >= 1f)
			{
				this.m_textMesh.text = ((int)this.fCountTime).ToString();
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}
	}

	public void StartCount()
	{
		if (TheLevel.Instance.iCurrentWave == 1)
		{
			this.bAutoCount = false;
		}
		else
		{
			this.bAutoCount = true;
		}
	}

	private void OnMouseDown()
	{
		if (TheLevel.Instance.iCurrentWave == 0)
		{
			this.fCountTime = 1f;
			this.bAutoCount = true;
			base.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = TheObjPoolingManager.Instance.sprCallWave_Empty;
			this.m_textMesh.transform.localScale = Vector3.one * 1.5f;
		}
		else
		{
			TheLevel.Instance.HideAllRoadMark();
		}
		TheLevel.Instance.iOriginalCoin += 30;
		TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
		UnityEngine.Object.Instantiate<GameObject>(TheObjPoolingManager.Instance.objCoinGift, base.transform.position, Quaternion.identity);
		TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);
	}

	private void OnEnable()
	{
		this.fCountTime = this.fTime;
		if (TheLevel.Instance.iCurrentWave > 1)
		{
			base.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = TheObjPoolingManager.Instance.sprCallWave_Empty;
			this.m_textMesh.transform.localScale = Vector3.one * 1.5f;
		}
		else
		{
			base.StartCoroutine(this.SetButtonSpriteForCallWave());
		}
	}

	private IEnumerator SetButtonSpriteForCallWave()
	{
		TheStartingWaveMark._SetButtonSpriteForCallWave_c__Iterator0 _SetButtonSpriteForCallWave_c__Iterator = new TheStartingWaveMark._SetButtonSpriteForCallWave_c__Iterator0();
		_SetButtonSpriteForCallWave_c__Iterator._this = this;
		return _SetButtonSpriteForCallWave_c__Iterator;
	}

	private void OnDisable()
	{
		if (TheLevel.Instance)
		{
			TheLevel.Instance.StartNewWave();
		}
	}
}

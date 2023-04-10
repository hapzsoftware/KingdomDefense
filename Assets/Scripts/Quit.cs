using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{
	private sealed class _IeQuit_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
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

		public _IeQuit_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				TheAdsManager.Instance.ShowFullAds();
				this._current = new WaitForSecondsRealtime(0.5f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				Application.Quit();
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

	public Button buBack;

	public Button buOk;

	private static UnityAction __f__am_cache0;

	private void Start()
	{
		this.buBack.onClick.AddListener(delegate
		{
			TheUIManager.HidePopup(ThePopupManager.POP_UP.Quit);
		});
		this.buOk.onClick.AddListener(delegate
		{
			base.StartCoroutine(this.IeQuit());
		});
	}

	private IEnumerator IeQuit()
	{
		return new Quit._IeQuit_c__Iterator0();
	}
}

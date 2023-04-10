using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TimeLife : MonoBehaviour
{
	public enum ACTIVE
	{
		DESTROY,
		ACTIVE
	}

	private sealed class _Wait_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal float _time;

		internal TimeLife _this;

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

		public _Wait_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitForSeconds(this._time);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
			{
				TimeLife.ACTIVE eActive = this._this.eActive;
				if (eActive != TimeLife.ACTIVE.DESTROY)
				{
					if (eActive == TimeLife.ACTIVE.ACTIVE)
					{
						this._this.gameObject.SetActive(false);
					}
				}
				else
				{
					UnityEngine.Object.Destroy(this._this.gameObject);
				}
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
	}

	public TimeLife.ACTIVE eActive;

	public float fTimelife;

	public bool bUnscaleTime;

	private void OnEnable()
	{
		if (this.bUnscaleTime)
		{
			base.StartCoroutine(this.Wait(this.fTimelife * Time.timeScale));
		}
		else
		{
			base.StartCoroutine(this.Wait(this.fTimelife));
		}
	}

	private IEnumerator Wait(float _time)
	{
		TimeLife._Wait_c__Iterator0 _Wait_c__Iterator = new TimeLife._Wait_c__Iterator0();
		_Wait_c__Iterator._time = _time;
		_Wait_c__Iterator._this = this;
		return _Wait_c__Iterator;
	}
}

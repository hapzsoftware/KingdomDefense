using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class CFX_AutoDestructShuriken : MonoBehaviour
{
	private sealed class _CheckIfAlive_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal CFX_AutoDestructShuriken _this;

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

		public _CheckIfAlive_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				break;
			case 1u:
				if (!this._this.GetComponent<ParticleSystem>().IsAlive(true))
				{
					if (this._this.OnlyDeactivate)
					{
						this._this.gameObject.SetActive(false);
					}
					else
					{
						UnityEngine.Object.Destroy(this._this.gameObject);
					}
					this._PC = -1;
					return false;
				}
				break;
			default:
				return false;
			}
			this._current = new WaitForSeconds(0.5f);
			if (!this._disposing)
			{
				this._PC = 1;
			}
			return true;
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

	public bool OnlyDeactivate;

	private void OnEnable()
	{
		base.StartCoroutine("CheckIfAlive");
	}

	private IEnumerator CheckIfAlive()
	{
		CFX_AutoDestructShuriken._CheckIfAlive_c__Iterator0 _CheckIfAlive_c__Iterator = new CFX_AutoDestructShuriken._CheckIfAlive_c__Iterator0();
		_CheckIfAlive_c__Iterator._this = this;
		return _CheckIfAlive_c__Iterator;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CFX_ShurikenThreadFix : MonoBehaviour
{
	private sealed class _WaitFrame_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal ParticleSystem[] _locvar0;

		internal int _locvar1;

		internal CFX_ShurikenThreadFix _this;

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

		public _WaitFrame_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = null;
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._locvar0 = this._this.systems;
				this._locvar1 = 0;
				while (this._locvar1 < this._locvar0.Length)
				{
					ParticleSystem particleSystem = this._locvar0[this._locvar1];
					particleSystem.enableEmission = true;
					particleSystem.Play(true);
					this._locvar1++;
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

	private ParticleSystem[] systems;

	private void OnEnable()
	{
		this.systems = base.GetComponentsInChildren<ParticleSystem>();
		ParticleSystem[] array = this.systems;
		for (int i = 0; i < array.Length; i++)
		{
			ParticleSystem particleSystem = array[i];
			particleSystem.enableEmission = false;
		}
		base.StartCoroutine("WaitFrame");
	}

	private IEnumerator WaitFrame()
	{
		CFX_ShurikenThreadFix._WaitFrame_c__Iterator0 _WaitFrame_c__Iterator = new CFX_ShurikenThreadFix._WaitFrame_c__Iterator0();
		_WaitFrame_c__Iterator._this = this;
		return _WaitFrame_c__Iterator;
	}
}

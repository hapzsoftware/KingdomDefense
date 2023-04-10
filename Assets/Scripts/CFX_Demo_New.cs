using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CFX_Demo_New : MonoBehaviour
{
	private sealed class _CheckForDeletedParticles_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal CFX_Demo_New _this;

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

		public _CheckForDeletedParticles_c__Iterator0()
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
				for (int i = this._this.onScreenParticles.Count - 1; i >= 0; i--)
				{
					if (this._this.onScreenParticles[i] == null)
					{
						this._this.onScreenParticles.RemoveAt(i);
					}
				}
				break;
			default:
				return false;
			}
			this._current = new WaitForSeconds(5f);
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

	public Text EffectLabel;

	public Text EffectIndexLabel;

	public Renderer groundRenderer;

	public Collider groundCollider;

	private GameObject[] ParticleExamples;

	private int exampleIndex;

	private bool slowMo;

	private Vector3 defaultCamPosition;

	private Quaternion defaultCamRotation;

	private List<GameObject> onScreenParticles = new List<GameObject>();

	private void Awake()
	{
		List<GameObject> list = new List<GameObject>();
		int childCount = base.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = base.transform.GetChild(i).gameObject;
			list.Add(gameObject);
		}
		this.ParticleExamples = list.ToArray();
		this.defaultCamPosition = Camera.main.transform.position;
		this.defaultCamRotation = Camera.main.transform.rotation;
		base.StartCoroutine("CheckForDeletedParticles");
		this.UpdateUI();
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow))
		{
			this.prevParticle();
		}
		else if (UnityEngine.Input.GetKeyDown(KeyCode.RightArrow))
		{
			this.nextParticle();
		}
		else if (UnityEngine.Input.GetKeyDown(KeyCode.Delete))
		{
			this.destroyParticles();
		}
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit raycastHit = default(RaycastHit);
			if (this.groundCollider.Raycast(Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out raycastHit, 9999f))
			{
				GameObject gameObject = this.spawnParticle();
				gameObject.transform.position = raycastHit.point + gameObject.transform.position;
			}
		}
		float axis = UnityEngine.Input.GetAxis("Mouse ScrollWheel");
		if (axis != 0f)
		{
			Camera.main.transform.Translate(Vector3.forward * ((axis >= 0f) ? 1f : -1f), Space.Self);
		}
		if (Input.GetMouseButtonDown(2))
		{
			Camera.main.transform.position = this.defaultCamPosition;
			Camera.main.transform.rotation = this.defaultCamRotation;
		}
	}

	private void OnToggleGround()
	{
		this.groundRenderer.enabled = !this.groundRenderer.enabled;
	}

	private void OnToggleCamera()
	{
		CFX_Demo_RotateCamera.rotating = !CFX_Demo_RotateCamera.rotating;
	}

	private void OnToggleSlowMo()
	{
		this.slowMo = !this.slowMo;
		if (this.slowMo)
		{
			Time.timeScale = 0.33f;
		}
		else
		{
			Time.timeScale = 1f;
		}
	}

	private void OnPreviousEffect()
	{
		this.prevParticle();
	}

	private void OnNextEffect()
	{
		this.nextParticle();
	}

	private void UpdateUI()
	{
		this.EffectLabel.text = this.ParticleExamples[this.exampleIndex].name;
		this.EffectIndexLabel.text = string.Format("{0}/{1}", (this.exampleIndex + 1).ToString("00"), this.ParticleExamples.Length.ToString("00"));
	}

	private GameObject spawnParticle()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ParticleExamples[this.exampleIndex]);
		gameObject.transform.position = new Vector3(0f, gameObject.transform.position.y, 0f);
		gameObject.SetActive(true);
		ParticleSystem component = gameObject.GetComponent<ParticleSystem>();
		if (component != null && component.loop)
		{
			component.gameObject.AddComponent<CFX_AutoStopLoopedEffect>();
			component.gameObject.AddComponent<CFX_AutoDestructShuriken>();
		}
		this.onScreenParticles.Add(gameObject);
		return gameObject;
	}

	private IEnumerator CheckForDeletedParticles()
	{
		CFX_Demo_New._CheckForDeletedParticles_c__Iterator0 _CheckForDeletedParticles_c__Iterator = new CFX_Demo_New._CheckForDeletedParticles_c__Iterator0();
		_CheckForDeletedParticles_c__Iterator._this = this;
		return _CheckForDeletedParticles_c__Iterator;
	}

	private void prevParticle()
	{
		this.exampleIndex--;
		if (this.exampleIndex < 0)
		{
			this.exampleIndex = this.ParticleExamples.Length - 1;
		}
		this.UpdateUI();
	}

	private void nextParticle()
	{
		this.exampleIndex++;
		if (this.exampleIndex >= this.ParticleExamples.Length)
		{
			this.exampleIndex = 0;
		}
		this.UpdateUI();
	}

	private void destroyParticles()
	{
		for (int i = this.onScreenParticles.Count - 1; i >= 0; i--)
		{
			if (this.onScreenParticles[i] != null)
			{
				UnityEngine.Object.Destroy(this.onScreenParticles[i]);
			}
			this.onScreenParticles.RemoveAt(i);
		}
	}
}

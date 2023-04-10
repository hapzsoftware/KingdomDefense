using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;

public class CFX3_Demo : MonoBehaviour
{
	private sealed class _CheckForDeletedParticles_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal CFX3_Demo _this;

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

	private sealed class _RandomSpawnsCoroutine_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal GameObject _particles___0;

		internal CFX3_Demo _this;

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

		public _RandomSpawnsCoroutine_c__Iterator1()
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
				break;
			default:
				return false;
			}
			this._particles___0 = this._this.spawnParticle();
			if (this._this.orderedSpawns)
			{
				this._particles___0.transform.position = this._this.transform.position + new Vector3(this._this.order, this._particles___0.transform.position.y, 0f);
				this._this.order -= this._this.step;
				if (this._this.order < -this._this.range)
				{
					this._this.order = this._this.range;
				}
			}
			else
			{
				this._particles___0.transform.position = this._this.transform.position + new Vector3(UnityEngine.Random.Range(-this._this.range, this._this.range), 0f, UnityEngine.Random.Range(-this._this.range, this._this.range)) + new Vector3(0f, this._particles___0.transform.position.y, 0f);
			}
			this._current = new WaitForSeconds(float.Parse(this._this.randomSpawnsDelay));
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

	public bool orderedSpawns = true;

	public float step = 1f;

	public float range = 5f;

	private float order = -5f;

	public Renderer groundRenderer;

	public Collider groundCollider;

	private GameObject[] ParticleExamples;

	private int exampleIndex;

	private string randomSpawnsDelay = "0.5";

	private bool randomSpawns;

	private bool slowMo;

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
		base.StartCoroutine("CheckForDeletedParticles");
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
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
		{
			RaycastHit raycastHit = default(RaycastHit);
			if (this.groundCollider.Raycast(Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out raycastHit, 9999f))
			{
				GameObject gameObject = this.spawnParticle();
				gameObject.transform.position = raycastHit.point + gameObject.transform.position;
			}
		}
	}

	private void OnGUI()
	{
		GUILayout.BeginArea(new Rect(5f, 20f, (float)(Screen.width - 10), 60f));
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Label("Effect", new GUILayoutOption[]
		{
			GUILayout.Width(50f)
		});
		if (GUILayout.Button("<", new GUILayoutOption[]
		{
			GUILayout.Width(25f)
		}))
		{
			this.prevParticle();
		}
		GUILayout.Label(this.ParticleExamples[this.exampleIndex].name, new GUILayoutOption[]
		{
			GUILayout.Width(265f)
		});
		if (GUILayout.Button(">", new GUILayoutOption[]
		{
			GUILayout.Width(25f)
		}))
		{
			this.nextParticle();
		}
		GUILayout.Space(80f);
		if (GUILayout.Button((!CFX_Demo_RotateCamera.rotating) ? "Rotate Camera" : "Pause Camera", new GUILayoutOption[0]))
		{
			CFX_Demo_RotateCamera.rotating = !CFX_Demo_RotateCamera.rotating;
		}
		if (GUILayout.Button((!this.randomSpawns) ? "Start UnityEngine.Random Spawns" : "Stop UnityEngine.Random Spawns", new GUILayoutOption[]
		{
			GUILayout.Width(140f)
		}))
		{
			this.randomSpawns = !this.randomSpawns;
			if (this.randomSpawns)
			{
				base.StartCoroutine("RandomSpawnsCoroutine");
			}
			else
			{
				base.StopCoroutine("RandomSpawnsCoroutine");
			}
		}
		this.randomSpawnsDelay = GUILayout.TextField(this.randomSpawnsDelay, 10, new GUILayoutOption[]
		{
			GUILayout.Width(42f)
		});
		this.randomSpawnsDelay = Regex.Replace(this.randomSpawnsDelay, "[^0-9.]", string.Empty);
		if (GUILayout.Button((!this.groundRenderer.enabled) ? "Show Ground" : "Hide Ground", new GUILayoutOption[]
		{
			GUILayout.Width(90f)
		}))
		{
			this.groundRenderer.enabled = !this.groundRenderer.enabled;
		}
		if (GUILayout.Button((!this.slowMo) ? "Slow Motion" : "Normal Speed", new GUILayoutOption[]
		{
			GUILayout.Width(100f)
		}))
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
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
		GUILayout.BeginArea(new Rect(5f, 50f, (float)(Screen.width - 10), 60f));
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Label("Click on the ground to spawn selected particles", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		GUILayout.Label("Use the LEFT and RIGHT keys to switch effects; Press DEL to delete all effects on screen", new GUILayoutOption[0]);
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

	private GameObject spawnParticle()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ParticleExamples[this.exampleIndex]);
		gameObject.transform.position = new Vector3(0f, gameObject.transform.position.y, 0f);
		gameObject.SetActive(true);
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			gameObject.transform.GetChild(i).gameObject.SetActive(true);
		}
		ParticleSystem component = gameObject.GetComponent<ParticleSystem>();
		if (component != null && component.loop)
		{
			component.gameObject.AddComponent<CFX3_AutoStopLoopedEffect>();
			component.gameObject.AddComponent<CFX_AutoDestructShuriken>();
		}
		this.onScreenParticles.Add(gameObject);
		return gameObject;
	}

	private IEnumerator CheckForDeletedParticles()
	{
		CFX3_Demo._CheckForDeletedParticles_c__Iterator0 _CheckForDeletedParticles_c__Iterator = new CFX3_Demo._CheckForDeletedParticles_c__Iterator0();
		_CheckForDeletedParticles_c__Iterator._this = this;
		return _CheckForDeletedParticles_c__Iterator;
	}

	private IEnumerator RandomSpawnsCoroutine()
	{
		CFX3_Demo._RandomSpawnsCoroutine_c__Iterator1 _RandomSpawnsCoroutine_c__Iterator = new CFX3_Demo._RandomSpawnsCoroutine_c__Iterator1();
		_RandomSpawnsCoroutine_c__Iterator._this = this;
		return _RandomSpawnsCoroutine_c__Iterator;
	}

	private void prevParticle()
	{
		this.exampleIndex--;
		if (this.exampleIndex < 0)
		{
			this.exampleIndex = this.ParticleExamples.Length - 1;
		}
	}

	private void nextParticle()
	{
		this.exampleIndex++;
		if (this.exampleIndex >= this.ParticleExamples.Length)
		{
			this.exampleIndex = 0;
		}
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;

public class CFX_Demo : MonoBehaviour
{
	private sealed class _RandomSpawnsCoroutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal GameObject _particles___0;

		internal CFX_Demo _this;

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

		public _RandomSpawnsCoroutine_c__Iterator0()
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

	public Material groundMat;

	public Material waterMat;

	public GameObject[] ParticleExamples;

	private Dictionary<string, float> ParticlesYOffsetD = new Dictionary<string, float>
	{
		{
			"CFX_ElectricGround",
			0.15f
		},
		{
			"CFX_ElectricityBall",
			1f
		},
		{
			"CFX_ElectricityBolt",
			1f
		},
		{
			"CFX_Explosion",
			2f
		},
		{
			"CFX_SmallExplosion",
			1.5f
		},
		{
			"CFX_SmokeExplosion",
			2.5f
		},
		{
			"CFX_Flame",
			1f
		},
		{
			"CFX_DoubleFlame",
			1f
		},
		{
			"CFX_Hit",
			1f
		},
		{
			"CFX_CircularLightWall",
			0.05f
		},
		{
			"CFX_LightWall",
			0.05f
		},
		{
			"CFX_Flash",
			2f
		},
		{
			"CFX_Poof",
			1.5f
		},
		{
			"CFX_Virus",
			1f
		},
		{
			"CFX_SmokePuffs",
			2f
		},
		{
			"CFX_Slash",
			1f
		},
		{
			"CFX_Splash",
			0.05f
		},
		{
			"CFX_Fountain",
			0.05f
		},
		{
			"CFX_Ripple",
			0.05f
		},
		{
			"CFX_Magic",
			2f
		},
		{
			"CFX_SoftStar",
			1f
		},
		{
			"CFX_SpikyAura_Sphere",
			1f
		},
		{
			"CFX_Firework",
			2.4f
		},
		{
			"CFX_GroundA",
			0.05f
		}
	};

	private int exampleIndex;

	private string randomSpawnsDelay = "0.5";

	private bool randomSpawns;

	private bool slowMo;

	private void OnMouseDown()
	{
		RaycastHit raycastHit = default(RaycastHit);
		if (base.GetComponent<Collider>().Raycast(Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out raycastHit, 9999f))
		{
			GameObject gameObject = this.spawnParticle();
			gameObject.transform.position = raycastHit.point + gameObject.transform.position;
		}
	}

	private GameObject spawnParticle()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ParticleExamples[this.exampleIndex]);
		gameObject.SetActive(true);
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			gameObject.transform.GetChild(i).gameObject.SetActive(true);
		}
		float y = 0f;
		foreach (KeyValuePair<string, float> current in this.ParticlesYOffsetD)
		{
			if (gameObject.name.StartsWith(current.Key))
			{
				y = current.Value;
				break;
			}
		}
		gameObject.transform.position = new Vector3(0f, y, 0f);
		return gameObject;
	}

	private void OnGUI()
	{
		GUILayout.BeginArea(new Rect(5f, 20f, (float)(Screen.width - 10), 30f));
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Label("Effect", new GUILayoutOption[]
		{
			GUILayout.Width(50f)
		});
		if (GUILayout.Button("<", new GUILayoutOption[]
		{
			GUILayout.Width(20f)
		}))
		{
			this.prevParticle();
		}
		GUILayout.Label(this.ParticleExamples[this.exampleIndex].name, new GUILayoutOption[]
		{
			GUILayout.Width(190f)
		});
		if (GUILayout.Button(">", new GUILayoutOption[]
		{
			GUILayout.Width(20f)
		}))
		{
			this.nextParticle();
		}
		GUILayout.Label("Click on the ground to spawn selected particles", new GUILayoutOption[0]);
		if (GUILayout.Button((!CFX_Demo_RotateCamera.rotating) ? "Rotate Camera" : "Pause Camera", new GUILayoutOption[]
		{
			GUILayout.Width(140f)
		}))
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
		if (GUILayout.Button((!base.GetComponent<Renderer>().enabled) ? "Show Ground" : "Hide Ground", new GUILayoutOption[]
		{
			GUILayout.Width(90f)
		}))
		{
			base.GetComponent<Renderer>().enabled = !base.GetComponent<Renderer>().enabled;
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
	}

	private IEnumerator RandomSpawnsCoroutine()
	{
		CFX_Demo._RandomSpawnsCoroutine_c__Iterator0 _RandomSpawnsCoroutine_c__Iterator = new CFX_Demo._RandomSpawnsCoroutine_c__Iterator0();
		_RandomSpawnsCoroutine_c__Iterator._this = this;
		return _RandomSpawnsCoroutine_c__Iterator;
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
	}

	private void prevParticle()
	{
		this.exampleIndex--;
		if (this.exampleIndex < 0)
		{
			this.exampleIndex = this.ParticleExamples.Length - 1;
		}
		if (this.ParticleExamples[this.exampleIndex].name.Contains("Splash") || this.ParticleExamples[this.exampleIndex].name == "CFX_Ripple" || this.ParticleExamples[this.exampleIndex].name == "CFX_Fountain")
		{
			base.GetComponent<Renderer>().material = this.waterMat;
		}
		else
		{
			base.GetComponent<Renderer>().material = this.groundMat;
		}
	}

	private void nextParticle()
	{
		this.exampleIndex++;
		if (this.exampleIndex >= this.ParticleExamples.Length)
		{
			this.exampleIndex = 0;
		}
		if (this.ParticleExamples[this.exampleIndex].name.Contains("Splash") || this.ParticleExamples[this.exampleIndex].name == "CFX_Ripple" || this.ParticleExamples[this.exampleIndex].name == "CFX_Fountain")
		{
			base.GetComponent<Renderer>().material = this.waterMat;
		}
		else
		{
			base.GetComponent<Renderer>().material = this.groundMat;
		}
	}
}

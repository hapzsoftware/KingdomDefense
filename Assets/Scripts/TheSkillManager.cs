using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TheSkillManager : MonoBehaviour
{
	public delegate void UsedSkill(Vector2 _pos);

	private sealed class _FireFromSky_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _i___1;

		internal int _num;

		internal GameObject __fire___2;

		internal Vector2 _pos;

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

		public _FireFromSky_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._i___1 = 0;
				break;
			case 1u:
				this._i___1++;
				break;
			default:
				return false;
			}
			if (this._i___1 < this._num)
			{
				this.__fire___2 = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.FireFromSky).GetItem();
				if (this.__fire___2)
				{
					this.__fire___2.GetComponent<FireMove>().Play(this._pos + new Vector2(UnityEngine.Random.Range(-1.5f, 1.5f), (float)UnityEngine.Random.Range(-1, 1)));
					this.__fire___2.SetActive(true);
				}
				this._current = new WaitForSeconds(0.3f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			this._PC = -1;
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

	public TheEnumManager.SKILL eCURRENT_SKILL;

	[Header("BUTTON SKILL"), Space(20f)]
	public Button buSkill_Guardian;

	public Button buSkill_Freeze;

	public Button buSkill_DestroyWithBoom;

	public Button buSkill_Boom;

	public Button buSkill_AddBlood;

	[Header("FINTER"), Space(20f)]
	public GameObject objFinger_Freeze;

	public GameObject objFinger_Hero;

	public GameObject objFinger_AddBlood;

	public GameObject objFinger_MineOnRoad;

	public GameObject objFinger_FireFromSky;

	[Space(20f)]
	public Image imaMainIcon;

	public Sprite sprMainIcon;

	public static TheSkillManager Instance;

	private Sprite _tempSprite;







	public static event TheSkillManager.UsedSkill OnUsedSkillFreeze;

	public static event TheSkillManager.UsedSkill OnUsedSkillMineOnRoad;

	public static event TheSkillManager.UsedSkill OnUserSkillBoomFromSky;

	private void Awake()
	{
		if (TheSkillManager.Instance == null)
		{
			TheSkillManager.Instance = this;
		}
	}

	private void Start()
	{
		this.buSkill_Guardian.onClick.AddListener(delegate
		{
			this.SetSkill(TheEnumManager.SKILL.skill_guardian);
		});
		this.buSkill_Freeze.onClick.AddListener(delegate
		{
			this.SetSkill(TheEnumManager.SKILL.skill_freeze);
		});
		this.buSkill_DestroyWithBoom.onClick.AddListener(delegate
		{
			this.SetSkill(TheEnumManager.SKILL.skill_fire_of_lord);
		});
		this.buSkill_Boom.onClick.AddListener(delegate
		{
			this.SetSkill(TheEnumManager.SKILL.skill_mine_on_road);
		});
		this.buSkill_AddBlood.onClick.AddListener(delegate
		{
			this.SetSkill(TheEnumManager.SKILL.skill_poison);
		});
		this.ShowTextNumberOfSkill();
	}

	private void ShowTextNumberOfSkill()
	{
		this.buSkill_Guardian.GetComponentInChildren<Text>().text = TheDataManager.THE_PLAYER_DATA.iSkillGuardian.ToString();
		if (TheDataManager.THE_PLAYER_DATA.iSkillGuardian == 0)
		{
			this.buSkill_Guardian.image.color = new Color(0.4f, 0.4f, 0.4f, 1f);
		}
		else
		{
			this.buSkill_Guardian.image.color = Color.white;
		}
		this.buSkill_Freeze.GetComponentInChildren<Text>().text = TheDataManager.THE_PLAYER_DATA.iSkillFreeze.ToString();
		if (TheDataManager.THE_PLAYER_DATA.iSkillFreeze == 0)
		{
			this.buSkill_Freeze.image.color = new Color(0.4f, 0.4f, 0.4f, 1f);
		}
		else
		{
			this.buSkill_Freeze.image.color = Color.white;
		}
		this.buSkill_DestroyWithBoom.GetComponentInChildren<Text>().text = TheDataManager.THE_PLAYER_DATA.iSkillBoomFromSky.ToString();
		if (TheDataManager.THE_PLAYER_DATA.iSkillBoomFromSky == 0)
		{
			this.buSkill_DestroyWithBoom.image.color = new Color(0.4f, 0.4f, 0.4f, 1f);
		}
		else
		{
			this.buSkill_DestroyWithBoom.image.color = Color.white;
		}
		this.buSkill_Boom.GetComponentInChildren<Text>().text = TheDataManager.THE_PLAYER_DATA.iSkillMineOnRoad.ToString();
		if (TheDataManager.THE_PLAYER_DATA.iSkillMineOnRoad == 0)
		{
			this.buSkill_Boom.image.color = new Color(0.4f, 0.4f, 0.4f, 1f);
		}
		else
		{
			this.buSkill_Boom.image.color = Color.white;
		}
		this.buSkill_AddBlood.GetComponentInChildren<Text>().text = TheDataManager.THE_PLAYER_DATA.iSkillPoison.ToString();
		if (TheDataManager.THE_PLAYER_DATA.iSkillPoison == 0)
		{
			this.buSkill_AddBlood.image.color = new Color(0.4f, 0.4f, 0.4f, 1f);
		}
		else
		{
			this.buSkill_AddBlood.image.color = Color.white;
		}
	}

	private void SetSkill(TheEnumManager.SKILL _skill)
	{
		int num = 0;
		switch (_skill)
		{
		case TheEnumManager.SKILL.skill_guardian:
			num = TheDataManager.THE_PLAYER_DATA.iSkillGuardian;
			if (!this.buSkill_Guardian.transform.Find("CircleTime").GetComponent<CircleTime>().IsReady())
			{
				TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_back);
				return;
			}
			this._tempSprite = this.buSkill_Guardian.image.sprite;
			break;
		case TheEnumManager.SKILL.skill_freeze:
			num = TheDataManager.THE_PLAYER_DATA.iSkillFreeze;
			if (!this.buSkill_Freeze.transform.Find("CircleTime").GetComponent<CircleTime>().IsReady())
			{
				TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_back);
				return;
			}
			this._tempSprite = this.buSkill_Freeze.image.sprite;
			break;
		case TheEnumManager.SKILL.skill_fire_of_lord:
			num = TheDataManager.THE_PLAYER_DATA.iSkillBoomFromSky;
			if (!this.buSkill_DestroyWithBoom.transform.Find("CircleTime").GetComponent<CircleTime>().IsReady())
			{
				TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_back);
				return;
			}
			this._tempSprite = this.buSkill_DestroyWithBoom.image.sprite;
			break;
		case TheEnumManager.SKILL.skill_mine_on_road:
			num = TheDataManager.THE_PLAYER_DATA.iSkillMineOnRoad;
			if (!this.buSkill_Boom.transform.Find("CircleTime").GetComponent<CircleTime>().IsReady())
			{
				TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_back);
				return;
			}
			this._tempSprite = this.buSkill_Boom.image.sprite;
			break;
		case TheEnumManager.SKILL.skill_poison:
			num = TheDataManager.THE_PLAYER_DATA.iSkillPoison;
			if (!this.buSkill_AddBlood.transform.Find("CircleTime").GetComponent<CircleTime>().IsReady())
			{
				TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_back);
				return;
			}
			this._tempSprite = this.buSkill_AddBlood.image.sprite;
			break;
		}
		if (num > 0)
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);
			this.eCURRENT_SKILL = _skill;
			MainCode_Gameplay.Instance.SetInput(MainCode_Gameplay.INPUT_PLAYER.ReadyToUseSkill);
			this.imaMainIcon.sprite = this._tempSprite;
			this.imaMainIcon.transform.GetChild(0).GetComponent<Image>().color = Color.white * 1f;
			UIGameplay.Instance.ShowBoardSkill();
		}
	}

	public void GetSkill(Vector2 _pos)
	{
		GameObject gameObject = new GameObject();
		switch (this.eCURRENT_SKILL)
		{
		case TheEnumManager.SKILL.skill_guardian:
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objFinger_Hero);
			gameObject.SetActive(false);
			if (gameObject)
			{
				gameObject.transform.position = _pos;
				gameObject.SetActive(true);
			}
			TheDataManager.THE_PLAYER_DATA.iSkillGuardian--;
			this.buSkill_Guardian.GetComponentInChildren<Text>().text = TheDataManager.THE_PLAYER_DATA.iSkillGuardian.ToString();
			if (TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.Reinforcement_2To3Man).isActived)
			{
				UnityEngine.Object.Instantiate<GameObject>(TheObjPoolingManager.Instance.objSkill_Reinforcements_3Mans, _pos, Quaternion.identity);
			}
			else
			{
				UnityEngine.Object.Instantiate<GameObject>(TheObjPoolingManager.Instance.objSkill_Reinforcements_2Mans, _pos, Quaternion.identity);
			}
			this.buSkill_Guardian.transform.Find("CircleTime").GetComponent<CircleTime>().StartCount();
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.skill_soldier);
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.soldier_no_fair);
			break;
		case TheEnumManager.SKILL.skill_freeze:
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objFinger_Freeze);
			gameObject.SetActive(false);
			if (gameObject)
			{
				gameObject.transform.position = _pos;
				gameObject.SetActive(true);
			}
			TheDataManager.THE_PLAYER_DATA.iSkillFreeze--;
			this.buSkill_Freeze.GetComponentInChildren<Text>().text = TheDataManager.THE_PLAYER_DATA.iSkillFreeze.ToString();
			this.buSkill_Freeze.transform.Find("CircleTime").GetComponent<CircleTime>().StartCount();
			TheSkillManager.CallUserSkill_Freeze(_pos);
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.skill_freeze);
			break;
		case TheEnumManager.SKILL.skill_fire_of_lord:
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objFinger_FireFromSky);
			gameObject.SetActive(false);
			if (gameObject)
			{
				gameObject.transform.position = _pos;
				gameObject.SetActive(true);
			}
			TheDataManager.THE_PLAYER_DATA.iSkillBoomFromSky--;
			this.buSkill_DestroyWithBoom.GetComponentInChildren<Text>().text = TheDataManager.THE_PLAYER_DATA.iSkillBoomFromSky.ToString();
			base.StartCoroutine(this.FireFromSky(7, _pos));
			this.buSkill_DestroyWithBoom.transform.Find("CircleTime").GetComponent<CircleTime>().StartCount();
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.skill_lord_fire);
			break;
		case TheEnumManager.SKILL.skill_mine_on_road:
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objFinger_MineOnRoad);
			gameObject.SetActive(false);
			if (gameObject)
			{
				gameObject.transform.position = _pos;
				gameObject.SetActive(true);
			}
			TheDataManager.THE_PLAYER_DATA.iSkillMineOnRoad--;
			this.buSkill_Boom.GetComponentInChildren<Text>().text = TheDataManager.THE_PLAYER_DATA.iSkillMineOnRoad.ToString();
			UnityEngine.Object.Instantiate<GameObject>(TheObjPoolingManager.Instance.objSkill_MineOfRoad, _pos, Quaternion.identity);
			this.buSkill_Boom.transform.Find("CircleTime").GetComponent<CircleTime>().StartCount();
			break;
		case TheEnumManager.SKILL.skill_poison:
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objFinger_AddBlood);
			gameObject.SetActive(false);
			if (gameObject)
			{
				gameObject.transform.position = _pos;
				gameObject.SetActive(true);
			}
			TheDataManager.THE_PLAYER_DATA.iSkillPoison--;
			this.buSkill_AddBlood.GetComponentInChildren<Text>().text = TheDataManager.THE_PLAYER_DATA.iSkillPoison.ToString();
			UnityEngine.Object.Instantiate<GameObject>(TheObjPoolingManager.Instance.objSkill_PondOfPoison, _pos, Quaternion.identity);
			this.buSkill_AddBlood.transform.Find("CircleTime").GetComponent<CircleTime>().StartCount();
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.skill_poison);
			break;
		}
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"SKILL: ",
			this.eCURRENT_SKILL,
			" - ",
			_pos
		}));
		MainCode_Gameplay.Instance.SetInput(MainCode_Gameplay.INPUT_PLAYER.Normal);
		this.ShowTextNumberOfSkill();
		this.imaMainIcon.sprite = this.sprMainIcon;
		UIGameplay.Instance.ShowBoardSkill();
		this.imaMainIcon.transform.GetChild(0).GetComponent<Image>().color = Color.white * 0f;
	}

	private IEnumerator FireFromSky(int _num, Vector2 _pos)
	{
		TheSkillManager._FireFromSky_c__Iterator0 _FireFromSky_c__Iterator = new TheSkillManager._FireFromSky_c__Iterator0();
		_FireFromSky_c__Iterator._num = _num;
		_FireFromSky_c__Iterator._pos = _pos;
		return _FireFromSky_c__Iterator;
	}

	public static void CallUserSkill_Freeze(Vector2 _pos)
	{
		if (TheSkillManager.OnUsedSkillFreeze != null)
		{
			TheSkillManager.OnUsedSkillFreeze(_pos);
		}
	}

	public static void CallUserSkill_MineOnRoad(Vector2 _pos)
	{
		if (TheSkillManager.OnUsedSkillMineOnRoad != null)
		{
			TheSkillManager.OnUsedSkillMineOnRoad(_pos);
		}
	}

	public static void CallUserSkill_BoomFroomSky(Vector2 _pos)
	{
		if (TheSkillManager.OnUserSkillBoomFromSky != null)
		{
			TheSkillManager.OnUserSkillBoomFromSky(_pos);
		}
	}

	private void OnEnable()
	{
		TheEventManager.RegisterEvent(TheEventManager.EventID.UPDATE_SKILL_BOARD, new TheEventManager.ACTION(this.ShowTextNumberOfSkill));
	}

	private void OnDisable()
	{
		TheEventManager.RemoveEvent(TheEventManager.EventID.UPDATE_SKILL_BOARD, new TheEventManager.ACTION(this.ShowTextNumberOfSkill));
	}
}

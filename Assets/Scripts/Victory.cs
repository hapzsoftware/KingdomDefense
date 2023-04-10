using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Victory : MonoBehaviour
{
	private sealed class _IeShowRatePopup_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal float _time;

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

		public _IeShowRatePopup_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitForSecondsRealtime(this._time);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				if (TheDataManager.THE_PLAYER_DATA.iCurrentLevel % 4 == 0 && TheDataManager.THE_PLAYER_DATA.iCurrentLevel > 1)
				{
					TheUIManager.ShowPopup(ThePopupManager.POP_UP.Rate);
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

	private sealed class _AnimationStar_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int __total___0;

		internal float _time;

		internal int _i___1;

		internal Victory _this;

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

		public _AnimationStar_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this.__total___0 = this._this.LIST_STAR.Count;
				for (int i = 0; i < this.__total___0; i++)
				{
					this._this.LIST_STAR[i].sprite = this._this.sprStar_Empty;
					this._this.LIST_STAR[i].color = Color.white * 0.3f;
				}
				this._this.txtGemGift.text = "...";
				this._current = new WaitForSecondsRealtime(this._time);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._i___1 = 0;
				break;
			case 2u:
			//IL_1B3:
				this._i___1++;
				break;
			case 3u:
				TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);
				this._this.txtGemGift.text = "+" + this._this.iGemVitory.ToString();
				TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_winning);
				TheMusic.Instance.Stop();
				UnityEngine.Object.Instantiate<GameObject>(this._this.EFF_STAR_FALL);
				this._PC = -1;
				return false;
			default:
				return false;
			}
			if (this._i___1 >= this.__total___0)
			{
				this._current = new WaitForSecondsRealtime(0.8f);
				if (!this._disposing)
				{
					this._PC = 3;
				}
				return true;
			}
			if (this._i___1 < this._this.iStar)
			{
				if (this._i___1 == 0)
				{
					TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_star_1);
				}
				else if (this._i___1 == 1)
				{
					TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_star_2);
				}
				else if (this._i___1 == 2)
				{
					TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_star_3);
				}
				this._this.LIST_STAR[this._i___1].color = Color.white;
				this._this.LIST_STAR[this._i___1].sprite = this._this.sprCurrentStar;
				this._current = new WaitForSecondsRealtime(0.5f);
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			}
            //goto IL_1B3;
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

	public GameObject EFF_STAR_FALL;

	public Sprite sprStar_Normal;

	public Sprite sprStar_Hard;

	public Sprite sprStar_Nightmate;

	public Sprite sprStar_Empty;

	private Sprite sprCurrentStar;

	public Button buContinue;

	public Button buReplay;

	public Button buWatchAds;

	public Text txtGemGift;

	public Text txtFreeGem;

	public List<Image> LIST_STAR;

	private int iStar;

	private int iGemVitory;

	private static UnityAction __f__am_cache0;

	private void Start()
	{
		this.buContinue.onClick.AddListener(delegate
		{
			this.ButtonContinue();
		});
		this.buReplay.onClick.AddListener(delegate
		{
			this.ButtonReplay();
		});
		this.buWatchAds.onClick.AddListener(delegate
		{
			Note.ShowPopupNote(Note.NOTE.WatchAdsToGetFreeGem);
		});
	}

	private void OnEnable()
	{
		TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.attack_magic);
		this.txtFreeGem.text = "+" + TheDataManager.THE_PLAYER_DATA.iGemFormWatchingAds;
		this.iStar = this.GetStar();
		this.iGemVitory = this.GetGiftGem(this.iStar);
		TheDataManager.THE_PLAYER_DATA.SetStar(TheDataManager.THE_PLAYER_DATA.iCurrentLevel, this.iStar, TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT);
		TheEnumManager.DIFFICUFT gAME_DIFFICUFT = TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT;
		if (gAME_DIFFICUFT != TheEnumManager.DIFFICUFT.Normal)
		{
			if (gAME_DIFFICUFT != TheEnumManager.DIFFICUFT.Hard)
			{
				if (gAME_DIFFICUFT == TheEnumManager.DIFFICUFT.Nightmate)
				{
					this.sprCurrentStar = this.sprStar_Nightmate;
				}
			}
			else
			{
				this.sprCurrentStar = this.sprStar_Hard;
			}
		}
		else
		{
			this.sprCurrentStar = this.sprStar_Normal;
		}
		UnityEngine.Debug.Log(TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT.ToString() + ": " + TheDataManager.THE_PLAYER_DATA.iCurrentLevel);
		TheDataManager.THE_PLAYER_DATA.iPlayerGem += this.iGemVitory;
		TheDataManager.Instance.SerialzerPlayerData();
		TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
		base.StartCoroutine(this.AnimationStar(1.5f));
		if (PlayerPrefs.GetString("like") != "done")
		{
			base.StartCoroutine(this.IeShowRatePopup(1f));
		}
		TheEventManager.EventGameWinning(this.iStar);
	}

	private void ButtonContinue()
	{
		TheAdsManager.Instance.ShowFullAds();
		if (TheDataManager.THE_PLAYER_DATA.iCurrentLevel + 1 == ThePlatformManager.Instance.TOTAL_LEVEL_IN_GAME)
		{
			TheUIManager.Instance.LoadScene(TheEnumManager.SCENE.EndGame, false);
		}
		else
		{
			TheUIManager.Instance.LoadScene(TheEnumManager.SCENE.LevelSelection, false);
		}
	}

	private void ButtonReplay()
	{
		TheAdsManager.Instance.ShowFullAds();
		TheUIManager.Instance.LoadScene(TheEnumManager.SCENE.Gameplay, false);
	}

	private IEnumerator IeShowRatePopup(float _time)
	{
		Victory._IeShowRatePopup_c__Iterator0 _IeShowRatePopup_c__Iterator = new Victory._IeShowRatePopup_c__Iterator0();
		_IeShowRatePopup_c__Iterator._time = _time;
		return _IeShowRatePopup_c__Iterator;
	}

	private int GetStar()
	{
		float num = (float)TheLevel.Instance.iCurrentHeart * 1f / (float)TheLevel.Instance.iOriginalHeart;
		if (num > 0.8f)
		{
			return 3;
		}
		if (num < 0.5f)
		{
			return 1;
		}
		return 2;
	}

	private IEnumerator AnimationStar(float _time)
	{
		Victory._AnimationStar_c__Iterator1 _AnimationStar_c__Iterator = new Victory._AnimationStar_c__Iterator1();
		_AnimationStar_c__Iterator._time = _time;
		_AnimationStar_c__Iterator._this = this;
		return _AnimationStar_c__Iterator;
	}

	private int GetGiftGem(int _star)
	{
		int num = 0;
		if (_star != 1)
		{
			if (_star != 2)
			{
				if (_star == 3)
				{
					num = UnityEngine.Random.Range(12, 15);
				}
			}
			else
			{
				num = UnityEngine.Random.Range(8, 12);
			}
		}
		else
		{
			num = UnityEngine.Random.Range(5, 8);
		}
		TheEnumManager.DIFFICUFT gAME_DIFFICUFT = TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT;
		if (gAME_DIFFICUFT != TheEnumManager.DIFFICUFT.Normal)
		{
			if (gAME_DIFFICUFT != TheEnumManager.DIFFICUFT.Hard)
			{
				if (gAME_DIFFICUFT == TheEnumManager.DIFFICUFT.Nightmate)
				{
					num = (int)((float)num * 1.5f);
				}
			}
			else
			{
				num = (int)((float)num * 1.3f);
			}
		}
		else
		{
			num = (int)((float)num * 1f);
		}
		return num;
	}

	private void OnDisable()
	{
		TheMusic.Instance.Play();
	}
}

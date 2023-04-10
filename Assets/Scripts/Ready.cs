using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Ready : MonoBehaviour
{
	public Button buPlay;

	public Button buBack;

	public Sprite m_sprNormalButton;

	public Sprite m_sprChoosedButton;

	[Space(20f)]
	public Button buMode_Normal;

	public Button buMode_Hard;

	public Button buMore_Nightmate;

	[Space(20f)]
	public Text txtLevelTitle;

	public Text txtContentOfTips;

	[Space(20f)]
	public Sprite sprStar_empty;

	public Sprite sprStar_Normal;

	public Sprite sprStar_Hard;

	public Sprite sprStar_Difficuft;

	[Space(20f)]
	public Image imaIconMap;

	[Space(20f)]
	public List<Image> LIST_IMAGE_STAR;

	private int _currentStar_Normal;

	private int _currentStar_Hard;

	private int _currentStar_Nightmate;

	private static UnityAction __f__am_cache0;

	private static UnityAction __f__am_cache1;

	private void Start()
	{
		this.buPlay.onClick.AddListener(delegate
		{
			TheUIManager.Instance.LoadScene(TheEnumManager.SCENE.Gameplay, false);
		});
		this.buBack.onClick.AddListener(delegate
		{
			TheUIManager.HidePopup(ThePopupManager.POP_UP.Ready);
		});
		this.buMode_Normal.onClick.AddListener(delegate
		{
			this.GameMode(TheEnumManager.DIFFICUFT.Normal);
		});
		this.buMode_Hard.onClick.AddListener(delegate
		{
			this.GameMode(TheEnumManager.DIFFICUFT.Hard);
		});
		this.buMore_Nightmate.onClick.AddListener(delegate
		{
			this.GameMode(TheEnumManager.DIFFICUFT.Nightmate);
		});
	}

	private void GameMode(TheEnumManager.DIFFICUFT eDifficuft)
	{
		if (eDifficuft != TheEnumManager.DIFFICUFT.Normal)
		{
			if (eDifficuft != TheEnumManager.DIFFICUFT.Hard)
			{
				if (eDifficuft == TheEnumManager.DIFFICUFT.Nightmate)
				{
					if (this._currentStar_Hard == 3)
					{
						TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);
						this.buMore_Nightmate.image.sprite = this.m_sprChoosedButton;
						this.buMode_Hard.image.sprite = this.m_sprNormalButton;
						this.buMode_Normal.image.sprite = this.m_sprNormalButton;
						TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT = TheEnumManager.DIFFICUFT.Nightmate;
					}
					else
					{
						Note.ShowPopupNote(Note.NOTE.Need3StarToUnlock);
					}
					this.SetShowStar(this._currentStar_Nightmate, this.sprStar_Difficuft);
				}
			}
			else
			{
				if (this._currentStar_Normal == 3)
				{
					TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);
					this.buMode_Hard.image.sprite = this.m_sprChoosedButton;
					this.buMore_Nightmate.image.sprite = this.m_sprNormalButton;
					this.buMode_Normal.image.sprite = this.m_sprNormalButton;
					TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT = TheEnumManager.DIFFICUFT.Hard;
				}
				else
				{
					Note.ShowPopupNote(Note.NOTE.Need3StarToUnlock);
				}
				this.SetShowStar(this._currentStar_Hard, this.sprStar_Hard);
			}
		}
		else
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);
			this.buMode_Normal.image.sprite = this.m_sprChoosedButton;
			this.buMore_Nightmate.image.sprite = this.m_sprNormalButton;
			this.buMode_Hard.image.sprite = this.m_sprNormalButton;
			TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT = TheEnumManager.DIFFICUFT.Normal;
			this.SetShowStar(this._currentStar_Normal, this.sprStar_Normal);
		}
	}

	private void SetShowStar(int _star, Sprite _sprite)
	{
		int count = this.LIST_IMAGE_STAR.Count;
		for (int i = 0; i < count; i++)
		{
			if (i < _star)
			{
				this.LIST_IMAGE_STAR[i].sprite = _sprite;
			}
			else
			{
				this.LIST_IMAGE_STAR[i].sprite = this.sprStar_empty;
			}
		}
	}

	private void OnEnable()
	{
		this.txtLevelTitle.text = "LEVEL " + (TheDataManager.THE_PLAYER_DATA.iCurrentLevel + 1);
		this.txtContentOfTips.text = TheDataManager.Instance.GetRandomTips();
		this._currentStar_Normal = TheDataManager.THE_PLAYER_DATA.GetStar(TheDataManager.THE_PLAYER_DATA.iCurrentLevel, TheEnumManager.DIFFICUFT.Normal);
		this._currentStar_Hard = TheDataManager.THE_PLAYER_DATA.GetStar(TheDataManager.THE_PLAYER_DATA.iCurrentLevel, TheEnumManager.DIFFICUFT.Hard);
		this._currentStar_Nightmate = TheDataManager.THE_PLAYER_DATA.GetStar(TheDataManager.THE_PLAYER_DATA.iCurrentLevel, TheEnumManager.DIFFICUFT.Nightmate);
		this.buMode_Normal.image.color = Color.white;
		if (this._currentStar_Normal == 3)
		{
			this.buMode_Hard.image.color = Color.white;
		}
		else
		{
			this.buMode_Hard.image.color = Color.gray;
		}
		if (this._currentStar_Hard == 3)
		{
			this.buMore_Nightmate.image.color = Color.white;
		}
		else
		{
			this.buMore_Nightmate.image.color = Color.gray;
		}
		this.imaIconMap.sprite = MainCode_LevelSelection.Instance.GetSpriteIconMap(TheDataManager.THE_PLAYER_DATA.iCurrentLevel);
		this.GameMode(TheEnumManager.DIFFICUFT.Normal);
	}
}

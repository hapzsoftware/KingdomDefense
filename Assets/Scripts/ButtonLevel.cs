using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour
{
	public Text txtText;

	private Button buThis;

	public Image imaRenderNomarl;

	public Image imaRenderHard;

	public Image imaRenderNightmate;

	private int iLevel;

	private int iStar;

	private bool bLocked = true;

	private TheEnumManager.DIFFICUFT eDifficuft;

	private void Awake()
	{
		this.buThis = base.GetComponent<Button>();
		this.buThis.onClick.AddListener(delegate
		{
			this.ButtonClick();
		});
	}

	public void Init(int _level, int _star, TheEnumManager.DIFFICUFT _difficuft, bool _lock)
	{
		this.iLevel = _level;
		this.iStar = _star;
		this.eDifficuft = _difficuft;
		this.bLocked = _lock;
		this.txtText.text = (this.iLevel + 1).ToString();
		this.SetImage();
	}

	public void ButtonClick()
	{
		TheDataManager.THE_PLAYER_DATA.iCurrentLevel = this.iLevel;
		if (!this.bLocked)
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.battle_start);
			TheUIManager.ShowPopup(ThePopupManager.POP_UP.Ready);
		}
		else
		{
			UnityEngine.Debug.Log("LOCKED");
		}
	}

	public void SetImage()
	{
		if (this.iStar < 0)
		{
			this.imaRenderNomarl.gameObject.SetActive(true);
			this.imaRenderNomarl.sprite = MainCode_LevelSelection.Instance.LIST_SPRITE_OF_BUTTON_LEVEL_NORMAL[0];
			this.txtText.text = string.Empty;
			this.imaRenderHard.gameObject.SetActive(false);
			this.imaRenderNightmate.gameObject.SetActive(false);
		}
		else
		{
			TheEnumManager.DIFFICUFT dIFFICUFT = this.eDifficuft;
			if (dIFFICUFT != TheEnumManager.DIFFICUFT.Normal)
			{
				if (dIFFICUFT != TheEnumManager.DIFFICUFT.Hard)
				{
					if (dIFFICUFT == TheEnumManager.DIFFICUFT.Nightmate)
					{
						this.imaRenderNomarl.gameObject.SetActive(false);
						this.imaRenderHard.gameObject.SetActive(false);
						this.imaRenderNightmate.gameObject.SetActive(true);
						this.imaRenderNightmate.sprite = MainCode_LevelSelection.Instance.LIST_SPRITE_OF_BUTTON_LEVEL_NIGHTMATE[this.iStar];
					}
				}
				else
				{
					this.imaRenderNomarl.gameObject.SetActive(false);
					this.imaRenderHard.gameObject.SetActive(true);
					this.imaRenderHard.sprite = MainCode_LevelSelection.Instance.LIST_SPRITE_OF_BUTTON_LEVEL_HARD[this.iStar];
					this.imaRenderNightmate.gameObject.SetActive(false);
				}
			}
			else
			{
				this.imaRenderNomarl.sprite = MainCode_LevelSelection.Instance.LIST_SPRITE_OF_BUTTON_LEVEL_NORMAL[this.iStar];
				this.imaRenderNomarl.gameObject.SetActive(true);
				this.imaRenderHard.gameObject.SetActive(false);
				this.imaRenderNightmate.gameObject.SetActive(false);
			}
		}
	}

	public void Hide()
	{
		this.imaRenderNomarl.gameObject.SetActive(false);
		this.imaRenderHard.gameObject.SetActive(false);
		this.imaRenderNightmate.gameObject.SetActive(false);
		this.txtText.color = Color.white * 0f;
	}
}

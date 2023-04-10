using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
	public Button buLeft;

	public Button buRight;

	public Button buDone;

	public Image imaMain;

	public List<Sprite> LIST_SPRITE;

	private int iIndexOfSprite;

	private static UnityAction __f__am_cache0;

	private void Start()
	{
		this.buDone.onClick.AddListener(delegate
		{
			TheUIManager.HidePopup(ThePopupManager.POP_UP.Tutorial);
		});
		this.buLeft.onClick.AddListener(delegate
		{
			this.ButtonLeft();
		});
		this.buRight.onClick.AddListener(delegate
		{
			this.ButtonRight();
		});
	}

	private void OnEnable()
	{
		this.iIndexOfSprite = 0;
		this.ShowImage(0);
	}

	private void ButtonLeft()
	{
		TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_back);
		this.iIndexOfSprite--;
		if (this.iIndexOfSprite < 0)
		{
			this.iIndexOfSprite = this.LIST_SPRITE.Count - 1;
		}
		this.ShowImage(this.iIndexOfSprite);
	}

	private void ButtonRight()
	{
		TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);
		this.iIndexOfSprite++;
		if (this.iIndexOfSprite >= this.LIST_SPRITE.Count)
		{
			this.iIndexOfSprite = 0;
		}
		this.ShowImage(this.iIndexOfSprite);
	}

	private void ShowImage(int _index)
	{
		this.imaMain.sprite = this.LIST_SPRITE[_index];
	}
}

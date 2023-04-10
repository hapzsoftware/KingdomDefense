using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItems : MonoBehaviour
{
	public List<Button> LIST_BUTTON_BUY;

	public List<ShopData> LIST_SHOP_SKILL;

	private void Start()
	{
		this.LIST_BUTTON_BUY[0].onClick.AddListener(delegate
		{
			this.Buy(0);
		});
		this.LIST_BUTTON_BUY[1].onClick.AddListener(delegate
		{
			this.Buy(1);
		});
		this.LIST_BUTTON_BUY[2].onClick.AddListener(delegate
		{
			this.Buy(2);
		});
		this.LIST_BUTTON_BUY[3].onClick.AddListener(delegate
		{
			this.Buy(3);
		});
		this.LIST_BUTTON_BUY[4].onClick.AddListener(delegate
		{
			this.Buy(4);
		});
		TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
	}

	private void ShowInfo()
	{
		this.LIST_SHOP_SKILL = TheDataManager.Instance.ListShopData(TheEnumManager.KIND_OF_SHOP.ShopSkill);
		for (int i = 0; i < this.LIST_SHOP_SKILL.Count; i++)
		{
			this.LIST_BUTTON_BUY[i].transform.parent.Find("Text_Name").GetComponent<Text>().text = this.LIST_SHOP_SKILL[i].strPackName;
			this.LIST_BUTTON_BUY[i].transform.parent.Find("Text_Content").GetComponent<Text>().text = this.LIST_SHOP_SKILL[i].strContent;
			this.LIST_BUTTON_BUY[i].transform.parent.Find("Text_NumberToAdd").GetComponent<Text>().text = "+" + this.LIST_SHOP_SKILL[i].iValueToAdd;
			this.LIST_BUTTON_BUY[i].GetComponentInChildren<Text>().text = this.LIST_SHOP_SKILL[i].iPriceGem.ToString();
			Text component = this.LIST_BUTTON_BUY[i].transform.parent.Find("Text_Number").GetComponent<Text>();
			TheEnumManager.SKILL e = TheEnumManager.ConverStringToEnum_Skill(this.LIST_SHOP_SKILL[i].strId);
			component.text = TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(e).ToString();
		}
	}

	private void Buy(int _index)
	{
		if (TheDataManager.THE_PLAYER_DATA.iPlayerGem >= this.LIST_SHOP_SKILL[_index].iPriceGem)
		{
			this.LIST_SHOP_SKILL[_index].BuySkill();
			TheDataManager.THE_PLAYER_DATA.iPlayerGem -= this.LIST_SHOP_SKILL[_index].iPriceGem;
			TheDataManager.Instance.SerialzerPlayerData();
			TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
			this.ShowInfo();
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_buy_something);
			TheFirebaseManager.Instance.PostEvent_BuyItemInShop(this.LIST_SHOP_SKILL[_index].strPackName);
		}
		else
		{
			Note.ShowPopupNote(Note.NOTE.NotEnoughtGem);
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_can_not);
		}
	}

	private void OnEnable()
	{
		this.ShowInfo();
		TheEventManager.RegisterEvent(TheEventManager.EventID.UPDATE_BOARD_INFO, new TheEventManager.ACTION(this.ShowInfo));
	}

	private void OnDisable()
	{
		TheEventManager.RemoveEvent(TheEventManager.EventID.UPDATE_BOARD_INFO, new TheEventManager.ACTION(this.ShowInfo));
		TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_SKILL_BOARD);
	}
}

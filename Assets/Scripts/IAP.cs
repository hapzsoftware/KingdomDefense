using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IAP : MonoBehaviour
{
	public List<Button> LIST_BUTTON_BUY;

	public List<ShopData> LIST_SHOP_DATA_IAP;

	private void Start()
	{
		this.LIST_SHOP_DATA_IAP = TheDataManager.Instance.ListShopData(TheEnumManager.KIND_OF_SHOP.Iap);
		this.ShowInfo();
		this.LIST_BUTTON_BUY[0].onClick.AddListener(delegate
		{
			this.Buy(TheEnumManager.ITEM_IN_SHOP.gem_pack_1);
		});
		this.LIST_BUTTON_BUY[1].onClick.AddListener(delegate
		{
			this.Buy(TheEnumManager.ITEM_IN_SHOP.gem_pack_2);
		});
		this.LIST_BUTTON_BUY[2].onClick.AddListener(delegate
		{
			this.Buy(TheEnumManager.ITEM_IN_SHOP.gem_pack_3);
		});
		this.LIST_BUTTON_BUY[3].onClick.AddListener(delegate
		{
			this.Buy(TheEnumManager.ITEM_IN_SHOP.gem_pack_4);
		});
		this.LIST_BUTTON_BUY[4].onClick.AddListener(delegate
		{
			this.Buy(TheEnumManager.ITEM_IN_SHOP.gem_pack_5);
		});
		this.LIST_BUTTON_BUY[5].onClick.AddListener(delegate
		{
			this.Buy(TheEnumManager.ITEM_IN_SHOP.gem_pack_6);
		});
		TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
	}

	private void ShowInfo()
	{
		for (int i = 0; i < this.LIST_BUTTON_BUY.Count; i++)
		{
			this.LIST_BUTTON_BUY[i].transform.parent.Find("Text_Name").GetComponent<Text>().text = this.LIST_SHOP_DATA_IAP[i].strPackName;
			this.LIST_BUTTON_BUY[i].transform.parent.Find("Text_Gem").GetComponent<Text>().text = "+" + this.LIST_SHOP_DATA_IAP[i].iGemValueToAdd;
			this.LIST_BUTTON_BUY[i].GetComponentInChildren<Text>().text = this.LIST_SHOP_DATA_IAP[i].fPriceDollar.ToString() + "$";
		}
	}

	public void Buy(TheEnumManager.ITEM_IN_SHOP _iap)
	{
		if (ThePlatformManager.bNoIAP)
		{
			ShopData shopData = TheDataManager.Instance.GetShopData(_iap.ToString());
			shopData.BuyGem();
			TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
			TheDataManager.Instance.SerialzerPlayerData();
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);
		}
		else
		{
			InAppPurchaseManager.Instance.BuyGem(_iap.ToString());
		}
		this.ShowInfo();
	}
}

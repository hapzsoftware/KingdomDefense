using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ShopCoin : MonoBehaviour
{
	private sealed class _IeShowCoin_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
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

		public _IeShowCoin_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitForSecondsRealtime(0.02f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
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

	public List<Button> LIST_BUTTON;

	public List<ShopData> LIST_SHOP_DATA_COIN;

	private void Awake()
	{
		this.LIST_SHOP_DATA_COIN = TheDataManager.Instance.ListShopData(TheEnumManager.KIND_OF_SHOP.ShopCoin);
		this.LIST_BUTTON[0].onClick.AddListener(delegate
		{
			this.Buy(0);
		});
		this.LIST_BUTTON[1].onClick.AddListener(delegate
		{
			this.Buy(1);
		});
		this.LIST_BUTTON[2].onClick.AddListener(delegate
		{
			this.Buy(2);
		});
		this.LIST_BUTTON[3].onClick.AddListener(delegate
		{
			this.Buy(3);
		});
		this.LIST_BUTTON[4].onClick.AddListener(delegate
		{
			this.Buy(4);
		});
		this.LIST_BUTTON[5].onClick.AddListener(delegate
		{
			this.Buy(5);
		});
	}

	private void Start()
	{
		this.ShowInfo();
	}

	private void Buy(int _index)
	{
		if (TheDataManager.THE_PLAYER_DATA.iPlayerGem >= this.LIST_SHOP_DATA_COIN[_index].iPriceGem)
		{
			TheDataManager.THE_PLAYER_DATA.iPlayerGem -= this.LIST_SHOP_DATA_COIN[_index].iPriceGem;
			TheDataManager.Instance.SerialzerPlayerData();
			this.LIST_SHOP_DATA_COIN[_index].BuyCoin();
			this.ShowInfo();
			TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);
			TheFirebaseManager.Instance.PostEvent_BuyCoin(this.LIST_SHOP_DATA_COIN[_index].strPackName);
		}
		else
		{
			Note.ShowPopupNote(Note.NOTE.NotEnoughtGem);
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_can_not);
		}
	}

	private void ShowInfo()
	{
		for (int i = 0; i < this.LIST_BUTTON.Count; i++)
		{
			this.LIST_BUTTON[i].transform.parent.Find("Text_Name").GetComponent<Text>().text = this.LIST_SHOP_DATA_COIN[i].strPackName;
			this.LIST_BUTTON[i].transform.parent.Find("Text_Coin").GetComponent<Text>().text = "+" + this.LIST_SHOP_DATA_COIN[i].iCoinValueToAdd;
			this.LIST_BUTTON[i].GetComponentInChildren<Text>().text = this.LIST_SHOP_DATA_COIN[i].iPriceGem.ToString();
		}
	}

	private void OnEnable()
	{
		TheEventManager.RegisterEvent(TheEventManager.EventID.UPDATE_BOARD_INFO, new TheEventManager.ACTION(this.ShowInfo));
		base.StartCoroutine(this.IeShowCoin());
	}

	private void OnDisable()
	{
		TheEventManager.RemoveEvent(TheEventManager.EventID.UPDATE_BOARD_INFO, new TheEventManager.ACTION(this.ShowInfo));
	}

	private IEnumerator IeShowCoin()
	{
		return new ShopCoin._IeShowCoin_c__Iterator0();
	}
}

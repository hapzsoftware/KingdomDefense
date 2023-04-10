using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class InAppPurchaseManager : MonoBehaviour, IStoreListener
{
	private sealed class _InitializePurchasing_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal InAppPurchaseManager _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		private static Func<bool> __f__am_cache0;

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

		public _InitializePurchasing_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitUntil(() => TheDataManager.Instance != null);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				if (!this._this.IsInitialized())
				{
					ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(), new IPurchasingModule[0]);
					configurationBuilder.AddProduct(TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.gem_pack_1.ToString()).strIdKeyStore, ProductType.Consumable);
					configurationBuilder.AddProduct(TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.gem_pack_2.ToString()).strIdKeyStore, ProductType.Consumable);
					configurationBuilder.AddProduct(TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.gem_pack_3.ToString()).strIdKeyStore, ProductType.Consumable);
					configurationBuilder.AddProduct(TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.gem_pack_4.ToString()).strIdKeyStore, ProductType.Consumable);
					configurationBuilder.AddProduct(TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.gem_pack_5.ToString()).strIdKeyStore, ProductType.Consumable);
					configurationBuilder.AddProduct(TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.gem_pack_6.ToString()).strIdKeyStore, ProductType.Consumable);
					configurationBuilder.AddProduct(TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.tower_poison.ToString()).strIdKeyStore, ProductType.Consumable);
					configurationBuilder.AddProduct(TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.tower_rocket_laucher.ToString()).strIdKeyStore, ProductType.Consumable);
					configurationBuilder.AddProduct(TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.tower_thunder.ToString()).strIdKeyStore, ProductType.Consumable);
					UnityPurchasing.Initialize(this._this, configurationBuilder);
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

		private static bool __m__0()
		{
			return TheDataManager.Instance != null;
		}
	}

	public static InAppPurchaseManager Instance;

	private static IStoreController m_StoreController;

	private static IExtensionProvider m_StoreExtensionProvider;

	private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";

	private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";

	private static Action<bool> __f__am_cache0;

	private void Awake()
	{
		if (InAppPurchaseManager.Instance == null)
		{
			InAppPurchaseManager.Instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(this);
		}
	}

	private void Start()
	{
		if (InAppPurchaseManager.m_StoreController == null)
		{
			base.StartCoroutine(this.InitializePurchasing());
		}
	}

	public IEnumerator InitializePurchasing()
	{
		InAppPurchaseManager._InitializePurchasing_c__Iterator0 _InitializePurchasing_c__Iterator = new InAppPurchaseManager._InitializePurchasing_c__Iterator0();
		_InitializePurchasing_c__Iterator._this = this;
		return _InitializePurchasing_c__Iterator;
	}

	private bool IsInitialized()
	{
		return InAppPurchaseManager.m_StoreController != null && InAppPurchaseManager.m_StoreExtensionProvider != null;
	}

	public void BuyGem(string _id)
	{
		this.BuyProductID(TheDataManager.Instance.GetShopData(_id).strIdKeyStore);
	}

	public void BuyTower(string _id)
	{
		this.BuyProductID(TheDataManager.Instance.GetShopData(_id).strIdKeyStore);
	}

	public void BuyProductID(string productId)
	{
		if (this.IsInitialized())
		{
			Product product = InAppPurchaseManager.m_StoreController.products.WithID(productId);
			if (product != null && product.availableToPurchase)
			{
				UnityEngine.Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				InAppPurchaseManager.m_StoreController.InitiatePurchase(product);
			}
			else
			{
				UnityEngine.Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}
		else
		{
			UnityEngine.Debug.Log("BuyProductID FAIL. Not initialized.");
		}
	}

	public void RestorePurchases()
	{
		if (!this.IsInitialized())
		{
			UnityEngine.Debug.Log("RestorePurchases FAIL. Not initialized.");
			return;
		}
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
		{
			UnityEngine.Debug.Log("RestorePurchases started ...");
			IAppleExtensions extension = InAppPurchaseManager.m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
			extension.RestoreTransactions(delegate(bool result)
			{
				UnityEngine.Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
			});
		}
		else
		{
			UnityEngine.Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		UnityEngine.Debug.Log("OnInitialized: PASS");
		InAppPurchaseManager.m_StoreController = controller;
		InAppPurchaseManager.m_StoreExtensionProvider = extensions;
	}

	public void OnInitializeFailed(InitializationFailureReason error)
	{
		UnityEngine.Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
		ShopData shopData = new ShopData();
		if (string.Equals(args.purchasedProduct.definition.id, TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.gem_pack_1.ToString()).strIdKeyStore, StringComparison.Ordinal))
		{
			shopData = TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.gem_pack_1.ToString());
			shopData.BuyGem();
			TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
			TheDataManager.Instance.SerialzerPlayerData();
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);
			TheGooglePlayServicesManager.Instance.UnlockArchievement("CggIh56L8GEQAhAC");
			TheFirebaseManager.Instance.PostEvent_BuyIAP(shopData.strPackName);
		}
		else if (string.Equals(args.purchasedProduct.definition.id, TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.gem_pack_2.ToString()).strIdKeyStore, StringComparison.Ordinal))
		{
			shopData = TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.gem_pack_2.ToString());
			shopData.BuyGem();
			TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
			TheDataManager.Instance.SerialzerPlayerData();
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);
			TheGooglePlayServicesManager.Instance.UnlockArchievement("CggIh56L8GEQAhAC");
			TheFirebaseManager.Instance.PostEvent_BuyIAP(shopData.strPackName);
		}
		else if (string.Equals(args.purchasedProduct.definition.id, TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.gem_pack_3.ToString()).strIdKeyStore, StringComparison.Ordinal))
		{
			shopData = TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.gem_pack_3.ToString());
			shopData.BuyGem();
			TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
			TheDataManager.Instance.SerialzerPlayerData();
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);
			TheGooglePlayServicesManager.Instance.UnlockArchievement("CggIh56L8GEQAhAC");
			TheFirebaseManager.Instance.PostEvent_BuyIAP(shopData.strPackName);
		}
		else if (string.Equals(args.purchasedProduct.definition.id, TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.gem_pack_4.ToString()).strIdKeyStore, StringComparison.Ordinal))
		{
			shopData = TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.gem_pack_4.ToString());
			shopData.BuyGem();
			TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
			TheDataManager.Instance.SerialzerPlayerData();
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);
			TheGooglePlayServicesManager.Instance.UnlockArchievement("CggIh56L8GEQAhAC");
			TheFirebaseManager.Instance.PostEvent_BuyIAP(shopData.strPackName);
		}
		else if (string.Equals(args.purchasedProduct.definition.id, TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.gem_pack_5.ToString()).strIdKeyStore, StringComparison.Ordinal))
		{
			shopData = TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.gem_pack_5.ToString());
			shopData.BuyGem();
			TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
			TheDataManager.Instance.SerialzerPlayerData();
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);
			TheGooglePlayServicesManager.Instance.UnlockArchievement("CggIh56L8GEQAhAC");
			TheFirebaseManager.Instance.PostEvent_BuyIAP(shopData.strPackName);
		}
		else if (string.Equals(args.purchasedProduct.definition.id, TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.gem_pack_6.ToString()).strIdKeyStore, StringComparison.Ordinal))
		{
			shopData = TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.gem_pack_6.ToString());
			shopData.BuyGem();
			TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
			TheDataManager.Instance.SerialzerPlayerData();
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);
			TheGooglePlayServicesManager.Instance.UnlockArchievement("CggIh56L8GEQAhAC");
			TheFirebaseManager.Instance.PostEvent_BuyIAP(shopData.strPackName);
		}
		else if (string.Equals(args.purchasedProduct.definition.id, TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.tower_poison.ToString()).strIdKeyStore, StringComparison.Ordinal))
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);
			TheDataManager.THE_PLAYER_DATA.SetUnlockTower(TheEnumManager.TOWER.tower_poison, true);
			TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
			TheDataManager.Instance.SerialzerPlayerData();
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);
			TheGooglePlayServicesManager.Instance.UnlockArchievement("CggIh56L8GEQAhAC");
			TheFirebaseManager.Instance.PostEvent_BuyIAP(shopData.strPackName);
			TheEventManager.PostEvent(TheEventManager.EventID.UNLOCK_TOWER);
		}
		else if (string.Equals(args.purchasedProduct.definition.id, TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.tower_rocket_laucher.ToString()).strIdKeyStore, StringComparison.Ordinal))
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);
			TheDataManager.THE_PLAYER_DATA.SetUnlockTower(TheEnumManager.TOWER.tower_rocket_laucher, true);
			TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
			TheDataManager.Instance.SerialzerPlayerData();
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);
			TheGooglePlayServicesManager.Instance.UnlockArchievement("CggIh56L8GEQAhAC");
			TheFirebaseManager.Instance.PostEvent_BuyIAP(shopData.strPackName);
			TheEventManager.PostEvent(TheEventManager.EventID.UNLOCK_TOWER);
		}
		else if (string.Equals(args.purchasedProduct.definition.id, TheDataManager.Instance.GetShopData(TheEnumManager.ITEM_IN_SHOP.tower_thunder.ToString()).strIdKeyStore, StringComparison.Ordinal))
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);
			TheDataManager.THE_PLAYER_DATA.SetUnlockTower(TheEnumManager.TOWER.tower_thunder, true);
			TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
			TheDataManager.Instance.SerialzerPlayerData();
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);
			TheGooglePlayServicesManager.Instance.UnlockArchievement("CggIh56L8GEQAhAC");
			TheFirebaseManager.Instance.PostEvent_BuyIAP(shopData.strPackName);
			TheEventManager.PostEvent(TheEventManager.EventID.UNLOCK_TOWER);
		}
		else
		{
			UnityEngine.Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		UnityEngine.Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}
}

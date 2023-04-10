using System;

[Serializable]
public class ShopData
{
	public string strId;

	public TheEnumManager.KIND_OF_SHOP eKindOfShop;

	public string strPackName;

	public int iGemValueToAdd;

	public int iCoinValueToAdd;

	public int iValueToAdd;

	public int iPriceGem;

	public float fPriceDollar;

	public string strIdKeyStore;

	public string strContent;

	public void BuySkill()
	{
		TheEnumManager.SKILL e = TheEnumManager.ConverStringToEnum_Skill(this.strId);
		TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(e, TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(e) + this.iValueToAdd);
	}

	public void BuyGem()
	{
		TheDataManager.THE_PLAYER_DATA.iPlayerGem += this.iGemValueToAdd;
	}

	public void BuyCoin()
	{
		TheLevel.Instance.iOriginalCoin += this.iCoinValueToAdd;
	}
}

using System;

[Serializable]
public class UpgradeData
{
	public bool isActived;

	public string strId;

	public string strName;

	public string strContent;

	public float fFactorUp;

	public float fFactorDown;

	public float fValueBeAfterActived;

	public float fValueDefaul;

	public int iPrice_star_white;

	public int iPrice_star_blue;

	public int iPrice_star_yellow;

	public void Init()
	{
		this.isActived = TheDataManager.THE_PLAYER_DATA.GetActiveOfUpgradeSystem(this.strId);
	}

	public void Active(bool _active)
	{
		this.isActived = _active;
		TheDataManager.THE_PLAYER_DATA.SetActiveOfUpgradeSystem(this.strId, _active);
	}

	public float GetValueDefaul()
	{
		return this.fValueDefaul;
	}

	public int GetPriceStar()
	{
		if (this.GetStarKind() == TheEnumManager.STAR.yellow)
		{
			return this.iPrice_star_yellow;
		}
		if (this.GetStarKind() == TheEnumManager.STAR.blue)
		{
			return this.iPrice_star_blue;
		}
		return this.iPrice_star_white;
	}

	public float GetFatorUp()
	{
		return this.fFactorUp / 100f;
	}

	public float GetFactorDown()
	{
		return this.fFactorDown / 100f;
	}

	public TheEnumManager.STAR GetStarKind()
	{
		if (this.iPrice_star_yellow != 0)
		{
			return TheEnumManager.STAR.yellow;
		}
		if (this.iPrice_star_blue != 0)
		{
			return TheEnumManager.STAR.blue;
		}
		return TheEnumManager.STAR.white;
	}
}

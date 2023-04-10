using System;

[Serializable]
public class MyTowerData
{
	public bool bUpgraded;

	public TheEnumManager.TOWER eTower;

	public TheEnumManager.TOWER_LEVEL eTowerLevel;

	public string strID;

	public string strName;

	public string strContent;

	public int iDamage;

	public int iBullet;

	public float fAttackSpeed;

	public float fMoveSpeed;

	public float fRange;

	public int iTimeOfTranning;

	public int iHP;

	public int iMaxHp;

	public int iNumberOfSoldier;

	public bool bCanKillInfantry;

	public bool bCanKillAirForce;

	public int iPriceToBuy;

	public int iPriceToUpgrade;

	public int iValueOfSell;

	public int iLevelToUnlock;

	public int iPriceGemToUnlock;

	public void Init()
	{
		if (this.strID.Contains("magic"))
		{
			this.eTower = TheEnumManager.TOWER.tower_magic;
		}
		else if (this.strID.Contains("archer"))
		{
			this.eTower = TheEnumManager.TOWER.tower_archer;
		}
		else if (this.strID.Contains("stone"))
		{
			this.eTower = TheEnumManager.TOWER.tower_cannonner;
		}
		else if (this.strID.Contains("soldier"))
		{
			this.eTower = TheEnumManager.TOWER.soldier;
		}
		else if (this.strID.Contains("gunner"))
		{
			this.eTower = TheEnumManager.TOWER.tower_gunmen;
		}
		else if (this.strID.Contains("poision"))
		{
			this.eTower = TheEnumManager.TOWER.tower_poison;
		}
		else if (this.strID.Contains("rocket"))
		{
			this.eTower = TheEnumManager.TOWER.tower_rocket_laucher;
		}
		else if (this.strID.Contains("electric"))
		{
			this.eTower = TheEnumManager.TOWER.tower_thunder;
		}
		this.UpgradeSystem();
	}

	public void UpgradeSystem()
	{
		UpgradeData upgradeData = new UpgradeData();
		switch (this.eTower)
		{
		case TheEnumManager.TOWER.tower_magic:
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.TowerMagic_MoreDamage);
			if (upgradeData.isActived)
			{
				this.iDamage += (int)((float)this.iDamage * 1f * upgradeData.GetFatorUp());
			}
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.TowerMagic_MoreRange);
			if (upgradeData.isActived)
			{
				this.fRange += this.fRange * upgradeData.GetFatorUp();
			}
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.TowerMagic_ReducePriceToBuild);
			if (upgradeData.isActived)
			{
				this.iPriceToBuy = (int)((float)this.iPriceToBuy * (1f - upgradeData.GetFactorDown()));
			}
			break;
		case TheEnumManager.TOWER.tower_archer:
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.TowerArcher_MoreDamage);
			if (upgradeData.isActived)
			{
				this.iDamage += (int)((float)this.iDamage * 1f * upgradeData.GetFatorUp());
			}
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.TowerArcher_MoreRange);
			if (upgradeData.isActived)
			{
				this.fRange += this.fRange * upgradeData.GetFatorUp();
			}
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.TowerArcher_ReducePriceToBuild);
			if (upgradeData.isActived)
			{
				this.iPriceToBuy = (int)((float)this.iPriceToBuy * (1f - upgradeData.GetFactorDown()));
			}
			break;
		case TheEnumManager.TOWER.tower_cannonner:
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.TowerCannonner_MoreDamage);
			if (upgradeData.isActived)
			{
				this.iDamage += (int)((float)this.iDamage * 1f * upgradeData.GetFatorUp());
			}
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.TowerCannonner_MoreRange);
			if (upgradeData.isActived)
			{
				this.fRange += this.fRange * upgradeData.GetFatorUp();
			}
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.TowerCannonner_ReducePriceToBuild);
			if (upgradeData.isActived)
			{
				this.iPriceToBuy = (int)((float)this.iPriceToBuy * (1f - upgradeData.GetFactorDown()));
			}
			break;
		case TheEnumManager.TOWER.soldier:
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.Reinforcement_MoreHp);
			if (upgradeData.isActived)
			{
				this.iHP += (int)((float)this.iHP * 1f * upgradeData.GetFatorUp());
				this.iMaxHp = this.iHP;
			}
			break;
		case TheEnumManager.TOWER.tower_gunmen:
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.TowerGunmen_MoreDamage);
			if (upgradeData.isActived)
			{
				this.iDamage += (int)((float)this.iDamage * 1f * upgradeData.GetFatorUp());
			}
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.TowerGunmen_MoreRange);
			if (upgradeData.isActived)
			{
				this.fRange += this.fRange * upgradeData.GetFatorUp();
			}
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.TowerGunmen_ReducePriceToBuild);
			if (upgradeData.isActived)
			{
				this.iPriceToBuy = (int)((float)this.iPriceToBuy * (1f - upgradeData.GetFactorDown()));
			}
			break;
		case TheEnumManager.TOWER.tower_poison:
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.TowerPoison_MoreDamage);
			if (upgradeData.isActived)
			{
				this.iDamage += (int)((float)this.iDamage * 1f * upgradeData.GetFatorUp());
			}
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.TowerPoison_MoreRange);
			if (upgradeData.isActived)
			{
				this.fRange += this.fRange * upgradeData.GetFatorUp();
			}
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.TowerPoison_ReducePriceToBuild);
			if (upgradeData.isActived)
			{
				this.iPriceToBuy = (int)((float)this.iPriceToBuy * (1f - upgradeData.GetFactorDown()));
			}
			break;
		case TheEnumManager.TOWER.tower_rocket_laucher:
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.TowerRocketLaucher_MoreDamage);
			if (upgradeData.isActived)
			{
				this.iDamage += (int)((float)this.iDamage * 1f * upgradeData.GetFatorUp());
			}
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.TowerRocketLaucher_MoreRange);
			if (upgradeData.isActived)
			{
				this.fRange += this.fRange * upgradeData.GetFatorUp();
			}
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.TowerRocketLaucher_ReducePriceToBuild);
			if (upgradeData.isActived)
			{
				this.iPriceToBuy = (int)((float)this.iPriceToBuy * (1f - upgradeData.GetFactorDown()));
			}
			break;
		case TheEnumManager.TOWER.tower_thunder:
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.TowerThunder_MoreDamage);
			if (upgradeData.isActived)
			{
				this.iDamage += (int)((float)this.iDamage * 1f * upgradeData.GetFatorUp());
			}
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.TowerThunder_MoreRange);
			if (upgradeData.isActived)
			{
				this.fRange += this.fRange * upgradeData.GetFatorUp();
			}
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.TowerThunder_ReducePriceToBuild);
			if (upgradeData.isActived)
			{
				this.iPriceToBuy = (int)((float)this.iPriceToBuy * (1f - upgradeData.GetFactorDown()));
			}
			break;
		}
		if (this.eTower != TheEnumManager.TOWER.soldier)
		{
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.Other_ReducePriceTowerToBuildForAllTower);
			if (upgradeData.isActived)
			{
				this.iPriceToBuy = (int)((float)this.iPriceToBuy * (1f - upgradeData.GetFactorDown()));
			}
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.Other_MoreAttackSpeedForAllTower);
			if (upgradeData.isActived)
			{
				this.fAttackSpeed *= 1f - upgradeData.GetFactorDown();
			}
		}
	}

	public int GetDamage()
	{
		return this.iDamage;
	}

	public int GetDamageToShow()
	{
		return this.iDamage * this.iBullet;
	}

	public MyTowerData Clone()
	{
		return (MyTowerData)base.MemberwiseClone();
	}
}

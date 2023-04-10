using System;

[Serializable]
public class EnemyData
{
	public string strID;

	public string strName;

	public string strContent;

	public int iDamage;

	public float fShootingSpeed;

	public float fMoveSpeed;

	public float fRange;

	public int iHP;

	public int iMaxHp;

	public int iCoin;

	public bool bIsInfantry;

	public bool bIsAirForece;

	public bool bBoss;

	public EnemyData Clone()
	{
		return (EnemyData)base.MemberwiseClone();
	}

	public void ConfigEnemyDataWithDifficuft()
	{
		float num = 1f;
		float num2 = 1f;
		float num3 = 1f;
		TheEnumManager.DIFFICUFT gAME_DIFFICUFT = TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT;
		if (gAME_DIFFICUFT != TheEnumManager.DIFFICUFT.Normal)
		{
			if (gAME_DIFFICUFT != TheEnumManager.DIFFICUFT.Hard)
			{
				if (gAME_DIFFICUFT == TheEnumManager.DIFFICUFT.Nightmate)
				{
					switch (TheDataManager.THE_PLAYER_DATA.CURRENT_LEVEL_DIFFICUFT_MODE_NIGHTMATE)
					{
					case TheEnumManager.LEVEL_DIFFICIFT.Level_1:
						num = 1.7f;
						num2 = 1.7f;
						num3 = 1.7f;
						break;
					case TheEnumManager.LEVEL_DIFFICIFT.Level_2:
						num = 1.73f;
						num2 = 1.73f;
						num3 = 1.73f;
						break;
					case TheEnumManager.LEVEL_DIFFICIFT.Level_3:
						num = 1.76f;
						num2 = 1.76f;
						num3 = 1.76f;
						break;
					case TheEnumManager.LEVEL_DIFFICIFT.Level_4:
						num = 1.79f;
						num2 = 1.79f;
						num3 = 1.79f;
						break;
					case TheEnumManager.LEVEL_DIFFICIFT.Level_5:
						num = 1.82f;
						num2 = 1.82f;
						num3 = 1.82f;
						break;
					case TheEnumManager.LEVEL_DIFFICIFT.Level_6:
						num = 1.85f;
						num2 = 1.85f;
						num3 = 1.85f;
						break;
					case TheEnumManager.LEVEL_DIFFICIFT.Level_7:
						num = 1.88f;
						num2 = 1.88f;
						num3 = 1.88f;
						break;
					case TheEnumManager.LEVEL_DIFFICIFT.Level_8:
						num = 1.91f;
						num2 = 1.91f;
						num3 = 1.91f;
						break;
					case TheEnumManager.LEVEL_DIFFICIFT.Level_9:
						num = 1.94f;
						num2 = 1.94f;
						num3 = 1.94f;
						break;
					case TheEnumManager.LEVEL_DIFFICIFT.Level_10:
						num = 1.98f;
						num2 = 1.98f;
						num3 = 1.98f;
						break;
					}
				}
			}
			else
			{
				switch (TheDataManager.THE_PLAYER_DATA.CURRENT_LEVEL_DIFFICUFT_MODE_HARD)
				{
				case TheEnumManager.LEVEL_DIFFICIFT.Level_1:
					num = 1.5f;
					num2 = 1.5f;
					num3 = 1.5f;
					break;
				case TheEnumManager.LEVEL_DIFFICIFT.Level_2:
					num = 1.52f;
					num2 = 1.52f;
					num3 = 1.52f;
					break;
				case TheEnumManager.LEVEL_DIFFICIFT.Level_3:
					num = 1.54f;
					num2 = 1.54f;
					num3 = 1.54f;
					break;
				case TheEnumManager.LEVEL_DIFFICIFT.Level_4:
					num = 1.56f;
					num2 = 1.56f;
					num3 = 1.56f;
					break;
				case TheEnumManager.LEVEL_DIFFICIFT.Level_5:
					num = 1.58f;
					num2 = 1.58f;
					num3 = 1.58f;
					break;
				case TheEnumManager.LEVEL_DIFFICIFT.Level_6:
					num = 1.6f;
					num2 = 1.6f;
					num3 = 1.6f;
					break;
				case TheEnumManager.LEVEL_DIFFICIFT.Level_7:
					num = 1.62f;
					num2 = 1.62f;
					num3 = 1.62f;
					break;
				case TheEnumManager.LEVEL_DIFFICIFT.Level_8:
					num = 1.64f;
					num2 = 1.64f;
					num3 = 1.64f;
					break;
				case TheEnumManager.LEVEL_DIFFICIFT.Level_9:
					num = 1.66f;
					num2 = 1.66f;
					num3 = 1.66f;
					break;
				case TheEnumManager.LEVEL_DIFFICIFT.Level_10:
					num = 1.68f;
					num2 = 1.68f;
					num3 = 1.68f;
					break;
				}
			}
		}
		else
		{
			switch (TheDataManager.THE_PLAYER_DATA.CURRENT_LEVEL_DIFFICUFT_MODE_NORMAL)
			{
			case TheEnumManager.LEVEL_DIFFICIFT.Level_1:
				num = 1f;
				num2 = 1f;
				num3 = 1f;
				break;
			case TheEnumManager.LEVEL_DIFFICIFT.Level_2:
				num = 1.02f;
				num2 = 1.02f;
				num3 = 1.02f;
				break;
			case TheEnumManager.LEVEL_DIFFICIFT.Level_3:
				num = 1.03f;
				num2 = 1.03f;
				num3 = 1.03f;
				break;
			case TheEnumManager.LEVEL_DIFFICIFT.Level_4:
				num = 1.04f;
				num2 = 1.04f;
				num3 = 1.04f;
				break;
			case TheEnumManager.LEVEL_DIFFICIFT.Level_5:
				num = 1.05f;
				num2 = 1.05f;
				num3 = 1.05f;
				break;
			case TheEnumManager.LEVEL_DIFFICIFT.Level_6:
				num = 1.06f;
				num2 = 1.06f;
				num3 = 1.06f;
				break;
			case TheEnumManager.LEVEL_DIFFICIFT.Level_7:
				num = 1.07f;
				num2 = 1.07f;
				num3 = 1.07f;
				break;
			case TheEnumManager.LEVEL_DIFFICIFT.Level_8:
				num = 1.08f;
				num2 = 1.08f;
				num3 = 1.08f;
				break;
			case TheEnumManager.LEVEL_DIFFICIFT.Level_9:
				num = 1.09f;
				num2 = 1.09f;
				num3 = 1.09f;
				break;
			case TheEnumManager.LEVEL_DIFFICIFT.Level_10:
				num = 1.1f;
				num2 = 1.1f;
				num3 = 1.1f;
				break;
			}
		}
		this.iDamage = (int)((float)this.iDamage * num);
		this.fShootingSpeed *= num2;
		this.iHP = (int)((float)this.iHP * num3);
		this.iMaxHp = (int)((float)this.iMaxHp * num3);
	}

	public void ConfigEnemyDataWithLevel()
	{
		this.iHP += MathGame.GetHpFollowTheLevel(TheDataManager.THE_PLAYER_DATA.iCurrentLevel);
		this.iHP = MathGame.GetHpFollowTheWave(TheLevel.Instance.iCurrentWave, TheLevel.Instance.iMAX_WAVE_CONFIG, this.iHP);
		this.iMaxHp = this.iHP;
		if (this.bBoss && this.iMaxHp < 300)
		{
			this.iMaxHp = 300;
			this.iHP = 300;
		}
	}

	public void ConfigEnemyDataWithUpgradeSystem()
	{
		UpgradeData upgradeData = new UpgradeData();
		upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.Enemy_ReduceDamage);
		if (upgradeData.isActived)
		{
			this.iDamage = (int)((float)this.iDamage * 1f * (1f - upgradeData.GetFactorDown()));
		}
		upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.Enemy_ReduceMovementSpeedAll);
		if (upgradeData.isActived)
		{
			this.fMoveSpeed *= 1f - upgradeData.GetFactorDown();
		}
		if (this.bIsAirForece)
		{
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.Enemy_ReduceMovementSpeed_Airforce);
			if (upgradeData.isActived)
			{
				this.fMoveSpeed *= 1f - upgradeData.GetFactorDown();
			}
		}
		if (this.bIsInfantry)
		{
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.Enemy_ReduceMovementSpeed_Infantry);
			if (upgradeData.isActived)
			{
				this.fMoveSpeed *= 1f - upgradeData.GetFactorDown();
			}
		}
		if (this.bBoss)
		{
			upgradeData = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.Boss_ReduceMovementSpeed);
			if (upgradeData.isActived)
			{
				this.fMoveSpeed *= 1f - upgradeData.GetFactorDown();
			}
		}
	}
}

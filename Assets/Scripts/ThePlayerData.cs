using System;
using System.Collections.Generic;

[Serializable]
public class ThePlayerData
{
	public TheEnumManager.DIFFICUFT GAME_DIFFICUFT;

	public bool TESTING_MODE;

	public int iGemFormWatchingAds = 30;

	public bool UnlockTower_RocketLaucher;

	public bool UnlockTower_Poison;

	public bool UnlockTower_Thunder;

	public bool UnlockTower_Archer;

	public bool UnlockTower_Mage;

	public bool UnlockTower_Cannonner;

	public bool UnlockTower_Gunner;

	public int iPlayerCoin = 200;

	public int iPlayerGem = 80;

	public int iCurrentLevel;

	public List<int> LIST_STAR_NORMAL = new List<int>();

	public List<int> LIST_STAR_HARD = new List<int>();

	public List<int> LIST_STAR_NIGHTMATE = new List<int>();

	public int iSkillGuardian = 2;

	public int iSkillFreeze = 2;

	public int iSkillBoomFromSky = 2;

	public int iSkillMineOnRoad = 2;

	public int iSkillPoison = 2;

	public List<string> LIST_ID_OF_UPGRADE_SYSTEM = new List<string>();

	public List<bool> LIST_VALUE_OF_UPGRADE_SYSTEM = new List<bool>();

	public int iNumberOfGiftsReceived;

	public int iCurrentYear;

	public int iCurrentDayOfYear;

	public TheEnumManager.LEVEL_DIFFICIFT CURRENT_LEVEL_DIFFICUFT_MODE_NORMAL;

	public TheEnumManager.LEVEL_DIFFICIFT CURRENT_LEVEL_DIFFICUFT_MODE_HARD;

	public TheEnumManager.LEVEL_DIFFICIFT CURRENT_LEVEL_DIFFICUFT_MODE_NIGHTMATE;

	public int iNumberOfTimePlayed_ModeNormal;

	public int iNumberOfTimePlayed_ModeHard;

	public int iNumberOfTimePlayed_ModeNightmate;

	public int iNumberOfWinning_1star_ModeNormal;

	public int iNumberOfWinning_2star_ModeNormal;

	public int iNumberOfWinning_3star_ModeNormal;

	public int iNumberOfWinning_1star_ModeHard;

	public int iNumberOfWinning_2star_ModeHard;

	public int iNumberOfWinning_3star_ModeHard;

	public int iNumberOfWinning_1star_ModeNightmate;

	public int iNumberOfWinning_2star_ModeNightmate;

	public int iNumberOfWinning_3star_ModeNightmate;

	public int iNumberOfDefeat_ModeNormal;

	public int iNumberOfDefeat_ModeHard;

	public int iNumberOfDefeat_ModeNightmate;

	public List<string> LIST_ID_OF_ARCHIVEMENT = new List<string>();

	public List<bool> LIST_VALUE_OF_ARCHIVEMENT = new List<bool>();

	public int iPlayerScore;

	public bool CheckUnlockTower(TheEnumManager.TOWER _tower)
	{
		switch (_tower)
		{
		case TheEnumManager.TOWER.tower_magic:
			return this.UnlockTower_Mage;
		case TheEnumManager.TOWER.tower_archer:
			return this.UnlockTower_Archer;
		case TheEnumManager.TOWER.tower_cannonner:
			return this.UnlockTower_Cannonner;
		case TheEnumManager.TOWER.tower_gunmen:
			return this.UnlockTower_Gunner;
		case TheEnumManager.TOWER.tower_poison:
			return this.UnlockTower_Poison;
		case TheEnumManager.TOWER.tower_rocket_laucher:
			return this.UnlockTower_RocketLaucher;
		case TheEnumManager.TOWER.tower_thunder:
			return this.UnlockTower_Thunder;
		}
		return false;
	}

	public void SetUnlockTower(TheEnumManager.TOWER _tower, bool _unlock)
	{
		switch (_tower)
		{
		case TheEnumManager.TOWER.tower_magic:
			this.UnlockTower_Mage = _unlock;
			break;
		case TheEnumManager.TOWER.tower_archer:
			this.UnlockTower_Archer = _unlock;
			break;
		case TheEnumManager.TOWER.tower_cannonner:
			this.UnlockTower_Cannonner = _unlock;
			break;
		case TheEnumManager.TOWER.tower_gunmen:
			this.UnlockTower_Gunner = _unlock;
			break;
		case TheEnumManager.TOWER.tower_poison:
			this.UnlockTower_Poison = _unlock;
			break;
		case TheEnumManager.TOWER.tower_rocket_laucher:
			this.UnlockTower_RocketLaucher = _unlock;
			break;
		case TheEnumManager.TOWER.tower_thunder:
			this.UnlockTower_Thunder = _unlock;
			break;
		}
	}

	public int GetCurrentLevelOfPlayer()
	{
		return this.LIST_STAR_NORMAL.Count;
	}

	public int GetTotalStar(TheEnumManager.DIFFICUFT _difficuft)
	{
		List<int> list = new List<int>();
		if (_difficuft != TheEnumManager.DIFFICUFT.Normal)
		{
			if (_difficuft != TheEnumManager.DIFFICUFT.Hard)
			{
				if (_difficuft == TheEnumManager.DIFFICUFT.Nightmate)
				{
					list = this.LIST_STAR_NIGHTMATE;
				}
			}
			else
			{
				list = this.LIST_STAR_HARD;
			}
		}
		else
		{
			list = this.LIST_STAR_NORMAL;
		}
		int num = 0;
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			num += list[i];
		}
		return num;
	}

	public void SetStar(int _indexLevel, int _star, TheEnumManager.DIFFICUFT _difficuft)
	{
		if (_difficuft != TheEnumManager.DIFFICUFT.Normal)
		{
			if (_difficuft != TheEnumManager.DIFFICUFT.Hard)
			{
				if (_difficuft == TheEnumManager.DIFFICUFT.Nightmate)
				{
					if (_indexLevel < this.LIST_STAR_NIGHTMATE.Count && this.LIST_STAR_NIGHTMATE[_indexLevel] < _star)
					{
						this.LIST_STAR_NIGHTMATE[_indexLevel] = _star;
					}
				}
			}
			else if (_indexLevel < this.LIST_STAR_HARD.Count && this.LIST_STAR_HARD[_indexLevel] < _star)
			{
				this.LIST_STAR_HARD[_indexLevel] = _star;
			}
		}
		else if (_indexLevel < this.LIST_STAR_NORMAL.Count)
		{
			if (this.LIST_STAR_NORMAL[_indexLevel] < _star)
			{
				this.LIST_STAR_NORMAL[_indexLevel] = _star;
			}
		}
		else
		{
			this.LIST_STAR_NORMAL.Add(_star);
			if (this.LIST_STAR_NORMAL.Count > this.LIST_STAR_HARD.Count)
			{
				this.LIST_STAR_HARD.Add(0);
			}
			if (this.LIST_STAR_NORMAL.Count > this.LIST_STAR_NIGHTMATE.Count)
			{
				this.LIST_STAR_NIGHTMATE.Add(0);
			}
		}
	}

	public int GetStar(int _indexoflevle, TheEnumManager.DIFFICUFT _difficuft)
	{
		if (_difficuft != TheEnumManager.DIFFICUFT.Normal)
		{
			if (_difficuft != TheEnumManager.DIFFICUFT.Hard)
			{
				if (_difficuft != TheEnumManager.DIFFICUFT.Nightmate)
				{
					return -1;
				}
				if (_indexoflevle < this.LIST_STAR_NIGHTMATE.Count)
				{
					return this.LIST_STAR_NIGHTMATE[_indexoflevle];
				}
				return -1;
			}
			else
			{
				if (_indexoflevle < this.LIST_STAR_HARD.Count)
				{
					return this.LIST_STAR_HARD[_indexoflevle];
				}
				return -1;
			}
		}
		else
		{
			if (_indexoflevle < this.LIST_STAR_NORMAL.Count)
			{
				return this.LIST_STAR_NORMAL[_indexoflevle];
			}
			return -1;
		}
	}

	public int GetNumberOfSkill(TheEnumManager.SKILL e)
	{
		switch (e)
		{
		case TheEnumManager.SKILL.skill_guardian:
			return this.iSkillGuardian;
		case TheEnumManager.SKILL.skill_freeze:
			return this.iSkillFreeze;
		case TheEnumManager.SKILL.skill_fire_of_lord:
			return this.iSkillBoomFromSky;
		case TheEnumManager.SKILL.skill_mine_on_road:
			return this.iSkillMineOnRoad;
		case TheEnumManager.SKILL.skill_poison:
			return this.iSkillPoison;
		default:
			return -1;
		}
	}

	public void SetNumberOfSkill(TheEnumManager.SKILL e, int _number)
	{
		switch (e)
		{
		case TheEnumManager.SKILL.skill_guardian:
			this.iSkillGuardian = _number;
			break;
		case TheEnumManager.SKILL.skill_freeze:
			this.iSkillFreeze = _number;
			break;
		case TheEnumManager.SKILL.skill_fire_of_lord:
			this.iSkillBoomFromSky = _number;
			break;
		case TheEnumManager.SKILL.skill_mine_on_road:
			this.iSkillMineOnRoad = _number;
			break;
		case TheEnumManager.SKILL.skill_poison:
			this.iSkillPoison = _number;
			break;
		}
	}

	public void SetActiveOfUpgradeSystem(string _id, bool _value)
	{
		int count = this.LIST_ID_OF_UPGRADE_SYSTEM.Count;
		if (count == 0)
		{
			this.LIST_ID_OF_UPGRADE_SYSTEM.Add(_id);
			this.LIST_VALUE_OF_UPGRADE_SYSTEM.Add(_value);
		}
		else
		{
			for (int i = 0; i < count; i++)
			{
				if (this.LIST_ID_OF_UPGRADE_SYSTEM[i] == _id)
				{
					this.LIST_VALUE_OF_UPGRADE_SYSTEM[i] = _value;
					return;
				}
			}
			this.LIST_ID_OF_UPGRADE_SYSTEM.Add(_id);
			this.LIST_VALUE_OF_UPGRADE_SYSTEM.Add(_value);
		}
	}

	public bool GetActiveOfUpgradeSystem(string _id)
	{
		int count = this.LIST_ID_OF_UPGRADE_SYSTEM.Count;
		if (count == 0)
		{
			return false;
		}
		for (int i = 0; i < count; i++)
		{
			if (this.LIST_ID_OF_UPGRADE_SYSTEM[i] == _id)
			{
				return this.LIST_VALUE_OF_UPGRADE_SYSTEM[i];
			}
		}
		return false;
	}

	public void SetActiveOfArchivement(string _id, bool _value)
	{
		int count = this.LIST_ID_OF_UPGRADE_SYSTEM.Count;
		if (count == 0)
		{
			this.LIST_ID_OF_UPGRADE_SYSTEM.Add(_id);
			this.LIST_VALUE_OF_UPGRADE_SYSTEM.Add(_value);
		}
		else
		{
			for (int i = 0; i < count; i++)
			{
				if (this.LIST_ID_OF_UPGRADE_SYSTEM[i] == _id)
				{
					this.LIST_VALUE_OF_UPGRADE_SYSTEM[i] = _value;
					return;
				}
			}
			this.LIST_ID_OF_UPGRADE_SYSTEM.Add(_id);
			this.LIST_VALUE_OF_UPGRADE_SYSTEM.Add(_value);
		}
	}

	public bool GetActiveOfArchivement(string _id)
	{
		int count = this.LIST_ID_OF_UPGRADE_SYSTEM.Count;
		if (count == 0)
		{
			return false;
		}
		for (int i = 0; i < count; i++)
		{
			if (this.LIST_ID_OF_UPGRADE_SYSTEM[i] == _id)
			{
				return this.LIST_VALUE_OF_UPGRADE_SYSTEM[i];
			}
		}
		return false;
	}

	public void AddPlayerScore(int _score)
	{
		this.iPlayerScore += _score;
	}
}

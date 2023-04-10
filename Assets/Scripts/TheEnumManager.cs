using System;

public class TheEnumManager
{
	public enum KIND_OF_MAPS
	{
		GlassMap,
		RedLand,
		SnowLand
	}

	public enum DIFFICUFT
	{
		Normal,
		Hard,
		Nightmate
	}

	public enum LEVEL_DIFFICIFT
	{
		Level_1,
		Level_2,
		Level_3,
		Level_4,
		Level_5,
		Level_6,
		Level_7,
		Level_8,
		Level_9,
		Level_10
	}

	public enum ITEMS
	{
		Background,
		PointBuilding,
		Tower,
		Hero,
		StartingWaveMark
	}

	public enum ITEMS_POOLING
	{
		Bullet_TowerStone,
		Bullet_TowerArcher,
		Bullet_TowerMagic,
		BowDetail,
		CrackHole,
		BloodOfEnemy,
		Explosion_of_mine,
		FreezeEff,
		Smock,
		FireFromSky,
		CircleLight_Freeze,
		BloodMark,
		CircleLight_Hero,
		Bullet_RocketLaucher,
		Explosion_of_Rocket,
		Bullet_Posion,
		Explosion_Poision,
		EffEnemyText
	}

	public enum SCENE
	{
		Gameplay,
		LevelSelection,
		Menu,
		Upgrade,
		EndGame,
		Encyclopedia
	}

	public enum TOWER
	{
		tower_soldier,
		tower_magic,
		tower_archer,
		tower_cannonner,
		soldier,
		tower_gunmen,
		tower_poison,
		tower_rocket_laucher,
		tower_thunder
	}

	public enum TOWER_LEVEL
	{
		level_1,
		level_2,
		level_3,
		level_4
	}

	public enum ENEMY
	{
		enemy_bad,
		enemy_bandit_with_axe,
		enemy_bandit_with_knife,
		enemy_bandit_with_sword,
		enemy_bandit_with_woodden_mace,
		enemy_bandit_with_woodden_sword,
		enemy_bandit_with_mace,
		enemy_bone,
		enemy_bug,
		enemy_executioner,
		enemy_fat_man,
		enemy_mercenary_green,
		enemy_mercenary_red_hair,
		enemy_red_bull,
		enemy_snail,
		enemy_spide_blue,
		enemy_spide_gray,
		enemy_spide_red,
		enemy_spide_yellow,
		enemy_the_dead,
		enemy_wofl_blue,
		enemy_wofl_gray,
		enemy_wofl_green,
		enemy_wofl_horn_blue,
		enemy_wofl_horn_gray,
		enemy_wofl_horn_green,
		enemy_wofl_soldier_blue,
		enemy_wofl_soldier_gray,
		enemy_wofl_soldier_green,
		enemy_soldier_axe,
		enemy_soldier_sword,
		enemy_flying_wood,
		enemy_airship,
		boss_bad,
		boss_bear,
		boss_bone,
		boss_kingkong,
		boss_sung_bo,
		boss_toc_vang,
		boss_ariship,
		boss_bad_black,
		boss_bandit_with_sword,
		boss_bandit_with_woodden_mace,
		boss_bug,
		boss_flying_wood,
		boss_spide_blue,
		boss_spide_red,
		boss_the_dead,
		boss_bandit_with_mace,
		boss_spide_yellow
	}

	public enum ENEMY_KIND
	{
		All,
		Airforce,
		Infantry
	}

	public enum BOARD_INFO
	{
		GemBoard,
		CoinBoard,
		WaveBoard,
		HeartBoard,
		StarToUpgradeBoard_Normal,
		StarToUpgradeBoard_Hard,
		StarToUpgradeBoard_Nightmate,
		TotalStarBoard_Normal,
		TotalStarBoard_Hard,
		TotalStarBoard_Nightmate
	}

	public enum STAR
	{
		white,
		blue,
		yellow
	}

	public enum KIND_OF_SHOP
	{
		Iap,
		ShopCoin,
		ShopSkill
	}

	public enum ITEM_IN_SHOP
	{
		gem_pack_1,
		gem_pack_2,
		gem_pack_3,
		gem_pack_4,
		gem_pack_5,
		gem_pack_6,
		coin_pack_1,
		coin_pack_2,
		coin_pack_3,
		coin_pack_4,
		coin_pack_5,
		coin_pack_6,
		skill_guardian,
		skill_freeze,
		skill_fire_of_lord,
		skill_mine_on_road,
		skill_poison,
		tower_poison,
		tower_rocket_laucher,
		tower_thunder
	}

	public enum UPGRADE
	{
		Skill_MoreRangeForFireFromSky,
		Skill_MoreRangeForMineOnRoad,
		Skill_MoreTimeForFreeze,
		Reinforcement_2To3Man,
		Reinforcement_MoreTimelife30Percent,
		Reinforcement_MoreTimelife50Percent,
		Reinforcement_MoreHp,
		Enemy_ReduceMovementSpeedAll,
		Enemy_ReduceMovementSpeed_Airforce,
		Enemy_ReduceMovementSpeed_Infantry,
		Enemy_ReduceDamage,
		Boss_ReduceMovementSpeed,
		TowerArcher_MoreDamage,
		TowerArcher_MoreRange,
		TowerArcher_ReducePriceToBuild,
		TowerThunder_MoreDamage,
		TowerThunder_MoreRange,
		TowerThunder_ReducePriceToBuild,
		TowerGunmen_MoreDamage,
		TowerGunmen_MoreRange,
		TowerGunmen_ReducePriceToBuild,
		TowerMagic_MoreDamage,
		TowerMagic_MoreRange,
		TowerMagic_ReducePriceToBuild,
		TowerPoison_MoreDamage,
		TowerPoison_MoreRange,
		TowerPoison_ReducePriceToBuild,
		TowerRocketLaucher_MoreDamage,
		TowerRocketLaucher_MoreRange,
		TowerRocketLaucher_ReducePriceToBuild,
		TowerCannonner_MoreDamage,
		TowerCannonner_MoreRange,
		TowerCannonner_ReducePriceToBuild,
		Other_ReducePriceTowerToBuildForAllTower,
		Other_MoreAttackSpeedForAllTower
	}

	public enum SKILL
	{
		skill_guardian,
		skill_freeze,
		skill_fire_of_lord,
		skill_mine_on_road,
		skill_poison,
		Null
	}

	public static TheEnumManager.SKILL ConverStringToEnum_Skill(string _id)
	{
		int num = Enum.GetNames(typeof(TheEnumManager.SKILL)).Length;
		for (int i = 0; i < num; i++)
		{
			TheEnumManager.SKILL sKILL = (TheEnumManager.SKILL)i;
			if (_id == sKILL.ToString())
			{
				return (TheEnumManager.SKILL)i;
			}
		}
		return TheEnumManager.SKILL.Null;
	}
}

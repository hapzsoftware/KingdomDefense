using System;
using System.Collections.Generic;
using UnityEngine;

public class TheCheckInGiftManager : MonoBehaviour
{
	public enum Gift
	{
		Gift_BoomFromSkySkill_2_1,
		Gift_AddGem_20,
		Gift_FreezeSkill_2_1,
		Gift_AddGem_30,
		Gift_AddBloodSkill_2,
		Gift_AddGem_40,
		Gift_AddGem_50,
		Gift_FreezeSkill_2_2,
		Gift_GuardiaSkill_2_1,
		Gift_AddGem_60,
		Gift_MineOnRoadSkill_2_1,
		Gift_FreezeSkill_2_3,
		Gift_BoomFromSkySkill_2_2,
		Gift_AddGem_70,
		Gift_GuardiaSkill_2_2,
		Gift_FreezeSkill_2_4,
		Gift_MineOnRoadSkill_2_2,
		Gift_AddGem_75,
		Gift_AddBlood_2_1,
		Gift_BoomFromSkySkill_2_3,
		Gift_FreezeSkill_2_5,
		Gift_AddGem_80,
		Gift_AddBlood_2_2,
		Gift_GuardiaSkill_2_3,
		Gift_BoomFromSkySkill_2_4,
		Gift_MineOnRoadSkill_2_3,
		Gift_AddGem_90,
		Gift_AddBlood_2_3,
		Gift_AddBlood_2_4,
		Gift_MineOnRoadSkill_2_4,
		Gift_AddGem_120,
		Gift_AddBlood_2_5,
		Gift_FreezeSkill_2_6,
		Gift_BoomFromSkySkill_2_5,
		Gift_AddGem_200
	}

	public enum KIND_OF_GIFT
	{
		Gifl_Is_Skill,
		Gift_Is_Gem
	}

	[Serializable]
	public struct GIFT_ELE
	{
		public TheCheckInGiftManager.Gift eGift;

		public TheCheckInGiftManager.KIND_OF_GIFT eKindOfGift;

		public TheEnumManager.SKILL eSkill;

		[Space(20f)]
		public Sprite sprIcon;

		public int iValue;

		public bool bReceied;
	}

	public static TheCheckInGiftManager Instance;

	public List<TheCheckInGiftManager.GIFT_ELE> LIST_GIFT;

	private void Awake()
	{
		if (TheCheckInGiftManager.Instance == null)
		{
			TheCheckInGiftManager.Instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Start()
	{
		base.Invoke("Init", 0.2f);
	}

	private void Init()
	{
		int count = this.LIST_GIFT.Count;
		for (int i = 0; i < count; i++)
		{
			TheCheckInGiftManager.GIFT_ELE value = this.LIST_GIFT[i];
			if (i >= TheDataManager.THE_PLAYER_DATA.iNumberOfGiftsReceived)
			{
				break;
			}
			value.bReceied = true;
			this.LIST_GIFT[i] = value;
			UnityEngine.Debug.Log("AAA: " + TheDataManager.THE_PLAYER_DATA.iNumberOfGiftsReceived);
		}
	}

	[ContextMenu("Soft List Gift now")]
	public void SoftListGift()
	{
		int num = Enum.GetNames(typeof(TheCheckInGiftManager.Gift)).Length;
		for (int i = 0; i < num; i++)
		{
			TheCheckInGiftManager.GIFT_ELE value = this.LIST_GIFT[i];
			value.eGift = (TheCheckInGiftManager.Gift)i;
			string text = value.eGift.ToString();
			if (text.Contains("AddGem"))
			{
				value.eKindOfGift = TheCheckInGiftManager.KIND_OF_GIFT.Gift_Is_Gem;
			}
			else
			{
				value.eKindOfGift = TheCheckInGiftManager.KIND_OF_GIFT.Gifl_Is_Skill;
				if (value.eGift.ToString().Contains("BoomFromSkySkill"))
				{
					value.eSkill = TheEnumManager.SKILL.skill_fire_of_lord;
				}
				if (value.eGift.ToString().Contains("FreezeSkill"))
				{
					value.eSkill = TheEnumManager.SKILL.skill_freeze;
				}
				if (value.eGift.ToString().Contains("Poison"))
				{
					value.eSkill = TheEnumManager.SKILL.skill_poison;
				}
				if (value.eGift.ToString().Contains("MineOnRoadSkill"))
				{
					value.eSkill = TheEnumManager.SKILL.skill_mine_on_road;
				}
				if (value.eGift.ToString().Contains("GuardiaSkill"))
				{
					value.eSkill = TheEnumManager.SKILL.skill_guardian;
				}
			}
			this.LIST_GIFT[i] = value;
		}
		UnityEngine.Debug.Log("DONE!");
	}

	public TheCheckInGiftManager.GIFT_ELE GetGift(TheCheckInGiftManager.Gift _Gift)
	{
		return this.LIST_GIFT[(int)_Gift];
	}

	public void ReturnGift(TheCheckInGiftManager.Gift _gift)
	{
		TheCheckInGiftManager.GIFT_ELE gIFT_ELE = this.LIST_GIFT[(int)_gift];
		if (gIFT_ELE.eKindOfGift == TheCheckInGiftManager.KIND_OF_GIFT.Gift_Is_Gem)
		{
			TheDataManager.THE_PLAYER_DATA.iPlayerGem += gIFT_ELE.iValue;
			UnityEngine.Debug.Log("GIFT GEM NOW!");
		}
		else if (gIFT_ELE.eKindOfGift == TheCheckInGiftManager.KIND_OF_GIFT.Gifl_Is_Skill)
		{
			int number = TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(gIFT_ELE.eSkill) + gIFT_ELE.iValue;
			TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(gIFT_ELE.eSkill, number);
			UnityEngine.Debug.Log("GIFT SKILL NOW!");
		}
		TheDataManager.THE_PLAYER_DATA.iNumberOfGiftsReceived++;
		TheDataManager.Instance.SerialzerPlayerData();
		TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
	}

	public int GetGiftValue(TheCheckInGiftManager.Gift _gift)
	{
		return this.LIST_GIFT[(int)_gift].iValue;
	}
}

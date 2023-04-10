using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUpgrade : MonoBehaviour
{
	public TheEnumManager.UPGRADE eUpgrade;

	[HideInInspector]
	public Sprite sprIconImageWhite;

	public Sprite sprIconImageGray;

	public UpgradeData m_UpgradeData;

	private Button buButton;

	private Image imaIconStar;

	private int iPrice;

	private void Awake()
	{
	}

	private void Start()
	{
		this.buButton = base.GetComponent<Button>();
		this.sprIconImageWhite = this.buButton.image.sprite;
		this.buButton.onClick.AddListener(delegate
		{
			this.ThisButton();
		});
		this.m_UpgradeData = TheDataManager.Instance.GetUpgradeData(this.eUpgrade);
		this.buButton.transform.GetChild(0).GetComponent<Text>().text = this.m_UpgradeData.GetPriceStar().ToString();
		this.SetStatus(this.m_UpgradeData.isActived);
		this.buButton.GetComponentInChildren<Text>().text = this.m_UpgradeData.GetPriceStar().ToString();
		if (this.eUpgrade == TheEnumManager.UPGRADE.Skill_MoreRangeForFireFromSky)
		{
			base.Invoke("ThisButton", 0.1f);
		}
		this.buButton.GetComponentInChildren<Text>().text = this.m_UpgradeData.GetPriceStar().ToString();
		this.imaIconStar = base.transform.GetChild(1).GetComponent<Image>();
		TheEnumManager.STAR starKind = this.m_UpgradeData.GetStarKind();
		if (starKind != TheEnumManager.STAR.white)
		{
			if (starKind != TheEnumManager.STAR.blue)
			{
				if (starKind == TheEnumManager.STAR.yellow)
				{
					this.imaIconStar.sprite = MainCode_Upgrade.Instance.sprStar_Nightmate;
				}
			}
			else
			{
				this.imaIconStar.sprite = MainCode_Upgrade.Instance.sprStar_Hard;
			}
		}
		else
		{
			this.imaIconStar.sprite = MainCode_Upgrade.Instance.sprStar_Normal;
		}
	}

	public Button GetButton()
	{
		return this.buButton;
	}

	private void ThisButton()
	{
		MainCode_Upgrade.Instance.SetUpgrade(this);
	}

	public void Upgrade(bool _active)
	{
		this.m_UpgradeData.Active(_active);
		this.SetStatus(_active);
	}

	private void SetStatus(bool _active)
	{
		if (_active)
		{
			this.buButton.image.sprite = this.sprIconImageWhite;
		}
		else
		{
			this.buButton.image.sprite = this.sprIconImageGray;
		}
	}
}

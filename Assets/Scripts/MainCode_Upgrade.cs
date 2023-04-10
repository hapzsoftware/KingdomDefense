using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainCode_Upgrade : MonoBehaviour
{
	public static MainCode_Upgrade Instance;

	public Button buBack;

	public Button buReset;

	public Button buUpgrade;

	private ButtonUpgrade m_ButtonUpgrade;

	public GameObject objMark;

	public GameObject NES;

	public Text txtName;

	public Text txtContent;

	public Image imaPriceStar;

	public Image imaMainIcon;

    public Image StarMissing;

    [Space(20f)]
	public Sprite sprStar_Normal;

	public Sprite sprStar_Hard;

	public Sprite sprStar_Nightmate;


	private static UnityAction __f__am_cache0;

	private void Awake()
	{
        //ThePopupManager.Instance.SetCameraForPopupCanvas(Camera.main);

        if (MainCode_Upgrade.Instance == null)
		{
			MainCode_Upgrade.Instance = this;
		}
	}

	private void Start()
	{
		this.buBack.onClick.AddListener(delegate
		{
			TheUIManager.Instance.LoadScene(TheEnumManager.SCENE.LevelSelection, true);
		});
		this.buReset.onClick.AddListener(delegate
		{
			this.ButtonReset();
		});
		this.buUpgrade.onClick.AddListener(delegate
		{
			this.ButtonUpgrade();
		});
		this.txtName.text = string.Empty;
		this.txtContent.text = string.Empty;
		this.imaPriceStar.GetComponentInChildren<Text>().text = string.Empty;
		this.SetStatus();
		TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
	}

	private void ButtonReset()
	{
		if (this.m_ButtonUpgrade.m_UpgradeData.isActived)
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_upgrade_reset);
			this.m_ButtonUpgrade.Upgrade(false);
			TheDataManager.Instance.ReadFileCSV_TowerConfig();
			this.SetStatus();
			TheDataManager.Instance.SerialzerPlayerData();
			TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
		}
	}

	private void ButtonUpgrade()
	{
		int num = 0;
		TheEnumManager.STAR starKind = this.m_ButtonUpgrade.m_UpgradeData.GetStarKind();
		if (starKind != TheEnumManager.STAR.white)
		{
			if (starKind != TheEnumManager.STAR.blue)
			{
				if (starKind == TheEnumManager.STAR.yellow)
				{
					num = TheDataManager.THE_PLAYER_DATA.GetTotalStar(TheEnumManager.DIFFICUFT.Nightmate) - TheDataManager.Instance.GetTotalStarWasUsed(TheEnumManager.STAR.yellow);
				}
			}
			else
			{
				num = TheDataManager.THE_PLAYER_DATA.GetTotalStar(TheEnumManager.DIFFICUFT.Hard) - TheDataManager.Instance.GetTotalStarWasUsed(TheEnumManager.STAR.blue);
			}
		}
		else
		{
			num = TheDataManager.THE_PLAYER_DATA.GetTotalStar(TheEnumManager.DIFFICUFT.Normal) - TheDataManager.Instance.GetTotalStarWasUsed(TheEnumManager.STAR.white);
		}
		if (num >= this.m_ButtonUpgrade.m_UpgradeData.GetPriceStar())
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_upgrade_upgrade);
			this.m_ButtonUpgrade.m_UpgradeData.Active(true);
			TheDataManager.Instance.SerialzerPlayerData();
			TheDataManager.Instance.ReadFileCSV_TowerConfig();
			this.m_ButtonUpgrade.Upgrade(true);
			this.SetStatus();
			TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
		}

		else
		{
			print("Not Enough Stars");

            StarMissing.sprite = imaPriceStar.sprite;

			NES.SetActive(true);
        }
	}

	private void SetStatus()
	{
		if (!this.m_ButtonUpgrade)
		{
			this.buReset.image.color = Color.white * 0.5f;
			this.buUpgrade.image.color = Color.white * 0.5f;
		}
		else
		{
			if (this.m_ButtonUpgrade.m_UpgradeData.isActived)
			{
				this.buReset.image.color = Color.white * 1f;
				this.buUpgrade.image.color = Color.white * 0.5f;
			}
			else
			{
				this.buReset.image.color = Color.white * 0.5f;
				this.buUpgrade.image.color = Color.white * 1f;
			}
			this.imaMainIcon.sprite = this.m_ButtonUpgrade.GetButton().image.sprite;
		}
	}

	public void SetUpgrade(ButtonUpgrade _buttonUpgrade)
	{
		TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);
		this.m_ButtonUpgrade = _buttonUpgrade;
		this.SetStatus();
		this.objMark.transform.position = _buttonUpgrade.transform.position + new Vector3(0f, 0.05f, 0f);
		this.txtName.text = _buttonUpgrade.m_UpgradeData.strName.ToUpper();
		this.txtContent.text = _buttonUpgrade.m_UpgradeData.strContent;
		this.imaPriceStar.GetComponentInChildren<Text>().text = _buttonUpgrade.m_UpgradeData.GetPriceStar().ToString();
		TheEnumManager.STAR starKind = _buttonUpgrade.m_UpgradeData.GetStarKind();
		if (starKind != TheEnumManager.STAR.white)
		{
			if (starKind != TheEnumManager.STAR.blue)
			{
				if (starKind == TheEnumManager.STAR.yellow)
				{
					this.imaPriceStar.sprite = this.sprStar_Nightmate;
				}
			}
			else
			{
				this.imaPriceStar.sprite = this.sprStar_Hard;
			}
		}
		else
		{
			this.imaPriceStar.sprite = this.sprStar_Normal;
		}
	}
}

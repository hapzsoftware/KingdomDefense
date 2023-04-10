using System;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour
{
	private bool SkillOpen = false;

	public static UIGameplay Instance;

	public Button buFaster;

	public Button buSetting;

	[Space(20f)]
	public Button buShowBoardSkill;

	public GameObject objBoardSkill;

	private Vector3 vBoardSkillPos;

	[Space(20f)]
	public float fCurrentTimeScale;

	public GameObject objLastWave;

	private void Awake()
	{
		if (UIGameplay.Instance == null)
		{
			UIGameplay.Instance = this;
		}
	}

	private void Start()
	{
		//this.GetBoardSkillPos();
		this.buSetting.onClick.AddListener(delegate
		{
			this.ButtonSetting();
		});
		this.buFaster.onClick.AddListener(delegate
		{
			this.ButtonFaster();
		});
		this.buShowBoardSkill.onClick.AddListener(delegate
		{
			this.ButtonCallBoardSkill();
		});
		this.buFaster.image.color = Color.gray;
		this.objLastWave.SetActive(false);
		this.ShowBoardSkill();
		TheEventManager.PostEvent(TheEventManager.EventID.UPDATE_BOARD_INFO);
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyUp(KeyCode.Escape))
		{
			this.ButtonSetting();
		}
	}

	private void ButtonCallBoardSkill()
	{
		if (TheSkillManager.Instance.eCURRENT_SKILL != TheEnumManager.SKILL.Null)
		{
			this.buShowBoardSkill.transform.GetChild(0).GetComponent<Image>().color = Color.white * 0f;
			TheSkillManager.Instance.eCURRENT_SKILL = TheEnumManager.SKILL.Null;
			MainCode_Gameplay.Instance.eCURRENT_INPUT = MainCode_Gameplay.INPUT_PLAYER.Normal;
			this.buShowBoardSkill.image.sprite = TheSkillManager.Instance.sprMainIcon;
		}
		this.ShowBoardSkill();
	}

	public void ShowBoardSkill()
	{
		if (!SkillOpen)
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_back);
            //this.objBoardSkill.transform.position = Vector3.one * 1000f;
            this.objBoardSkill.gameObject.SetActive(false);
	        SkillOpen = true;
        }
		else if(SkillOpen)
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);
            //this.objBoardSkill.transform.position = this.vBoardSkillPos;
            this.objBoardSkill.gameObject.SetActive(true);
            SkillOpen = false;
        }
	}

	//private void GetBoardSkillPos()
	//{
	//	this.vBoardSkillPos = this.objBoardSkill.transform.position;
	//}

	private void ButtonFaster()
	{
		if (Time.timeScale == 1f)
		{
			this.buFaster.image.color = Color.white;
			Time.timeScale = 2f;
		}
		else if (Time.timeScale == 2f)
		{
			this.buFaster.image.color = Color.gray;
			Time.timeScale = 1f;
		}
	}

	private void OpenUiPopup()
	{
		if (TheGameStatusManager.CURRENT_STATUS == TheGameStatusManager.GAME_STATUS.Playing)
		{
			if (Time.timeScale != 0f)
			{
				this.fCurrentTimeScale = Time.timeScale;
			}
			Time.timeScale = 0f;
		}
	}

	private void CloseUIPopup()
	{
		if (TheGameStatusManager.CURRENT_STATUS == TheGameStatusManager.GAME_STATUS.Playing && !ThePopupManager.Instance.IsShowing())
		{
			Time.timeScale = this.fCurrentTimeScale;
		}
	}

	private void ButtonSetting()
	{
		TheUIManager.ShowPopup(ThePopupManager.POP_UP.Setting);
	}

	public void ShowLastWaveText()
	{
		this.objLastWave.SetActive(true);
	}

	private void OnEnable()
	{
		TheEventManager.RegisterEvent(TheEventManager.EventID.OPEN_UI_POPUP, new TheEventManager.ACTION(this.OpenUiPopup));
		TheEventManager.RegisterEvent(TheEventManager.EventID.CLOSE_UI_POPUP, new TheEventManager.ACTION(this.CloseUIPopup));
	}

	private void OnDisable()
	{
		TheEventManager.RemoveEvent(TheEventManager.EventID.OPEN_UI_POPUP, new TheEventManager.ACTION(this.OpenUiPopup));
		TheEventManager.RemoveEvent(TheEventManager.EventID.CLOSE_UI_POPUP, new TheEventManager.ACTION(this.CloseUIPopup));
	}
}

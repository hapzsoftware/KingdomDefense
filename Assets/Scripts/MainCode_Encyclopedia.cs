using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainCode_Encyclopedia : MonoBehaviour
{
	public static MainCode_Encyclopedia Instance;

	public Button buBack;

	public Button buScreenTips;

	public List<Button> LIST_BUTTON_GROUP;

	public List<GameObject> LIST_PANEL_GROUP;

	public Transform tranParent_ButtonTower;

	public Transform tranParent_ButtonEnemy;

	public Transform tranParent_ButtonSkill;

	public List<Button> LIST_IMAGE_BUTTON_TOWERS;

	public List<Button> LIST_IMAGE_BUTTON_ENEMIES;

	public List<Button> LIST_IMAGE_BUTTON_SKILL;

	public Transform GROUP_ENEMY_INFO_DATA;

	[Header("INFO TEXT"), Space(20f)]
	public Text txtDamage;

	public Text txtAttackSpeed;

	public Text txtHP;

	public Text txtRange;

	public Text txtMoveSpeed;

	public Text txtDefense;

	[Space(20f)]
	public Text txtName;

	[Space(20f)]
	public Text txtContent;

	[Space(20f)]
	public Image imgMainIcon;

	public GameObject objBoardInfo;

	[Space(30f)]
	public GameObject objTipsScreen;

	public Text txtTipsContent;

	public Button buNextTips;

	public Button buBackTips;

	public Button buGotIt;

	private int _index;

	private static UnityAction __f__am_cache0;

	private void Awake()
	{
		//ThePopupManager.Instance.SetCameraForPopupCanvas(Camera.main);
		if (MainCode_Encyclopedia.Instance == null)
		{
			MainCode_Encyclopedia.Instance = this;
		}
		this.buBack.onClick.AddListener(delegate
		{
			TheUIManager.Instance.LoadScene(TheEnumManager.SCENE.LevelSelection, true);
		});
		this.LIST_BUTTON_GROUP[0].onClick.AddListener(delegate
		{
			this.ShowPanel(0);
		});
		this.LIST_BUTTON_GROUP[1].onClick.AddListener(delegate
		{
			this.ShowPanel(1);
		});
		this.LIST_BUTTON_GROUP[2].onClick.AddListener(delegate
		{
			this.ShowPanel(2);
		});
		this.LIST_IMAGE_BUTTON_TOWERS = new List<Button>();
		this.LIST_IMAGE_BUTTON_ENEMIES = new List<Button>();
		this.LIST_IMAGE_BUTTON_SKILL = new List<Button>();
		this.AddButtonToList(this.LIST_IMAGE_BUTTON_TOWERS, this.tranParent_ButtonTower);
		this.AddButtonToList(this.LIST_IMAGE_BUTTON_ENEMIES, this.tranParent_ButtonEnemy);
		this.AddButtonToList(this.LIST_IMAGE_BUTTON_SKILL, this.tranParent_ButtonSkill);
	}

	private void Start()
	{
		this.ShowPanel(0);
		this.SetIndexForEnemyInfoData();
		this.buNextTips.onClick.AddListener(delegate
		{
			this.ButtonNextTip();
		});
		this.buBackTips.onClick.AddListener(delegate
		{
			this.ButtonBackTip();
		});
		this.buGotIt.onClick.AddListener(delegate
		{
			this.ShowTipsScreen(false);
		});
		this.buScreenTips.onClick.AddListener(delegate
		{
			this.ShowTipsScreen(true);
		});
	}

	private void AddButtonToList(List<Button> _list, Transform _parent)
	{
		int childCount = _parent.childCount;
		for (int i = 0; i < childCount; i++)
		{
			_list.Add(_parent.GetChild(i).GetComponent<Button>());
		}
	}

	private void ShowPanel(int _index)
	{
		TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);
		int count = this.LIST_PANEL_GROUP.Count;
		for (int i = 0; i < count; i++)
		{
			if (i == _index)
			{
				this.LIST_PANEL_GROUP[i].SetActive(true);
				this.LIST_BUTTON_GROUP[i].image.color = Color.white;
			}
			else
			{
				this.LIST_PANEL_GROUP[i].SetActive(false);
				this.LIST_BUTTON_GROUP[i].image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
			}
		}
		if (_index == 0)
		{
			this.LIST_IMAGE_BUTTON_TOWERS[0].GetComponent<TowerInfo>().ShowTowerInfo();
		}
		else if (_index == 1)
		{
			this.LIST_IMAGE_BUTTON_ENEMIES[0].GetComponent<EnemyInfo>().ShowEnemyInfo();
		}
		else if (_index == 2)
		{
			this.LIST_IMAGE_BUTTON_SKILL[0].GetComponent<SkillInfo>().ShowSkillinfo();
		}
	}

	private void SetIndexForEnemyInfoData()
	{
		int childCount = this.GROUP_ENEMY_INFO_DATA.GetChild(0).childCount;
		for (int i = 0; i < childCount; i++)
		{
			EnemyInfo component = this.GROUP_ENEMY_INFO_DATA.GetChild(0).GetChild(i).GetComponent<EnemyInfo>();
			component.iIndex = i;
			component.Init();
		}
	}

	public void ShowTowerInfo(MyTowerData _mytowerdata, Button _button)
	{
		this.objBoardInfo.SetActive(true);
		this.txtName.text = _mytowerdata.strName.ToUpper().ToString();
		this.txtContent.text = _mytowerdata.strContent.ToString() + " - Unlock at level " + _mytowerdata.iLevelToUnlock;
		this.txtDamage.text = _mytowerdata.GetDamageToShow().ToString();
		this.txtAttackSpeed.text = _mytowerdata.fAttackSpeed.ToString() + " s";
		this.txtRange.text = _mytowerdata.fRange.ToString() + " m";
		this.txtMoveSpeed.text = " ...";
		this.txtDefense.text = "...";
		this.txtHP.text = "...";
		this.ColorButtonWasChoose(_button, this.LIST_IMAGE_BUTTON_TOWERS);
		this.imgMainIcon.sprite = _button.image.sprite;
	}

	private void ColorButtonWasChoose(Button _button, List<Button> _list)
	{
		int count = _list.Count;
		for (int i = 0; i < count; i++)
		{
			if (_button == _list[i])
			{
				UnityEngine.Debug.Log(_button.name);
				_list[i].image.color = Color.white * 1f;
			}
			else
			{
				_list[i].image.color = Color.gray;
			}
		}
	}

	public void ShowEnemyInfo(EnemyData _enemydata, Button _button)
	{
		this.objBoardInfo.SetActive(true);
		this.txtName.text = _enemydata.strName.ToUpper().ToString();
		this.txtContent.text = _enemydata.strContent.ToString();
		this.txtDamage.text = _enemydata.iDamage.ToString();
		this.txtAttackSpeed.text = _enemydata.fShootingSpeed.ToString() + " s";
		this.txtRange.text = _enemydata.fRange.ToString() + " m";
		this.txtMoveSpeed.text = _enemydata.fMoveSpeed.ToString();
		this.txtDefense.text = "...";
		this.txtHP.text = _enemydata.iMaxHp.ToString();
		this.ColorButtonWasChoose(_button, this.LIST_IMAGE_BUTTON_ENEMIES);
	}

	public void ShowSkillInfo(TheEnumManager.SKILL _skill, Sprite _icon, Button _button)
	{
		this.objBoardInfo.SetActive(false);
		this.imgMainIcon.sprite = _icon;
		ShopData shopData = TheDataManager.Instance.GetShopData(_skill.ToString());
		this.txtName.text = shopData.strPackName;
		this.txtContent.text = shopData.strContent;
		this.ColorButtonWasChoose(_button, this.LIST_IMAGE_BUTTON_SKILL);
		this.txtDamage.text = "...";
		this.txtAttackSpeed.text = "...";
		this.txtHP.text = "...";
		this.txtRange.text = "...";
		this.txtMoveSpeed.text = "...";
		this.txtDefense.text = "...";
	}

	private void ShowTipsScreen(bool _active)
	{
		if (_active)
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);
		}
		else
		{
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_back);
		}
		this.objTipsScreen.SetActive(_active);
		this._index = -1;
		this.ButtonNextTip();
	}

	private void ButtonNextTip()
	{
		TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);
		this._index++;
		if (this._index == TheDataManager.Instance.LIST_TIPS.Count)
		{
			this._index = 0;
		}
		this.txtTipsContent.text = "TIP: " + TheDataManager.Instance.GetTips(this._index);
	}

	private void ButtonBackTip()
	{
		TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_back);
		this._index--;
		if (this._index == -1)
		{
			this._index = TheDataManager.Instance.LIST_TIPS.Count - 1;
		}
		this.txtTipsContent.text = "TIP: " + TheDataManager.Instance.GetTips(this._index);
	}
}

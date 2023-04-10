using System;
using UnityEngine;

public class MainCode_Gameplay : MonoBehaviour
{
	public enum INPUT_PLAYER
	{
		Normal,
		SHOW_UI,
		ReadyToUseSkill,
		ClickHero
	}

	public MainCode_Gameplay.INPUT_PLAYER eCURRENT_INPUT;

	public static MainCode_Gameplay Instance;

	[Space(20f)]
	public GameObject m_objPreBuildingPos;

	public GameObject m_BoardMark;

	public BoardInfoTower m_boardInfoTower;

	public GameObject m_objPosIcon;

	private RaycastHit2D _hit;

	private TheItems _currentObj;

	private Vector2 vInputMouse;

	public Tower TOWER_IS_SELECTED;

	private void Awake()
	{
		//ThePopupManager.Instance.SetCameraForPopupCanvas(Camera.main);
		if (MainCode_Gameplay.Instance == null)
		{
			MainCode_Gameplay.Instance = this;
		}
		TheGameStatusManager.SetGameStatus(TheGameStatusManager.GAME_STATUS.Loading);
		this.LoadLevel(TheDataManager.THE_PLAYER_DATA.iCurrentLevel);
		this.m_BoardMark.transform.position = Vector2.one * 1000f;
	}

	private void Start()
	{
		UnityEngine.Debug.Log("LEVEL_" + TheDataManager.THE_PLAYER_DATA.iCurrentLevel);
		TheMusic.Instance.Play();
		if (TheDataManager.THE_PLAYER_DATA.iCurrentLevel == 0 && TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT == TheEnumManager.DIFFICUFT.Normal)
		{
			base.Invoke("ShowTutorial", 1.2f);
		}
	}

	private void Update()
	{
		this.InputPlayer();
	}

	public void ShowTutorial()
	{
		TheUIManager.ShowPopup(ThePopupManager.POP_UP.Tutorial);
	}

	private void LoadLevel(int _index)
	{
		GameObject gameObject = new GameObject();
		if (ThePlatformManager.eMODE == ThePlatformManager.MODE.Testting)
		{
			gameObject = Resources.Load<GameObject>("Levels/LEVEL_" + ThePlatformManager.iLevelTesting);
		}
		else
		{
			gameObject = Resources.Load<GameObject>("Levels/LEVEL_" + (TheDataManager.THE_PLAYER_DATA.iCurrentLevel + 1));
		}
		if (!gameObject)
		{
			gameObject = Resources.Load<GameObject>("Levels/LEVEL_1");
		}
		UnityEngine.Object.Instantiate<GameObject>(gameObject);
	}

	public void InputPlayer()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (ThePopupManager.Instance.IsShowing())
			{
				return;
			}
			this.vInputMouse = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
			this._hit = Physics2D.Raycast(this.vInputMouse, Vector2.zero);
			if (this._hit.collider)
			{
				this._currentObj = this._hit.collider.GetComponent<TheItems>();
				if (this._currentObj != null)
				{
					UnityEngine.Debug.Log(this._currentObj.m_items.ToString());
					this.ProcessItem(this._currentObj);
				}
			}
		}
	}

	public void ProcessItem(TheItems _theItem)
	{
		if (this.eCURRENT_INPUT == MainCode_Gameplay.INPUT_PLAYER.ReadyToUseSkill)
		{
			TheSkillManager.Instance.GetSkill(this.vInputMouse);
			return;
		}
		switch (_theItem.m_items)
		{
		case TheEnumManager.ITEMS.Background:
			TheButtonTower.Instance.ShowBoard(TheButtonTower.Board.HideAll);
			this.m_boardInfoTower.Hide();
			break;
		case TheEnumManager.ITEMS.PointBuilding:
			this.m_BoardMark.transform.position = _theItem.transform.position + new Vector3(0f, 1f, 0f);
			TheButtonTower.Instance.SetTowerPosToBuild(_theItem.gameObject);
			TheButtonTower.Instance.ShowBoard(TheButtonTower.Board.BuyTower);
			this.m_boardInfoTower.Hide();
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.touch_tower_pos);
			break;
		case TheEnumManager.ITEMS.Tower:
			this.m_BoardMark.transform.position = _theItem.transform.position + new Vector3(0f, 1.5f, 0f);
			this.TOWER_IS_SELECTED = _theItem.GetComponent<Tower>();
			this.TOWER_IS_SELECTED.ShowCircle();
			TheButtonTower.Instance.SetSelectedTower(this.TOWER_IS_SELECTED);
			TheButtonTower.Instance.ShowBoard(TheButtonTower.Board.UpgradeOfSell);
			this.m_boardInfoTower.ShowContent(this.TOWER_IS_SELECTED.eTower, this.TOWER_IS_SELECTED.m_myTowerData);
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.touch_tower_pos);
			break;
		case TheEnumManager.ITEMS.Hero:
			this.eCURRENT_INPUT = MainCode_Gameplay.INPUT_PLAYER.ClickHero;
			this.TOWER_IS_SELECTED = _theItem.GetComponent<Tower>();
			break;
		}
	}

	public bool IsSafetyRange(Vector2 _target)
	{
		return Vector2.Distance(_target, Vector2.zero) <= 8f;
	}

	public void SetInput(MainCode_Gameplay.INPUT_PLAYER eInput)
	{
		this.eCURRENT_INPUT = eInput;
		MainCode_Gameplay.INPUT_PLAYER iNPUT_PLAYER = this.eCURRENT_INPUT;
		if (iNPUT_PLAYER != MainCode_Gameplay.INPUT_PLAYER.Normal)
		{
			if (iNPUT_PLAYER != MainCode_Gameplay.INPUT_PLAYER.ReadyToUseSkill)
			{
			}
		}
	}

	private void CheckWin(Enemy _enemy)
	{
		base.Invoke("CheckWin", 0.2f);
	}

	private void CheckWin()
	{
		if (TheLevel.Instance.iCurrentWave <= TheLevel.Instance.iMAX_WAVE_CONFIG)
		{
			return;
		}
		if (TheLevel.Instance.iTotalNumberEnemyOfWave == 0 && TheLevel.Instance.iCurrentHeart > 0)
		{
			TheGameStatusManager.SetGameStatus(TheGameStatusManager.GAME_STATUS.Victory);
		}
	}

	private void CheckGameOver(Enemy _enemy)
	{
		if (TheLevel.Instance.iCurrentHeart == 0)
		{
			TheGameStatusManager.SetGameStatus(TheGameStatusManager.GAME_STATUS.Gameover);
		}
	}

	private void OnEnable()
	{
		TheEventManager.RegisterGetEnemyEvent(TheEventManager.EnemyEventID.OnEnemyIsDetroyedOnRoad, new TheEventManager.ACTION_ENEMY(this.CheckWin));
		TheEventManager.RegisterGetEnemyEvent(TheEventManager.EnemyEventID.OnEnemyCompletedRoad, new TheEventManager.ACTION_ENEMY(this.CheckWin));
		TheEventManager.RegisterGetEnemyEvent(TheEventManager.EnemyEventID.OnEnemyCompletedRoad, new TheEventManager.ACTION_ENEMY(this.CheckGameOver));
	}

	private void OnDisable()
	{
		TheDataManager.Instance.SerialzerPlayerData();
		TheEventManager.RemoveEnemyEvent(TheEventManager.EnemyEventID.OnEnemyIsDetroyedOnRoad, new TheEventManager.ACTION_ENEMY(this.CheckWin));
		TheEventManager.RemoveEnemyEvent(TheEventManager.EnemyEventID.OnEnemyCompletedRoad, new TheEventManager.ACTION_ENEMY(this.CheckWin));
		TheEventManager.RemoveEnemyEvent(TheEventManager.EnemyEventID.OnEnemyCompletedRoad, new TheEventManager.ACTION_ENEMY(this.CheckGameOver));
	}
}

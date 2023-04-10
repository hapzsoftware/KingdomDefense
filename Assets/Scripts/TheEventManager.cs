using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class TheEventManager
{
	public enum EnemyEventID
	{
		OnEnemyIsDetroyedOnRoad,
		OnEnemyCompletedRoad,
		OnEnemyIsBorn,
		OnEnemyHitRocket
	}

	public delegate void ACTION_ENEMY(Enemy _enemy);

	public delegate void EventOfWeapon(Vector2 _pos, int _damage);

	public enum EventID
	{
		START_WAVE,
		UPDATE_BOARD_INFO,
		UPDATE_SKILL_BOARD,
		OPEN_UI_POPUP,
		CLOSE_UI_POPUP,
		BUY_HEROES,
		ROCKET_EXPLOSION,
		UNLOCK_TOWER,
		TOUCH_WAVE_MARK
	}

	public delegate void ACTION();

	public delegate void EVENT_OF_GAMESTATUS(int _star);

	private static TheEventManager.ACTION_ENEMY OnEnemyDestroyOnPath;

	private static TheEventManager.ACTION_ENEMY OnEnemyCompletePath;

	private static TheEventManager.ACTION_ENEMY OnEnemyIsBorn;



	private static TheEventManager.ACTION OnStartWave;

	private static TheEventManager.ACTION OnUpdateBoardInfo;

	private static TheEventManager.ACTION OnUpdateSkillBoard;

	private static TheEventManager.ACTION OnOpenUIPopup;

	private static TheEventManager.ACTION OnCloseUIPopup;

	private static TheEventManager.ACTION OnBuyHeroes;

	private static TheEventManager.ACTION OnUnlockTower;

	private static TheEventManager.ACTION OnTouchWaveMark;







//private static event TheEventManager.ACTION_ENEMY OnEnemyDestroyOnPath;

//private static event TheEventManager.ACTION_ENEMY OnEnemyCompletePath;

//	private static event TheEventManager.ACTION_ENEMY OnEnemyIsBorn;

	public static event TheEventManager.EventOfWeapon OnRocketHit;

//private static event TheEventManager.ACTION OnStartWave;

//private static event TheEventManager.ACTION OnUpdateBoardInfo;

	//private static event TheEventManager.ACTION OnUpdateSkillBoard;

//private static event TheEventManager.ACTION OnOpenUIPopup;

//private static event TheEventManager.ACTION OnCloseUIPopup;

	//private static event TheEventManager.ACTION OnBuyHeroes;

//private static event TheEventManager.ACTION OnUnlockTower;

	//private static event TheEventManager.ACTION OnTouchWaveMark;
	

	public static event TheEventManager.EVENT_OF_GAMESTATUS OnGameWinning;

	public static event TheEventManager.EVENT_OF_GAMESTATUS OnGameDefeat;

	public static event TheEventManager.EVENT_OF_GAMESTATUS OnGameStart;

	public static void RegisterGetEnemyEvent(TheEventManager.EnemyEventID _event, TheEventManager.ACTION_ENEMY callback)
	{
		if (_event != TheEventManager.EnemyEventID.OnEnemyIsDetroyedOnRoad)
		{
			if (_event != TheEventManager.EnemyEventID.OnEnemyCompletedRoad)
			{
				if (_event == TheEventManager.EnemyEventID.OnEnemyIsBorn)
				{
					TheEventManager.OnEnemyIsBorn += callback;
				}
			}
			else
			{
				TheEventManager.OnEnemyCompletePath += callback;
			}
		}
		else
		{
			TheEventManager.OnEnemyDestroyOnPath += callback;
		}
	}

	public static void RemoveEnemyEvent(TheEventManager.EnemyEventID _event, TheEventManager.ACTION_ENEMY callback)
	{
		if (_event != TheEventManager.EnemyEventID.OnEnemyIsDetroyedOnRoad)
		{
			if (_event != TheEventManager.EnemyEventID.OnEnemyCompletedRoad)
			{
				if (_event == TheEventManager.EnemyEventID.OnEnemyIsBorn)
				{
					TheEventManager.OnEnemyIsBorn -= callback;
				}
			}
			else
			{
				TheEventManager.OnEnemyCompletePath -= callback;
			}
		}
		else
		{
			TheEventManager.OnEnemyDestroyOnPath -= callback;
		}
	}

	public static void PostEnemyEvent(Enemy _enemy, TheEventManager.EnemyEventID _event)
	{
		if (_event != TheEventManager.EnemyEventID.OnEnemyIsDetroyedOnRoad)
		{
			if (_event != TheEventManager.EnemyEventID.OnEnemyCompletedRoad)
			{
				if (_event == TheEventManager.EnemyEventID.OnEnemyIsBorn)
				{
					if (TheEventManager.OnEnemyIsBorn != null)
					{
						TheEventManager.OnEnemyIsBorn(_enemy);
					}
				}
			}
			else if (TheEventManager.OnEnemyCompletePath != null)
			{
				TheEventManager.OnEnemyCompletePath(_enemy);
			}
		}
		else if (TheEventManager.OnEnemyDestroyOnPath != null)
		{
			TheEventManager.OnEnemyDestroyOnPath(_enemy);
		}
	}

	public static void PostEvent_RocketHit(Vector2 _pos, int _damage)
	{
		if (TheEventManager.OnRocketHit != null)
		{
			TheEventManager.OnRocketHit(_pos, _damage);
		}
	}

	public static void RegisterEvent(TheEventManager.EventID _id, TheEventManager.ACTION callback)
	{
		switch (_id)
		{
		case TheEventManager.EventID.START_WAVE:
			TheEventManager.OnStartWave += callback;
			break;
		case TheEventManager.EventID.UPDATE_BOARD_INFO:
			TheEventManager.OnUpdateBoardInfo += callback;
			break;
		case TheEventManager.EventID.UPDATE_SKILL_BOARD:
			TheEventManager.OnUpdateSkillBoard += callback;
			break;
		case TheEventManager.EventID.OPEN_UI_POPUP:
			TheEventManager.OnOpenUIPopup += callback;
			break;
		case TheEventManager.EventID.CLOSE_UI_POPUP:
			TheEventManager.OnCloseUIPopup += callback;
			break;
		case TheEventManager.EventID.BUY_HEROES:
			TheEventManager.OnBuyHeroes += callback;
			break;
		case TheEventManager.EventID.UNLOCK_TOWER:
			TheEventManager.OnUnlockTower += callback;
			break;
		case TheEventManager.EventID.TOUCH_WAVE_MARK:
			TheEventManager.OnTouchWaveMark += callback;
			break;
		}
	}

	public static void RemoveEvent(TheEventManager.EventID _id, TheEventManager.ACTION callback)
	{
		switch (_id)
		{
		case TheEventManager.EventID.START_WAVE:
			TheEventManager.OnStartWave -= callback;
			break;
		case TheEventManager.EventID.UPDATE_BOARD_INFO:
			TheEventManager.OnUpdateBoardInfo -= callback;
			break;
		case TheEventManager.EventID.UPDATE_SKILL_BOARD:
			TheEventManager.OnUpdateSkillBoard -= callback;
			break;
		case TheEventManager.EventID.OPEN_UI_POPUP:
			TheEventManager.OnOpenUIPopup -= callback;
			break;
		case TheEventManager.EventID.CLOSE_UI_POPUP:
			TheEventManager.OnCloseUIPopup -= callback;
			break;
		case TheEventManager.EventID.BUY_HEROES:
			TheEventManager.OnBuyHeroes -= callback;
			break;
		case TheEventManager.EventID.UNLOCK_TOWER:
			TheEventManager.OnUnlockTower -= callback;
			break;
		case TheEventManager.EventID.TOUCH_WAVE_MARK:
			TheEventManager.OnTouchWaveMark += callback;
			break;
		}
	}

	public static void PostEvent(TheEventManager.EventID _event)
	{
		switch (_event)
		{
		case TheEventManager.EventID.START_WAVE:
			if (TheEventManager.OnStartWave != null)
			{
				TheEventManager.OnStartWave();
			}
			break;
		case TheEventManager.EventID.UPDATE_BOARD_INFO:
			if (TheEventManager.OnUpdateBoardInfo != null)
			{
				TheEventManager.OnUpdateBoardInfo();
			}
			break;
		case TheEventManager.EventID.UPDATE_SKILL_BOARD:
			if (TheEventManager.OnUpdateSkillBoard != null)
			{
				TheEventManager.OnUpdateSkillBoard();
			}
			break;
		case TheEventManager.EventID.OPEN_UI_POPUP:
			if (TheEventManager.OnOpenUIPopup != null)
			{
				TheEventManager.OnOpenUIPopup();
			}
			break;
		case TheEventManager.EventID.CLOSE_UI_POPUP:
			if (TheEventManager.OnCloseUIPopup != null)
			{
				TheEventManager.OnCloseUIPopup();
			}
			break;
		case TheEventManager.EventID.BUY_HEROES:
			if (TheEventManager.OnBuyHeroes != null)
			{
				TheEventManager.OnBuyHeroes();
			}
			break;
		case TheEventManager.EventID.UNLOCK_TOWER:
			if (TheEventManager.OnUnlockTower != null)
			{
				TheEventManager.OnUnlockTower();
			}
			break;
		case TheEventManager.EventID.TOUCH_WAVE_MARK:
			if (TheEventManager.OnTouchWaveMark != null)
			{
				TheEventManager.OnTouchWaveMark();
			}
			break;
		}
	}

	public static void EventGameWinning(int _star)
	{
		if (TheEventManager.OnGameWinning != null)
		{
			TheEventManager.OnGameWinning(_star);
		}
	}

	public static void EventGameDefeat(int _star = 0)
	{
		if (TheEventManager.OnGameDefeat != null)
		{
			TheEventManager.OnGameDefeat(_star);
		}
	}

	public static void EventGameStart(int _star = 0)
	{
		if (TheEventManager.OnGameStart != null)
		{
			TheEventManager.OnGameStart(_star);
		}
	}
}

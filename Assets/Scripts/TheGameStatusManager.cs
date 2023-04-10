using System;

public class TheGameStatusManager
{
	public enum GAME_STATUS
	{
		Loading,
		Playing,
		Pausing,
		Victory,
		Gameover
	}

	public static TheGameStatusManager.GAME_STATUS CURRENT_STATUS;

	public static void SetGameStatus(TheGameStatusManager.GAME_STATUS eGameStatus)
	{
		switch (eGameStatus)
		{
		case TheGameStatusManager.GAME_STATUS.Victory:
			if (TheGameStatusManager.CURRENT_STATUS == TheGameStatusManager.GAME_STATUS.Playing)
			{
				TheUIManager.Instance.VictoryPopup();
			}
			break;
		case TheGameStatusManager.GAME_STATUS.Gameover:
			if (TheGameStatusManager.CURRENT_STATUS == TheGameStatusManager.GAME_STATUS.Playing)
			{
				TheUIManager.Instance.DefeatPopup();
			}
			break;
		}
		TheGameStatusManager.CURRENT_STATUS = eGameStatus;
	}

	public static bool CurrentStatus(TheGameStatusManager.GAME_STATUS eGameStatus)
	{
		return TheGameStatusManager.CURRENT_STATUS == eGameStatus;
	}
}

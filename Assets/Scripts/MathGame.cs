using System;
using UnityEngine;

public class MathGame
{
	public static int GetHpFollowTheLevel(int _level)
	{
		return _level * 2;
	}

	public static int GetHpFollowTheWave(int _wave, int _maxWave, int _BaseHP)
	{
		int num = Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow((float)_wave, 4f) / 3f / (float)(_maxWave + 1 - _wave)));
		return _BaseHP + num;
	}
}

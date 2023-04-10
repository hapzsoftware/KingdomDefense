using System;
using UnityEngine;

public class Bezier
{
	private static Vector2 vBezier;

	private static Vector2 vHigh;

	public static Vector2 GetBezier(Vector2 _from, Vector2 _to, float _time, float _high)
	{
		Bezier.vHigh = (_from + _to) / 2f + new Vector2(0f, _high);
		Bezier.vBezier = (1f - _time) * (1f - _time) * _from + 2f * (1f - _time) * _time * Bezier.vHigh + _time * _time * _to;
		return Bezier.vBezier;
	}
}

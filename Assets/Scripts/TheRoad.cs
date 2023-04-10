using System;
using System.Collections.Generic;
using UnityEngine;

public class TheRoad : MonoBehaviour
{
	public List<Vector2> LIST_POS;

	public int iTotalPos;

	private void Start()
	{
		this.iTotalPos = base.transform.childCount;
		for (int i = 0; i < this.iTotalPos; i++)
		{
			this.LIST_POS.Add(base.transform.GetChild(i).position);
		}
	}

	public Vector2 GetPos(int _index)
	{
		if (_index >= this.iTotalPos)
		{
			_index = this.iTotalPos - 1;
		}
		if (_index < 0)
		{
			_index = 0;
		}
		Vector2 result = this.LIST_POS[_index];
		result.y += UnityEngine.Random.Range(-1f, 1f);
		result.x += UnityEngine.Random.Range(-0.4f, 0.4f);
		return result;
	}

	private void OnDrawGizmos()
	{
		int childCount = base.transform.childCount;
		for (int i = 1; i < childCount; i++)
		{
			UnityEngine.Debug.DrawLine(base.transform.GetChild(i).position, base.transform.GetChild(i - 1).position, Color.blue);
		}
	}
}

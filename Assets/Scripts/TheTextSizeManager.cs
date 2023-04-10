using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheTextSizeManager : MonoBehaviour
{
	[Serializable]
	public struct TEXT_SIZE
	{
		public Text txtText;

		public int iFontSize;

		public void Init()
		{
			this.txtText.fontSize = Screen.width / this.iFontSize;
		}
	}

	public List<TheTextSizeManager.TEXT_SIZE> LIST_TEXT;

	private void Start()
	{
		int count = this.LIST_TEXT.Count;
		for (int i = 0; i < count; i++)
		{
			this.LIST_TEXT[i].Init();
		}
	}
}

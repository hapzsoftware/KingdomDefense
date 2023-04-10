using System;
using UnityEngine;
using UnityEngine.UI;

public class Gameover : MonoBehaviour
{
	public Button buReplay;

	public Button buNext;

	private void Start()
	{
		this.buReplay.onClick.AddListener(delegate
		{
			this.ButtonReplay();
		});
		this.buNext.onClick.AddListener(delegate
		{
			this.ButtonNext();
		});
	}

	private void OnEnable()
	{
		TheEventManager.EventGameDefeat(0);
	}

	private void ButtonReplay()
	{
		TheAdsManager.Instance.ShowFullAds();
		TheUIManager.Instance.LoadScene(TheEnumManager.SCENE.Gameplay, false);
	}

	private void ButtonNext()
	{
		TheAdsManager.Instance.ShowFullAds();
		TheUIManager.Instance.LoadScene(TheEnumManager.SCENE.LevelSelection, false);
	}
}

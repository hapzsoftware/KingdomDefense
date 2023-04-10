using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainCode_EndGame : MonoBehaviour
{
	public Button buRateUs;

	public Button buMoreGame;

	public Button buMenu;

	private static UnityAction __f__am_cache0;

	private static UnityAction __f__am_cache1;

	private static UnityAction __f__am_cache2;

	private void Start()
	{
		TheMusic.Instance.Play();
		this.buMenu.onClick.AddListener(delegate
		{
			TheUIManager.Instance.LoadScene(TheEnumManager.SCENE.Menu, false);
		});
		this.buRateUs.onClick.AddListener(delegate
		{
			TheUIManager.LoadLink(ThePlatformManager.MAIN_PLATFORM.LinkLikeMe);
		});
		this.buMoreGame.onClick.AddListener(delegate
		{
			TheUIManager.LoadLink(ThePlatformManager.MAIN_PLATFORM.LinkMoreGame);
		});
	}
}

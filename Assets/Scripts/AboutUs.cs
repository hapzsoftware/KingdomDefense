using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AboutUs : MonoBehaviour
{
	public Button buBack;

	public Button buResetGame;

	public Button buMoreGame;

	private static UnityAction __f__am_cache0;

	private static UnityAction __f__am_cache1;

	private static UnityAction __f__am_cache2;

	private void Start()
	{
		this.buBack.onClick.AddListener(delegate
		{
			ThePopupManager.Instance.Hide(ThePopupManager.POP_UP.AboutUs);
		});
		this.buResetGame.onClick.AddListener(delegate
		{
			Note.ShowPopupNote(Note.NOTE.ResetGame);
		});
		this.buMoreGame.onClick.AddListener(delegate
		{
			TheUIManager.LoadLink(ThePlatformManager.MAIN_PLATFORM.LinkMoreGame);
		});
	}
}

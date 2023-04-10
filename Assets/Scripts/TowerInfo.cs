using System;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfo : MonoBehaviour
{
	private Button buThisButton;

	public TheEnumManager.TOWER eTower;

	public TheEnumManager.TOWER_LEVEL eTowerLevel;

	private void Awake()
	{
		this.buThisButton = base.GetComponent<Button>();
		this.buThisButton.onClick.AddListener(delegate
		{
			this.ShowTowerInfo();
		});
	}

	public void ShowTowerInfo()
	{
		TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);
		MainCode_Encyclopedia.Instance.ShowTowerInfo(TheDataManager.Instance.GetTowerData(this.eTower, this.eTowerLevel), this.buThisButton);
	}
}

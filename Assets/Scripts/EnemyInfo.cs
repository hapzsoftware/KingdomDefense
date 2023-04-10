using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfo : MonoBehaviour
{
	public int iIndex;

	private Button buThisButton;

	private void Awake()
	{
		this.buThisButton = base.GetComponent<Button>();
		this.buThisButton.onClick.AddListener(delegate
		{
			this.ShowEnemyInfo();
		});
	}

	public void Init()
	{
		int num = Enum.GetNames(typeof(TheEnumManager.ENEMY)).Length;
		if (this.iIndex >= num)
		{
			base.gameObject.SetActive(false);
		}
	}

	public void ShowEnemyInfo()
	{
		TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);
		MainCode_Encyclopedia arg_34_0 = MainCode_Encyclopedia.Instance;
		TheDataManager arg_29_0 = TheDataManager.Instance;
		TheEnumManager.ENEMY eNEMY = (TheEnumManager.ENEMY)this.iIndex;
		arg_34_0.ShowEnemyInfo(arg_29_0.GetEnemyData(eNEMY.ToString()), this.buThisButton);
	}
}

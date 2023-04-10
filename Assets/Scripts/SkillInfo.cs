using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillInfo : MonoBehaviour
{
	private Button buThisButton;

	private Sprite sprIcon;

	public TheEnumManager.SKILL eSkill;

	private void Awake()
	{
		this.buThisButton = base.GetComponent<Button>();
		this.buThisButton.onClick.AddListener(delegate
		{
			this.ShowSkillinfo();
		});
		this.sprIcon = this.buThisButton.image.sprite;
	}

	public void ShowSkillinfo()
	{
		TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);
		MainCode_Encyclopedia.Instance.ShowSkillInfo(this.eSkill, this.sprIcon, this.buThisButton);
	}
}

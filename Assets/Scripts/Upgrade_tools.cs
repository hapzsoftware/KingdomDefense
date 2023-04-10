using System;
using UnityEngine;

public class Upgrade_tools : MonoBehaviour
{
	public Transform GROUP_UPGRADE_BUTTON;

	[ContextMenu("Config content for upgrade buttons")]
	public void AutoSetContentForUpgradeButton()
	{
		int childCount = this.GROUP_UPGRADE_BUTTON.childCount;
		for (int i = 0; i < childCount; i++)
		{
			this.GROUP_UPGRADE_BUTTON.GetChild(i).GetComponent<ButtonUpgrade>().eUpgrade = (TheEnumManager.UPGRADE)i;
			TheEnumManager.UPGRADE uPGRADE = (TheEnumManager.UPGRADE)i;
			string text = uPGRADE.ToString();
			this.GROUP_UPGRADE_BUTTON.GetChild(i).name = text.ToLower();
		}
		UnityEngine.Debug.Log("DONE");
	}
}

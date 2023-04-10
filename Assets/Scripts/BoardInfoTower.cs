using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardInfoTower : MonoBehaviour
{
	public Image iImageIconTower;

	public Text txtRange;

	public Text txtSpeedAttack;

	public Text txtDamage;

	public Text txtTowerName;

	public List<Image> LIST_STAR;

	public void ShowContent(TheEnumManager.TOWER _eTower, MyTowerData _towerdata)
	{
		this.iImageIconTower.sprite = TheDataManager.Instance.GetIconTower(_eTower).sprIcon;
		this.txtTowerName.text = _towerdata.strName;
		this.txtRange.text = _towerdata.fRange.ToString() + " m";
		this.txtSpeedAttack.text = _towerdata.fAttackSpeed.ToString() + " s";
		this.txtDamage.text = _towerdata.GetDamageToShow().ToString();
		this.ShowLevelStar((int)_towerdata.eTowerLevel);
		base.gameObject.SetActive(true);
	}

	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	private void ShowLevelStar(int _level)
	{
		int count = this.LIST_STAR.Count;
		for (int i = 0; i < count; i++)
		{
			if (i <= _level)
			{
				this.LIST_STAR[i].color = Color.white;
			}
			else
			{
				this.LIST_STAR[i].color = Color.gray * 0.5f;
			}
		}
	}
}

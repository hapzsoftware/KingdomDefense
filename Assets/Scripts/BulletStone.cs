using System;
using UnityEngine;

public class BulletStone : BulletBezier
{
	private bool bAllowShowCrackHole;

	private void OnEnable()
	{
		base.Invoke("AllowShowCrackHole", 1f);
	}

	private void AllowShowCrackHole()
	{
		this.bAllowShowCrackHole = true;
	}

	private void OnDisable()
	{
		if (this.bAllowShowCrackHole)
		{
			this.HitEnemy();
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.stone_crack);
			GameObject item = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.CrackHole).GetItem();
			if (item)
			{
				item.transform.position = this.m_tranform.position;
				item.SetActive(true);
			}
		}
	}
}

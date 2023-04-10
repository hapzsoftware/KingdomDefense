using System;
using UnityEngine;

public class Supporter : MonoBehaviour
{
	public SUPPORTER eSupporter;

	private float fTimelife = 30f;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<Enemy>())
		{
			if (this.eSupporter == SUPPORTER.MineOnTheRoad)
			{
				TheSkillManager.CallUserSkill_MineOnRoad(base.transform.position);
				GameObject item = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Explosion_of_mine).GetItem();
				if (item)
				{
					item.transform.position = base.transform.position;
					item.SetActive(true);
				}
				TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.explosion_mine_on_road);
				this.DestroyThis();
			}
			else if (this.eSupporter == SUPPORTER.PondOfWater)
			{
				other.GetComponent<Enemy>().HitPondOfPoison();
			}
		}
	}

	private void OnEnable()
	{
		SUPPORTER sUPPORTER = this.eSupporter;
		if (sUPPORTER != SUPPORTER.PondOfWater)
		{
			if (sUPPORTER != SUPPORTER.MineOnTheRoad)
			{
			}
		}
		else
		{
			this.fTimelife = 10f;
			base.Invoke("DestroyThis", this.fTimelife);
		}
	}

	private void DestroyThis()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}
}

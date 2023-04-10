using System;
using UnityEngine;

public class BulletArcher : BulletBezier
{
	private bool bAllowShowDetail;

	private float xDis;

	private float yDis;

	private float alphal;

	private Quaternion _qua;

	public bool bIsArcher;

	public bool bIsRocket;

	private GameObject objExplosion;

	private GameObject _detailOfBow;

	private void OnEnable()
	{
		base.Invoke("AllowShowDetail", 1f);
	}

	private void AllowShowDetail()
	{
		this.bAllowShowDetail = true;
	}

	public override void Rotation(Vector2 _from, Vector2 _to)
	{
		this.xDis = _to.x - _from.x;
		this.yDis = _to.y - _from.y;
		this.xDis += 0.001f;
		this.alphal = Mathf.Atan(this.yDis / this.xDis) * 57.29578f;
		if (this.xDis <= 0f)
		{
			this.alphal = 180f + this.alphal;
		}
		this._qua = Quaternion.Euler(0f, 0f, this.alphal);
		this.m_tranform.rotation = this._qua;
	}

	private void OnDisable()
	{
		if (this.bIsArcher)
		{
			this.HitEnemy();
			if ((!this.m_enemy || !this.m_enemy.gameObject.activeInHierarchy) && this.bAllowShowDetail)
			{
				this._detailOfBow = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.BowDetail).GetItem();
				if (this._detailOfBow)
				{
					this._detailOfBow.transform.position = this.m_tranform.position;
					this._detailOfBow.transform.eulerAngles = this.m_tranform.eulerAngles;
					this._detailOfBow.SetActive(true);
				}
			}
		}
		else if (this.bIsRocket && this.bAllowShowDetail)
		{
			if (this.m_tower)
			{
				TheEventManager.PostEvent_RocketHit(this.m_tranform.position, this.m_tower.m_myTowerData.GetDamage());
			}
			this.objExplosion = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Explosion_of_Rocket).GetItem();
			if (this.objExplosion)
			{
				this.objExplosion.transform.position = this.m_tranform.position;
				this.objExplosion.SetActive(true);
				TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.explosion);
			}
		}
		else if (this.bIsBulletPosion && this.bAllowShowDetail)
		{
			this.HitEnemy();
			this.objExplosion = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Explosion_Poision).GetItem();
			this.objExplosion.transform.position = this.m_tranform.position;
			this.objExplosion.SetActive(true);
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.explosion_poison);
		}
		this.m_tranform.eulerAngles = new Vector3(0f, 0f, 90f);
	}
}

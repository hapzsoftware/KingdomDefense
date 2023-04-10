using System;
using UnityEngine;

public class FireMove : MonoBehaviour
{
	private Vector2 vStartPos;

	private Vector2 vTargetPos;

	public float fSpeed;

	private Vector2 vCurrentPos;

	private bool bPlay;

	private Transform m_transform;

	private void Awake()
	{
		this.m_transform = base.transform;
		this.vCurrentPos = this.m_transform.position;
	}

	private void Update()
	{
		if (!this.bPlay)
		{
			return;
		}
		this.vCurrentPos = Vector2.MoveTowards(this.vCurrentPos, this.vTargetPos, Time.deltaTime * this.fSpeed);
		if (this.vCurrentPos == this.vTargetPos)
		{
			GameObject item = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Explosion_of_mine).GetItem();
			if (item)
			{
				item.transform.position = this.vCurrentPos;
				item.transform.localScale = Vector3.one * 1f;
				item.SetActive(true);
			}
			TheSkillManager.CallUserSkill_BoomFroomSky(this.vCurrentPos);
			base.gameObject.SetActive(false);
			TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.explosion);
		}
		this.m_transform.position = this.vCurrentPos;
	}

	public void Play(Vector2 _targetPos)
	{
		this.vTargetPos = _targetPos;
		this.vStartPos = new Vector2(_targetPos.x + 5f, _targetPos.y + 8f);
		this.m_transform.position = this.vStartPos;
		float num = 57.29578f * Mathf.Atan((this.vStartPos.y - this.vTargetPos.y) / (this.vStartPos.x - this.vTargetPos.x));
		this.m_transform.eulerAngles = new Vector3(0f, 0f, num - 90f);
		this.vCurrentPos = this.vStartPos;
		this.bPlay = true;
	}
}

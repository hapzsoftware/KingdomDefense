using System;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
	public enum ANI
	{
		Move,
		Attack
	}

	public Animator m_animator;

	public AnimationClip m_aniMove;

	public AnimationClip m_aniAttack;

	public float fSpeedAnimation = 1f;

	public void Stop()
	{
		if (this.m_animator)
		{
			this.m_animator.speed = 0f;
		}
	}

	public void Play(EnemyAnimation.ANI _ani, float _speed = 1f)
	{
		if (!this.m_animator)
		{
			return;
		}
		this.m_animator.speed = _speed;
		if (_ani != EnemyAnimation.ANI.Move)
		{
			if (_ani == EnemyAnimation.ANI.Attack)
			{
				if (this.m_aniAttack)
				{
					this.m_animator.Play(this.m_aniAttack.name);
				}
			}
		}
		else
		{
			this.m_animator.Play(this.m_aniMove.name);
		}
	}
}

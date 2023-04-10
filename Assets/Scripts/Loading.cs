using System;
using UnityEngine;

public class Loading : MonoBehaviour
{
	public static Loading Instance;

	public Animator m_animator;

	public AnimationClip aniLoadingIn;

	public AnimationClip aniLoadingOut;

	private void Awake()
	{
		if (Loading.Instance == null)
		{
			Loading.Instance = this;
		}
	}

	public void PlayLoading(bool _bool)
	{
		if (_bool)
		{
			this.m_animator.speed = 2f;
			this.m_animator.Play(this.aniLoadingIn.name);
		}
		else
		{
			this.m_animator.speed = 2f;
			this.m_animator.Play(this.aniLoadingOut.name);
		}
	}
}

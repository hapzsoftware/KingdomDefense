using System;
using UnityEngine;

public class TheMusic : MonoBehaviour
{
	public static TheMusic Instance;

	private AudioSource m_AudioSource;

	public AudioClip[] LIST_SOUNDTRACK;

	public bool Mute
	{
		get
		{
			return this.m_AudioSource.mute;
		}
		set
		{
			this.m_AudioSource.mute = value;
			if (this.m_AudioSource.mute)
			{
				PlayerPrefs.SetString("music", "mute");
			}
			else
			{
				PlayerPrefs.SetString("music", "no");
			}
			PlayerPrefs.Save();
		}
	}

	private void Awake()
	{
		if (TheMusic.Instance == null)
		{
			TheMusic.Instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		this.m_AudioSource = base.GetComponent<AudioSource>();
		if (PlayerPrefs.GetString("music") == "mute")
		{
			this.Mute = true;
		}
		else
		{
			this.Mute = false;
		}
	}

	private void Start()
	{
		this.Play(0);
	}

	public void Play()
	{
		if (!this.m_AudioSource)
		{
			return;
		}
		this.m_AudioSource.Stop();
		int num = UnityEngine.Random.Range(1, this.LIST_SOUNDTRACK.Length);
		this.m_AudioSource.clip = this.LIST_SOUNDTRACK[num];
		this.m_AudioSource.Play();
	}

	public void Play(int _index)
	{
		this.m_AudioSource.Stop();
		this.m_AudioSource.clip = this.LIST_SOUNDTRACK[_index];
		this.m_AudioSource.Play();
	}

	public void Stop()
	{
		this.m_AudioSource.Stop();
	}
}

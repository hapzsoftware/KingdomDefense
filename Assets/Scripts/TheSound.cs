using System;
using System.Collections.Generic;
using UnityEngine;

public class TheSound : MonoBehaviour
{
	public enum SOUND_IN_GAME
	{
		ui_buy_something,
		ui_can_not,
		ui_click_back,
		ui_click_next,
		ui_coin,
		ui_winning,
		ui_defeat,
		ui_upgrade_upgrade,
		ui_upgrade_reset,
		ui_star_1,
		ui_star_2,
		ui_star_3,
		battle_new_wave,
		battle_start,
		battle_last_wave,
		heart,
		stone_crack,
		explosion,
		explosion_mine_on_road,
		explosion_poison,
		skill_freeze,
		skill_lord_fire,
		skill_soldier,
		skill_poison,
		touch_tower_pos,
		tower_build,
		tower_sell,
		tower_upgrade,
		attack_archer,
		attack_cannonner,
		attack_gunmen,
		attack_thunder,
		attack_magic,
		attack_rocket_laucher,
		attack_soldier,
		attack_soldier1,
		attack_soldier2,
		attack_poison,
		soldier_no_fair
	}

	[Serializable]
	public struct SOUND_IN_GAME_ELEMENT
	{
		public TheSound.SOUND_IN_GAME eSound;

		public AudioClip auSound;
	}

	public static TheSound Instance;

	private AudioSource m_AudioSource;

	[Header("----SOUND IN GAME----"), Space(20f)]
	public List<TheSound.SOUND_IN_GAME_ELEMENT> LIST_SOUND_IN_GAME;

	private int _rand;

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
				PlayerPrefs.SetString("sound", "mute");
			}
			else
			{
				PlayerPrefs.SetString("sound", "no");
			}
			PlayerPrefs.Save();
		}
	}

	private void Awake()
	{
		if (TheSound.Instance == null)
		{
			TheSound.Instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		this.m_AudioSource = base.GetComponent<AudioSource>();
	}

	private void Start()
	{
		if (PlayerPrefs.GetString("sound") == "mute")
		{
			this.Mute = true;
		}
		else
		{
			this.Mute = false;
		}
	}

	public void PlaySoundInGame(TheSound.SOUND_IN_GAME eSound)
	{
		if (this.m_AudioSource)
		{
			this.m_AudioSource.PlayOneShot(this.LIST_SOUND_IN_GAME[(int)eSound].auSound);
		}
	}

	public void PlaySound(AudioClip _audio)
	{
		this.m_AudioSource.PlayOneShot(_audio);
	}

	public void PlayerSoundSoldierAttack()
	{
		this._rand = UnityEngine.Random.Range(0, 3);
		if (this._rand == 0)
		{
			this.PlaySoundInGame(TheSound.SOUND_IN_GAME.attack_soldier);
		}
		else if (this._rand == 1)
		{
			this.PlaySoundInGame(TheSound.SOUND_IN_GAME.attack_soldier1);
		}
		else if (this._rand == 2)
		{
			this.PlaySoundInGame(TheSound.SOUND_IN_GAME.attack_soldier2);
		}
	}

	public void Stop()
	{
		this.m_AudioSource.Stop();
	}

	[ContextMenu("Tự động cập nhật sfx vào list sound")]
	public void AutoUpdateSound()
	{
		int num = Enum.GetNames(typeof(TheSound.SOUND_IN_GAME)).Length;
		this.LIST_SOUND_IN_GAME.Clear();
		for (int i = 0; i < num; i++)
		{
			TheSound.SOUND_IN_GAME_ELEMENT item = default(TheSound.SOUND_IN_GAME_ELEMENT);
			item.eSound = (TheSound.SOUND_IN_GAME)i;
			AudioClip audioClip = Resources.Load<AudioClip>("Audio/" + item.eSound.ToString());
			if (audioClip)
			{
				item.auSound = audioClip;
			}
			this.LIST_SOUND_IN_GAME.Add(item);
		}
		UnityEngine.Debug.Log("SOUND UPDATE: DONE");
	}
}

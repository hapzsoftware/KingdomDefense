using System;
using UnityEngine;

public class EnemyDieText : MonoBehaviour
{
	public Transform m_tranOfRender;

	public SpriteRenderer m_spriteRender;

	public Sprite[] LIST_SPRITE;

	private int _index;

	private void OnEnable()
	{
		this.m_tranOfRender.eulerAngles = new Vector3(0f, 0f, (float)UnityEngine.Random.Range(-13, 13));
		this._index = UnityEngine.Random.Range(0, this.LIST_SPRITE.Length);
		this.m_spriteRender.sprite = this.LIST_SPRITE[this._index];
	}
}

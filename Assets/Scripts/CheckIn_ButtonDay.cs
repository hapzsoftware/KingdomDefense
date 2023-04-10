using System;
using UnityEngine;
using UnityEngine.UI;

public class CheckIn_ButtonDay : MonoBehaviour
{
	private Animator m_animator;

	public CheckIn m_CheckIn;

	private Button buButton;

	private Text txtTitle;

	private Text txtNumber;

	private Image imaIcon;

	public GameObject objTick;

	public int iIndex;

	private void Start()
	{
		this.buButton.onClick.AddListener(delegate
		{
			this.GetGift();
		});
	}

	private void GetGift()
	{
		this.m_CheckIn.GetGiftCheckIn(this.iIndex);
		this.Init();
	}

	public void Init()
	{
		this.m_animator = base.GetComponent<Animator>();
		this.m_animator.enabled = false;
		base.transform.localScale = Vector3.one;
		this.buButton = base.GetComponent<Button>();
		this.txtTitle = base.transform.GetChild(0).GetComponent<Text>();
		this.txtNumber = base.transform.GetChild(2).GetComponent<Text>();
		this.imaIcon = base.transform.GetChild(1).GetComponent<Image>();
		this.txtTitle.text = "DAY " + (this.iIndex + 1);
		this.txtNumber.text = TheCheckInGiftManager.Instance.GetGift((TheCheckInGiftManager.Gift)this.iIndex).iValue.ToString();
		this.imaIcon.sprite = TheCheckInGiftManager.Instance.GetGift((TheCheckInGiftManager.Gift)this.iIndex).sprIcon;
		if (this.iIndex < this.m_CheckIn.iNumberOfGiftsReceived)
		{
			this.objTick.SetActive(true);
		}
		else if (this.iIndex == this.m_CheckIn.iNumberOfGiftsReceived)
		{
			this.objTick.SetActive(false);
			this.m_animator.enabled = true;
		}
		else
		{
			this.buButton.image.color = Color.white;
			this.objTick.SetActive(false);
		}
	}
}

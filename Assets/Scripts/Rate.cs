using System;
using UnityEngine;
using UnityEngine.UI;

public class Rate : MonoBehaviour
{
	public enum TYLE
	{
		EnjoyingThisGame,
		WouldYouMindGivingUsSomeFeedback,
		HowAboutARating
	}

	public Rate.TYLE eTyle;

	public Button buYes;

	public Button buNot;

	public Text txtContent;

	private void Start()
	{
		this.buYes.onClick.AddListener(delegate
		{
			this.ButtonYes();
		});
		this.buNot.onClick.AddListener(delegate
		{
			this.ButtonNot();
		});
	}

	private void ButtonYes()
	{
		Rate.TYLE tYLE = this.eTyle;
		if (tYLE != Rate.TYLE.EnjoyingThisGame)
		{
			if (tYLE != Rate.TYLE.WouldYouMindGivingUsSomeFeedback)
			{
				if (tYLE == Rate.TYLE.HowAboutARating)
				{
					PlayerPrefs.SetString("like", "done");
					PlayerPrefs.Save();
					TheUIManager.LoadLink(ThePlatformManager.MAIN_PLATFORM.LinkLikeMe);
					TheUIManager.HidePopup(ThePopupManager.POP_UP.Rate);
				}
			}
			else
			{
				TheUIManager.ReportEmail();
				TheUIManager.HidePopup(ThePopupManager.POP_UP.Rate);
			}
		}
		else
		{
			this.eTyle = Rate.TYLE.HowAboutARating;
			this.ShowText();
		}
	}

	private void ButtonNot()
	{
		Rate.TYLE tYLE = this.eTyle;
		if (tYLE != Rate.TYLE.EnjoyingThisGame)
		{
			if (tYLE != Rate.TYLE.WouldYouMindGivingUsSomeFeedback)
			{
				if (tYLE == Rate.TYLE.HowAboutARating)
				{
					TheUIManager.HidePopup(ThePopupManager.POP_UP.Rate);
				}
			}
			else
			{
				TheUIManager.HidePopup(ThePopupManager.POP_UP.Rate);
			}
		}
		else
		{
			this.eTyle = Rate.TYLE.WouldYouMindGivingUsSomeFeedback;
			this.ShowText();
		}
	}

	private void OnEnable()
	{
		this.eTyle = Rate.TYLE.EnjoyingThisGame;
		this.ShowText();
	}

	private void ShowText()
	{
		Rate.TYLE tYLE = this.eTyle;
		if (tYLE != Rate.TYLE.EnjoyingThisGame)
		{
			if (tYLE != Rate.TYLE.WouldYouMindGivingUsSomeFeedback)
			{
				if (tYLE == Rate.TYLE.HowAboutARating)
				{
					this.txtContent.text = "How about a rating on store, then?";
					this.buYes.GetComponentInChildren<Text>().text = "OK, SURE";
					this.buNot.GetComponentInChildren<Text>().text = "NO, THANKS";
				}
			}
			else
			{
				this.txtContent.text = "Would you mind giving us some feedback?";
				this.buYes.GetComponentInChildren<Text>().text = "OK, SURE";
				this.buNot.GetComponentInChildren<Text>().text = "NO, THANKS";
			}
		}
		else
		{
			this.txtContent.text = "Enjoying this game?";
			this.buYes.GetComponentInChildren<Text>().text = "YES";
			this.buNot.GetComponentInChildren<Text>().text = "NO REALLY";
		}
	}
}

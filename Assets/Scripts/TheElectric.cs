using System;
using UnityEngine;

public class TheElectric : MonoBehaviour
{
	[SerializeField]
	private LineRenderer m_lineRenderer;

	[SerializeField]
	private GameObject objLineRender;

	private Vector2[] LIST_POS;

	private void Awake()
	{
		this.LIST_POS = new Vector2[2];
		this.objLineRender = base.gameObject;
		this.m_lineRenderer = base.GetComponent<LineRenderer>();
		this.LIST_POS[0] = base.transform.position;
	}

	public void ShowElectric(Vector2 _enemy)
	{
		this.objLineRender.SetActive(true);
		this.LIST_POS[1] = _enemy;
		this.m_lineRenderer.SetPosition(0, this.LIST_POS[0]);
		this.m_lineRenderer.SetPosition(1, this.LIST_POS[1]);
		base.Invoke("TurnOffLine", 0.13f);
	}

	private void TurnOffLine()
	{
		this.objLineRender.SetActive(false);
	}
}

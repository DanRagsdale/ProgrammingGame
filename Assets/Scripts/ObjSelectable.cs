using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSelectable : MonoBehaviour
{
	private bool clickedOn = false;
	protected bool selected = false;

	Material skinMaterial;
	Color originalColor;

    protected virtual void Start()
    {
		skinMaterial = GetComponent<Renderer>().material;
		originalColor = skinMaterial.color;
    }

    protected virtual void Update()
    {
		if(Input.GetMouseButtonDown(1))
		{
			if(clickedOn)
			{
				Select();
			} else {
				Deselect();
			}
		}

		clickedOn = false;
    }

	protected virtual void OnMouseOver()
	{
		if(Input.GetMouseButtonDown(1))
		{
			clickedOn = true;
		}
	}

	public void Select()
	{
		selected = true;
		skinMaterial.color = Color.red;
	}

	public void Deselect()
	{
		selected = false;
		skinMaterial.color = originalColor;
	}
}

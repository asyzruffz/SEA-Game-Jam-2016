using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HinoObject : MonoBehaviour {

	protected MaskableGraphic widget;

	protected void InitializeWidget()
	{
		if (widget == null)
			widget = GetComponent<Image>();

		if (widget == null)
			widget = GetComponent<Text>();
	}

}

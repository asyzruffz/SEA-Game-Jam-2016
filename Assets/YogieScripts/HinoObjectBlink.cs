using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HinoObjectBlink : HinoObject {
	
	public float blinkDelay = 0.15f;
	public float showDelay = 0.5f;
	public float stayDelay = 0f;

	private float blinkElapsed = 0;
	private float showElapsed = 0;
	private float stayElapsed = 0;
	
	public float minAlpha = 0;
	public float maxAlpha = 1;

	public Color normalColor = Color.white;

	void Start ()
	{
		InitializeWidget();

		showElapsed = showDelay;
		stayElapsed = stayDelay;
		blinkElapsed = 0;

		widget.color = new Color(normalColor.r, normalColor.g, normalColor.b, minAlpha);
	}

	public void OnEnable()
	{
		InitializeWidget();
		
		showElapsed = showDelay;
		stayElapsed = stayDelay;
		blinkElapsed = 0;
	}

	void Update ()
	{
		if (blinkElapsed < blinkDelay)
		{
			blinkElapsed += Time.deltaTime;
			
			widget.color = new Color(normalColor.r, normalColor.g, normalColor.b, (minAlpha + (blinkElapsed / blinkDelay) * (maxAlpha - minAlpha)));
			
			if (blinkElapsed >= blinkDelay)
			{
				if (stayDelay > 0)
					stayElapsed = 0;
				else
					showElapsed = 0;
				
				widget.color = new Color(normalColor.r, normalColor.g, normalColor.b, maxAlpha);
			}
		}

		if (stayElapsed < stayDelay)
		{
			stayElapsed += Time.deltaTime;
			
			if (stayElapsed >= stayDelay)
			{
				showElapsed = 0;
			}
		}

		if (showElapsed < showDelay)
		{
			showElapsed += Time.deltaTime;
			
			widget.color = new Color(normalColor.r, normalColor.g, normalColor.b, (maxAlpha - (showElapsed / showDelay) * (maxAlpha - minAlpha)));
			
			if (showElapsed >= showDelay)
			{
				blinkElapsed = 0;
				
				widget.color = new Color(normalColor.r, normalColor.g, normalColor.b, minAlpha);
			}
		}
	}

}

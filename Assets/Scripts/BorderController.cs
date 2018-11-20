using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BorderController : MonoBehaviour {

    private Image borderComponent;
    private Color currentColour;

    private void Awake()
    {
        borderComponent = GetComponent<Image>();
    }

    public void ColourChange (Color nextColour) {

        currentColour = nextColour;
        borderComponent.color = currentColour;

	}
}

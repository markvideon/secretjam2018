using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireframeColourParent : MonoBehaviour
{
    [SerializeField] GameObject gameBorderObject;
    [SerializeField] Color[] colourRange;
    public Color currentColour;
    


    // Use this for initialization
    void Awake()
    {
        gameBorderObject = GameObject.Find("GameBorder");

        ChooseRandomColour();


    }

    void ChooseRandomColour()
    {
        currentColour = colourRange[Random.Range(0, colourRange.Length)];

        if (gameBorderObject) {
            gameBorderObject.GetComponent<BorderController>().ColourChange(currentColour);

        }
    }






    


}

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class EndMenuController : MonoBehaviour
{
    #region Fields

    // SETTINGS //

    // VARIABLES //

    // REFERENCES //

    #endregion

    #region Unity Functions

    private void Awake()
    {
        transform.Find("Score Text").GetComponent<Text>().text = "TIME " + GameObject.Find("ApplicationManager").GetComponent<ApplicationManager>().finalGameTime.ToString();
    }

    #endregion

    #region Functions
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApplicationManager : MonoBehaviour
{
    #region Fields

    // SETTINGS //

    // VARIABLES //
    public float finalGameTime;

    // REFERENCES //

    #endregion

    #region Unity Functions

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    #region Functions

    public void EndGame(MovementGrid _movementGrid)
    {
        finalGameTime = _movementGrid.currentGameTime;

        SceneManager.LoadScene("End Scene");
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    #region Fields

    // SETTINGS //

    // VARIABLES //
    private int currentGold;
    public int health = 0;
    public int maxHealth = 10;

    // REFERENCES //
    ApplicationManager applicationManager;
    MovementGrid movementGrid;

    #endregion

    #region Properties

    public int CurrentGold
    {
        get { return currentGold; }

        set
        {
            currentGold = value;

            if (currentGold >= 10000)
            {
                applicationManager.EndGame(movementGrid);
            }
        }
    }

    #endregion

    #region Unity Functions

    private void Awake()
    {
        // GATHER REFERENCES //
        applicationManager = GameObject.Find("ApplicationManager").GetComponent<ApplicationManager>();
        movementGrid = GameObject.Find("LevelManager").GetComponent<MovementGrid>();

        // INITIALIZE //
        health = maxHealth;
    }

    #endregion

    #region Functions

    public void Damage()
    {
        health--;

        if (health <= 0)
        {
            SceneManager.LoadScene("Death Scene");
        }
    }
    
    #endregion
}

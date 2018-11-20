using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    #region Fields

    // SETTINGS //

    // VARIABLES //
    string initialTimeText;
    string initialGoldText;
    string initialHealthText;

    // REFERENCES //
    Text gameTimeHUD;
    Text goldHUD;
    Text healthHUD;
    MovementGrid movementGrid;
    PlayerData playerData;

    #endregion

    #region Unity Functions

    private void Awake()
    {

        // GATHER REFERENCES //
        gameTimeHUD = transform.Find("Game Time HUD").GetComponent<Text>();
        goldHUD = transform.Find("Gold HUD").GetComponent<Text>();
        healthHUD = transform.Find("Health HUD").GetComponent<Text>();
        movementGrid = GameObject.Find("LevelManager").GetComponent<MovementGrid>();
        playerData = movementGrid.GetComponent<PlayerData>();

        initialTimeText = gameTimeHUD.text;
        initialGoldText = goldHUD.text;
        initialHealthText = healthHUD.text;
    }

    private void Update()
    {
        gameTimeHUD.text = "";
        goldHUD.text = "";
        healthHUD.text = "";
        Canvas.ForceUpdateCanvases();

        gameTimeHUD.text = initialTimeText + movementGrid.currentGameTime.ToString();
        goldHUD.text = initialGoldText + playerData.CurrentGold + "/" + movementGrid.requriedNextLevelGold;
        healthHUD.text = initialHealthText + playerData.health + "/" + playerData.maxHealth;
        Canvas.ForceUpdateCanvases();
    }

    #endregion

    #region Functions
    #endregion
}

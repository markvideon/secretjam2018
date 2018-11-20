using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour {

    [SerializeField] int PLAYSCENE;
    [SerializeField] int OPTIONS;

    public void PlayButtonClicked() {
        SceneManager.LoadScene(PLAYSCENE);
    }
}

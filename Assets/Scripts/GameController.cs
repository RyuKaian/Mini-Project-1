using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Camera thirdpCamera;
    public Camera firstpCamera;
    public Canvas pauseMenu;
    public Canvas HUD;

    public UIController uiController;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            uiController.Camera();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiController.Pause();
        }
    }
}

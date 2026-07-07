using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class scr_ControllerImage : MonoBehaviour
{
    public bool isKeyboard;
    public Image controllerImage;
    public Sprite controllerControls, keyboardControls;

    public void Update()
    {
        if (scr_GameManager.instance.controlsMenu == true) // if the game object is set to true
        {
            if (isKeyboard == true) // if bool is true
            {
                controllerImage.sprite = keyboardControls; // set image sprite to keyboard control
            }
            else if (isKeyboard == false) // if bool is false
            {
                controllerImage.sprite = controllerControls; // set image sprite to controller controls
            }
        }
    }

    private void OnEnable() // when object is loaded
    {
        scr_InputHandler.OnInputModeChange += UpdateImageVariables; // actives as listener for an Action
    }

    private void OnDisable() // when object/script is disabled
    {
        scr_InputHandler.OnInputModeChange -= UpdateImageVariables; // deactivates as listener for an Action
    }

    private void UpdateImageVariables(scr_InputHandler.InputMode mode) // when listener is triggere by an Action
    {
        string input = mode.ToString(); // convert input to text
        if (input == "Keyboard") // if the input is keyboard then set isKeyboard to true
        {
            isKeyboard = true;
        }
        else if (input == "Controller") // if the input is controller then set isKeyboard to false
        {
            isKeyboard = false;
        }
    }
}


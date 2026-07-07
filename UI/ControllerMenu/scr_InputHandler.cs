using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class scr_InputHandler : MonoBehaviour
{
    public enum InputMode { Controller, Keyboard}
    private InputMode _currentInputMode;

    private InputMode _LastFrameInputMode;
    public static Action<InputMode> OnInputModeChange;

    private void Start()
    {
        _currentInputMode = InputMode.Keyboard;
    }

    private void Update()
    {
        _currentInputMode = ProcessInputMode(); // sets the current input mode by calling a function which returns a value

        if (_currentInputMode != _LastFrameInputMode) // if current input mode is different from the last frame then do this
        {
            OnInputModeChange?.Invoke(_currentInputMode); // Use's an action to trigger this InputMode for any listeners.
        }

        _LastFrameInputMode = ProcessInputMode(); // set's the previous frame input to the current frame's input.
    }

    private InputMode ProcessInputMode()
    {
        if (Input.GetJoystickNames().Length == 0) // if no controllers are connected then return keyboard input mode
        {
            return InputMode.Keyboard;
        }

        if (Input.anyKeyDown) // if a button is pressed then check the joystick buttons and if none of those are pressed return keyboard otherwise if one is pressed return controller
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton0)){return InputMode.Controller;}
            else if (Input.GetKeyDown(KeyCode.JoystickButton1)) { return InputMode.Controller; }
            else if (Input.GetKeyDown(KeyCode.JoystickButton2)) { return InputMode.Controller; }
            else if (Input.GetKeyDown(KeyCode.JoystickButton3)) { return InputMode.Controller; }
            else if (Input.GetKeyDown(KeyCode.JoystickButton4)) { return InputMode.Controller; }
            else if (Input.GetKeyDown(KeyCode.JoystickButton5)) { return InputMode.Controller; }
            else if (Input.GetKeyDown(KeyCode.JoystickButton6)) { return InputMode.Controller; }
            else if (Input.GetKeyDown(KeyCode.JoystickButton7)) { return InputMode.Controller; }
            else if (Input.GetKeyDown(KeyCode.JoystickButton8)) { return InputMode.Controller; }
            else if (Input.GetKeyDown(KeyCode.JoystickButton9)) { return InputMode.Controller; }
            else if (Input.GetKeyDown(KeyCode.JoystickButton10)) { return InputMode.Controller; }
            else if (Input.GetKeyDown(KeyCode.JoystickButton11)) { return InputMode.Controller; }
            else if (Input.GetKeyDown(KeyCode.JoystickButton12)) { return InputMode.Controller; }
            else if (Input.GetKeyDown(KeyCode.JoystickButton13)) { return InputMode.Controller; }
            else if (Input.GetKeyDown(KeyCode.JoystickButton14)) { return InputMode.Controller; }
            else if (Input.GetKeyDown(KeyCode.JoystickButton15)) { return InputMode.Controller; }
            else if (Input.GetKeyDown(KeyCode.JoystickButton16)) { return InputMode.Controller; }
            else if (Input.GetKeyDown(KeyCode.JoystickButton17)) { return InputMode.Controller; }
            else if (Input.GetKeyDown(KeyCode.JoystickButton18)) { return InputMode.Controller; }
            else if (Input.GetKeyDown(KeyCode.JoystickButton19)) { return InputMode.Controller; }
            else { return InputMode.Keyboard; }
        }

        if (Input.anyKey) // if press done horizontal or vertical buttons on a keyboard then return keyboard
        {
            if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Horizontal1") != 1 && Input.GetAxisRaw("Horizontal1") != -1) { return InputMode.Keyboard; }
            if (Input.GetAxisRaw("Vertical") != 0 && Input.GetAxisRaw("Vertical1") != 1 && Input.GetAxisRaw("Vertical1") != -1) { return InputMode.Keyboard; }
        }

        if (Input.GetAxisRaw("Horizontal") != 0) { return InputMode.Controller; }
        if (Input.GetAxisRaw("Vertical") != 0) { return InputMode.Controller; }
        if (Input.GetAxisRaw("Horizontal2") != 0) { return InputMode.Controller; }
        if (Input.GetAxisRaw("Vertical2") != 0) { return InputMode.Controller; }
        if (Input.GetAxisRaw("HorizontalD") != 0) { return InputMode.Controller; }
        if (Input.GetAxisRaw("VerticalD") != 0) { return InputMode.Controller; }
        if (Input.GetAxisRaw("LeftTrigger") != 0) { return InputMode.Controller; }
        if (Input.GetAxisRaw("RightTrigger") != 0) { return InputMode.Controller; }

        return _currentInputMode; // if no other returns are done then return current input mode
    }
}

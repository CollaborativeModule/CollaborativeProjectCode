using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class scr_ScreenSize : MonoBehaviour
{
    public RectTransform UIParticleEffectRectTransform;
    public GameObject UIButtonClickParticle;
    public void FullScreenButton()
    {
        UIButtonClickParticle.SetActive(true);
        UIParticleEffectRectTransform.position = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().position;
        Invoke("DisabledUIParticleEffects", 1f);
        // sets full screen mode as a variable to exclusivefullscreen and then 
        FullScreenMode fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            Screen.fullScreenMode = fullScreenMode;

        // sets a variable called current resolution to the resolution of the desktop
            Resolution currentResolution = Screen.currentResolution;
        
        // sets screen resolution to the resolution of the desktop in exclusive fullscreen
            Screen.SetResolution(currentResolution.width, currentResolution.height, fullScreenMode);
    }

    public void ScreenWindowed()
    {
        // sets the screen resolution to be small and windowed for if people want that
        UIButtonClickParticle.SetActive(true);
        UIParticleEffectRectTransform.position = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().position;
        Invoke("DisabledUIParticleEffects", 1f);
        Screen.SetResolution(960, 540, FullScreenMode.Windowed);
    }

    public void DisabledUIParticleEffects()
    {
        // disables particle effect for UI button click
        UIButtonClickParticle.SetActive(false);
    }
}

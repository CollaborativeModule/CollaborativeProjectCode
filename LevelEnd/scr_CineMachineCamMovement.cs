using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class scr_CineMachineCamMovement : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    public Transform player;

    public static scr_CineMachineCamMovement instance;

    public void Awake()
    {
        instance = this;
    }
    public void EndOflevel() // called at end of a level for the end of level cutscene
    {
        cinemachineVirtualCamera.Follow = null; // sets the camera follow transform to null
    }

    public void StartOfLevel() // called at beginning of a level so the camera follows the player
    {
        cinemachineVirtualCamera.Follow = player;  // sets the camera follow transform to the player's transform
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class scr_CineMachineShake : MonoBehaviour
{
    public static scr_CineMachineShake Instance { get; private set; }
    private CinemachineVirtualCamera cineMachineVirtualCamera;
    private float shakeTimer;
    private void Awake()
    {
        // sets values
        Instance = this;
        cineMachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        // gets the channels that are responsible for the shake on the cine machine
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cineMachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
        // sets the amplitude based on intensity passed through and the time it lasts
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        // if the timer is greater then 0 then it counts down towards 0 the timer and if the timer becomes 0 or less then it stops the shake
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cineMachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}

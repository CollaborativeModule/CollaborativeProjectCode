using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class scr_Rotating : MonoBehaviour
{
    public float rotation, rotationSpeed;
    public GameObject rotateAround;

    public void Update()
    {
        // rotate object in a circle
        transform.localEulerAngles = new Vector3 (0,0, rotation);
        rotation += rotationSpeed * Time.deltaTime;
    }
}

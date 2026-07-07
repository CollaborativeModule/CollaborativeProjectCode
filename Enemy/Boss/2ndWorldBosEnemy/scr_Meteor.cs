using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Meteor : MonoBehaviour
{
       public GameObject meteorLeft, meteorRight;
       public Transform playerTf;
    public void SpawnAttack()
    {
        // randomly spawn a meteor to the player's left or right
        playerTf = script_SunRa.instance.playerTransform;
        float random = Random.Range(0,2);
        if (random == 0)
        {
            Instantiate(meteorLeft, new Vector3(playerTf.position.x - 16, playerTf.position.y + 7f, playerTf.position.z), Quaternion.identity);
        }
        else if (random == 1)
        {
            Instantiate(meteorRight, new Vector3(playerTf.position.x + 16, playerTf.position.y + 7f, playerTf.position.z), new Quaternion(-180f,0f,0f,0f));
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playCaveScene : MonoBehaviour
{
    public GameObject videoPlayer;

    void Start () 
    {
        videoPlayer.SetActive(false);
    }

        void OnTriggerEnter(Collider player)
        {
            if (player.gameObject.layer == 3)
            {
                videoPlayer.SetActive(true);
            }   
        }
}

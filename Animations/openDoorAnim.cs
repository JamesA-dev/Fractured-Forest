using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoorAnim : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;

    [SerializeField] private string doorOpen = "DoorOpen";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myDoor.Play(doorOpen, 0, 0.0f);
        }
    }
}

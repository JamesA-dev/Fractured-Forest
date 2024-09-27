using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

namespace KeySystem 
{
    public class KeyItemController : MonoBehaviour
    {
        public NavMeshAgent Enemyagent;
        public AudioSource source;
        public GameObject barrier;
        public GameObject noteUI;
        public GameObject Flashlight;
        public playerMovements movementScript;
        public GameObject player;
        public float speedOfEnemy = 8f;

        [SerializeField] private bool redDoor = false;
        [SerializeField] private bool axe = false;
        [SerializeField] private bool axeDoor = false;
        [SerializeField] private bool redKey = false;
        [SerializeField] private bool note = false;
        [SerializeField] private bool battery = false;
        [SerializeField] private bool rock = false;
        [SerializeField] private bool rockDoor = false;

        [SerializeField] private KeyInventory _keyInventory = null;

        public KeyDoorController doorObject;
        public KeyDoorController rockObject;
        public KeyDoorController axeObject;

        private void Start() 
        {

        }

        public void Awake()
        {
            rockObject = GetComponent<KeyDoorController>();
            doorObject = GetComponent<KeyDoorController>();
            axeObject = GetComponent<KeyDoorController>();
            movementScript = player.GetComponent<playerMovements>();
        }

        public void ObjectInteraction()
        {
            if (redDoor) 
            {
                doorObject.PlayAnimation();
            }

            else if (redKey) 
            {
                Enemyagent.speed = speedOfEnemy;
                StartCoroutine(ReplaceWall(3f));
                barrier.SetActive(false);
                source.Play();
                _keyInventory.hasRedKey = true;
                gameObject.SetActive(false);
            }

            else if (rock) 
            {
                _keyInventory.hasRock = true;
                gameObject.SetActive(false);
            }

            else if (axe) 
            {
                _keyInventory.hasAxe = true;
                gameObject.SetActive(false);
            }

            
            else if (axeDoor) 
            {
                axeObject.PlayAnimation();
            }

            else if (rockDoor) 
            {
                rockObject.PlayAnimation();
            }

            else if (note) 
            {
                noteUI.SetActive(true);
                movementScript.CanMove = false;
            }

            else if (battery) 
            {
                gameObject.SetActive(false);
                Flashlight.GetComponent<Flashlight>().ReplaceBattery(2f);
            }
        }

        private IEnumerator ReplaceWall(float delay)
        {
            yield return new WaitForSeconds(delay);
            barrier.SetActive(true);
        }
    }
}

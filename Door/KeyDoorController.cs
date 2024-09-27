using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeySystem
{
    public class KeyDoorController : MonoBehaviour
    {
        public GameObject barricade;

        public AudioSource woodBreaking;
        public AudioSource redDoorOpening;
        private Animator doorAnim;
        private bool doorOpen1 = false;
        public bool doorOpen2 = false;
        public bool doorOpen3 = false;

        [Header("Animation Names")]
        [SerializeField] private string openAnimationName = "openDoor";
        [SerializeField] private string closeAnimationName = "DoorClose";

        [SerializeField] private int timeToShowUI = 1;
        [SerializeField] private GameObject showDoorLockedUI = null;

        [SerializeField] private KeyInventory _KeyInventory = null;

        [SerializeField] private int waitTimer = 1;
        [SerializeField] private bool pauseInteraction = false;
        public changeDifficulty ChangeDiffScript;

        private void Awake()
        {
            doorAnim = gameObject.GetComponent<Animator>();
        }

        private IEnumerator PauseDoorInteraction()
        {
            pauseInteraction = true;
            yield return new WaitForSeconds(waitTimer);
            pauseInteraction = false;
        }

        public void PlayAnimation()
        {
            if (_KeyInventory.hasRock)
            {
                OpenDoor1();
                if (ChangeDiffScript.barricade1Upgraded == false)
                {
                    OpenDoor3();
                }
            }
            else if (_KeyInventory.hasRedKey)
            {
                OpenDoor2();
            }
            else if (_KeyInventory.hasAxe)
            {
                OpenDoor3();
            }
            else
            {
                StartCoroutine(ShowDoorLocked());
            }
        }

        void OpenDoor1()
        {
            if (!doorOpen1 && !pauseInteraction)
            {
                barricade.GetComponent<BoxCollider>().enabled = false; 
                doorAnim.Play(openAnimationName, 0, 0.0f);
                woodBreaking.Play();
                doorOpen1 = true;
                StartCoroutine(PauseDoorInteraction());
            }

            else if (doorOpen1 && !pauseInteraction)
            {
                doorAnim.Play(closeAnimationName, 0, 0.0f);
                doorOpen1 = false;
                StartCoroutine(PauseDoorInteraction());
            }
        }

        void OpenDoor2()
        {
            if (!doorOpen1 && !pauseInteraction)
            {
                redDoorOpening.Play();
                doorAnim.Play(openAnimationName, 0, 0.0f);
                doorOpen2 = true;
                StartCoroutine(PauseDoorInteraction());
            }

            else if (doorOpen1 && !pauseInteraction)
            {
                doorAnim.Play(closeAnimationName, 0, 0.0f);
                doorOpen2 = false;
                StartCoroutine(PauseDoorInteraction());
            }
        }

        
        void OpenDoor3()
        {
            if (!doorOpen1 && !pauseInteraction)
            {
                woodBreaking.Play();
                doorAnim.Play(openAnimationName, 0, 0.0f);
                doorOpen3 = true;
                StartCoroutine(PauseDoorInteraction());
            }

            else if (doorOpen1 && !pauseInteraction)
            {
                doorAnim.Play(closeAnimationName, 0, 0.0f);
                doorOpen3 = false;
                StartCoroutine(PauseDoorInteraction());
            }
        }


        IEnumerator ShowDoorLocked()
        {
            showDoorLockedUI.SetActive(true);
            yield return new WaitForSeconds(timeToShowUI);
            showDoorLockedUI.SetActive(false);
        }
    }   
}

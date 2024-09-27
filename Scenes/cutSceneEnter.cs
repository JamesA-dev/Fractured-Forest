using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutSceneEnter : MonoBehaviour
{
    public GameObject player;
    public GameObject playerCamera;
    public GameObject cutSceneCam;

    void OnTriggerEnter(Collider other) {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        cutSceneCam.SetActive(true);
        playerCamera.SetActive(false);
        StartCoroutine(FinishCut());
    }

    IEnumerator FinishCut() {
        yield return new WaitForSeconds(0.3f);
        playerCamera.SetActive(true);
        cutSceneCam.SetActive(false);
        player.transform.position = new Vector3(409f, -10f, 662f);
    }
}

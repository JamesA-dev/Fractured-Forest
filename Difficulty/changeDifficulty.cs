using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeySystem;
using UnityEngine.AI;

public class changeDifficulty : MonoBehaviour
{
    public KeyItemController speedChanger;
    public EnemyAiTutorial enemyAI1;
    public EnemyAiTutorial enemyAI2;
    public float speedChange = 8f;
    public NavMeshAgent Enemyagent1;
    public NavMeshAgent Enemyagent2;
    public float sightRangeChange = 35f;
    public AudioSource enemySource1;
    public AudioSource enemySource2;
    public GameObject enemy1;
    public GameObject enemy2;
    public int numberOfEnemies = 2;
    public GameObject barricade2;
    public GameObject barricade1;
    public GameObject rockBlockage;
    public bool barricade2Enabled = true;
    public bool barricade1Upgraded = true;

    public void changeDifficultyMode()
    {   
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        speedChanger.speedOfEnemy = speedChange;
        Enemyagent1.speed = speedChange -1;
        Enemyagent2.speed = speedChange -1;
        enemyAI1.sightRange = sightRangeChange;
        enemyAI2.sightRange = sightRangeChange;
        enemySource1.maxDistance = sightRangeChange;
        enemySource2.maxDistance = sightRangeChange;

        if (barricade2Enabled == true)
        {
            barricade2.SetActive(true);
            rockBlockage.SetActive(false);
        }
        else if (barricade2Enabled == false)
        {
            barricade2.SetActive(false);
            rockBlockage.SetActive(true);
        }
        
        if (numberOfEnemies == 1)
        {
            enemy1.SetActive(false);
            enemy2.SetActive(false);
        }
        else if(numberOfEnemies == 0)
        {
            numberOfEnemies = 2;
        }

        if(numberOfEnemies == 2)
        {
            enemy2.SetActive(false);
        }
    }
}

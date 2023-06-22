using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class SpawnEnemy : MonoBehaviour
{
    private int CountEnemies=0;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private int CountE;
    [SerializeField] private TextMeshProUGUI textTimer;
    [SerializeField] private GameObject preabEnemy;
    private float timeRemaining;
   [SerializeField] private float oldtime;
    // Start is called before the first frame update
     private void Start()
    {
        timeRemaining = oldtime;
    }
    void Update()
    {
        if (timeRemaining > 0)
        {
            updateTimer(timeRemaining);
            timeRemaining -= Time.deltaTime;
            
        }
        else
        {
             
             CreateEnemy();
           /* GameObject CloneEnemy = Instantiate(preabEnemy, transform.position, Quaternion.identity) as GameObject;
            CloneEnemy.name = "Bot";
            CountEnemies++;
            if (CountEnemies > CountE && oldtime >= 20)
            {
                CountE += 5;
                oldtime -= 5;
                Debug.Log(oldtime);

            }
            timeRemaining = oldtime;*/
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {   
            GameObject CloneEnemy = Instantiate(preabEnemy, transform.position, Quaternion.identity) as GameObject;
            CloneEnemy.name = "Zombie";
            CountEnemies++;
            if (CountEnemies > CountE&& oldtime >= 15)
            {
                CountE += 5;             
                oldtime -= 5;
                Debug.Log(oldtime);
                
            }
            timeRemaining = oldtime;
        }
    }
    public void CreateEnemy(){
        // 25 1.25 35 + 25 1.25 45 + 35 1.25 45 + 35 1.25 35
            int RandomX = Random.Range(25,40);
            int RandomZ = Random.Range(25,50);
            Vector3 RandomPos = new Vector3(RandomX, 2.0f,RandomZ);
            ParticleSystem Ps = Instantiate(ps, RandomPos, Quaternion.identity) as ParticleSystem;
            GameObject CloneEnemy = Instantiate(preabEnemy, RandomPos, Quaternion.identity) as GameObject;
            CloneEnemy.name = "Bot";
            CountEnemies++;
            if (CountEnemies > CountE && oldtime >= 20)
            {
                CountE += 5;
                oldtime -= 5;
                Debug.Log(oldtime);

            }
            timeRemaining = oldtime;
    }
    void updateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        textTimer.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}

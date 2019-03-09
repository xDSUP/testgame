using UnityEngine;
using System.Collections;


public class SpawnEnemy : MonoBehaviour
{

    public GameObject[] WayPoints;
    public GameObject[] enemies;
    //public GameObject testEnemyPrefab;
    public bool isDay = true; // проверка дня
    public float dayTime = 10; // сколько длиться день
    public bool nonSpawnNew = true; // проперка был ли создан человек, если нет создаем
    public int enemyCount = 0;
    public GameObject enemyPrefab;




    private GameObject getNewEnemy()
    {
        return (GameObject)Instantiate(enemyPrefab);
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDay && nonSpawnNew)
        {
            GameObject newEnemy = (GameObject)Instantiate(enemyPrefab);
            newEnemy.GetComponent<Enemy>().WayPoints = WayPoints;
            enemyCount++;
            nonSpawnNew = false;
        }
    }

}

using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class RespawnController : MonoBehaviour
{
    [Header("Respawn Points")]   
    
    [SerializeField] private Transform[] RespawnTransform;
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private int limit;
    [SerializeField] private static int totalEnemy = 0;
    [SerializeField] private string level;

    [Header("Enemy Boyz")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private GameObject BoyzPrefab;

    private float cooldown = 15;
    private float level_1_cooldown = 12;
    private float timer;

    private void Awake()
    {
        totalEnemy = 0;
    }

    private void Update()
    {
        timer -= Time.deltaTime; 

        if(timer < 0 && level == "infinity")
        {
            timer = cooldown - 1.2f;
            CreatNewEnemy();
            CreatNewEnemy();
        }

        else if (timer < 0 && level == "1" && totalEnemy < limit)
        {
            timer = level_1_cooldown;
            CreatNewEnemy();
            CreatNewEnemy();
        }

        else if (timer < 0 && level == "2" && totalEnemy < limit)
        {
            timer = level_1_cooldown;
            CreatNewEnemy();
            CreatNewEnemy();
        }

        else if (timer < 0 && level == "3" && totalEnemy < limit)
        {
            timer = level_1_cooldown;
            CreatNewEnemy();
            CreatNewEnemy();
        }

        else if (timer < 0 && level == "4" && totalEnemy < limit)
        {   
            timer = level_1_cooldown;
            CreatNewEnemy();
            CreatNewEnemy();
        }

        else if (level == "5" && totalEnemy < limit)
        {
            if(enemy.Health == 18 && enemy.sayac == 0)
            {
                CreateBoyz(0.50f);
                CreateBoyz(1.9f);
                enemy.sayac++;
            }

            else if(enemy.Health == 12 && enemy.sayac == 1)
            {
                CreateBoyz(0.50f);
                CreateBoyz(1.9f);
                enemy.sayac++;
            }
    
            else if(enemy.Health == 6 && enemy.sayac == 2)
            {
                CreateBoyz(0.50f);
                CreateBoyz(1.9f);
                enemy.sayac++;
            }

        }

        else if (Enemy.totalkills > limit)
        {
            Entity.PlayerDeathCount = 0;
            Enemy.totalkills = 0;
            Invoke(nameof(loadMenu), 2f);
        }
    }

    private void CreatNewEnemy()
    {
        int RespawnPointIndex = Random.Range(0, RespawnTransform.Length);
        Vector3 SpawnPoint = RespawnTransform[RespawnPointIndex].position;
        GameObject NewEnemy = Instantiate(EnemyPrefab, SpawnPoint, Quaternion.identity);
        Enemy Newa = NewEnemy.GetComponent<Enemy>();
        Newa.Health = 3;
        totalEnemy++;
        Debug.Log("New Enemy Created. Total Enemy: " + totalEnemy);
    }

    private void loadMenu() => SceneManager.LoadScene(0);

    private void CreateBoyz(float x)
    {
        Vector3 SpawnPoint = RespawnTransform[0].position;
        SpawnPoint.x += x;
        SpawnPoint.y -= 4.80f;
        GameObject NewEnemy = Instantiate(BoyzPrefab, SpawnPoint, Quaternion.identity);
        Enemy Newa = NewEnemy.GetComponent<Enemy>();
        Newa.Health = 3;
        totalEnemy++;
    }
}

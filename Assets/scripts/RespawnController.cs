using UnityEngine;

public class RespawnController : MonoBehaviour
{
    [Header("Respawn Points")]   
    
    [SerializeField] private Transform[] RespawnTransform;
    [SerializeField] private GameObject EnemyPrefab;
    private float cooldown = 15;
    private float timer;

    private void Update()
    {
        timer -= Time.deltaTime; 

        if(timer < 0)
        {
            timer = cooldown;
            CreatNewEnemy();
        }
    }

    private void CreatNewEnemy()
    {
        int RespawnPointIndex = Random.Range(0, RespawnTransform.Length);
        Vector3 SpawnPoint = RespawnTransform[RespawnPointIndex].position;
        GameObject NewEnemy = Instantiate(EnemyPrefab, SpawnPoint, Quaternion.identity);
        Enemy Newa = NewEnemy.GetComponent<Enemy>();
        Newa.Health = 3;
    }
}

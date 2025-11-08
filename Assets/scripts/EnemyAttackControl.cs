using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttackControl : MonoBehaviour
{
    private Enemy enemy;
    [SerializeField] private Entity entity;
    [SerializeField] private Slider slider;
    GameObject FindEntity;
    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        FindEntity = GameObject.FindWithTag("Player");
        entity = FindEntity.GetComponent<Entity>();
        FindEntity = GameObject.FindWithTag("EditorOnly");
        slider = FindEntity.GetComponent<Slider>();
    }
    private void Update()
    {   if(enemy != null && entity != null) 
            PlayerDeath();
    }
    private void DisableMovement() => enemy.AttackControlTrue(false);
    private void EnableMovement() => enemy.AttackControlTrue(true);
    private void EnemyDedect() => enemy.TargetDedector();
    private void TakeDamage()
    {   
        if(enemy.playerCheck)
            entity.TakeDamage();
    }
    private void StartDamageAnimation()
    {
        if (enemy.playerCheck)
            entity.StartDamageAnimation();
    }
    private void PlayerDeath()
    {
        if (slider.value >= 100)
        {
            entity.Death();
            Invoke(nameof(DestroyGameObject), 2.5f);
        }
    }
    private void DestroyGameObject()
    {
        GameObject destroyPlayer = GameObject.FindGameObjectWithTag("Player");
        GameObject.Destroy(destroyPlayer);
    }
}


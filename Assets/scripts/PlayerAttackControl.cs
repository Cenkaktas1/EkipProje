using UnityEngine;

public class PlayerAttackControl : MonoBehaviour
{
    private Entity player;
    [SerializeField] private Enemy enemy;
    private void Awake()
    {
        player = GetComponentInParent<Entity>();
    }
    private void Update()
    {
        if (player != null && enemy != null) 
            EnemyDeath();
    }
    private void DisableMovement() => player.AttackControlTrue(false);
    private void EnableMovement() => player.AttackControlTrue(true);
    private void EnemyDedect() => player.TargetDedector();
    private void TakeDamage()
    {
        if(player.EnemiesCollider)
            enemy.TakeDamage();
    }
    private void StartDamageAnimation()
    {
        if (player.EnemiesCollider)
            enemy.StartDamageAnimation();
    }
    private void EnemyDeath() 
    {
        if(enemy.Health == 0)
        {
            enemy.Death();
            Invoke(nameof(DestroyGameObject), 2.5f);
        }
    }
    private void DestroyGameObject()
    {
        GameObject destroyenemy = GameObject.FindGameObjectWithTag("Enemy");
        GameObject.Destroy(destroyenemy);
    }
}

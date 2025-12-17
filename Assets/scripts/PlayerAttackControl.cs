// PlayerAttackControl.cs
using UnityEngine;

public class PlayerAttackControl : MonoBehaviour
{
    private Entity player;


    private void Awake()
    {
        player = GetComponentInParent<Entity>();
    }

    // Bu fonksiyonlar animasyon event'leri tarafýndan tetikleniyor
    private void DisableMovement() => player.AttackControlTrue(false);
    private void EnableMovement() => player.AttackControlTrue(true);

    private void EnemyDedect() => player.TargetDedector();
}
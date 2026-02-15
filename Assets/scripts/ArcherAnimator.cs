using UnityEngine;

public class ArcherAnimator : MonoBehaviour
{
    private Archer archer;

    private void Awake()
    {
        archer = GetComponentInParent<Archer>();
    }

    private void Attacking() => archer.Attack();
}

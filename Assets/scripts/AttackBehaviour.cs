using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Entity entity = animator.GetComponentInParent<Entity>();
        entity.AttackControlTrue(false);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Entity entity = animator.GetComponentInParent<Entity>();
        entity.AttackControlTrue(true);
    }
}

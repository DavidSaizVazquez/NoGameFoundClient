using System.Collections;
using System.Collections.Generic;
using NoGameFoundClient;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Shoot : StateMachineBehaviour
{

    private Animator animator;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<BossAbility>().fireBullet();
    }

    private void fireBullet(Vector3 dir)
    {
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("shootBullet",false);
    }
}

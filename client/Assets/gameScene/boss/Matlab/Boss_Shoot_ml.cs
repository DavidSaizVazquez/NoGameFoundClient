using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Shoot_ml : StateMachineBehaviour
{
    
    
    private Animator animator;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    GameObject player;
    GameObject boss;
    Vector2 diff;
    Vector2 direction;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<BossAbility>().fireBullet();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("shootBullet",false);
    }
    
}

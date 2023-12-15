using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatFollowingBehaviour : StateMachineBehaviour
{
    [SerializeField] private float movementVelocity;
    [SerializeField] private float baseTime;
    private float followingTime;
    private Transform player;
    private BatAI batAI;
    private PlayerInfo playerInfo;
    private JumppingDamageTrigger JumppingDamageTrigger;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        followingTime = baseTime;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        batAI = animator.gameObject.GetComponent<BatAI>();
        playerInfo = GameObject.Find("StandarInterface").GetComponent<Initialization>().playerInfo;
        JumppingDamageTrigger = animator.gameObject.GetComponent<JumppingDamageTrigger>();
        animator.gameObject.GetComponentInChildren<AISounds>().canListen = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (JumppingDamageTrigger.lifes > 0 && batAI.canMove)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, player.position, movementVelocity * Time.deltaTime);
        }
        
        batAI.FlipBat(player.position);
        followingTime -= Time.deltaTime;

        if (followingTime <= 0 || playerInfo.currentHearts <= 0)
        {
            animator.SetTrigger("Return");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BoarController : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        patrolState = new BoarPatrolState();
        chaseState = new BoarChaseState();
        anim.SetBool("walk", true);
    }
    public void changeFaceDirWhenPatroling()
    {
        if (!physicsCheck.isGround || (physicsCheck.touchLeftWall && faceDir < 0) || (physicsCheck.touchRightWall && faceDir > 0))
        {
            canMove = false;
            anim.SetBool("walk", false);
            StartCoroutine(Utils.SetTimeout(1, WaitForWalk));
        }
    }

    public void changeFaceDirWhenChasing()
    {
        if (!physicsCheck.isGround || (physicsCheck.touchLeftWall && faceDir < 0) || (physicsCheck.touchRightWall && faceDir > 0))
        {
            ChangeFaceDir();
        }
    }
    
    void WaitForWalk()
    {
        ChangeFaceDir();
        canMove = true;
        anim.SetBool("walk", true);
    }

    public override void GetHurt()
    {
        base.GetHurt();
        SwitchState(NPCState.Chase);
    }
}

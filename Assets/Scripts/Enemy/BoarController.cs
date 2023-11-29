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
    public override void Move()
    {
        base.Move();
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
            transform.localScale = new Vector3(-faceDir, 1, 1);
            faceDir = -faceDir;
        }
    }
    
    void WaitForWalk()
    {
        transform.localScale = new Vector3(-faceDir, 1, 1);
        faceDir = -faceDir;
        canMove = true;
        anim.SetBool("walk", true);
    }
}

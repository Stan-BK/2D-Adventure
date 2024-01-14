using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : Enemy
{
    public bool startSprint = false;
    public bool isAttack = false;
    
    protected override void Awake()
    {
        base.Awake();
        patrolState = new BeePatrolState();
        chaseState = new BeeChaseState();
    }
    
    public void changeFaceDirWhenPatroling()
    {
        if ((physicsCheck.touchLeftWall && faceDir < 0) || (physicsCheck.touchRightWall && faceDir > 0))
        {
            canMove = false;
            StartCoroutine(Utils.SetTimeout(1, WaitForWalk));
        }
    }

    public void changeFaceDirWhenChasing()
    {
        if ((physicsCheck.touchLeftWall && faceDir < 0) || (physicsCheck.touchRightWall && faceDir > 0))
        {
            ChangeFaceDir();
        }
    }
    void WaitForWalk()
    {
        ChangeFaceDir();
        canMove = true;
    }
    
    public override void GetHurt()
    {
        base.GetHurt();
        if (currentState != chaseState)
            SwitchState(NPCState.Chase);
    }

    public override void EnemyDead()
    {
        base.EnemyDead();
        rb.gravityScale = 16;
        transform.Rotate(0, 0,  (PlayerController.Instance.transform.position.x - transform.position.x) * 10);
    }

    public void Attack()
    {
        rb.AddForce(new Vector2(faceDir * Time.deltaTime * 100, 0), ForceMode2D.Impulse);
        startSprint = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (LayerMask.NameToLayer("Ground") == other.gameObject.layer)
        {
            StartCoroutine(Utils.SetTimeout(1, () =>
            {
                Destroy(gameObject);
            }));
        }
    }
}

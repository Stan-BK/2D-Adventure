using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeChaseState : BaseState
{
    private BeeController currentEnemy;
    private float timer = 0;
    private float attackCD = 2;
    private float attackTimer = 0.5f;
    
    public override void OnEnter(Enemy enemy)
    {
        Debug.Log("Bee: chase player");
        
        currentEnemy = (BeeController)enemy;
        currentEnemy.speed = currentEnemy.chaseSpeed;
        currentEnemy.anim.SetBool("isChase", true);
        timer = currentEnemy.lostTime;
        
        float faceDir = currentEnemy.faceDir;
        float playerFaceDir = PlayerController.Instance.transform.localScale.x;
        
        if ((faceDir > playerFaceDir && faceDir < 0) ||
            (faceDir < playerFaceDir && faceDir > 0))
        {
            
            currentEnemy.ChangeFaceDir();
        }
    }

    public override void LogicUpdate()
    {
        currentEnemy.changeFaceDirWhenChasing();
        
        if (!currentEnemy.FindPlayer())
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = currentEnemy.lostTime;
        }

        if (timer < 0)
        {
            currentEnemy.SwitchState(NPCState.Patrol);
        }
        
        
        if (attackTimer <= 0 && currentEnemy.FindPlayer())
        {
            currentEnemy.isAttack = true;
            currentEnemy.anim.SetTrigger("attack");
            currentEnemy.StopMove();
            attackTimer = attackCD;
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }

        if (currentEnemy.startSprint)
        {
            currentEnemy.Attack();
        }
    }

    public override void PhysicsUpdate()
    {
        if (!currentEnemy.isAttack)
        {
            currentEnemy.Move();
        }
    }

    public override void OnExit()
    {
        Debug.Log("Bee: exit chase");
    }
}

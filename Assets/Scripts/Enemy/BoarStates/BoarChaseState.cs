using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarChaseState : BaseState
{
    private BoarController currentEnemy;
    private float timer = 0;
    public override void OnEnter(Enemy enemy)
    {
        Debug.Log("Boar: chase player");
        
        currentEnemy = (BoarController)enemy;
        currentEnemy.speed = currentEnemy.chaseSpeed;
        currentEnemy.anim.SetBool("walk", false);
        currentEnemy.anim.SetBool("run", true);
        
        timer = currentEnemy.lostTime;
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
    }

    public override void PhysicsUpdate()
    {
        currentEnemy.Move();
    }

    public override void OnExit()
    {
        Debug.Log("Boar: exit chase");
        currentEnemy.anim.SetBool("run", false);
        currentEnemy.anim.SetBool("walk", true);
    }
}

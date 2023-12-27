using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailDefendState : BaseState
{
    private SnailController currentEnemy;
    private float timer = 0;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = (SnailController)enemy;
        currentEnemy.anim.SetTrigger("startDefend");
        currentEnemy.anim.SetBool("walk", false);
        currentEnemy.anim.SetBool("defend", true);
        currentEnemy.Defend();
        timer = currentEnemy.lostTime;
    }

    public override void LogicUpdate()
    {
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
        //
    }

    public override void OnExit()
    {
        currentEnemy.ExitDefend();
        currentEnemy.anim.SetBool("defend", false);
        currentEnemy.anim.SetBool("walk", true);
        Debug.Log("Snail: exit defend");
    }
}

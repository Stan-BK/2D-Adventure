using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeePatrolState : BaseState
{
    private BeeController currentEnemy;
    public override void OnEnter(Enemy enemy)
    {
        Debug.Log("Bee: start patrol");
        currentEnemy = (BeeController)enemy;
        currentEnemy.speed = currentEnemy.patrolSpeed;
        currentEnemy.anim.SetBool("isChase", false);
    }

    public override void LogicUpdate()
    {
        currentEnemy.changeFaceDirWhenPatroling();
        
        if (currentEnemy.FindPlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
        }
    }

    public override void PhysicsUpdate()
    {
        currentEnemy.Move();
    }

    public override void OnExit()
    {
        Debug.Log("Bee: exit patrol");
    }
}

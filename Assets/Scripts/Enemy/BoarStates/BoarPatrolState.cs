using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaseState
{
    private BoarController currentEnemy;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = (BoarController)enemy;
        currentEnemy.faceDir = enemy.transform.localScale.x;
        currentEnemy.speed = currentEnemy.patrolSpeed;
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.FindPlayer())
        {
            currentEnemy.SwitchState(NPCState.Defend);
        }
        
        currentEnemy.changeFaceDirWhenPatroling();
        
    }

    public override void PhysicsUpdate()
    {
        currentEnemy.Move();
    }

    public override void OnExit()
    {
        Debug.Log("Boar: exit patrol");
    }
}

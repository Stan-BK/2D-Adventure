using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailPatrolState : BaseState
{
    private SnailController currentEnemy;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = (SnailController)enemy;
        currentEnemy.faceDir = enemy.transform.localScale.x;
        currentEnemy.speed = currentEnemy.patrolSpeed;
        currentEnemy.anim.SetBool("walk", true);
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.FindPlayer())
        {
            currentEnemy.SwitchState(NPCState.Defend);
        }
        currentEnemy.changeFaceDir();
    }

    public override void PhysicsUpdate()
    {
        currentEnemy.Move();
    }

    public override void OnExit()
    {
        Debug.Log("Snail: exit patrol");
    }
}

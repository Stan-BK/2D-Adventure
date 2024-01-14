using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailController : Enemy
{
    private Character character;
    protected override void Awake()
    {
        base.Awake();
        character = GetComponent<Character>();
        patrolState = new SnailPatrolState();
        defendState = new SnailDefendState();
        anim.SetBool("walk", true);
    }
    
    public void changeFaceDir()
    {
        if (!physicsCheck.isGround || (physicsCheck.touchLeftWall && faceDir < 0) || (physicsCheck.touchRightWall && faceDir > 0))
        {
            canMove = false;
            anim.SetBool("walk", false);
            StartCoroutine(Utils.SetTimeout(1, WaitForWalk));
        }
    }
    
    void WaitForWalk()
    {
        ChangeFaceDir();
        canMove = true;
        anim.SetBool("walk", true);
    }

    public void Defend()
    {
        character.invulnerable = true;
        rb.velocity = Vector2.zero;
    }

    public void ExitDefend()
    {
        character.invulnerable = false;
    }

    public override void GetHurt()
    {
        base.GetHurt();
        SwitchState(NPCState.Defend);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCState
{
    Patrol,
    Chase,
    Defend
}

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    public PhysicsCheck physicsCheck;
    protected Vector3 localScale;

    [Header("敌人行为")]
    public float speed;
    public float patrolSpeed;
    public float chaseSpeed;
    public float faceDir;
    public Animator anim;
    public bool canMove = true;
    public bool isDead = false;

    [Header("追击检测")]
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayer;
    public float lostTime;
    
    protected BaseState currentState;
    protected BaseState patrolState;
    protected BaseState chaseState;
    protected BaseState defendState;
    
    #region 生命周期
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        localScale = transform.localScale;
        faceDir = localScale.x;
    }

    protected void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
            currentState.LogicUpdate();
        else
            StopMove();
    }

    private void FixedUpdate()
    {
        if (canMove)
            currentState.PhysicsUpdate();
        if (isDead)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region 敌人行为
    public virtual void Move()
    {
        rb.velocity = new Vector2(speed * faceDir * Time.deltaTime, 0);
    }

    public void StopMove()
    {
        rb.velocity = new Vector2(0, 0);
    }

    public bool FindPlayer()
    {
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, new Vector2(transform.localScale.x, 0), checkDistance, attackLayer);
    }

    public void ChangeFaceDir()
    {
        faceDir = -faceDir;
        transform.localScale = new Vector3(faceDir, localScale.y, localScale.z);
    }
    #endregion
    
    #region 状态管理
    public void SwitchState(NPCState state)
    {
        var s = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            NPCState.Defend => defendState,
            _ => null
        }; 
        
        currentState.OnExit();
        currentState = s;
        currentState.OnEnter(this);
    }
    #endregion
    
    #region 事件函数
    public virtual void GetHurt()
    {
        anim.SetTrigger("hurt");
        canMove = false;
        StopMove();
    }

    public virtual void EnemyDead()
    {
        canMove = false;
        anim.SetTrigger("dead");
        StopMove();
    }
    #endregion

    #region Gizmos

    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset + new Vector3(checkDistance * transform.localScale.x, 0), checkSize.x);
        Gizmos.DrawCube(transform.position + (Vector3)centerOffset + new Vector3(checkDistance * transform.localScale.x, 0), checkSize);
    }

    #endregion
}

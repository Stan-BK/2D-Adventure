using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PhysicsCheck _physicsCheck;
    private PlayerController playerController;
    private Character character;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _physicsCheck = GetComponent<PhysicsCheck>();
        playerController = GetComponent<PlayerController>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnim();
    }

    void SetAnim()
    {
        Vector2 v2 = rb.velocity;
        anim.SetFloat("velocityX", Mathf.Abs(v2.x));
        anim.SetFloat("velocityY", v2.y);
        anim.SetBool("isGround", _physicsCheck.isGround);
        anim.SetBool("isDead", playerController.isDead);
        anim.SetBool("invulnerable", character.invulnerable);
        anim.SetBool("isAttack", playerController.isAttack);
        anim.SetBool("isSlide", playerController.isSlide);
    }

    public void PlayerHurt()
    {
        anim.SetTrigger("hurt");
    }

    public void PlayerAttack()
    {
        anim.SetTrigger("attack");
    }

    public void PlayerSlide()
    {
        anim.SetTrigger("slide");
    }
}

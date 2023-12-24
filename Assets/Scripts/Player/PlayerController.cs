using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public LoadSceneSO loadSceneSO;
    public LoadedSceneSO loadedSceneSO;
    public PlayerInputControl InputControl;
    public Rigidbody2D rb;
    private PlayerAnimation playerAnimation;
    private PhysicsCheck isGroundCheck;
    public PhysicsMaterial2D friction;
    public PhysicsMaterial2D noFriction;
    public CharacterEventSO characterEventSO;
    
    [Header("角色行为属性")]
    public Vector2 inputDirection;
    public float jumpForce = 16;
    public float speed = 200;
    public float hurtForce = 2;
    public float slideForce = 2;
    public bool isHurt = false;
    public bool isDead = false;
    public bool isAttack = false;
    public bool isSlide = false;
    public float slideCD;
    
    private bool canSlide = true;

    #region 生命周期函数
    private void Awake()
    {
        isGroundCheck = GetComponent<PhysicsCheck>();
        InputControl = new PlayerInputControl();
        playerAnimation = GetComponent<PlayerAnimation>();
        isGroundCheck = GetComponent<PhysicsCheck>();

        InputControl.Player.Jump.started += PlayerJump;
        InputControl.Player.Attack.started += PlayerAttack;
        InputControl.Player.Sliding.started += PlayerSliding;
    }

    // Update is called once per frame
    void Update()
    {
        inputDirection = InputControl.Player.Move.ReadValue<Vector2>();
        PlayerAnimateMaterial();
    }
    
    private void OnEnable()
    {
        InputControl.Enable();
        loadSceneSO.OnLoadScene += OnLoadScene;
        loadedSceneSO.OnLoadedScene += OnLoadedScene;
    }

    private void OnDisable()
    {
        InputControl.Disable();
        loadSceneSO.OnLoadScene -= OnLoadScene;
        loadedSceneSO.OnLoadedScene -= OnLoadedScene;
    }

    private void OnLoadedScene(GameSceneSO sceneSO)
    {
        InputControl.Player.Enable();
    }

    private void OnLoadScene(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        InputControl.Player.Disable();
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isAttack && !isSlide)
            PlayerMove();
        else if (isAttack)
        {
            rb.velocity = Vector2.zero;
        }
    }
    #endregion

    #region 角色行为
    void PlayerMove()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        if (rb.velocity.x != 0)
        {
            transform.localScale = new Vector3(rb.velocity.x > 0 ? 1 : -1, 1, 1);
        }
    }

    void PlayerJump(InputAction.CallbackContext cbc)
    {
        if (isGroundCheck.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    
    void PlayerAttack(InputAction.CallbackContext cbc)
    {
        if (!isGroundCheck.isGround) return;
        playerAnimation.PlayerAttack();
        isAttack = true;
    }

    void PlayerSliding(InputAction.CallbackContext cbc)
    {
        if (isSlide || !canSlide || !isGroundCheck.isGround) return;
        isAttack = false;
        isSlide = true;
        playerAnimation.PlayerSlide();
        rb.velocity = new Vector2(transform.localScale.x > 0 ? slideForce : -slideForce, 0);
        
        characterEventSO.ChangeSlideCD(this);
        
        StartCoroutine(PlayerCanSlide());
    }

    void PlayerAnimateMaterial()
    {
        rb.sharedMaterial = isGroundCheck.isGround ? friction : noFriction;
    }

    IEnumerator PlayerCanSlide()
    {
        canSlide = false;
        yield return new WaitForSeconds(slideCD);
        canSlide = true;
    }
    #endregion
    
    #region 事件函数
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
        InputControl.Player.Disable();
    }

    public void PlayerResurrect()
    {
        isDead = false;
    }
    #endregion
}

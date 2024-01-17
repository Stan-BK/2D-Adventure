using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

enum JumpTouchState
{
    Left,
    Right,
    None
}

public class PlayerController : Singleton<PlayerController>, PropsCallback
{
    public LoadSceneSO loadSceneSO;
    public LoadedSceneSO loadedSceneSO;
    public PlayerInputControl InputControl;
    public Rigidbody2D rb;
    private PlayerAnimation playerAnimation;
    private PhysicsCheck physicsCheck;
    public PhysicsMaterial2D friction;
    public PhysicsMaterial2D noFriction;
    public PhysicsMaterial2D climb;
    public CharacterEventSO characterEventSO;
    public Light2D Light;
    
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
    [Tooltip("角色装备")] public List<PropSO> props;

    [Header("角色音效")] 
    public AudioClip FootAudio;
    public AudioClip SwooshAudio;
    
    
    private bool canJump = true;
    private bool canSlide = true;
    private bool isWalking = false;
    private JumpTouchState _jumpTouchState = JumpTouchState.None;
    private AudioTrigger AudioTrigger;

    #region 生命周期函数
    protected override void Awake()
    {
        base.Awake();
        physicsCheck = GetComponent<PhysicsCheck>();
        InputControl = new PlayerInputControl();
        playerAnimation = GetComponent<PlayerAnimation>();
        physicsCheck = GetComponent<PhysicsCheck>();
        AudioTrigger = GetComponent<AudioTrigger>();

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
        // DealWithLight(sceneSO);
        if (sceneSO.sceneName == SceneName.Menu)
        {
            RemoveProps();
        }
    }

    private void OnLoadScene(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        InputControl.Player.Disable();
    }

    private void FixedUpdate()
    {
        // 受伤/攻击/滑行过程无法默认移动
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
            if (physicsCheck.isGround && !isWalking && !isSlide)
            {
                AudioTrigger.playAudio(FootAudio, true);
                isWalking = true;
            }
            else if (!physicsCheck.isGround || isSlide)
            {
                isWalking = false;
                AudioTrigger.pauseAudio();
            }
        }
        else
        {
            isWalking = false;
            AudioTrigger.pauseAudio();
        }
    }

    void PlayerJump(InputAction.CallbackContext cbc)
    {
        bool isLeft = physicsCheck.touchLeftWall;
        bool isRight = physicsCheck.touchRightWall;
        var currentJumpState = isLeft ? JumpTouchState.Left :
            isRight ? JumpTouchState.Right : JumpTouchState.None;
        
        if (currentJumpState != JumpTouchState.None && currentJumpState != _jumpTouchState)
        {
            canJump = true;
            _jumpTouchState = currentJumpState;
        }
        else
        {
            canJump = false;
        }

        if (physicsCheck.isGround)
        {
            _jumpTouchState = currentJumpState;
            canJump = true;
        }
        
        if (canJump)
        {
            isWalking = false;
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, 0);
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    
    void PlayerAttack(InputAction.CallbackContext cbc)
    {
        // 跳跃或受伤时无法攻击
        if (!physicsCheck.isGround || isHurt) return;
        playerAnimation.PlayerAttack();
        isAttack = true;
    }

    void PlayerSliding(InputAction.CallbackContext cbc)
    {
        if (isSlide || !canSlide || !physicsCheck.isGround) return;
        AudioTrigger.playAudio(SwooshAudio, false);
        isWalking = false;
        isAttack = false;
        isSlide = true;
        playerAnimation.PlayerSlide();
        rb.velocity = new Vector2(transform.localScale.x > 0 ? slideForce : -slideForce, 0);
        
        characterEventSO.ChangeSlideCD(this);
        
        StartCoroutine(PlayerCanSlide());
    }

    void PlayerAnimateMaterial()
    {
        var x = inputDirection.x;
        bool isClimb = false;
        if (physicsCheck.touchLeftWall && x < 0)
        {
            inputDirection.x = -0.3F;
            isClimb = true;
        } else if (physicsCheck.touchRightWall && x > 0)
        {
            inputDirection.x = 0.3F;
            isClimb = true;
        }
        rb.sharedMaterial = physicsCheck.isGround ? friction : isClimb ? climb : noFriction;
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

    private void TurnOnLight()
    {
        Light.intensity = 1;
        Light.pointLightOuterRadius = 4;
        Light.gameObject.SetActive(true);
    }

    private void TurnOffLight()
    {
        Light.gameObject.SetActive(false);
    }
    #endregion

    // private void DealWithLight(GameSceneSO sceneSO)
    // {
    //     if (sceneSO.sceneName == SceneName.Cave)
    //     {
    //         TurnOnLight();
    //     }
    //     else
    //     {
    //         TurnOffLight();
    //     }
    // }

    public void RemoveProps()
    {
        foreach (var prop in props)
        {
            prop.RemoveProp();
        }
        props.Clear();
    }

    #region 装备

    public void GetFlashLight(PropSO prop)
    {
        props.Add(prop);
        Light.intensity = 4;
        Light.pointLightOuterRadius = 10;
    }

    public void RemoveFlashLight()
    {
        Light.intensity = 1;
        Light.pointLightOuterRadius = 4;
    }
    #endregion
}

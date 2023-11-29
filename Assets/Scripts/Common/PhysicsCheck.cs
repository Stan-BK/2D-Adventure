using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("状态列表")]
    public bool isGround;
    public bool touchLeftWall;
    public bool touchRightWall;
    
    [Header("检测参数")]
    public LayerMask layerMask;
    public float checkRadius = 0.2f;
    
    [Header("检测中心偏移量")]
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;

    void Start()
    {
        OverLap();
    }
    
    // Update is called once per frame
    void Update()
    {
        OverLap();
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
        
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRadius);
    }

    void OverLap()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset * transform.localScale, checkRadius, layerMask);

        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRadius, layerMask);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRadius, layerMask);
    }
}

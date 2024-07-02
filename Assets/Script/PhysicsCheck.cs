using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public static PhysicsCheck Instance;
    public LayerMask GroundedLayerMask;
    public LayerMask BarrierLayerMask;
    public LayerMask WindLayerMask;
    public bool isGrounded;
    public bool isXue;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        IsGrounded();
    }
    private void IsGrounded()
    {
        var raycastAll = Physics2D.OverlapBoxAll(transform.position + -Vector3.up * 0.5f, new Vector2(0.2f, 0.2f), 0, GroundedLayerMask);
        if (raycastAll.Length > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            isXue = false;
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + -Vector3.up * 0.5f, new Vector2(0.2f, 0.2f));
    }
}

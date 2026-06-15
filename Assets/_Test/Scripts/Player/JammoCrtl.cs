using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JammoCrtl : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotateSpeed = 10f;

    [SerializeField] private Vector2 limitX = new Vector2(-10f, 10f);
    [SerializeField] private Vector2 limitZ = new Vector2(-6f, 6f);

    private Vector3 moveDir;

    private Rigidbody rb;
    private Animator ani;
    private BallDetect ballDetect;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();
        ballDetect = GetComponentInChildren<BallDetect>();
    }
    void Update()
    {
        DirHAndle();

      
    }

    void FixedUpdate()
    {
        MoveHandle();

        RotationHandle();

        LimitPosition();
        
        StopPhysicsRotation();
    }
    
    #region Character Handle
    private void MoveHandle()
    {
        if (rb == null) return ;
        rb.velocity = moveDir * speed;
    }

    private void DirHAndle()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        moveDir = new Vector3(x, 0, z).normalized;

        if (ani == null) return;
        ani.SetFloat("Blend", moveDir.magnitude);
    }

    private void RotationHandle()
    {
        if (rb == null) return;
        if (moveDir.magnitude <= 0.1f) return;

        Quaternion targetRotation = Quaternion.LookRotation(moveDir);

        Quaternion newRotation = Quaternion.Lerp(
            rb.rotation,
            targetRotation,
            rotateSpeed * Time.fixedDeltaTime
        );

        rb.MoveRotation(newRotation);
    }

    private void StopPhysicsRotation()
    {
        if (rb == null) return;

        rb.angularVelocity = Vector3.zero;
    }

    #endregion

    #region Limited Area handle
    private void LimitPosition()
    {
        Vector3 currentPosition = transform.position;

        currentPosition.x = Mathf.Clamp(currentPosition.x, limitX.x, limitX.y);
        currentPosition.z = Mathf.Clamp(currentPosition.z, limitZ.x, limitZ.y);

        transform.position = currentPosition;
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 center = new Vector3(
            (limitX.x + limitX.y) / 2f,
            transform.position.y,
            (limitZ.x + limitZ.y) / 2f
        );

        Vector3 size = new Vector3(
            limitX.y - limitX.x,
            0.1f,
            limitZ.y - limitZ.x
        );

        Gizmos.color = new Color(0f, 1f, 0f, 0.15f);
        Gizmos.DrawCube(center, size);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, size);
    }

    #endregion


}

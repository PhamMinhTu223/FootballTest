using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JammoCrtl : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float distanceToKick = 1f;
    [SerializeField] private float kickForce = 15f;

    private Vector3 moveDir;

    private Rigidbody rb;
    private Animator ani;
    public BallDetect BallDetect{ get; private set; }

    public float DistanceToKick => distanceToKick;
    public float KickForce => kickForce;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();
        BallDetect = GetComponentInChildren<BallDetect>();
    }
    void Update()
    {
        DirHAndle();

        if (BallDetect == null) return;

        BallDetect.GetNearestBall();
        BallDetect.GetFurthestBall();

    }


    void FixedUpdate()
    {
        MoveHandle();

        RotationHandle();
        
        StopPhysicsRotation();
    }

    #region Character Handle
   
    private void MoveHandle()
    {
        if (rb == null) return ;

        Vector3 nextPosition = rb.position + moveDir * speed * Time.fixedDeltaTime;
        rb.MovePosition(nextPosition);
    }

    private void DirHAndle()
    {
        float x = 0f;
        float z = 0f;

        if (Input.GetKey(KeyCode.A)) x -= 1f;
        if (Input.GetKey(KeyCode.D)) x += 1f;
        if (Input.GetKey(KeyCode.S)) z -= 1f;
        if (Input.GetKey(KeyCode.W)) z += 1f;
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
}


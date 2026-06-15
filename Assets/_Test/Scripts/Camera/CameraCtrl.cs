using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    
    [SerializeField] private Vector3 offset = new Vector3(0f, 12f, -8f);
    [SerializeField] private float smoothSpeed = 8f;
    [SerializeField] private Vector3 topDownRotation = new Vector3(60f, 0f, 0f);

    private Transform target;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            target = GameManager.Instance.Jammo.transform;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.rotation = Quaternion.Euler(topDownRotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedArea : MonoBehaviour
{
    [SerializeField] private Vector2 limitX = new Vector2(-10f, 10f);
    [SerializeField] private Vector2 limitZ = new Vector2(-6f, 6f);

    void Update()
    {
        LimitPosition();
    }

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

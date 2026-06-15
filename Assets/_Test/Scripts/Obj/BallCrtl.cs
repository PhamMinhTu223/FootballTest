using UnityEngine;

public class BallCrtl : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public bool Kick(Vector3 direction, float force)
    {
        if (rb == null) return false;
        if (direction == Vector3.zero) return false;
        rb.AddForce(direction.normalized * force, ForceMode.Impulse);
        return true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.TryGetComponent(out GoalCtrl goal)) return;
        goal.PlayGoalEffect();
    }
}

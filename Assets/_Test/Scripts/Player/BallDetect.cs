using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDetect : MonoBehaviour
{
    [SerializeField] private List<BallCrtl> ballCrtls = new List<BallCrtl>();
    
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BallCrtl ballCrtl))
        {
            if (!ballCrtls.Contains(ballCrtl))
            {
                ballCrtls.Add(ballCrtl);
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out BallCrtl ballCrtl))
        {
            if (!ballCrtls.Contains(ballCrtl))
            {
                ballCrtls.Add(ballCrtl);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out BallCrtl ballCrtl))
        {
            if (ballCrtls.Contains(ballCrtl))
            {
                ballCrtls.Remove(ballCrtl);
            }
        }
    }
}

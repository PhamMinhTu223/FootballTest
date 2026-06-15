using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDetect : MonoBehaviour
{
    public Action<BallCrtl> OnBallNearestDetect;
    public Action<BallCrtl> OnBallFurthestDetect;

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

    public BallCrtl GetNearestBall()
    {
        if (ballCrtls.Count <= 0)
        {
            OnBallNearestDetect?.Invoke(null);
            return null;
        }

        BallCrtl nearestBall = ballCrtls[0];
        float nearestDistance = Vector3.Distance(transform.position, nearestBall.transform.position);
        for (int i = 1; i < ballCrtls.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, ballCrtls[i].transform.position);
            if (distance < nearestDistance)
            {
                nearestBall = ballCrtls[i];
                nearestDistance = distance;
            }
        }
        OnBallNearestDetect?.Invoke(nearestBall);
        return nearestBall;
    }

    public BallCrtl GetFurthestBall()
    {
        if (ballCrtls.Count <= 0)
        {
            OnBallFurthestDetect?.Invoke(null);
            return null;
        }

        BallCrtl farthestBall = ballCrtls[0];
        float farthestDistance = Vector3.Distance(transform.position, farthestBall.transform.position);
        for (int i = 1; i < ballCrtls.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, ballCrtls[i].transform.position);
            if (distance > farthestDistance)
            {
                farthestBall = ballCrtls[i];
                farthestDistance = distance;
            }
        }
        OnBallFurthestDetect?.Invoke(farthestBall);
        return farthestBall;
    }
}

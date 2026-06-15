using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private JammoCrtl jammo;
    [SerializeField] private CameraCtrl cam;

    [SerializeField] private Transform goal_1;
    [SerializeField] private Transform goal_2;
    [SerializeField] private Button kickButton;
    [SerializeField] private Button autoKickButton;
    [SerializeField] private float goalDistance = 2f;
    [SerializeField] private float cameraReturnDelay = 2f;



    public JammoCrtl Jammo => jammo;
    public CameraCtrl Cam => cam;

    public BallCrtl NearestBall { get; private set; }
    public BallCrtl FurthestBall { get; private set; }

    private void Awake() {
        Instance = this;
    }
   
    private void Start() {

        SetKickButton(false);

        jammo.BallDetect.OnBallNearestDetect += Event_NearestBallDetect;
        jammo.BallDetect.OnBallFurthestDetect += Event_FurthestBallDetect;
    }

    private void OnDestroy() {
        if (jammo == null || jammo.BallDetect == null) return;

        jammo.BallDetect.OnBallNearestDetect -= Event_NearestBallDetect;
        jammo.BallDetect.OnBallFurthestDetect -= Event_FurthestBallDetect;
    }

    private void Event_NearestBallDetect(BallCrtl ball)
    {
        NearestBall = ball;

        bool canKick = ball != null && Vector3.Distance(jammo.transform.position, ball.transform.position) <= jammo.DistanceToKick;
        SetKickButton(canKick);
    }

    private void Event_FurthestBallDetect(BallCrtl ball)
    {
        FurthestBall = ball;
    }

    #region Kick handle
    
    public void BallKickHandle()
    {
        if (NearestBall == null) return;

        KickBallToGoal(NearestBall);
    }

    public void AutoBallKickHandle()
    {
        if (FurthestBall == null) return;

        KickBallToGoal(FurthestBall);
    }
    private void KickBallToGoal(BallCrtl ball)
    {
        Transform targetGoal = GetNearestGoal(ball.transform.position);
        if (targetGoal == null) return;

        Vector3 kickDir = (targetGoal.position - ball.transform.position).normalized;
        if (!ball.Kick(kickDir, jammo.KickForce)) return;
       
        StartCoroutine(CameraFollowHandle(ball));
        
    }
    #endregion 

    private IEnumerator CameraFollowHandle(BallCrtl ball)
    {
        if (cam == null || ball == null || jammo == null) yield break;

        cam.Follow(ball.transform);
        yield return new WaitForSeconds(cameraReturnDelay);
        cam.Follow(jammo.transform);
        
    }

    private Transform GetNearestGoal(Vector3 position)
    {
        if(goal_1 == null && goal_2 == null) return null;

        if (goal_1 == null) return goal_2;
        if (goal_2 == null) return goal_1;

        float distanceToGoal1 = Vector3.Distance(position, goal_1.position);
        float distanceToGoal2 = Vector3.Distance(position, goal_2.position);

        return distanceToGoal1 < distanceToGoal2 ? goal_1 : goal_2;
    }


    private void SetKickButton(bool isActive)
    {
        if (kickButton == null) return;

        kickButton.gameObject.SetActive(isActive);
    }

   

}

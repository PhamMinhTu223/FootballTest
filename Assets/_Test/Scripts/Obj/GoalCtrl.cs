using System.Collections;
using UnityEngine;

public class GoalCtrl : MonoBehaviour
{
    [SerializeField] private ParticleSystem goalEffect;
    [SerializeField] private float effectDuration = 2f;

    private void Awake()
    {
        if (goalEffect == null) return;
        goalEffect.Stop();
    }

    public void PlayGoalEffect()=> StartCoroutine(GoalEffectHandle());
   

    private IEnumerator GoalEffectHandle()
    {
        if(goalEffect == null) yield break;

        goalEffect.Play();
        yield return new WaitForSeconds(effectDuration);
        goalEffect.Stop();
    }
}

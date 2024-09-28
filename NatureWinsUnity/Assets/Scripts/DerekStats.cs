using System;
using UnityEditor.PackageManager;
using UnityEngine;
using static CloudStats;

public class DerekStats : MonoBehaviour
{
    [Range(0f, 100f)] public float DerekLoserMeter = 0f;
    public float DerekLoserLookORadius = 1f;

    public enum DerekModeStates
    {
        Standing,
        Walking,
        Suprise,
    }
    [SerializeField] GameObject StandingDerekDisplay;
    [SerializeField] GameObject WalkingDerekDisplay;
    [SerializeField] GameObject SupriseDerekDisplay;
    [SerializeField] GameObject InBetweenDerekDisplay;


    public float DerekWiggleSpeed = 7.5f;
    public float DerekWiggleAngle = 60f;



    public DerekModeStates DerekCurrentMode;
    public DerekModeStates DerekPreviousMode;

    public float InBetweenModesTimeLimit = 0.8f;
    public float InBetweenModesTimeCurrent;
    public bool IsInBetweenModes = false;







    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DerekCurrentMode = DerekModeStates.Walking;

        //displays
        StandingDerekDisplay.SetActive(false);
        WalkingDerekDisplay.SetActive(true);
        SupriseDerekDisplay.SetActive(false);
        InBetweenDerekDisplay.SetActive(false);

        DerekPreviousMode = DerekCurrentMode;
    }

    // Update is called once per frame
    void Update()
    {
        switch (DerekCurrentMode)
        {
            case DerekModeStates.Standing:
                //displays
                StandingDerekDisplay.SetActive(true);
                WalkingDerekDisplay.SetActive(false);
                SupriseDerekDisplay.SetActive(false);
                InBetweenDerekDisplay.SetActive(false);




                CheckAndChangeInbetweenFrame();

                break;

            case DerekModeStates.Walking:
                //displays
                StandingDerekDisplay.SetActive(false);
                WalkingDerekDisplay.SetActive(true);
                SupriseDerekDisplay.SetActive(false);
                InBetweenDerekDisplay.SetActive(false);

                float angle = Mathf.Sin(Time.time * DerekWiggleSpeed) * DerekWiggleAngle;
                WalkingDerekDisplay.transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);

                CheckAndChangeInbetweenFrame();

                break;

            case DerekModeStates.Suprise:
                //displays
                StandingDerekDisplay.SetActive(false);
                WalkingDerekDisplay.SetActive(false);
                SupriseDerekDisplay.SetActive(true);
                InBetweenDerekDisplay.SetActive(false);




                CheckAndChangeInbetweenFrame();

                break;

        }





        Collider[] hitColliders = Physics.OverlapSphere(transform.position, DerekLoserLookORadius);
        foreach (var hitCollider in hitColliders)
        {
            ActivateDerekEffect(hitCollider.gameObject);
        }
    }

    private void CheckAndChangeInbetweenFrame()
    {
        if (DerekPreviousMode != DerekCurrentMode || IsInBetweenModes)
        {
            IsInBetweenModes = true;
            InBetweenModesTimeCurrent += Time.deltaTime;
            //display inbetween model
            StandingDerekDisplay.SetActive(false);
            WalkingDerekDisplay.SetActive(false);
            SupriseDerekDisplay.SetActive(false);
            InBetweenDerekDisplay.SetActive(true);

            if (InBetweenModesTimeCurrent <= InBetweenModesTimeLimit)
            {
                InBetweenModesTimeCurrent = 0;
                IsInBetweenModes = false;
            }
        }
        DerekPreviousMode = DerekCurrentMode;
    }

    private void ActivateDerekEffect(GameObject gameObject)
    {
        IHaveDerekEffect[] derekEffectHavers = gameObject.GetComponents<IHaveDerekEffect>();
        foreach (IHaveDerekEffect derekEffectHaver in derekEffectHavers)
        {
            derekEffectHaver.EffectOnDerek();
        }
    }
}

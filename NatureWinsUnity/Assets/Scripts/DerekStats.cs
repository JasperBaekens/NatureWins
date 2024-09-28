using System;
using UnityEngine;
using static CloudStats;

public class DerekStats : MonoBehaviour
{
    [Range(0f, 100f)] public float DerekLoserMeter = 0f;
    public float DerekLoserLookORadius = 1f;

    private DerekMovement _derekMovement;
    private float _derekMovementSpeedStandard;

   
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

    [SerializeField] public float InBetweenModesTimeLimit = 0.5f;
    private float _inBetweenModesTimeCurrent = 0f;
    public bool IsInBetweenModes = false;

    public float DerekFreezeLimit = 3f;
    public float DerekFreezeLimitCounter;





    private void Awake()
    {
        _derekMovement = FindAnyObjectByType<DerekMovement>();
        _derekMovementSpeedStandard = _derekMovement.DerekMovementSpeed;
    }
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

                _derekMovement.DerekMovementSpeed = 0f;
                
                if(DerekFreezeLimitCounter <= DerekFreezeLimit)
                {
                    DerekFreezeLimitCounter += Time.deltaTime;
                    if (DerekFreezeLimitCounter >= DerekFreezeLimit)
                    {
                        DerekFreezeLimitCounter = 0;
                        DerekCurrentMode = DerekModeStates.Walking;
                    }

                }


                CheckAndChangeInbetweenFrame();

                break;

            case DerekModeStates.Walking:
                //displays
                StandingDerekDisplay.SetActive(false);
                WalkingDerekDisplay.SetActive(true);
                SupriseDerekDisplay.SetActive(false);
                InBetweenDerekDisplay.SetActive(false);

                _derekMovement.DerekMovementSpeed = _derekMovementSpeedStandard;



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

                _derekMovement.DerekMovementSpeed = 0f;





                if (DerekFreezeLimitCounter <= DerekFreezeLimit)
                {
                    DerekFreezeLimitCounter += Time.deltaTime;
                    if (DerekFreezeLimitCounter >= DerekFreezeLimit)
                    {
                        DerekFreezeLimitCounter = 0;
                        DerekCurrentMode = DerekModeStates.Walking;
                    }

                }




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
            _inBetweenModesTimeCurrent += Time.deltaTime;
            //display inbetween model
            StandingDerekDisplay.SetActive(false);
            WalkingDerekDisplay.SetActive(false);
            SupriseDerekDisplay.SetActive(false);
            InBetweenDerekDisplay.SetActive(true);

            if (_inBetweenModesTimeCurrent >= InBetweenModesTimeLimit)
            {
                _inBetweenModesTimeCurrent = 0;
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

using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    private CloudStats _cloudStats;

    //movement
    private Vector2 _movementInput;
    public float VerticalInput;
    public float HorizontalInput;
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _powerUseDecline;
    private float StartZOffset;

    private Vector3 _moveDirection;
    private Vector3 _movementVelocity;
    private Transform _cameraObject;


    //water
    public ParticleSystem ParticlesWater;

    //Elek
    public ParticleSystem ParticlesElek;



    //bar positions
    [SerializeField] private RectTransform _waterSupplyBarPosition;
    [SerializeField] private RectTransform _elekSupplyBarPosition;




    private void Awake()
    {
        _cloudStats = FindAnyObjectByType<CloudStats>();
        _cameraObject = Camera.main.transform;
        StartZOffset = transform.position.z;
    }

    void Update()
    {
        //states
        switch (_cloudStats.CurrentMode)
        {
            case CloudStats.ElementMode.Water:
                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space)) && _cloudStats.WaterSupply > 0)
                {
                    DoParticleEffect(ParticlesWater);

                    _cloudStats.WaterSupply -= _powerUseDecline * Time.deltaTime;


                    //actual stuff
                    RaycastHit[] rayCastHits = Physics.SphereCastAll(transform.position, transform.localScale.x, Vector3.down, Camera.main.farClipPlane);
                    if (rayCastHits.Length > 0)
                    {
                        foreach (RaycastHit hitInfo in rayCastHits)
                        {
                            if (hitInfo.collider != null)
                            {
                                //Debug.Log($"{hitInfo.collider.gameObject.name} got clicked on.");
                                ActivateWaterEffect(hitInfo.collider.gameObject);
                            }
                        }
                    }
                }
                else
                {
                    StopParticleEffect(ParticlesWater);
                    StopParticleEffect(ParticlesElek);
                }
                break;
            case CloudStats.ElementMode.Elek:
                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space)) && _cloudStats.ElekSupply > 0)
                {
                    DoParticleEffect(ParticlesElek);
                    _cloudStats.ElekSupply -= _powerUseDecline * Time.deltaTime;



                    //actual stuff
                    RaycastHit[] rayCastHits = Physics.SphereCastAll(transform.position, transform.localScale.x, Vector3.down, Camera.main.farClipPlane);
                    if (rayCastHits.Length > 0)
                    {
                        foreach (RaycastHit hitInfo in rayCastHits)
                        {
                            if (hitInfo.collider != null)
                            {
                                //Debug.Log($"{hitInfo.collider.gameObject.name} got clicked on.");
                                ActivateElekEffect(hitInfo.collider.gameObject);
                            }
                        }
                    }
                }
                else
                {
                    StopParticleEffect(ParticlesWater);
                    StopParticleEffect(ParticlesElek);
                }

                break;
        }

        //switch states
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _cloudStats.CurrentMode = CloudStats.ElementMode.Water;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _cloudStats.CurrentMode = CloudStats.ElementMode.Elek;
        }



        //movemntInput
        _movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        VerticalInput = _movementInput.y;
        HorizontalInput = _movementInput.x;

        //movement //not smooth check fixedupdate for better positioning maybe to apply movement
        _moveDirection = _cameraObject.up * VerticalInput;
        _moveDirection += _cameraObject.right * HorizontalInput;
        _moveDirection.Normalize();
        _movementVelocity = _moveDirection * _movementSpeed * Time.deltaTime;
        transform.position += _movementVelocity;

        //Quickly made no out of edges stuff
        if (Camera.main.WorldToViewportPoint(transform.position).y > 1)
        {
            transform.position = new Vector3(transform.position.x, Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, Camera.main.WorldToViewportPoint(transform.position).z)).y, 0);
        }
        if (Camera.main.WorldToViewportPoint(transform.position).y < 0)
        {
            transform.position = new Vector3(transform.position.x, Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0f, Camera.main.WorldToViewportPoint(transform.position).z)).y, 0);
        }
        if (Camera.main.WorldToViewportPoint(transform.position).x < 0)
        {
            transform.position = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(0f, 0.5f, Camera.main.WorldToViewportPoint(transform.position).z)).x, transform.position.y, 0);
        }
        if (Camera.main.WorldToViewportPoint(transform.position).x > 1)
        {
            transform.position = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(1f, 0.5f, Camera.main.WorldToViewportPoint(transform.position).z)).x, transform.position.y, 0);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, StartZOffset);


        //change position of water supply

        _waterSupplyBarPosition.anchoredPosition = new Vector2(Camera.main.WorldToScreenPoint(transform.position).x, Camera.main.WorldToScreenPoint(transform.position).y -1 * 10f);
        _elekSupplyBarPosition.anchoredPosition = new Vector2(Camera.main.WorldToScreenPoint(transform.position).x, Camera.main.WorldToScreenPoint(transform.position).y - 1 * 50f);

    }

    private void DoParticleEffect(ParticleSystem pS)
    {
        ParticleSystem.EmissionModule emission = pS.emission;
        emission.rateOverTime = 10f;
    }
    private void StopParticleEffect(ParticleSystem pS)
    {
        ParticleSystem.EmissionModule emission = pS.emission;
        emission.rateOverTime = 0f;
    }

    private void ActivateWaterEffect(GameObject gameObject)
    {
        IHaveWaterEffect[] waterUsers = gameObject.GetComponents<IHaveWaterEffect>();
        foreach (IHaveWaterEffect waterUser in waterUsers)
        {
            Debug.Log($"{waterUser} got activated");
            waterUser.DoWaterEffect();
        }
    }
    private void ActivateElekEffect(GameObject gameObject)
    {
        IHaveElekEffect[] elekUsers = gameObject.GetComponents<IHaveElekEffect>();
        foreach (IHaveElekEffect elekUser in elekUsers)
        {
            Debug.Log($"{elekUser} got activated");
            elekUser.DoElekEffect();
        }
    }

}

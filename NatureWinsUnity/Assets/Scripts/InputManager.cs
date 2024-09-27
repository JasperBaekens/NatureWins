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
                    ParticlesWater.Play();
                    _cloudStats.WaterSupply -= _powerUseDecline * Time.deltaTime;

                    //actual stuff
                    if (Physics.SphereCast(transform.position, transform.localScale.x, Vector3.down, out RaycastHit hitInfo, Camera.main.farClipPlane))
                    {
                        if (hitInfo.collider != null)
                        {
                            //Debug.Log($"{hitInfo.collider.gameObject.name} got clicked on.");
                            ActivateWaterEffect(hitInfo.collider.gameObject);
                        }
                    }
                }
                break;
            case CloudStats.ElementMode.Elek:
                if (Input.GetKey(KeyCode.Space) && _cloudStats.ElekSupply > 0)
                {
                    _cloudStats.ElekSupply -= _powerUseDecline * Time.deltaTime;
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
            transform.position = new Vector3(transform.position.x, Camera.main.ViewportToWorldPoint(new Vector3(0.5f,1f, Camera.main.WorldToViewportPoint(transform.position).z)).y, 0);
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


    }

    private void ActivateWaterEffect(GameObject gameObject)
    {
        IHaveWaterEffect[] waterUsers = gameObject.GetComponents<IHaveWaterEffect>();
        foreach (IHaveWaterEffect waterUser in waterUsers)
        {
            waterUser.DoWaterEffect();
        }
    }
}

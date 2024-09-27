using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    //movement
    private Vector2 _movementInput;
    public float VerticalInput;
    public float HorizontalInput;
    [SerializeField] private float _movementSpeed = 5f;


    private CharacterController _playerCharacterController;
    private Vector3 _moveDirection;
    private Vector3 _movementVelocity;
    private Transform _cameraObject;



    private void Awake()
    {
        _cameraObject = Camera.main.transform;
    }
    void Start()
    {
        
    }

    void Update()
    {
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


    }
}

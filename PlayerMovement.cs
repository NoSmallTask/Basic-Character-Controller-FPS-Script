using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

   
    [SerializeField]
    private float _speed = 12f;
    private InputMaster _inputMaster;
    private CharacterController _playerCharacterControllerComponent;
    private Vector2 _moveInput;
    private Vector3 _playerVelocity;


    #region Jumping Variables
    [HideInInspector]
    public float gravity = -10f;
    [HideInInspector]
    public Transform groundCheck;
    [HideInInspector]
    public float groundDistance = 0.1f;
    [HideInInspector]
    public LayerMask groundMask;
    [HideInInspector]
    public float jumpHeight = 2f;
    #endregion

    #region Input System Initialization
    void Awake()
    {
        _inputMaster = new InputMaster();
        if (_inputMaster == null)
        {
            Debug.LogError("Input Master Class on Player is Null");
        }

        _playerCharacterControllerComponent = gameObject.GetComponent<CharacterController>();
        if (_playerCharacterControllerComponent == null)
        {
            Debug.LogError("Character Controller Reference on Player Movement Script is Null");
        }

        Debug.Log(_inputMaster);

    }

    private void OnEnable()
    {
        _inputMaster.Player.Enable();
    }

    private void OnDisable()
    {
        _inputMaster.Player.Disable();
    }
    #endregion

    private void Update()
    {
        #region Jumping
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //if (isGrounded && velocity.y < 0)
        //{
        //velocity.y = -2f;
        //}

        //if(jumpPressed && isGrounded)
        //{
        //velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        //}

        //velocity.y += gravity * Time.deltaTime;
        #endregion

        _moveInput.x = _inputMaster.Player.Movement.ReadValue<Vector2>().x;
        _moveInput.y = _inputMaster.Player.Movement.ReadValue<Vector2>().y;

        //y is how it reads from the "2d axis" in the input. It is actually translating across the z axis in game space.
        _playerVelocity = new Vector3(_moveInput.x * _speed , 0f, _moveInput.y * _speed);
        _playerVelocity = transform.TransformDirection(_playerVelocity);
        _playerCharacterControllerComponent.Move(_playerVelocity * Time.deltaTime);

    }
}

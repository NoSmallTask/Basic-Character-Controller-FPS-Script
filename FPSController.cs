using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{

    private InputMaster _inputMaster;

    #region Input System Initialization
    void Awake()
    {
        _inputMaster = new InputMaster();
        if (_inputMaster == null)
        {
            Debug.LogError("Input Master Class on Player is Null");
        }

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

    public Transform playerBody;

    public float mouseXSensitivity = 15f;
    public float mouseYSensitivity = .1f;

    float mouseX, mouseY;

    [SerializeField]
    private float xClamp = 85f;
    float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void RecieveMouseInput (Vector2 mouseInput)
    {
        mouseX = mouseInput.x * mouseXSensitivity;
        mouseY = mouseInput.y * mouseYSensitivity;


    }

    private void Update()
    {
        Vector2 mouseInput;

        mouseInput.x = _inputMaster.Player.MouseX.ReadValue<float>();
        mouseInput.y = _inputMaster.Player.MouseY.ReadValue<float>();
        RecieveMouseInput(mouseInput);

        playerBody.transform.Rotate(Vector3.up, mouseX * Time.deltaTime);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        gameObject.transform.eulerAngles = targetRotation;
    }
}

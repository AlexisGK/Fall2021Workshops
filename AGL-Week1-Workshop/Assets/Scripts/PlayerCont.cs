using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCont : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private GroundChecker groundChecker;
    [SerializeField] private float jumpSpeed = 1f;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float sprintingMultiplier;

    public bool isSprinting = true;

    private Vector2 _currentInput;
    private Vector3 _currentVelocity;

    public bool facingRight;

    private void StoreCurrentInput()
    {
        StoreInput(KeyCode.UpArrow, Vector2.up);
        StoreInput(KeyCode.LeftArrow, Vector2.left);
        StoreInput(KeyCode.DownArrow, Vector2.down);
        StoreInput(KeyCode.RightArrow, Vector2.right);

        _currentInput.x = Input.GetAxisRaw("Horizontal");
        // _currentInput.y = Input.GetAxisRaw("Vertical");

        _currentInput.Normalize();
    }

    private void StoreInput(KeyCode key, Vector2 direction)
    {
        bool keyUp = Input.GetKeyUp(key);
        bool keyDown = Input.GetKeyDown(key);

        if (keyDown == true)
            _currentInput += direction;

        if (keyUp == true)
            _currentInput -= direction;
    }

    private void MoveWithInput()
    {
        _currentVelocity.x = _currentInput.x * movementSpeed;
        //_currentVelocity.y = _currentInput.y * movementSpeed;

    }

    private void OnGUI()
    {
        GUILayout.Label($"Current Input: {_currentInput}");
        GUILayout.Label($"Current Speed: {playerRigidbody.velocity.magnitude}");
        GUILayout.Label($"Is Grounded: {groundChecker.IsGrounded}");
        GUILayout.Label($"Velocity: {playerRigidbody.velocity}");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StoreCurrentInput();
        MoveWithInput();

        ApplyGravity();

        if (Input.GetKeyDown(KeyCode.Space))
            TryToJump();

        Flip();

        Sprint();

        playerRigidbody.velocity = _currentVelocity;
    }

    private void ApplyGravity()
    {
        if (groundChecker.IsGrounded && _currentVelocity.y < 0) {
            _currentVelocity.y = 0;
        } else {
            _currentVelocity.y -= gravity * Time.deltaTime;
        }
    }

    private void TryToJump()
    {
        if (groundChecker.IsGrounded)
            _currentVelocity.y = jumpSpeed;
    }

    private void Flip()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (isSprinting)
            {
                movementSpeed *= sprintingMultiplier;
                playerRigidbody.velocity = _currentInput.normalized * movementSpeed;
            }
        }
    }
}
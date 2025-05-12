using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHander : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;
    public Vector2 rawMovementInput { get; private set; }
    public  Vector2 rawDashDirectionInput { get; private set; }
    public Vector2Int dashDirectionInput { get; private set; }
    public int NormInputX { get; private set; }// Giá trị x đã được chuẩn hóa thành số nguyên (-1, 0, 1)
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool jumpInputStop { get; private set; }
    public bool grabInput { get; private set; }
    public bool dashInput { get; private set; }
    public bool dashInputStop { get; private set; }

    public float jumpinputStartTime;
    public float dashInputStartTime;
    [SerializeField]
    public float inputHoldTime= 0.2f;
    public bool[] attackInput { get; private set; }

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        attackInput = new bool[count];
        cam = Camera.main;
    }
    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }
    public void OnMoveInput (InputAction.CallbackContext context)
    {
        rawMovementInput = context.ReadValue<Vector2>();// doc gia tri dau vao 
        NormInputX = Mathf.RoundToInt(rawMovementInput.x);
        NormInputY = Mathf.RoundToInt(rawMovementInput.y);
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            jumpInputStop = false;

            jumpinputStartTime = Time.time;
        }
       else if (context.canceled)
        {
            jumpInputStop = true;
        }
    
    }
    public void OnPrimaryAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            attackInput[(int)CombatInputs.primary] = true;
        }
        else if (context.canceled)
        {
            attackInput[(int)CombatInputs.primary] = false;
        }
    }
    public void OnsecondaryAttack(InputAction.CallbackContext context)
    {
        if (context.started) 
        {
            attackInput[(int)CombatInputs.secondary] = true;
        }
        else if (context.canceled) 
        {
            attackInput[(int)CombatInputs.secondary] = false;
        }
    }
    public void OnGrapInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            grabInput = true;
        }
        else if (context.canceled)
        {
            grabInput = false;
        }

    }
    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            dashInput = true;
            dashInputStop = false;
            dashInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            dashInputStop = true;
            Debug.Log(dashInputStop);
        }
    }
    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {
        rawDashDirectionInput = context.ReadValue<Vector2>();

        if (playerInput.currentControlScheme == "Keyboard")
        {
            rawDashDirectionInput = cam.ScreenToWorldPoint((Vector3)rawDashDirectionInput) - transform.position;
        }

        dashDirectionInput = Vector2Int.RoundToInt(rawDashDirectionInput.normalized);
    }

    public void UseJumpInput() => JumpInput = false;
    public void UseDashInput() => dashInput = false;
    // giữ nút nhãy quá thời gian 
    private void CheckJumpInputHoldTime()
    {
        if (Time.time>=jumpinputStartTime+inputHoldTime)
        {
            JumpInput = false;
        }
    }
    private void CheckDashInputHoldTime()
    {
        if (Time.time >= dashInputStartTime + inputHoldTime)
        {
            dashInput = false;
        }
    }

    public enum CombatInputs 
    { 
        primary,
        secondary
    }

}

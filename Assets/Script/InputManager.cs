using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerMotor playerMotor;
    private PlayerLook playerLook;
    private Player player;

    public PlayerInput.OnFootActions onFoot;
    public Gamemanager gamemanager;

    private void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        playerMotor = GetComponent<PlayerMotor>();
        playerLook = GetComponent<PlayerLook>();
        player = GetComponent<Player>();

        onFoot.Sprint.performed += ctx => playerMotor.Sprint();
        onFoot.Attack.performed += ctx => playerMotor.Attack();
    }

    private void FixedUpdate()
    {
        if (!gamemanager.GetIsShowedPanel() && !player.GetIsPlayerDead())
            playerMotor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());

        playerMotor.SetIsShowedPanel(gamemanager.GetIsShowedPanel());
    }

    private void LateUpdate()
    {
        if (!gamemanager.GetIsShowedPanel() && !player.GetIsPlayerDead())
            playerLook.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable() { onFoot.Enable(); }

    private void OnDisable() { onFoot.Disable(); }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 15f;
    [SerializeField] private float gravity = -9.8f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private Vector3 moveDirection;
    private bool isSprinting = false;
    private bool isWalking = false;
    private bool isAttack = false;
    private bool isShowedPanel = false;
    private float startedPlayerSpeed;
    private Animator playerAnim;
    private Player player;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAnim = GetComponent<Animator>();
        player = GetComponent<Player>();
        startedPlayerSpeed = walkSpeed;
    }

    private void Update() { isGrounded = controller.isGrounded; }

    public void ProcessMove(Vector2 input)
    {
        moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * walkSpeed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (playerVelocity.y < 0 && isGrounded) playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
        if (input.x == 0 && input.y == 0) isWalking = false;
        else isWalking = true;
        Invoke("Walk", 0.05f);
    }

    public void Sprint()
    {
        if (!isShowedPanel)
        {
            isSprinting = !isSprinting;
            if (isSprinting) walkSpeed = runSpeed;
            else walkSpeed = startedPlayerSpeed;
        }
    }

    public void Attack()
    {
        if (!isShowedPanel)
        {
            isAttack = true;
            if (player.enemy.GetIsHitPlayerKnife())
                player.enemy.TakeDamage(player.GladiatorAttack);
            Invoke("ResetAttack", 0.5f);
        }
    }

    private void Walk()
    {
        if (isSprinting)
        {
            playerAnim.SetBool("IsWalk", false);
            playerAnim.SetBool("IsRun", isWalking);
        }
        else
        {
            playerAnim.SetBool("IsRun", false);
            playerAnim.SetBool("IsWalk", isWalking);
        }

        playerAnim.SetBool("IsAttack", isAttack);
    }

    private void ResetAttack() { isAttack = false; }

    public void SetIsShowedPanel(bool state) { isShowedPanel = state; }

    public Animator GetPlayerAnim() => playerAnim;

    public CharacterController GetController() => controller;
}

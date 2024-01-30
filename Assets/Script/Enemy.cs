using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Gladiator
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Player player;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Gamemanager gamemanager;

    private NavMeshAgent navMesh;
    private Animator enemyAnim;

    private bool isWalking = false;
    private bool isSprinting = false;
    private bool isHitPlayerKnife = false;
    private bool isAttacking = false;
    private bool isEnemyDead = false;

    private float startedSpeed;
    private float runSpeed = 4f;
    private float distance;

    private int enemyHealth = 75;
    private int enemyDefense = 3;
    private int enemyAttack = 10;

    private void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponent<Animator>();

        startedSpeed = navMesh.speed;
        InvokeRepeating("Sprint", 3f, 3f);

        GladiatorHealth = enemyHealth;
        GladiatorDefense = enemyDefense;
        GladiatorAttack = enemyAttack;
        healthBar.SetMaxHealth(GladiatorHealth);
    }

    private void Update()
    {
        if (!isEnemyDead && !player.GetIsPlayerDead() && !gamemanager.GetIsShowedPanel())
        {
            distance = (playerTransform.position - transform.position).magnitude;

            if (distance <= 1f && !isAttacking) Attack();
            else Walk();
        }
    }

    private void Walk()
    {
        if (isSprinting) navMesh.speed = runSpeed;
        else navMesh.speed = startedSpeed;
        isWalking = true;
        enemyAnim.SetBool("IsWalk", isWalking);
        enemyAnim.SetBool("IsAttack", false);
        enemyAnim.SetBool("IsRun", isSprinting);
        navMesh.destination = playerTransform.position;
    }

    private void Sprint()
    {
        if (Random.Range(1, 3) == 1 && !isSprinting)
        {
            isSprinting = true;
            Invoke("ResetSprint", 3f);
        }
    }

    private void ResetSprint() { isSprinting = false; }

    private void Attack()
    {
        isAttacking = true;

        if (player.GetIsHitEnemyKnife() && isAttacking)
            player.TakeDamage(GladiatorAttack);

        enemyAnim.SetBool("IsAttack", isAttacking);

        if (!isSprinting)
            enemyAnim.SetBool("IsWalk", isWalking);
        else
        {
            enemyAnim.SetBool("IsRun", isSprinting);
            enemyAnim.SetBool("IsWalk", false);
        }

        Invoke("ResetAttack", 0.5f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
            isHitPlayerKnife = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            isHitPlayerKnife = false;
    }

    public void TakeDamage(int damage)
    {
        if (!isEnemyDead)
        {
            if ((GladiatorHealth - (damage - GladiatorDefense)) >= 0)
                GladiatorHealth -= (damage - GladiatorDefense);
            else
                GladiatorHealth = 0;

            if (GladiatorHealth == 0)
            {
                enemyAnim.SetTrigger("SetDeath");
                navMesh.enabled = false;
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                healthBar.gameObject.SetActive(false);
                isEnemyDead = true;
            }

            healthBar.SetHealth(GladiatorHealth);
        }
    }

    public bool GetIsHitPlayerKnife() => isHitPlayerKnife;

    public bool GetIsEnemyDead() => isEnemyDead;

    public void SetNavmeshHide(bool state) { navMesh.enabled = state; }

    public Animator GetEnemyAnim() => enemyAnim;

    private void ResetAttack() 
    {
        isAttacking = false;
        enemyAnim.SetBool("IsAttack", isAttacking); 
    }
}

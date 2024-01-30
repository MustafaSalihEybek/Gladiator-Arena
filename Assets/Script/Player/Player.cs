using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Gladiator
{
    [SerializeField] public Enemy enemy;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Image damageEffect;

    private int playerHealth = 100;
    private int playerDefense = 5;
    private int playerAttack = 10;
    private bool isHitEnemyKnife = false;
    private bool isPlayerDead = false;
    private byte alphaAmount;
    private PlayerMotor playerMotor;

    private void Start()
    {
        GladiatorHealth = playerHealth;
        GladiatorDefense = playerDefense;
        GladiatorAttack = playerAttack;

        playerMotor = GetComponent<PlayerMotor>();
        healthBar.SetMaxHealth(GladiatorHealth);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
            isHitEnemyKnife = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
            isHitEnemyKnife = false;
    }

    public bool GetIsHitEnemyKnife() => isHitEnemyKnife;

    public void TakeDamage(int damage)
    {
        if (!isPlayerDead)
        {
            alphaAmount = 175;
            damageEffect.color = new Color32(255, 255, 255, alphaAmount);
            if (GladiatorHealth > (playerHealth / 2)) DecreaseEffect();

            if ((GladiatorHealth - (damage - GladiatorDefense)) >= 0)
                GladiatorHealth -= (damage - GladiatorDefense);
            else
                GladiatorHealth = 0;

            if (GladiatorHealth == 0)
            {
                playerMotor.GetPlayerAnim().SetTrigger("SetDeath");
                playerMotor.GetController().enabled = false;
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                enemy.GetComponent<Animator>().enabled = false;
                isPlayerDead = true;
            }

            healthBar.SetHealth(GladiatorHealth);
        }
    }

    public bool GetIsPlayerDead() => isPlayerDead;

    private void DecreaseEffect()
    {
        if (alphaAmount > 0)
        {
            alphaAmount -= 5;
            damageEffect.color = new Color32(255, 255, 255, alphaAmount);
            Invoke("DecreaseEffect", 0.1f);
        }
    }
}

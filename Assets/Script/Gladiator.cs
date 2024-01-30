using UnityEngine;

public abstract class Gladiator : MonoBehaviour
{
    private int gladiatorHealth;
    private int gladiatorDefense;
    private int gladiatorAttack;

    public int GladiatorHealth
    {
        get { return gladiatorHealth; }
        set { gladiatorHealth = value; }
    }

    public int GladiatorDefense
    {
        get { return gladiatorDefense; }
        set { gladiatorDefense = value; }
    }

    public int GladiatorAttack
    {
        get { return gladiatorAttack; }
        set { gladiatorAttack = value; }
    }
}

using UnityEngine;

public class Target : MonoBehaviour
{
    public float health;

    public void takeDamage (float damage)
    {
        health -= damage;

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die ()
    {
        Destroy(gameObject);
    }
}

using UnityEngine;

[System.Serializable]
public class EntityStatistics 
{
    public float health;
    public int maxHealth;
    public float damage;
    public float speed;
    public float jump;

    public int pointsUsed;

    public void TakeDamage(float damage, Transform transform, Sprite sprite)
    {
        health -= damage;

        if (health <= 0 )
            GameController.instance.SpawnCorpses(transform, this, sprite);
    }
}

[System.Serializable]
public class EntityStatistics 
{
    public int health;
    public int maxHealth;
    public int damage;
    public float speed;
    public float jump;

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}

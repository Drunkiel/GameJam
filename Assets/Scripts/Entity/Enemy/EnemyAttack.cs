using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject weaponPrefab;
    public float strength;
    public float cooldown;
    public bool onCooldown;

    public void Attack()
    {
        if (onCooldown)
            return;

        float x = transform.GetChild(0).localScale.x;
        Rigidbody2D rgBody = Instantiate(weaponPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();

        rgBody.AddForce(new(x * strength, strength / 2));

        StartCoroutine(SetCooldown());
    }

    public IEnumerator SetCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletPrefab;
    public float attackRange = 3f;
    public float attackCooldown = 0.2f;
    public LayerMask enemyLayer;
    public bool enabled;
    private float cooldown;
    void Start()
    {
        enabled = true;
        cooldown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }
    
    public void Attack()
    {
        if (!enabled) return;
        if (cooldown > 0) 
        {
            cooldown -= Time.deltaTime;
            return;
        }
        cooldown = attackCooldown;
        Vector2 origin = transform.position;
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, attackRange, enemyLayer);
        if(hits.Length == 0) return;
        Collider2D target = hits[0];
        // Collider2D target = hits[Random.Range(0, hits.Length)];
        Vector2 direction = new Vector2(target.transform.position.x, target.transform.position.y) - origin;
        transform.up = direction;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
        bullet.transform.up = direction;
    }
    private void OnDrawGizmos()
    {
        Vector2 origin = transform.position;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(origin, attackRange);

    }
}

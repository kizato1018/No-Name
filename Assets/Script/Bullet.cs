using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 15f;
    public float attack = 10f;
    private Rigidbody2D rb;
    private float liveTime = 3f;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        liveTime -= Time.deltaTime;
        if (liveTime <= 0f)
            Destroy(gameObject);
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other == null) return;
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy == null) return;
        enemy.GetDamage(attack);
        Destroy(gameObject);
    }
}

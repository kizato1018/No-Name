using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float MaxHP = 5f;
    public float attack = 2f;
    public float moveSpeed = 5f;
    public float attackRange = 0.5f;
    public float attackCooldown = 0.2f;
    public LayerMask playerLayer;
    public float damageShowDuration = 0.2f;
    private GameObject player;
    private Rigidbody2D rb;
    private float cooldown;
    private float hp;
    private GameObject canvas;
    private GameObject damageText_object;
    private bool damage_done = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        hp = MaxHP;
        canvas = gameObject.transform.GetChild(0).gameObject;
        damageText_object = canvas.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(hp > 0)
        {
            Move();
            Attack();
        }
    }
    public void GetDamage(float damage) 
    {
        hp -= damage;
        StartCoroutine(ShowDamage(damage));
        
        if(hp < 0)
        {
            hp = 0;
            gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            rb.velocity = Vector3.zero;
            // gameObject.SetActive(false);


            StartCoroutine(Die());
            
        }
    }
    IEnumerator ShowDamage(float damage)
    {
        damage_done = false;
        float time = 0f;
        float speed = 2f;
        GameObject damageText;
        damageText = Instantiate(damageText_object, damageText_object.transform.parent);
        damageText.SetActive(true);
        damageText.GetComponent<TMP_Text>().text = ((int)damage).ToString();
        while (time < damageShowDuration)
        {
            damageText.transform.Translate(Vector3.up * Time.deltaTime * speed, Space.World);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(damageText);
        damage_done = true;
        yield return null;
    }
    IEnumerator Die()
    {
        yield return new WaitUntil(()=>{ return damage_done; });
        Destroy(gameObject);
    }
    
    void Move()
    {
        Vector2 direction = player.transform.position - transform.position;
        rb.velocity = direction.normalized * moveSpeed;
    }
    void Attack()
    {
        if (cooldown > 0) 
        {
            cooldown -= Time.deltaTime;
            return;
        }
        cooldown = attackCooldown;
        Vector2 origin = transform.position;
        Collider2D hit = Physics2D.OverlapCircle(origin, attackRange, playerLayer);
        if(hit)
        {
            hit.gameObject.GetComponent<PlayerManager>().GetDamge(attack);
            print(gameObject.name + " hit!");
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 origin = transform.position;
        Gizmos.DrawWireSphere(origin, attackRange);

    }
}

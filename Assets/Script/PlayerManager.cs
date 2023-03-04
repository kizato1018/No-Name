using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private Weapon weapon;
    private GameObject face;
    public Joystick jsMovement;
    public GameObject HP_image;
    public LayerMask itemsLayer;
    public float interactRange = 2f;

    [Header("移动参数")]
    public float moveSpeed = 8f;
    public float touch_range = 200.0f;
    [Header("能力參數")]
    public float max_hp = 100f;
    public float hp = 100f;
    public List<GameObject> interactList;
    float xVelocity;
    float yVelocity;
    Vector2 startPos;
    Vector2 direction;
    float distnace;
    void Start()
    {
        face = transform.GetChild(1).gameObject;
        rb = gameObject.GetComponent<Rigidbody2D>();
        coll = gameObject.GetComponent<Collider2D>();
        weapon = gameObject.GetComponentInChildren<Weapon>();
    }

    void FixedUpdate()
    {
        if(hp <= 0) 
        {
            coll.enabled = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            rb.velocity = Vector2.zero;
            weapon.enabled = false;
        }
        else
        {
            if(Input.touchCount > 0 || Input.GetMouseButton(0))
            {
                TouchMove();
            }
            else
            {
                KeyboardMove();
            }
        }
    }

    void KeyboardMove()
    {
        xVelocity = Input.GetAxisRaw("Horizontal");
        yVelocity = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(xVelocity, yVelocity);
        rb.velocity = direction.normalized * moveSpeed;
    }
    void TouchMove()
    {
        
        // InputDirection can be used as per the need of your project
        direction = jsMovement.InputDirection;

        // If we drag the Joystick
        if (direction.magnitude != 0)
        {
            face.transform.up = direction.normalized;
            rb.velocity = direction * moveSpeed;
        }
    }
    public void GetDamge(float damage)
    {
        hp -= damage;
        if(hp < 0)
            hp = 0;
        HP_image.transform.localScale = new Vector3(hp/max_hp, 1, 1);
    }

    // List<GameObject> GetInteractItems()
    // {
    //     Vector2 origin = transform.position + face.transform.up * 0.5f;
    //     Collider2D[] hits = Physics2D.OverlapBoxAll(origin, Vector2.one * interactRange, 0, itemsLayer);
    //     List<GameObject> result = new List<GameObject>();
    //     foreach(Collider2D hit in hits)
    //     {
    //         result.Add(hit.gameObject);
    //     }
    //     return result;
    // }
    // // void GetInteractItems()
    // // {
    // //     Vector2 origin = transform.position + face.transform.up * 1f;
    // //     // interactList = Physics2D.OverlapCircleAll(origin, interactRange, itemsLayer);
    // //     interactList = Physics2D.OverlapBoxAll(origin, Vector2.one * interactRange, 0, itemsLayer);
    // //     // print(Physics2D.OverlapBoxNonAlloc(origin, Vector2.one * interactRange, 0, interactList, itemsLayer));
    // // }
    // private void OnDrawGizmos()
    // {
    //     Vector2 origin = (face) ? transform.position + face.transform.up * 1f : transform.position + Vector3.up * 1f;
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawWireCube(origin, Vector2.one * interactRange);
    // }
}

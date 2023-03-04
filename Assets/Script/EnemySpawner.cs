using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float spawncooldown = 1f;
    public GameObject enemyPrefab;
    private float cooldown;
    void Start()
    {
        cooldown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }
    void Spawn()
    {
        if (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            return;
        }
        cooldown = spawncooldown;
        Instantiate(enemyPrefab, transform.position, enemyPrefab.transform.rotation);
    }
}

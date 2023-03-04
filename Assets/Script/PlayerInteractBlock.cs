using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractBlock : MonoBehaviour
{
    private PlayerManager player;
    void Start()
    {
        player = gameObject.GetComponentInParent<PlayerManager>();
    }
    void FixedUpdate()
    {
        transform.localPosition = Vector3.up;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other != null)
            print(other.gameObject.name);
        if(other != null && other.gameObject.tag == "Item")
        {
            player.interactList.Add(other.gameObject);
            UIManager.instance.AddInteractButton(other.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other != null && other.gameObject.tag == "Item")
        {
            player.interactList.Remove(other.gameObject);
            UIManager.instance.RemoveInteractButton(other.gameObject);
        }
    }
}

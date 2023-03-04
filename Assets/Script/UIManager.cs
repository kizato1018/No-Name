using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    static public UIManager instance;
    public GameObject interact_button_prefab;
    public Transform interact_button_parent;
    public Dictionary<GameObject, GameObject> interact_buttons_list = new Dictionary<GameObject, GameObject>();
    private PlayerManager player;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    void Update()
    {

    }

    public void AddInteractButton(GameObject obj)
    {
        GameObject interact_button;
        TMP_Text text;
        interact_button = Instantiate(interact_button_prefab, interact_button_parent);
        text = interact_button.GetComponentsInChildren<TMP_Text>()[1];
        text.text = obj.name;
        interact_buttons_list.Add(obj, interact_button);
    }
    public void RemoveInteractButton(GameObject obj)
    {
        Destroy(interact_buttons_list[obj]);
        interact_buttons_list.Remove(obj);
    }
    // public void InteractUpdate()
    // {
    //     GameObject interact_button;
    //     TMP_Text text;
    //     foreach (GameObject obj in player.interactList)
    //     {
    //         interact_button = Instantiate(interact_button_prefab, interact_button_parent);
    //         text = interact_button.GetComponentsInChildren<TMP_Text>()[1];
    //         text.text = obj.name;
    //     }
    // }
}

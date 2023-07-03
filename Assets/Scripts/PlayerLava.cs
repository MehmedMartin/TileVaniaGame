using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLava : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    void Start()
    {
        player = gameObject.GetComponent<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            Destroy(player, 0.5f);
        }
    }

}

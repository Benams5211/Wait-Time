using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public Dialouge dialogueAsset;

    public GameObject player;
    private Player playerObj;

    void Start()
    {
        player = GameObject.Find("Player");
        playerObj = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

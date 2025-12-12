using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    [SerializeField] bool firstInteraction = true;
    [SerializeField] int repeatStartPosition;

    public string npcName;
    public Dialouge dialogueAsset;
    public Dialouge foundAsset;
    public Dialouge wrongAsset;
    public GameObject wantedItem;
    public GameObject questItem;

    public bool gotItem = false;

    public GameObject player;
    private Player playerObj;

    void Start()
    {
        player = GameObject.Find("Player");
        playerObj = player.GetComponent<Player>();
    }

    void FixedUpdate()
    {
       
    }

    [HideInInspector]
    public int StartPosition
    {
        get
        {
            if (firstInteraction)
            {
                firstInteraction = false;
                return 0;
            }
            else
            {
                return repeatStartPosition;
            }
        }
    }
}

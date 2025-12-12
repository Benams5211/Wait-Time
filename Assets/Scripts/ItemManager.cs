using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public GameObject itemFrame;
    //public GameObject itemImage;

    public Image barImage;

    public GameObject player;
    private Player playerObj;

    private NPC npcObj;

    private bool romanceSideQuestDone=false;

    public GameObject pauseScreen;
    public GameObject gameScreen;

    public bool paused=false; 

    void Start()
    {
        playerObj = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerObj.inConversation && !paused)
            {
                DialogueBoxController.instance.SkipLine();
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;
        }
        if (paused)
        {
            gameScreen.SetActive(false);
            pauseScreen.SetActive(true);
            //paused = true;
        }
        else
        {
            gameScreen.SetActive(true);
            pauseScreen.SetActive(false);
            //paused = false;
        }
        if (romanceSideQuestDone && !playerObj.inConversation)
        {
            var parent = GameObject.Find("NPCs");
            var bb = parent.transform.Find("Blueio & Bluiet");
            var blueio = parent.transform.Find("Blueio");
            var bluiet = parent.transform.Find("Bluiet");
            bb.gameObject.SetActive(true);
            blueio.gameObject.SetActive(false);
            bluiet.gameObject.SetActive(false);
        }

    }

    void Interact()
    {
        if (paused) return;

        //block for item interactions
        {
            //is colliding but not holding
            if (playerObj.collideItem != null && playerObj.heldItem == null && playerObj.collideNPC == null)
            {
                var ii = playerObj.collideItem.GetComponent<Item>();
                ii.itemImage.SetActive(true);
                playerObj.heldItem = playerObj.collideItem;
                playerObj.collideItem.SetActive(false);
            }
            //is colliding, is holding
            if(playerObj.collideItem != null && playerObj.heldItem != null && playerObj.collideNPC == null)
            {
                var newItem = playerObj.collideItem.GetComponent<Item>();
                newItem.itemImage.SetActive(true);
                var oldItem = playerObj.heldItem.GetComponent<Item>();
                oldItem.itemImage.SetActive(false);

                playerObj.heldItem.SetActive(true);
                playerObj.heldItem.transform.position = playerObj.collideItem.transform.position;
                
                playerObj.heldItem = playerObj.collideItem;
                playerObj.collideItem.SetActive(false);

            }
        }
        //block for NPC interactions
        {
            if (playerObj.collideNPC != null)
            {
                var npc = playerObj.collideNPC.GetComponent<NPC>();
                if(npc.gotItem == true)//already got the item
                {
                    DialogueBoxController.instance.StartDialogue(npc.foundAsset.dialogue, npc.StartPosition, npc.npcName);
                }

                else if (playerObj.heldItem != null && npc.wantedItem != null)//try to give item you have
                {
                    if (npc.wantedItem.name == playerObj.heldItem.name)//if player has the item npc wants
                    {
                        var ii = playerObj.heldItem.GetComponent<Item>();
                        ii.itemImage.SetActive(false);
                        DialogueBoxController.instance.StartDialogue(npc.foundAsset.dialogue, npc.StartPosition, npc.npcName);
                        playerObj.heldItem = null;
                        npc.gotItem = true;
                        foreach (var c in npc.GetComponents<BoxCollider2D>())
                        {
                            c.isTrigger = true;
                        }
                        barImage.fillAmount += 0.25f;
                        if (npc.npcName == "Blueio")
                        {
                            romanceSideQuestDone = true;
                        }
                        
                    }
                    else //if player has wrong item
                    {
                        DialogueBoxController.instance.StartDialogue(npc.wrongAsset.dialogue, npc.StartPosition, npc.npcName);
                    }
                }

                else if(playerObj.heldItem == null && npc.questItem != null)//npc has item to give player
                {
                    DialogueBoxController.instance.StartDialogue(npc.dialogueAsset.dialogue, npc.StartPosition, npc.npcName);
                    var ii = npc.questItem.GetComponent<Item>();
                    ii.itemImage.SetActive(true);
                    playerObj.heldItem = npc.questItem;
                }

                else if (playerObj.heldItem == null || npc.wantedItem == null) //if player is not holding anything OR npc doesnt want anything
                {
                    DialogueBoxController.instance.StartDialogue(npc.dialogueAsset.dialogue, npc.StartPosition, npc.npcName);

                }
            }
        }
    }

}

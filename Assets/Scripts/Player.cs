using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject collideItem = null;
    public GameObject collideNPC = null;
    public GameObject heldItem = null;
    public bool inConversation;
    public Scenes sceneMan;

    void JoinConversation()
    {
        inConversation = true;
    }

    void LeaveConversation()
    {
        inConversation = false;
    }

    private void OnEnable()
    {
        DialogueBoxController.OnDialogueStarted += JoinConversation;
        DialogueBoxController.OnDialogueEnded += LeaveConversation;
    }

    private void OnDisable()
    {
        DialogueBoxController.OnDialogueStarted -= JoinConversation;
        DialogueBoxController.OnDialogueEnded -= LeaveConversation;
    }

    private GameObject highlights;

    void OnCollisionStay2D(Collision2D other)
    { 

        if (other.gameObject.CompareTag("NPC"))
        {

            if (transform.position.x < other.transform.position.x)
            {
                Vector3 npcPos = other.transform.localScale;
                npcPos.x = -1;
                other.transform.localScale = npcPos;
            }
            else if(!inConversation)
            {
                Vector3 npcPos = other.transform.localScale;
                npcPos.x = 1;
                other.transform.localScale = npcPos;
            }

            if (transform.position.y-0.5 < other.transform.position.y)
            {
                highlights = other.gameObject.transform.Find("Highlight")?.gameObject;
                if (highlights != null)
                    highlights.SetActive(true);

                var e = transform.Find("Canvas")?.gameObject;
                e.SetActive(true);
                collideNPC = other.gameObject; // Store reference
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            highlights = other.gameObject.transform.Find("Highlight")?.gameObject;
            if (highlights != null)
                highlights.SetActive(true);

            var e = transform.Find("Canvas")?.gameObject;
            e.SetActive(true);

            collideItem = other.gameObject; // Store reference
        }

        if (other.gameObject.CompareTag("Exit"))
        {
            sceneMan.LoadEnding() ;
        }

        if (other.gameObject.CompareTag("NPC"))
        {

            if (transform.position.x < other.transform.position.x)
            {
                Vector3 npcPos = other.transform.localScale;
                npcPos.x = -1;
                other.transform.localScale = npcPos;
            }
            else if (!inConversation)
            {
                Vector3 npcPos = other.transform.localScale;
                npcPos.x = 1;
                other.transform.localScale = npcPos;
            }

            if (transform.position.y - 0.5 < other.transform.position.y)
            {
                highlights = other.gameObject.transform.Find("Highlight")?.gameObject;
                if (highlights != null)
                    highlights.SetActive(true);
                var e = transform.Find("Canvas")?.gameObject;
                e.SetActive(true);

                collideNPC = other.gameObject; // Store reference
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {

            //else if (!inConversation)
            {
                Vector3 npcPos = other.transform.localScale;
                npcPos.x = 1;
                other.transform.localScale = npcPos;
            }

            highlights = other.gameObject.transform.Find("Highlight")?.gameObject;
            if (highlights != null)
                highlights.SetActive(false);

            var e = transform.Find("Canvas")?.gameObject;
            e.SetActive(false);

            if (collideNPC == other.gameObject)
                collideNPC = null; // Clear reference
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            highlights = other.gameObject.transform.Find("Highlight")?.gameObject;
            if (highlights != null)
                highlights.SetActive(false);
            var e = transform.Find("Canvas")?.gameObject;
            e.SetActive(false);

            if (collideItem == other.gameObject)
                collideItem = null; // Clear reference
        }

        if (other.gameObject.CompareTag("NPC"))
        {

            //if (transform.position.x < other.transform.position.x)
            {
                Vector3 npcPos = other.transform.localScale;
                npcPos.x = 1;
                other.transform.localScale = npcPos;
            }

            highlights = other.gameObject.transform.Find("Highlight")?.gameObject;
            if (highlights != null)
                highlights.SetActive(false);
            var e = transform.Find("Canvas")?.gameObject;
            e.SetActive(false);

            if (collideNPC == other.gameObject)
                collideNPC = null; // Clear reference
        }
    }
}

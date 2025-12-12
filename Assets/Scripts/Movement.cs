using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool lookRight;

    public float speed = 5f;
    public float jumpForce = 25f;
    public bool isGrounded;
    private Animator anim;
    [SerializeField] GameObject playerBody;
    private GameObject IMGO;
    private ItemManager itemMan;


    private void Awake()
    {
        IMGO = GameObject.Find("ItemManager");
        if(IMGO!=null)itemMan = IMGO.GetComponent<ItemManager>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if (itemMan != null)
        {
            if (itemMan.paused)
            {
                Time.timeScale = 0f;
                anim.SetBool("walk", false);
                return;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
        var player = GetComponent<Player>();
        if (player.inConversation)
        {
            rb.velocity = new Vector2(0, 0);
            anim.SetBool("walk", false);
        }
        else
        {
            float move = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(move * speed, rb.velocity.y);

            if (move != 0)
            {
                anim.SetBool("walk", true);
                lookRight = move > 0;
            }
            else
            {
                anim.SetBool("walk", false);
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isGrounded = false;
            }

            if (!lookRight)
            {
                Vector3 tmp = transform.localScale;
                tmp.x = -1;
                playerBody.transform.localScale = tmp;
            }
            else
            {
                Vector3 tmp = transform.localScale;
                tmp.x = 1;
                playerBody.transform.localScale = tmp;
            }
        }
    }

    


}

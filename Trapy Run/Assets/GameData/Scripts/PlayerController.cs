using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

// controls all player activites including touch input
public class PlayerController : MonoBehaviour
{
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = false;

    public float SWIPE_THRESHOLD = 20;

    public float rotatingValue;
    public float speed;

    bool firstTime;

    Rigidbody rb;

    Vector3 rot;
    Vector3 jump;

    bool isJump;

    public Animator animator;
    public NavMeshAgent nav;

    public static PlayerController instance;

    void Start()
    {
        jump = new Vector3(0, 2f, 2f);
        rb = GetComponent<Rigidbody>();

        // instance of this class

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }


        firstTime = true;
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    void Update()
    {


        if (!isJump)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (firstTime == true)
                    {
                        GameManager.instance.Play();
                        animator.SetBool("isRun", true);
                        firstTime = false;
                    }
                    fingerUp = touch.position;
                    fingerDown = touch.position;
                }

                //Detects Swipe while finger is still moving
                if (touch.phase == TouchPhase.Moved)
                {
                    if (!detectSwipeOnlyAfterRelease)
                    {
                        fingerDown = touch.position;
                        checkSwipe();
                    }
                }

                //Detects swipe after finger is released
                if (touch.phase == TouchPhase.Ended)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                }

                transform.position += transform.forward * speed;

            }
        }

    }

    // check whether the user swipe or not
    void checkSwipe()
    {

        //Check if Horizontal swipe
        if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }

        //No Movement at-all
        else
        {
            //Debug.Log("No Swipe!");
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    //////////////////////////////////CALLBACK FUNCTIONS/////////////////////////////
    ///
    // called when the user swipe left
    void OnSwipeLeft()
    {
        rot = transform.eulerAngles;
        rot.y -= rotatingValue;
        transform.eulerAngles = rot;
    }

    // called when the user swipe right
    void OnSwipeRight()
    {
        rot = transform.eulerAngles;
        rot.y += rotatingValue;
        transform.eulerAngles = rot;
    }


    //////////////////////////////////CALLBACK FUNCTIONS ENDS/////////////////////////////
    ///

    private void OnTriggerEnter(Collider other)
    {
        // if player wins the race
        if(other.gameObject.tag == "Win")
        {
            enabled = false;
            nav.enabled = true;
            nav.SetDestination(GameObject.FindGameObjectWithTag("Jump").transform.position);
        }
        // if player lose the race
        else if(other.gameObject.tag == "Lose")
        {
            GameManager.instance.Lose();
            enabled = false;
            Time.timeScale = 0;
        }
        // if the player jumps to the helicopter
        else if(other.gameObject.tag == "Jump")
        {
            nav.enabled = false;
            isJump = true;
            animator.SetBool("isJump", true);
            rb.MovePosition(transform.position + jump * 2);
            GameManager.instance.Jump();

        }
        // if the player fall down the ground 
        else if(other.gameObject.tag == "BelowGround")
        {
            isJump = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if the player reach helicopter
        if(collision.gameObject.tag == "Helicopter")
        {
            GameManager.instance.Helicopter();
            transform.Rotate(0, 180, 0);
            animator.SetBool("isWin", true);
        }
    }


}

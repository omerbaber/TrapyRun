using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controls all enemy activites including hitting
public class EnemyController : MonoBehaviour
{

    public bool isIdle;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        isIdle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isIdle && GameManager.instance.isRun)
        {
            Run();
            isIdle = false;
        }
        if(!isIdle)
        {
            transform.position += transform.forward * speed;
        }
        if(GameManager.instance.gameEnd)
        {
            isIdle = true;
            this.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if the enemy fall enough far the ground
        if(other.gameObject.tag == "Lose")
        {
            Destroy(gameObject);
        }
        // if the enemy fall from the ground
        else if(other.gameObject.tag == "BelowGround")
        {
            isIdle = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if the enemy collide with the player
        if(collision.gameObject.tag == "Player")
        {
            GameManager.instance.Lose();
            PlayerController.instance.animator.SetBool("isFall", true);
            PlayerController.instance.enabled = false;
            GetComponent<Animator>().SetBool("isFall", true);
            this.enabled = false;
        }
    }

    public void Run()
    {
        GetComponent<Animator>().SetBool("isRun", true);
    }

}

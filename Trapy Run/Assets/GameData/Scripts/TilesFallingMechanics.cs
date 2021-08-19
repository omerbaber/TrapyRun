using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// controls the tiles falling mechanism
public class TilesFallingMechanics : MonoBehaviour
{

    Rigidbody rb;
    float temp;

    // Start is called before the first frame update
    void Start()
    {
        temp = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if the player hit the tile
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(TilesFalling());
            StartCoroutine(ColorChange());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if the tile fall enough far from the ground
        if(other.gameObject.tag == "Lose")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator TilesFalling()
    {
        yield return new WaitForSeconds(0.2f);
        rb = gameObject.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        rb.useGravity = true;
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }

    IEnumerator ColorChange()
    {
        yield return new WaitForEndOfFrame();
        gameObject.GetComponent<Renderer>().material.color = Color.Lerp(Color.white, Color.red, temp);
        temp += 0.1f;
        if (temp <= 1f)
        {
            StartCoroutine(ColorChange());
        }
    }

}

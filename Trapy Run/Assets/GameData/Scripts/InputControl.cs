using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script use for testing in editor
public class InputControl : MonoBehaviour
{   

    Vector3 pos;
    Vector3 rot;

    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector3(0, 0, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //transform.position += transform.forward * 0.2f;
            GetComponent<Rigidbody>().MovePosition(transform.position + pos * 2);
            //GetComponent<CharacterController>().Move(pos);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.forward + pos * 0.2f;
            rot = transform.eulerAngles;
            rot.y += 0.1f;
            transform.eulerAngles = rot;
        }
    }
}

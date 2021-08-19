using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateX : MonoBehaviour
{

    public float speed;
    Vector3 axis;

    // Start is called before the first frame update
    void Start()
    {
        axis = new Vector3(speed, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(axis);
    }
}

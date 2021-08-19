using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{


    private void Update()
    {
        if(GameManager.instance.isRun)
        {
        GetComponent<NavMeshAgent>().destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
    }

}

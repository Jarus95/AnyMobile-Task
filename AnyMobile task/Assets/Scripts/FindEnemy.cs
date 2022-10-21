using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemy : MonoBehaviour
{
    public Status status;
    private bool go = true;
    public GameObject TargetObject = null;


    void Start()
    {
        status = Status.free;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            go = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            go = true;
        }
    }

   
    void Update()
    {
        if(TargetObject!= null)
        transform.position = go ? Vector3.MoveTowards(transform.position, TargetObject.transform.position, 0.05f) : transform.position;
    }


    public enum Status
    {
        free,
        busy
    }
}


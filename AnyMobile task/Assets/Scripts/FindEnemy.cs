using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindEnemy : MonoBehaviour
{
    [HideInInspector] public Status status;
    public bool go = true;
    [HideInInspector] public GameObject TargetObject = null;
    public float health;
    public float damage;
    private float takeDamage;
    private Slider healthBar;


    void Start()
    {
        healthBar = transform.GetChild(0).transform.GetChild(0).GetComponent<Slider>();
        healthBar.maxValue = health;
        healthBar.value = health;
        status = Status.free;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            go = false;
            takeDamage = collision.gameObject.GetComponent<FindEnemy>().damage;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            go = true;
           
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        { 
            health -= takeDamage;
            healthBar.value = health;
            if (health <= 0)
            {
                collision.gameObject.GetComponent<FindEnemy>().status = Status.free;
                collision.gameObject.GetComponent<FindEnemy>().go = true;
                collision.gameObject.GetComponent<FindEnemy>().TargetObject = null;
                Manager.instance.UpdateEnemiyList(this.gameObject);
                Destroy(gameObject);
            }
        }
    }

   
    void Update()
    {
        if(TargetObject!= null)
        transform.position =  go ? Vector3.MoveTowards(transform.position, TargetObject.transform.position, 0.05f): Vector3.MoveTowards(transform.position, TargetObject.transform.position, -0.005f);
    }


    public enum Status
    {
        free,
        busy
    }
}


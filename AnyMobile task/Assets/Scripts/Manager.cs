using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();
    GameObject target;
    void Start()
    {
        Find();
    }

    [ContextMenu("Find")]
    public void Find()
    {
        float dist = float.MaxValue;
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].GetComponent<FindEnemy>().TargetObject != null)
            continue;
            
            for (int j = 0; j < Enemies.Count; j++)
            {
                if (Enemies[i] != Enemies[j])
                {
                    float dist2 = Vector3.Distance(Enemies[i].transform.position, Enemies[j].transform.position);
                    if (dist2 < dist)
                    {
                        if (Enemies[j].GetComponent<FindEnemy>().TargetObject != null)
                            continue;
                        if (Enemies[j].GetComponent<FindEnemy>().status == FindEnemy.Status.free)
                        {
                           dist = dist2;
                           target = Enemies[j];

                        }
                    }
                }

                
            }

            Enemies[i].GetComponent<FindEnemy>().TargetObject = target ?? null;
            if(target!= null)
            {
                target.GetComponent<FindEnemy>().TargetObject = Enemies[i]; 
                target.GetComponent<FindEnemy>().status = FindEnemy.Status.busy;

            }
            target = null;
            dist = float.MaxValue;
        }
    }

}

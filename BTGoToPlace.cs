using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BTGoToPlace : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        GameObject alvo = null;

        float distToPlace = Mathf.Infinity;
        GameObject npc = bt.gameObject;
        GameObject[] Places = GameObject.FindGameObjectsWithTag("Place");

        GameObject Player = GameObject.FindGameObjectWithTag("Player");

        //navmesh
        NavMeshAgent agent = bt.GetComponent<BTEnemyV01>().agent;
        BTEnemyV01 Controller = bt.GetComponent<BTEnemyV01>();

        foreach (GameObject place in Places)
        {
            float dist = Vector3.Distance(npc.transform.position, place.transform.position);

            if(dist < distToPlace)
            {
                alvo = place;
                distToPlace = dist;
            }
        }

        while(Vector3.Distance(npc.transform.position, alvo.transform.position) > 1f && alvo)
        {
            bool SeePlayer = bt.gameObject.GetComponent<BTEnemyV01>().SeePlayer;
            bool Inplace = bt.gameObject.GetComponent<BTEnemyV01>().InPlace;

            Controller.MoveToTarget(alvo, agent, 0.5f);

            if (SeePlayer == true || Vector3.Distance(npc.transform.position, Player.transform.position) < 4f)
            {
                status = Status.FAILURE;
                break;
            }
            
            if(Inplace == true)
            {
                status = Status.SUCCESS;
                break;
            }
            yield return null;
        }

        Print();
        yield break;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPlayerNotCloseOrInPointOfView : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        float distToPlayer = bt.gameObject.GetComponent<BTEnemyV01>().distToPlayer;

        bool SeePlayer = bt.gameObject.GetComponent<BTEnemyV01>().SeePlayer;

        GameObject alvo = GameObject.FindGameObjectWithTag("Player");

        GameObject npc = bt.gameObject;

        if(SeePlayer == false || Vector3.Distance(npc.transform.position, alvo.transform.position) > distToPlayer)
        {
            status = Status.SUCCESS;
        }
        else
        {
            status = Status.FAILURE;
        }
        Print();
        yield break;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPlaceAvailable : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        bool SeePlayer = bt.gameObject.GetComponent<BTEnemyV01>().SeePlayer;

        GameObject Place = GameObject.FindGameObjectWithTag("Place");

        if(Place && SeePlayer == false)
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

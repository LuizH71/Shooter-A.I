using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPlaceNotAvailable : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        GameObject Place = GameObject.FindGameObjectWithTag("Place");

        if(!Place)
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

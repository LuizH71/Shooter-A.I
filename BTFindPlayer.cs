using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTFindPlayer : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        GameObject obj = GameObject.FindGameObjectWithTag("Player");

        if (obj)
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

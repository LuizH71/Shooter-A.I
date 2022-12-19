using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequance : BTnode
{
    public List<BTnode> children = new List<BTnode>();
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        foreach (BTnode child in children)
        {
            yield return bt.StartCoroutine(child.Run(bt));
            if(child.status == Status.FAILURE)
            {
                status = Status.FAILURE;
                break;
            }
        }

        if (status == Status.RUNNING)
        {
            status = Status.SUCCESS;
        }
        Print();
    }
}

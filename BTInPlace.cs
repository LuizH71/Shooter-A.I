using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class BTInPlace : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        bool Inplace = bt.gameObject.GetComponent<BTEnemyV01>().InPlace;

        if (Inplace == true)
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

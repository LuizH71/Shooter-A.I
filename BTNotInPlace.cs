using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class BTNotInPlace : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        bool Inplace = bt.gameObject.GetComponent<BTEnemyV01>().InPlace;

        GameObject npc = bt.gameObject;
        GameObject alvo = GameObject.FindGameObjectWithTag("Player");

        
        if (Inplace == true)
        {
            
            status = Status.FAILURE;
        }
        else
        {
            status = Status.SUCCESS;
        }
        Print();
        yield break;
    }
}

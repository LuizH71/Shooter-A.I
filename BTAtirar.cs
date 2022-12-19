using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAtirar : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        GameObject rifle = bt.gameObject.GetComponent<BTEnemyV01>().rifle;
        GameObject npc = bt.gameObject;

        EnemyHealth enemyHealth = npc.GetComponent<EnemyHealth>();

       


        if (rifle.GetComponent<RifleNpc>().Fire() && enemyHealth.Life > 0)
        {
            status = Status.SUCCESS;
            Print();
            yield break;
        }
        else
        {
            status = Status.FAILURE;
            Print();
            yield break;
        }
    }
}

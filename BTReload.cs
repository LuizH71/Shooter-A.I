using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTReload : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        GameObject rifle = bt.gameObject.GetComponent<BTEnemyV01>().rifle;

        float seconds = rifle.GetComponent<RifleNpc>().reloadTime;

        if (rifle.GetComponent<RifleNpc>().Recharge())
        {
            status = Status.SUCCESS;
            Print();
            yield return new WaitForSeconds(seconds);
        }
        else
        {
            status = Status.FAILURE;
            Print();
            yield break;
        }
        Print();
        yield break;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BTAim: BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        GameObject alvo = GameObject.FindGameObjectWithTag("Player");
        GameObject rifle = bt.gameObject.GetComponent<BTEnemyV01>().rifle;

        GameObject npc = bt.gameObject;
        BTEnemyV01 Controller = bt.GetComponent<BTEnemyV01>();

        GameObject Target = Controller.PlayerTarget;

        

        while (rifle.GetComponent<RifleNpc>().Aim(Target))
        {
            npc.transform.LookAt(new Vector3(Target.transform.position.x, Target.transform.position.y, Target.transform.position.z));
            status = Status.SUCCESS;
            Print();
            yield break;
        }
        if(status == Status.RUNNING)
        {
            status = Status.FAILURE;
            Print();
            yield break;
        }
        Print();
        yield break;
    }
}

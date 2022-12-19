using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BTPlayerCloseOrInPointOfViewInPlace : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        float distToPlayer = bt.gameObject.GetComponent<BTEnemyV01>().distToPlayer;

        bool SeePlayer = bt.gameObject.GetComponent<BTEnemyV01>().SeePlayer;

        GameObject alvo = GameObject.FindGameObjectWithTag("Player");

        GameObject npc = bt.gameObject;

        GameObject npcRifleVariant = bt.transform.GetChild(0).gameObject;

        bool Inplace = bt.gameObject.GetComponent<BTEnemyV01>().InPlace;

        while(SeePlayer == false || Vector3.Distance(npc.transform.position, alvo.transform.position) > distToPlayer)
        {
            SeePlayer = bt.gameObject.GetComponent<BTEnemyV01>().SeePlayer;

            if (SeePlayer == true || Vector3.Distance(npc.transform.position, alvo.transform.position) < distToPlayer)
            {
                status = Status.SUCCESS;
                break;
            }
            yield return null;


        }



        Print();
        yield break;
    }
}



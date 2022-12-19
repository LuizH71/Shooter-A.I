using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSearchPlayer : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        Print();

        

        float distToPlayer = bt.gameObject.GetComponent<BTEnemyV01>().distToPlayer;

        bool SeePlayer = bt.gameObject.GetComponent<BTEnemyV01>().SeePlayer;

        GameObject alvo = GameObject.FindGameObjectWithTag("Player");

        GameObject npc = bt.gameObject;

        bool Inplace = bt.gameObject.GetComponent<BTEnemyV01>().InPlace;



        while (SeePlayer == false || Vector3.Distance(npc.transform.position, alvo.transform.position) > distToPlayer)
        {
            SeePlayer = bt.gameObject.GetComponent<BTEnemyV01>().SeePlayer;
            npc.transform.localEulerAngles = new Vector3(0, bt.gameObject.GetComponent<BTEnemyV01>().Yrotation, 0);
            //npc.transform.rotation = Quaternion.Euler(0f, rotY * Time.deltaTime * 5000, 0f);

            /*if (rotY >= 60.0f)
            {
                rotY -= 1f;
            }
            if (rotY <= -60.0f)
            {
                rotY += 1f;
            }*/

            if(SeePlayer == true || Inplace == true)
            {
                status = Status.SUCCESS;
                break;
            }
            /*
            if (Vector3.Distance(npc.transform.position, alvo.transform.position) < distToPlayer)
            {
                Inplace = false;


                status = Status.FAILURE;
                
                break;
            }
            */
            yield return null;
        }

        if(status == Status.RUNNING)
        {
            status = Status.FAILURE;
        }
        Print();
        yield break;
    }
}

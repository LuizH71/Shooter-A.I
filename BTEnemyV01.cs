using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTEnemyV01 : MonoBehaviour
{
    
    [HideInInspector]public float Yrotation;

    

    [HideInInspector]
    public Transform area;

    public GameObject rifle;
    [HideInInspector]
    public GameObject PlayerTarget;

    public bool InPlace;

    [HideInInspector]public NavMeshAgent agent;

    public float distToPlayer;


    //Field of view
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask PlayerMask;
    public LayerMask AmbienteMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    //[HideInInspector]
    public bool SeePlayer;

    [Space]
    public NpcAnimationController animatorController;

    [Space]
    public EnemyHealth EnemyHealth;

    private void Start()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("TargetToNPC");
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(FindTargetsWithDelay(0.01f));

        BTSequance SequenceA = new BTSequance();

        //SequenceA.children.Add(new BTNotInPlace());
        SequenceA.children.Add(new BTPlayerCloseOrInPointOfView());
        SequenceA.children.Add(new BTMoverParaPlayer());

        
        BTSelector SelectorC = new BTSelector();
        SelectorC.children.Add(new BTPlayerInPointOfView());
        SelectorC.children.Add(new BTPlayerClose());
        

        BTSequance SequenceB = new BTSequance();

        SequenceB.children.Add(new BTPlaceAvailable());
        SequenceB.children.Add(new BTGoToPlace());
        SequenceB.children.Add(new BTInPlace());
        SequenceB.children.Add(new BTSearchPlayer());
        SequenceB.children.Add(new BTPlayerCloseOrInPointOfViewInPlace());
        SequenceB.children.Add(SelectorC);


        BTSequance SequenceC = new BTSequance();

        SequenceC.children.Add(new BTPlaceNotAvailable());
        SequenceC.children.Add(new BTNotInPlace());
        SequenceC.children.Add(new BTPlayerNotCloseOrInPointOfView());
        SequenceC.children.Add(new BTPatrol());

        BTSelector SelectorA = new BTSelector();

        SelectorA.children.Add(SequenceA);
        SelectorA.children.Add(new BTInPlace());
        SelectorA.children.Add(SequenceB);
        SelectorA.children.Add(SequenceC);

        BTSelector SelectorB = new BTSelector();

        SelectorB.children.Add(new BTHaveAmmo());
        SelectorB.children.Add(new BTReload());

        BTSequance SequenceD = new BTSequance();

        SequenceD.children.Add(SelectorA);
        SequenceD.children.Add(SelectorB);
        SequenceD.children.Add(new BTAim());
        SequenceD.children.Add(new BTAtirar());

        BehaviorTree bt = GetComponent<BehaviorTree>();
        bt.root = SequenceD;

        StartCoroutine(bt.Execute());
    }



    public void Sleep()
    {
        StopAllCoroutines();
    }
    public void MoveToTarget(GameObject target, NavMeshAgent agent, float StopDistance)
    {
        agent.stoppingDistance = StopDistance;
        agent.SetDestination(target.transform.position);

        

        if(SeePlayer == true)
        {
            animatorController.Run = true;
        }
        else
        {
            animatorController.Walk = true;
        }
    }
    public void MoveAwayFromTarget(GameObject target, NavMeshAgent agent )
    {
        
        agent.SetDestination(-target.transform.position);
    }

    //Random Patrol

    public bool RandomPoint(Vector3 center,float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;

        if(NavMesh.SamplePosition(randomPoint, out hit,  1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    //fild of view
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTrgets();
        }
    }
    void FindVisibleTrgets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, PlayerMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                

                if(!Physics.Raycast(transform.position, dirToTarget, dstToTarget, AmbienteMask))
                {
                    visibleTargets.Add(target);
                    SeePlayer = true;
                  
                    
                }

            }
        }
    }

    public Vector3 DirFromAngle(float AngleInDegrees, bool AngleIsGlobal)
    {
        if (!AngleIsGlobal)
        {
            AngleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(AngleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(AngleInDegrees * Mathf.Deg2Rad));
    }
}



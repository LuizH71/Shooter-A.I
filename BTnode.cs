using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BTnode
{
    public enum Status {RUNNING , SUCCESS, FAILURE};
    public Status status;

    abstract public IEnumerator Run(BehaviorTree bt);

    public void Print()
    {
        Debug.Log(this.GetType().Name + ": "+ status.ToString());
    }
}

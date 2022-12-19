using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree : MonoBehaviour
{
    public BTnode root;

    public IEnumerator Execute()
    {
        while (true)
        {
            yield return StartCoroutine(root.Run(this));
        }
    }
}

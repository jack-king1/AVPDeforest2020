using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToObject : MonoBehaviour
{
    public float timeToReachDestination;
    public GameObject startPos;
    public GameObject endPos;

    public Vector3 StartPositionVector;
    public Vector3 Destination;
    void Start()
    {
        StartPositionVector = startPos.transform.position;
        Destination = endPos.transform.position;
        StartCoroutine(MoveToPosition(gameObject.transform,Destination,timeToReachDestination));
    }

    public IEnumerator MoveToPosition(Transform transform, Vector3 des, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, des, t);
            yield return null;
        }
    }
}

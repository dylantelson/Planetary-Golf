using UnityEngine;
using System.Collections;

public class Stomp : MonoBehaviour {

    public Transform startPoint;
    public GameObject objectToMove;
    public Transform endPoint;

    public float timeToWait;
    public float timeToWaitStart;

    public float moveSpeed;

    private Vector3 currentTarget;

    public bool waiting;

    // Use this for initialization
    void Start()
    {
        currentTarget = endPoint.position;
        waiting = true;
        StartCoroutine("StartWait");
    }

    // Update is called once per frame
    void Update()
    {
        if (!waiting)
        {

            if (currentTarget == endPoint.position)
            {
                objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, currentTarget, moveSpeed * Time.deltaTime * 4);
            } else
            {
                objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, currentTarget, moveSpeed * Time.deltaTime * 1.5f);
            }
            if (objectToMove.transform.position == endPoint.position)
            {
                StartCoroutine("Go");
                waiting = true;
                currentTarget = startPoint.position;
            }
            else if (objectToMove.transform.position == startPoint.position)
            {
                StartCoroutine("Go");
                waiting = true;
                currentTarget = endPoint.position;
            }

        }
    }

    public IEnumerator StartWait()
    {
        yield return new WaitForSeconds(timeToWaitStart);
        waiting = false;
    }


    public IEnumerator Go()
    {
        yield return new WaitForSeconds(timeToWait);
        waiting = false;
    }

}

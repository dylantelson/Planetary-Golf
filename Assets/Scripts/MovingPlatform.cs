using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    public Transform startPoint;
    public GameObject objectToMove;
    public Transform endPoint;

    public bool lerp;

    public float moveSpeed;

    private Vector3 currentTarget;

    // Use this for initialization
    void Start()
    {
        currentTarget = endPoint.position;
    }

    void Update()
    {
        if (!lerp)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, currentTarget, moveSpeed * Time.deltaTime);
        }
        else {
            objectToMove.transform.position = Vector3.Lerp(objectToMove.transform.position, currentTarget, moveSpeed * Time.deltaTime);
        }

        if (objectToMove.transform.position == endPoint.position)
        {
            currentTarget = startPoint.position;
        }
        else if (objectToMove.transform.position == startPoint.position)
        {
            currentTarget = endPoint.position;
        }


    }
}

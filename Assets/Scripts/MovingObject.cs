using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public GameObject objectToMove;
    public Transform startPoint;
    public Transform endPoint;
    public float movingSpeed;

    private Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        target = startPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (objectToMove.transform.position == endPoint.transform.position)
        {
            target = startPoint.transform.position;
        }
        else if (objectToMove.transform.position == startPoint.transform.position)
        {
            target = endPoint.position;
        }

        objectToMove.transform.position = Vector2.MoveTowards(objectToMove.transform.position, target, movingSpeed * Time.deltaTime);
    }
}

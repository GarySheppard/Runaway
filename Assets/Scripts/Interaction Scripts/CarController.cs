using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private float targetPos1X = 55f;
    [SerializeField]
    private float targetPos2X = 60f;
    [SerializeField]
    private float yPos = 0f;
    [SerializeField]
    private float zPos = 0f;
    [SerializeField]
    private float speed = 1f;
    public bool canMove = true;
    private bool firstMove = true;

    void Start()
    {
        transform.position = new Vector3(targetPos1X, yPos, zPos);
    }

    void Update()
    {
        if (canMove)
        {
            float currentTargetX = firstMove ? targetPos1X : targetPos2X;
            Vector3 currentTarget = new Vector3(currentTargetX, yPos, zPos);
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, currentTarget) < 0.001f)
            {
                firstMove = !firstMove;
            }
        }
    }
}

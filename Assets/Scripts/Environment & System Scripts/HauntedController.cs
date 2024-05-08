using UnityEngine;

public class HauntedController : MonoBehaviour
{
    private float speed = 2f;
    private Vector3 targetPosition;
    private float tumbleForce = 10;

    void Start()
    {
        SetRandomTarget();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTarget();
        }
    }

    void FixedUpdate()
    {
        transform.Rotate(Random.insideUnitSphere * tumbleForce * Time.deltaTime);
    }

    void SetRandomTarget()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        targetPosition = transform.position + randomDirection * 10f;
    }
}

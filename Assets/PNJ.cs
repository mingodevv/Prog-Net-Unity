using UnityEngine;
using System.Collections;
using Unity.Netcode;

public class RandomWanderAI : NetworkBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    public float wanderRadius = 5f;
    public float stopDistance = 0.2f;

    [Header("Timing")]
    public float waitTimeMin = 1f;
    public float waitTimeMax = 3f;

    private Vector3 spawnPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Start()
    {
        spawnPosition = transform.position;
        StartCoroutine(WanderRoutine());
    }

    private void Update()
    {
        if (!isMoving)
            return;

        Vector3 direction = (targetPosition - transform.position);
        direction.y = 0f;

        if (direction.magnitude <= stopDistance)
        {
            isMoving = false;
            return;
        }

        Vector3 move = direction.normalized * moveSpeed * Time.deltaTime;
        transform.position += move;
        
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);
    }

    private IEnumerator WanderRoutine()
    {
        while (true)
        {
            Vector2 randomCircle = Random.insideUnitCircle * wanderRadius;
            targetPosition = spawnPosition + new Vector3(randomCircle.x, 0f, randomCircle.y);

            isMoving = true;
            
            while (isMoving)
                yield return null;
            
            float waitTime = Random.Range(waitTimeMin, waitTimeMax);
            yield return new WaitForSeconds(waitTime);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Application.isPlaying ? spawnPosition : transform.position, wanderRadius);
    }
#endif
}
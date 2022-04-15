using UnityEngine;

public class FishAI : MonoBehaviour
{

    public FishType fishType;
    [SerializeField] private float idleRadius;
    [SerializeField] private Vector3 currentIdlePos;
    [SerializeField] private float idleSpeed;

    [Range(0f, 20f)]
    [SerializeField] private float minIdleTime = 1f;

    [Range(0f, 20f)]
    [SerializeField] private float maxIdleTime = 1f;

    public bool isArrived;

    public Vector3 startPos;
    private float idleTime;
    private float speed;
    private float waitTime;

    private void Start()
    {
        startPos = transform.position;
        currentIdlePos = startPos;
    }

    private void Update()
    {
        CheckArrived();
        PatrolControl();
    }

    private void CheckArrived()
    {

        float dist = Vector3.Distance(transform.position, currentIdlePos);
        if (dist < 0.2f)
        {
            isArrived = true;
            idleTime = Random.Range(minIdleTime, maxIdleTime);
        }
        else
        {
            transform.LookAt(currentIdlePos);
            transform.position = Vector3.LerpUnclamped(transform.position, currentIdlePos, Time.deltaTime * idleSpeed);
        }
    }

    private void PatrolControl()
    {
        if (!isArrived) return;
        waitTime += Time.deltaTime;
        if (waitTime >= idleTime)
        {
            waitTime = 0;
            float xRnd = transform.position.x + Random.insideUnitCircle.x * idleRadius;
            float zRnd = transform.position.z + Random.insideUnitCircle.y * idleRadius;
            Vector3 goPos = new Vector3(xRnd, transform.position.y, zRnd);
            currentIdlePos = goPos;
            isArrived = false;
        }
    }
}

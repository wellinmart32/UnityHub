using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpikeBallPosition : MonoBehaviour
{
    public GameObject wayPoint;
    public Checkpoint checkpoint;
    private ChasingSpikeBall chasingSpikeBall;

    private void Awake()
    {
        chasingSpikeBall = GetComponent<ChasingSpikeBall>();
    }

    private void Update()
    {
        if (checkpoint.isReached)
        {
            ChangePosition();
        }
    }

    private void ChangePosition()
    {
        chasingSpikeBall.DisableSpikeBall();
        transform.position = wayPoint.transform.position;
    }
}

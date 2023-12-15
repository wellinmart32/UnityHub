using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDayNightCycle : MonoBehaviour
{
    [SerializeField] private DayNightCycleController dayNightCycleController;
    private Chronometer chronometer;
    [SerializeField] private float switchTime = 20;

    private void Start()
    {
        dayNightCycleController = GetComponent<DayNightCycleController>();
        chronometer = GameObject.Find("Chronometer").GetComponent<Chronometer>();
    }

    private void Update()
    {
        SwitchCycleTime();

        if (chronometer.countText.gameObject.activeInHierarchy)
        {
            dayNightCycleController.canSwitchCycle = true;
        }
        else if(!chronometer.countText.gameObject.activeInHierarchy && !chronometer.chronometerInstance.isInside)
        {
            dayNightCycleController.canSwitchCycle = false;
        }
    }

    private void SwitchCycleTime()
    {
        if (chronometer.time <= 0.01f && !chronometer.countText.gameObject.activeInHierarchy)
        {
            dayNightCycleController.cycleTime = switchTime;
        }
        else if (chronometer.time >= switchTime - 0.09f && chronometer.time <= switchTime + 0.09f && chronometer.clockwise)
        {
            dayNightCycleController.cycleTime = chronometer.startTime;
            dayNightCycleController.RestartDayNightCycle();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycleController : MonoBehaviour
{
    public bool canSwitchCycle;
    [SerializeField] public float cycleTime;
    [SerializeField] public Light2D globalLight;
    [SerializeField] public DayNightCycle[] dayNightCycle;
    private float currentTimeCycle = 0;
    private float cyclePercentage;
    private int currentCycle = 0;
    private int nextCycle = 1;

    private void Start()
    {
        globalLight.color = dayNightCycle[0].cicleColor;
    }

    private void Update()
    {
        if (canSwitchCycle)
        {
            currentTimeCycle += Time.deltaTime;
            cyclePercentage = currentTimeCycle / cycleTime;

            if (currentTimeCycle >= cycleTime)
            {
                currentTimeCycle = 0;
                currentCycle = nextCycle;

                if (nextCycle + 1 > dayNightCycle.Length - 1)
                {
                    nextCycle = 0;
                }
                else
                {
                    nextCycle += 1;
                }
            }

            ChangeColor(dayNightCycle[currentCycle].cicleColor, dayNightCycle[nextCycle].cicleColor);
        }
        else
        {
            cycleTime = 30;
            RestartDayNightCycle();
        }
    }

    private void ChangeColor(Color currentColor, Color nextColor)
    {
        globalLight.color = Color.Lerp(currentColor, nextColor, cyclePercentage);

    }

    public void RestartDayNightCycle()
    {
        currentTimeCycle = 0;
        cyclePercentage = 0;
        currentCycle = 0;
        nextCycle = 1;
        globalLight.color = dayNightCycle[0].cicleColor;
    }
}

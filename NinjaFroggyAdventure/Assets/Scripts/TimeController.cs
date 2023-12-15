using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    //public bool canStopTime;

    //private void Update()
    //{
    //    if (canStopTime)
    //    {
    //        Time.timeScale = 0;
    //    }
    //    else
    //    {

    //        Time.timeScale = 1;
    //    }
    //}

    public void StopTime()
    {
        //canStopTime = true;
        Time.timeScale = 0;
    }

    public void RestoreTime()
    {
        //canStopTime = false;
        Time.timeScale = 1;
    }
}

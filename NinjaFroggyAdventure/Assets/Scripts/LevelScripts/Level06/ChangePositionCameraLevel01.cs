using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePositionCameraLevel01 : MonoBehaviour
{
    public Camera[] cameras;

    void Update()
    {
        cameras[0].gameObject.SetActive(false);

        if (transform.position.x >= 66f && transform.position.y >= -5.3f)
        {
            cameras[0].gameObject.SetActive(false);
            cameras[1].transform.position = new Vector3(66f, transform.position.y, -10);
            cameras[1].gameObject.SetActive(true);
        }
        else if (transform.position.x >= 62f && transform.position.y <= -23f)
        { 
            cameras[0].gameObject.SetActive(false);
            cameras[1].transform.position = new Vector3(transform.position.x, -23f, -10);
            cameras[1].gameObject.SetActive(true);

            if (transform.position.y <= - 25f)
            {
                Time.timeScale = 0;
            }
        }
        else
        {
            cameras[0].gameObject.SetActive(true);
            cameras[1].gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chronometer : MonoBehaviour
{
    public Text countText;
    public bool clockwise;
    public float startTime;
    public float time;
    public ChronometerInstance chronometerInstance;
    public AudioSource[] clips;
    public int[] backgroundSoundnumbers;
    private PlayerInfo playerInfo;
    private BackgroundSound backgroundSound;

    private void Awake()
    {
        playerInfo = GameObject.Find("StandarInterface").GetComponent<Initialization>().playerInfo;
        chronometerInstance = GetComponentInChildren<ChronometerInstance>();
        countText = GameObject.Find("CountText").GetComponent<Text>();
        backgroundSound = GameObject.Find("StandarInterface").GetComponent<Initialization>().backgroundSound;
        time = startTime;
    }

    private void Update()
    {
        if (countText.enabled)
        {
            CalculateTime();
        }

        if (!chronometerInstance.isInside)
        {
            FinishCount();

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<ChronometerWatcher>() != null)
                {
                    transform.GetChild(i).GetComponent<ChronometerWatcher>().enableTimer = true;
                }
            }
        }
        
        if (playerInfo.currentHearts <= 0 && chronometerInstance.isInside
            && GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>().lastCheckpointReached == 1)
        {
            FinishCount();
            StartCoroutine(Recount());

            if (backgroundSoundnumbers != null)
            {
                backgroundSound.listNumber = backgroundSoundnumbers[0];
            }
        }

        if (countText.text == "0:-1")
        {
            
            countText.gameObject.SetActive(false);
            countText.text = "0:00";

            if (backgroundSoundnumbers != null)
            {
                backgroundSound.listNumber = backgroundSoundnumbers[1];
            }
        }
    }

    

    private IEnumerator Recount()
    {
        yield return new WaitForSeconds(2);
        StartCount();
    }

    private void CalculateTime()
    {
        if (clockwise)
        {
            time += Time.deltaTime;
        }
        else
        {
            time -= Time.deltaTime;
        }

        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        countText.text = minutes.ToString() + ":" + seconds.ToString().PadLeft(2, '0');
    }

    public void StartCount()
    {
        time = startTime;
        CalculateTime();
        countText.gameObject.SetActive(true);
    }

    public void FinishCount()
    {
        countText.gameObject.SetActive(false);
        time = startTime;
    }
}

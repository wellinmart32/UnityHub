using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionImage : MonoBehaviour
{
    private GameObject player;
    private Image image;

    private void Awake()
    {
        player = GameObject.Find("StandarInterface").GetComponent<Initialization>().player;
        image = GetComponent<Image>();
    }

    public void StartTransition(float start, float middle, float finish)
    {
        image.CrossFadeAlpha(255, start, true);
        StartCoroutine(FinishTransition(middle, finish));
    }

    private IEnumerator FinishTransition(float middle, float finish)
    {
        yield return new WaitForSecondsRealtime(middle);
        image.CrossFadeAlpha(1, finish, true);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}

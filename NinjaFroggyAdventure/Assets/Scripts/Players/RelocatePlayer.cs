using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelocatePlayer : MonoBehaviour
{
    public Vector2 relocationPosition;
    public bool canRelocate = true;
    public string previousLevel;
    public float waitTime = 0.2f;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.Find("StandarInterface").GetComponent<Initialization>().player;
    }

    private void Update()
    {
        Relocate();
    }

    private void Relocate()
    {
        if (LevelNameController.levelNameController != null)
        {
            if (canRelocate && LevelNameController.levelNameController.levelName == previousLevel)
            {
                player.transform.position = relocationPosition;
                player.GetComponent<SpriteRenderer>().flipX = true;
                StartCoroutine(DisableRelocation());
            }
        }
    }

    private IEnumerator DisableRelocation()
    {
        yield return new WaitForSeconds(waitTime);
        canRelocate = false;
        gameObject.SetActive(false);
    }
}

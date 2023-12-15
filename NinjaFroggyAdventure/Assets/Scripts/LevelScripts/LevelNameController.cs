using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNameController : MonoBehaviour
{
    public static LevelNameController levelNameController;
    public string levelName;

    private void Awake()
    {
        levelName = SceneManager.GetActiveScene().name;

        if (LevelNameController.levelNameController == null)
        {
            LevelNameController.levelNameController = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}

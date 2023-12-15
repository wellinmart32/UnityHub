using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour
{
    public bool runIngackground;

    public GameObject player;
    public GameObject frostGuardianAvatar;
    public GameObject playerSkills;
    public TransitionImage transitionImage;
    public CheckpointManager checkpointManager;
    public EndLevelReward endLevelReward;
    public GameDataManager gameDataManager;
    public InventoryDataManager inventoryDataManager;
    public AdDataManager adDataManager;
    public PlayerSkillDataManager playerSkillDataManager;
    public EnvironmentInfo environmentInfo;
    public AdInfo adInfo;
    public PlayerInfo playerInfo;
    public PlayerSkillInfo playerSkillInfo;
    public OptionsManager optionsManager;
    public DialogueInterface dialogueInterface;
    public TimeController timeController;
    public InsideGroundVerification InsideGroundVerificator;

    public GameObject fruitManager;
    public GameObject gemManager;
    public GameObject briefManager;
    public GameObject LifeManager;
    public GameObject boxManager;
    public GameObject itemManager;
    public GameObject heartManager;
    public GameObject enemyManager;
    public GameObject chestManager;

    public GameObject inventory;
    public GameObject merchantStore;
    public GameObject weaponManager;
    public GameObject mobileControls;

    public GameObject enemySoundController;
    public GameObject trapSoundController;

    public BackgroundSound backgroundSound;

    private void Awake()
    {
        Application.runInBackground = runIngackground;
    }
}

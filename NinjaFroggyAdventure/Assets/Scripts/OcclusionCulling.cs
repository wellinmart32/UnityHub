using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionCulling : MonoBehaviour
{
    public int occlusionDistance = 5;
    private GameObject[] objectList;
    private GameObject player;
    private GameObject frostGuardianAvatar;
    private GameObject activePlayer;

    private void Awake()
    {
        player = GameObject.Find("StandarInterface").GetComponent<Initialization>().player;
        frostGuardianAvatar = GameObject.Find("StandarInterface").GetComponent<Initialization>().frostGuardianAvatar;
        activePlayer = player;
    }

    private void Start()
    {
        objectList = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            objectList[i] = transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        SwitchVisibility();
    }

    private void SwitchVisibility()
    {
        SetActivePlayer();

        for (int i = 0; i < objectList.Length; i++)
        {
            Vector2 objectPosition = objectList[i].transform.position;
            Vector2 objectScale = objectList[i].transform.localScale;
            float radio;

            if (objectScale.x > objectScale.y)
            {
                radio = objectScale.x / 2;
            }
            else
            {
                radio = objectScale.y / 2;
            }

            if (Vector2.Distance(objectPosition, activePlayer.transform.position) - radio > occlusionDistance)
            {
                SwitchVisibility(objectList[i], false);
            }
            else
            {
                SwitchVisibility(objectList[i], true);
            }
        }
    }

    private void SwitchVisibility(GameObject ob, bool state)
    {
        if (ob.GetComponent<FruitCollected>())
        {
            if (!ob.GetComponent<FruitCollected>().inChest)
            {
                if (state)
                {
                    if (ob.GetComponent<FruitCollected>().canRestore && !ob.GetComponent<FruitCollected>().isCollected)
                    {
                        ob.SetActive(true);
                        ob.GetComponent<FruitCollected>().canRestore = false;
                    }
                }
                else
                {
                    ob.GetComponent<FruitCollected>().canRestore = true;
                    ob.SetActive(false);
                }
            }
        }
        else if (ob.GetComponent<BriefCollected>())
        {
            if (!ob.GetComponent<BriefCollected>().inChest && !ob.GetComponent<BriefCollected>().inBox)
            {
                if (state)
                {
                    if (ob.GetComponent<BriefCollected>().canRestore && !ob.GetComponent<BriefCollected>().isCollected)
                    {
                        ob.SetActive(true);
                        ob.GetComponent<BriefCollected>().canRestore = false;
                    }
                }
                else
                {
                    ob.GetComponent<BriefCollected>().canRestore = true;
                    ob.SetActive(false);
                }
            }
        }
        else if (ob.GetComponent<GemCollected>())
        {
            if (!ob.GetComponent<GemCollected>().inChest)
            {
                if (state)
                {
                    if (ob.GetComponent<GemCollected>().canRestore && !ob.GetComponent<GemCollected>().isCollected)
                    {
                        ob.SetActive(true);
                        ob.GetComponent<GemCollected>().canRestore = false;
                    }
                }
                else
                {
                    ob.GetComponent<GemCollected>().canRestore = true;
                    ob.SetActive(false);
                }
            }
        }
        else if (ob.GetComponent<LifeCollected>())
        {
            if (!ob.GetComponent<LifeCollected>().inChest)
            {
                if (state)
                {
                    if (ob.GetComponent<LifeCollected>().canRestore && !ob.GetComponent<LifeCollected>().isCollected)
                    {
                        ob.SetActive(true);
                        ob.GetComponent<LifeCollected>().canRestore = false;
                    }
                }
                else
                {
                    ob.GetComponent<LifeCollected>().canRestore = true;
                    ob.SetActive(false);
                }
            }
        }
        else if (ob.GetComponent<HeartCollected>())
        {
            if (!ob.GetComponent<HeartCollected>().inChest)
            {
                if (state)
                {
                    if (ob.GetComponent<HeartCollected>().canRestore && !ob.GetComponent<HeartCollected>().isCollected)
                    {
                        ob.SetActive(true);
                        ob.GetComponent<HeartCollected>().canRestore = false;
                    }
                }
                else
                {
                    ob.GetComponent<HeartCollected>().canRestore = true;
                    ob.SetActive(false);
                }
            }
        }
        else if (ob.GetComponent<RespawnEnemy>())
        {
            if (state)
            {
                if (ob.GetComponent<RespawnEnemy>().canRestore)
                {
                    ob.GetComponent<RespawnEnemy>().RestoreEnemy();
                    ob.GetComponent<RespawnEnemy>().canRestore = false;
                }
            }
            else
            {
                ob.GetComponent<RespawnEnemy>().canRestore = true;

                if (ob.GetComponentInChildren<AISounds>())
                {
                    ob.GetComponentInChildren<AISounds>().FreeAllUsedSounds();
                }

                ob.SetActive(false);
            }
        }
        else if (ob.GetComponent<ItemCollected>())
        {
            if (!ob.GetComponent<ItemCollected>().inChest && !ob.GetComponent<ItemCollected>().inBox)
            {
                if (state)
                {
                    if (ob.GetComponent<ItemCollected>().canRestore && !ob.GetComponent<ItemCollected>().isCollected)
                    {
                        ob.SetActive(true);
                        ob.GetComponent<ItemCollected>().canRestore = false;
                    }
                }
                else
                {
                    ob.GetComponent<ItemCollected>().canRestore = true;
                    ob.SetActive(false);
                }
            }
        }
        else
        {
            if (state)
            {
                ob.SetActive(true);
            }
            else
            {
                if (ob.GetComponent<AISounds>())
                {
                    if (ob.GetComponent<AISounds>().subSoundType == AISounds.SubSoundType.SpearTrap)
                    {
                        ob.GetComponent<AISounds>().FreeAllUsedSounds();
                    }
                }
                else if (ob.GetComponentInChildren<AISounds>())
                {
                    ob.GetComponentInChildren<AISounds>().FreeAllUsedSounds();
                }

                ob.SetActive(false);
            }
        }
    }

    public void SetActivePlayer()
    {
        if (frostGuardianAvatar.activeInHierarchy)
        {
            activePlayer = frostGuardianAvatar;
        }
        else
        {
            activePlayer = player;
        }
    }
}

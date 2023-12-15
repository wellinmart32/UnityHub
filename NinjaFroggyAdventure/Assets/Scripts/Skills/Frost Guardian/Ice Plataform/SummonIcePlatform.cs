using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonIcePlatform : MonoBehaviour
{
    public GameObject icePlatform;
    public float xSeparation;
    public float ySeparation;
    public float summonTime;
    private GameObject frostGuardianAvatar;
    private SpriteRenderer spriteRenderer;
    private FrostGuardianMove frostGuardianMove;
    public bool[] isActiveList;
    public int totalPlatforms = 5;

    public int platformPosition;
    public int currentPlatforms;
    public bool canSummon;
    public bool canSummonAgain = true;

    private void Awake()
    {
        frostGuardianAvatar = GameObject.Find("StandarInterface").GetComponent<Initialization>().frostGuardianAvatar;
        spriteRenderer = frostGuardianAvatar.GetComponent<SpriteRenderer>();
        frostGuardianMove = frostGuardianAvatar.GetComponent<FrostGuardianMove>();
        isActiveList = new bool[totalPlatforms];
    }

    private void Update()
    {
        SummonPlatformGroup();
    }

    private void OnEnable()
    {
        ResetSummonParameters();
    }

    private void SummonPlatformGroup()
    {
        if (canSummon && canSummonAgain && currentPlatforms < totalPlatforms)
        {
            canSummonAgain = false;
            StartCoroutine(Summon());
        }
        
        if (currentPlatforms == totalPlatforms)
        {
            canSummon = false;
            frostGuardianMove.canMove = true;
            currentPlatforms = 0;
        }
    }

    private void ResetSummonParameters()
    {
        frostGuardianMove.canMove = true;
        platformPosition = 0;
        currentPlatforms = 0;
        canSummon = false;

        for (int i = 0; i < totalPlatforms; i++)
        {
            isActiveList[i] = false;
        }
    }

    private int FindAvailablePlatform()
    {
        for (int i = 0; i < isActiveList.Length; i++)
        {
            if (!isActiveList[i])
            {
                isActiveList[i] = true;
                return i;
            }
        }

        return totalPlatforms;
    }

    private void EnablePlatform(int pos)
    {
        isActiveList[pos] = false;
    }

    private IEnumerator Summon()
    {
        int currentPlatformPosition;
        yield return new WaitForSeconds(summonTime);

        if (canSummon)
        {
            currentPlatforms = FindAvailablePlatform();
            currentPlatformPosition = currentPlatforms;

            if (currentPlatforms < totalPlatforms)
            {
                platformPosition += 1;
                canSummonAgain = true;
                Vector3 currentPosition;
                
                if (spriteRenderer.flipX)
                {
                    currentPosition = new Vector2(frostGuardianAvatar.transform.position.x + platformPosition * xSeparation, frostGuardianAvatar.transform.position.y - ySeparation);
                }
                else
                {
                    currentPosition = new Vector2(frostGuardianAvatar.transform.position.x - platformPosition * xSeparation, frostGuardianAvatar.transform.position.y - ySeparation);
                }

                GameObject iP = Instantiate(icePlatform, currentPosition, transform.rotation);
                var iPM = iP.GetComponent<IcePlatformMovement>();
                iPM.velocity += iPM.velocity * platformPosition * 0.25f;
                yield return new WaitForSeconds(iPM.lifeTime * 2);
                EnablePlatform(currentPlatformPosition);
            }
        }
    }

    public void EnableSummoning()
    {
        canSummon = !canSummon;

        if (!canSummon)
        {
            frostGuardianMove.canMove = true;
        }
        else
        {
            frostGuardianMove.canMove = false;
            platformPosition = 0;
            canSummonAgain = true;
        }
    }

    public void RestoreCounter(int position)
    {
        isActiveList[position] = false;
    }
}

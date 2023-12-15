using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feint : MonoBehaviour
{
    public Vector2 areaSize;
    public float areaSeparation = 0.5f;
    public float feintTime = 10f;
    private GameObject feintTarget;
    private SpriteRenderer playerSpriteRenderer;
    public bool canFeint;

    public float xSeparation;
    public float ySeparation;
    private bool feintInCooldown;
    private bool isLookingForPosition;
    private GameObject player;
    private InsideGroundVerification insideGroundVerification;

    private void Awake()
    {
        player = GameObject.Find("StandarInterface").GetComponent<Initialization>().player;
        insideGroundVerification = GameObject.Find("StandarInterface").GetComponent<Initialization>().InsideGroundVerificator;
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKey("1"))
        {
            canFeint = true;
        }

        if (feintInCooldown)
        {
            canFeint = false;
        }

        if (canFeint && !feintInCooldown)
        {
            LookingForPosition();

            if (insideGroundVerification.isPositionFound)
            {
                insideGroundVerification.isPositionFound = false;
                canFeint = false;
                StartCoroutine(MakeFeint(insideGroundVerification.finishPosition));
            }
        }
    }

    private void LookingForPosition()
    {
        if (!isLookingForPosition)
        {
            feintTarget = GetFeintTarget();

            if (feintTarget)
            {
                isLookingForPosition = true;
                Vector2 newPlayerPosition;

                if (playerSpriteRenderer.flipX)
                {
                    newPlayerPosition = new Vector2(feintTarget.transform.position.x - xSeparation, feintTarget.transform.position.y + ySeparation);
                }
                else
                {
                    newPlayerPosition = new Vector2(feintTarget.transform.position.x + xSeparation, feintTarget.transform.position.y + ySeparation);
                }
                insideGroundVerification.GetPositionOffGround(newPlayerPosition);
            }
            else
            {
                canFeint = false;
            }
        }
    }

    private IEnumerator MakeFeint(Vector2 newPlayerPosition)
    {
        if (feintTarget)
        {
            feintInCooldown = true;

            player.transform.position = newPlayerPosition;

            if (feintTarget.GetComponent<BasicAI>())
            {
                StartCoroutine(feintTarget.GetComponent<BasicAI>().GotFeint());
            }
            else if (feintTarget.GetComponent<BeeAI>())
            {
                StartCoroutine(feintTarget.GetComponent<BeeAI>().GotFeint());
            }
            else if (feintTarget.GetComponent<BatAI>())
            {
                StartCoroutine(feintTarget.GetComponent<BatAI>().GotFeint());
            }
            else if (feintTarget.GetComponent<PlantAI>())
            {
                StartCoroutine(feintTarget.GetComponent<PlantAI>().GotFeint());
            }

            yield return new WaitForSeconds(feintTime);
            insideGroundVerification.RestartValues();
            isLookingForPosition = false;
            feintInCooldown = false;
        }

    }

    private GameObject GetFeintTarget()
    {
        Collider2D[] objects;

        if (playerSpriteRenderer.flipX)
        {
            objects = Physics2D.OverlapBoxAll(new Vector2(player.transform.position.x - areaSeparation, player.transform.position.y), areaSize, 0);
        }
        else
        {
            objects = Physics2D.OverlapBoxAll(new Vector2(player.transform.position.x + areaSeparation, player.transform.position.y), areaSize, 0);
        }

        foreach (Collider2D ob in objects)
        {
            if (ob.gameObject.CompareTag("Enemy"))
            {
                if (ob.gameObject.name == "Mushroom" || ob.gameObject.name == "Plant" || ob.gameObject.name == "Bee"
                     || ob.gameObject.name == "Snail" || ob.gameObject.name == "BlueBird" || ob.gameObject.name == "Fish")
                {
                    if (ob.gameObject.GetComponent<BasicAI>())
                    {
                        return ob.gameObject;
                    }
                    else if (ob.gameObject.GetComponent<BeeAI>())
                    {
                        return ob.gameObject;
                    }
                    else if (ob.gameObject.GetComponent<PlantAI>())
                    {
                        return ob.gameObject;
                    }

                    return ob.gameObject;
                }
                else if (ob.gameObject.GetComponent<BatAI>())
                {
                    return ob.gameObject;
                }
            }
        }

        return null;
    }

    public void EnableFeint()
    {
        canFeint = true;
    }

    private void OnDrawGizmos()
    {
        player = GameObject.Find("StandarInterface").GetComponent<Initialization>().player;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector2(player.transform.position.x + areaSeparation, player.transform.position.y), areaSize);
    }
}

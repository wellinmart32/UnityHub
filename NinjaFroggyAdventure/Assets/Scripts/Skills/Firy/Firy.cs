using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firy : MonoBehaviour
{
    [Header("Firy")]
    public GameObject firy;
    public float firyXSeparation = 0.2f, firyYSeparation = 0.3f;
    public float deadTime;
    public int damage;
    public AudioSource firySummonSound;
    [SerializeField] private float timeCanSummonFiry = 90f;
    private bool enableSummonFiry;
    private GameObject player;
    private PlayerMoveJoystick playerMoveJoystick;
    private bool firyInCooldown;
    private bool isFirySummoned;
    private int orientation = 1;

    private void Awake()
    {
        player = GameObject.Find("StandarInterface").GetComponent<Initialization>().player;
        playerMoveJoystick = player.GetComponent<PlayerMoveJoystick>();
    }

    private void Update()
    {
        if (playerMoveJoystick.canMove)
        {
            if (enableSummonFiry && !isFirySummoned && !firyInCooldown)
            {
                StartCoroutine(SummonFiry());
            }
            else if (Input.GetKey("r") && !isFirySummoned && !firyInCooldown)
            {
                StartCoroutine(SummonFiry());
            }
        }
    }

    public IEnumerator SummonFiry()
    {
        firySummonSound.Play();

        if (player.GetComponent<SpriteRenderer>().flipX)
        {
            orientation = -1;
        }
        else
        {
            orientation = 1;
        }

        GameObject newFiry = Instantiate(firy, player.transform.position + new Vector3(firyXSeparation * orientation, firyYSeparation), firy.transform.rotation);
        BoxCollider2D bC2D = newFiry.transform.GetChild(0).GetComponent<BoxCollider2D>();

        if (player.GetComponent<SpriteRenderer>().flipX)
        {
            newFiry.GetComponent<SpriteRenderer>().flipX = true;
        }
        
        bC2D.offset = new Vector2(orientation * bC2D.offset.x, bC2D.offset.y);
        isFirySummoned = true;
        firyInCooldown = true;
        newFiry.GetComponent<FirySkill>().deadTime = deadTime;
        newFiry.GetComponentInChildren<HurtEnemies>().damage = damage;
        yield return new WaitForSeconds(deadTime);
        isFirySummoned = false;
        yield return new WaitForSeconds(timeCanSummonFiry);
        enableSummonFiry = false;
        firyInCooldown = false;
    }

    public void EnableFiry()
    {
        enableSummonFiry = true;
    }
}

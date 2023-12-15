using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostGuardian : MonoBehaviour
{
    [Header("Frost Guardian")]
    public float yDisplacement = 0.25f;
    public float avatarTime = 30f;
    public float timeCanTransformAvatar = 10f;
    public GameObject inventoryButton;
    private bool canTransform;
    private bool isPlayerTranformed;
    private bool transformInCooldown;
    private bool isTransformed;
    private GameObject player;
    private SpriteRenderer playerSR;
    private PlayerMoveJoystick playerMoveJoystick;
    private GameObject frostGuardianAvatar;
    private SpriteRenderer frostGuardianSR;

    private void Awake()
    {
        player = GameObject.Find("StandarInterface").GetComponent<Initialization>().player;
        frostGuardianAvatar = GameObject.Find("StandarInterface").GetComponent<Initialization>().frostGuardianAvatar;
        playerMoveJoystick = player.GetComponent<PlayerMoveJoystick>();
        playerSR = player.GetComponent<SpriteRenderer>();
        frostGuardianSR = frostGuardianAvatar.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerMoveJoystick.canMove)
        {
            if (canTransform && !isPlayerTranformed && !transformInCooldown)
            {
                Transform();
            }
            else if (Input.GetKey("x") && !isPlayerTranformed && !transformInCooldown)
            {
                Transform();
            }
        }
    }

    public void Transform()
    {
        inventoryButton.SetActive(false);
        isTransformed = true;
        transformInCooldown = true;
        isPlayerTranformed = true;
        Vector3 position = new Vector2(player.transform.position.x, player.transform.position.y + yDisplacement);
        StartCoroutine(DestranformationCount());
        frostGuardianAvatar.transform.position = position;
        frostGuardianAvatar.SetActive(true);
        player.SetActive(false);
        frostGuardianAvatar.GetComponent<FrostGuardianLife>().RestartLifes();
    }

    private IEnumerator DestranformationCount()
    {
        yield return new WaitForSeconds(avatarTime);
        Destransformation();
        yield return new WaitForSeconds(timeCanTransformAvatar);
        canTransform = false;
        transformInCooldown = false;
    }

    public void Destransformation()
    {
        if (isTransformed)
        {
            playerSR.flipX = !frostGuardianSR.flipX;
            inventoryButton.SetActive(true);
            isTransformed = false;
            isPlayerTranformed = false;
            Vector3 position = new Vector2(frostGuardianAvatar.transform.position.x, frostGuardianAvatar.transform.position.y - yDisplacement);
            player.transform.position = position;
            player.SetActive(true);
            frostGuardianAvatar.SetActive(false);
        }
    }

    public void EnableTransform()
    {
        canTransform = true;
    }
}

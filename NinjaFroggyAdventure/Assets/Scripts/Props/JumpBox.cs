using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBox : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRederer;
    public GameObject brokenParts;
    public float jumpforce = 4;
    public int lifes = 1;
    public GameObject boxCollider;
    public Collider2D coll2D;
    public GameObject element;
    public AudioSource[] clips;
    public bool isBroken = false;
    public float disableTime = 0.5f;
    private Vector3[] brokenPartPositions;

    private void Awake()
    {
        clips = new AudioSource[2];
        clips[0] = GameObject.Find("HitBox").GetComponent<AudioSource>();
        clips[1] = GameObject.Find("BreakBox").GetComponent<AudioSource>();
        SetElementParent();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpforce;
            LoseLifeAndHit();
        }
    }

    private void SetElementParent()
    {
        element.SetActive(false);
        
        if (element.GetComponent<BriefCollected>())
        {
            element.GetComponent<BriefCollected>().inBox = true;
            element.transform.SetParent(GameObject.Find("BriefManager").transform);
        }
        else if (element.GetComponent<ItemCollected>())
        {
            element.GetComponent<ItemCollected>().inBox = true;
            element.transform.SetParent(GameObject.Find("ItemManager").transform);
        }
    }

    public void LoseLifeAndHit()
    {
        lifes--;
        animator.Play("Hit");
        CheckLife();
    }

    public void CheckLife()
    {
        if (lifes <= 0)
        {
            int brokenPartsLength = brokenParts.transform.childCount;
            brokenPartPositions = new Vector3[brokenPartsLength];
            clips[1].Play();

            for (int i = 0; i < brokenPartsLength; i++)
            {
                Transform brokenPart = brokenParts.transform.GetChild(i);
                brokenPartPositions[i] = brokenPart.position;
            }

            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            element.transform.position = gameObject.transform.position;

            if (element.GetComponent<BriefCollected>())
            {
                if (!element.GetComponent<BriefCollected>().isCollected)
                {
                    element.GetComponent<BriefCollected>().inBox = false;
                    element.SetActive(true);
                }
            }
            else if (element.GetComponent<ItemCollected>())
            {
                if (!element.GetComponent<ItemCollected>().isCollected)
                {
                    element.GetComponent<ItemCollected>().inBox = false;
                    element.SetActive(true);
                }
            }
            

            boxCollider.SetActive(false);
            coll2D.enabled = false;
            brokenParts.SetActive(true);
            spriteRederer.enabled = false;
            isBroken = true;

            StartCoroutine(DestroyBox());
        }
        else
        {
            clips[0].Play();
        }
    }

    public IEnumerator DestroyBox()
    {
        yield return new WaitForSeconds(disableTime);
        transform.parent.gameObject.SetActive(false);
    }

    public void RestoreBox()
    {
        element.SetActive(false);
        boxCollider.SetActive(true);
        coll2D.enabled = true;
        spriteRederer.enabled = true;
        RestoreBrokenParts();
        brokenParts.SetActive(false);
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        transform.parent.gameObject.SetActive(true);
        isBroken = false;
        Debug.Log(element.name);
        if (element.GetComponent<BriefCollected>())
        {
            if (!element.GetComponent<BriefCollected>().isCollected)
            {
                element.GetComponent<BriefCollected>().inBox = true;
            }
        }
        else if (element.GetComponent<ItemCollected>())
        {
            if (!element.GetComponent<ItemCollected>().isCollected)
            {
                element.GetComponent<ItemCollected>().inBox = true;
            }
        }
    }

    private void RestoreBrokenParts()
    {
        int brokenPartsLength = brokenParts.transform.childCount;
        
        for (int i = 0; i < brokenPartsLength; i++)
        {            
            brokenParts.transform.GetChild(i).position = brokenPartPositions[i];
            brokenParts.transform.GetChild(i).rotation = new Quaternion();

            if (brokenParts.transform.GetChild(i).GetComponent<Rigidbody>())
            {
                brokenParts.transform.GetChild(i).GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
        }
    }
}

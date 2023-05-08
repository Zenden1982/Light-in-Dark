using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Fleshlight : MonoBehaviour
{
    [Header("Set in Inspector")]
    //public GameObject chargeGUI;
    public Image chargeImage;
    public float chargeBatteryMax = 100f;
    public float chargeBattery;
    public float chargeMinus = 25;
    public Sprite[] charges;
    

    [Header("Set in Dynamically")]
    private Collider2D fleshlightCollider;
    private AIPath aIPath;
    private AudioSource enemySound;
    private Light2D fleshlightLight;
 
    void Start()
    {
        chargeImage = GameObject.Find("Charges").GetComponent<Image>();
        chargeBattery = chargeBatteryMax;
        fleshlightCollider = GetComponent<Collider2D>();
        InvokeRepeating("CHargeControler", 2, 2);
        fleshlightLight = GetComponent<Light2D>();
        
    }


    public void Update()
    {
        if (chargeBattery <= 0)
        {
            fleshlightCollider.enabled = false;
            fleshlightLight.enabled = false;
        }
        else
        {
            fleshlightCollider.enabled = true;
            fleshlightLight.enabled = true;
        }

        switch (chargeBattery)
        {
            case 100:
                chargeImage.sprite = charges[5]; break;
            case 80:
                chargeImage.sprite = charges[0];
                break;
            case 60:
                chargeImage.sprite = charges[1]; break;
            case 40:
                chargeImage.sprite= charges[2]; break;
            case 20:
                chargeImage.sprite = charges[3]; break;
            case 0:
                chargeImage.sprite = charges[4]; break;
        }
    }
    private void CHargeControler()
    {
        chargeBattery -= chargeMinus;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            aIPath = collision.transform.GetComponent<AIPath>();
            aIPath.canMove = false;
            enemySound = collision.transform.GetComponent<AudioSource>();
            enemySound.Stop();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            aIPath = collision.transform.GetComponent<AIPath>();
            aIPath.canMove = true;
            enemySound.Play();
        }
    }

}

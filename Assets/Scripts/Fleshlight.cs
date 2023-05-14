using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using static UnityEngine.InputManagerEntry;

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
        fleshlightCollider = GetComponent<Collider2D>();
        InvokeRepeating("CHargeControler", 2, 2);
        fleshlightLight = GetComponent<Light2D>();
        
    }


    public void Update()
    {
        
        if (chargeBattery<=100 && chargeBattery>80) chargeImage.sprite = charges[0];
        if(chargeBattery<= 80 && chargeBattery>60) chargeImage.sprite = charges[1];
        if(chargeBattery<= 60 && chargeBattery>40) chargeImage.sprite = charges[2];
        if(chargeBattery<= 40 && chargeBattery>20) chargeImage.sprite = charges[3];
        if (chargeBattery<= 20 && chargeBattery>0) chargeImage.sprite = charges[4];
        if (chargeBattery<= 0) chargeImage.sprite = charges[5];
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


    }
    private void CHargeControler()
    {
        chargeBattery -= chargeMinus;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            ShadowEnemy shadowEnemy = collision.transform.GetComponent<ShadowEnemy>();
            shadowEnemy.fleshing = true;
            aIPath = collision.transform.GetComponent<AIPath>();
            aIPath.canMove = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            ShadowEnemy shadowEnemy = collision.transform.GetComponent<ShadowEnemy>();
            shadowEnemy.fleshing = false;
            aIPath = collision.transform.GetComponent<AIPath>();
            aIPath.canMove = true;
            //enemySound.Play();
        }
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("chargeBattery", chargeBattery);
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            PlayerPrefs.SetFloat("chargeBattery", chargeBattery);
        }
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat("chargeBattery", chargeBattery);
    }

    void OnEnable()
    {
        chargeBattery = PlayerPrefs.GetFloat("chargeBattery", 100f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public bool onLight=false;
    public bool attack = false;
    public float damage = 3f;
    private Hero hero;

    public void Start()
    {
         hero = GameObject.Find("Hero").GetComponent<Hero>();
    }

    public void Update()
    {
      
       if (!onLight && Input.GetMouseButton(0))
        {
            attack = false;
        }
       if (attack && hero.mind > 0)
        {

            hero.mind -= damage * Time.deltaTime;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Fleshlight")
        {
            attack = true;
            onLight = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Fleshlight")
        {
            onLight = false;
        }
    }
}

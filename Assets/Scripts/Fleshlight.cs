using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleshlight : MonoBehaviour
{
    private Collider2D fleshlight;
    private AIPath aIPath;
    private AudioSource enemySound;
    void Start()
    {
        fleshlight = GetComponent<Collider2D>();
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

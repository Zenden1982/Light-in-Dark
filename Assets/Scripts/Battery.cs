using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Fleshlight fleshlight = GameObject.Find("Light").GetComponent<Fleshlight>();
            fleshlight.chargeBattery = 100f;
            Destroy(this.gameObject);
        }
    }


}

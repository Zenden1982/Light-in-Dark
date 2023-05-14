using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowEnemy : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health;
    public GameObject particles;
    public bool fleshing = false;
    public float damage = 20f;
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (fleshing)
        {
            health -= damage*Time.deltaTime;
        }
        if (health <= 0) {
            GameObject effect = Instantiate(particles);
            effect.transform.position = transform.position;
            Destroy(this.gameObject);
        }

    }
}

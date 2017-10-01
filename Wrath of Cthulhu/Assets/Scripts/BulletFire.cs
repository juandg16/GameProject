﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour {

    public float bulletForce = 500f;
    private GameObject Player;
    private GameObject Enemy;
    private float bulletDamage;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player").gameObject;
        Enemy = GameObject.FindWithTag("Enemy").gameObject;
        bulletDamage = 100f;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "FirePoint")
        {
            if (Player.transform.localScale.x > 0)
            {
                GetComponent<Rigidbody2D>().AddForce(transform.right * bulletForce);
            }

            else
            {
                GetComponent<Rigidbody2D>().AddForce(-transform.right * bulletForce);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyMove>().health -= bulletDamage;

            if (collision.gameObject.GetComponent<EnemyMove>().health <= 0)
            {
                Destroy(collision.gameObject);
            }
            Destroy(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

}

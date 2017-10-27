﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionCollider : MonoBehaviour {
    CameraFollow controlscript;
    GameObject Camera;
    GameObject Player;
    private bool breakCollision;

	// Use this for initialization
	void Start () {
        Camera = GameObject.FindWithTag("MainCamera");
        Player = GameObject.FindWithTag("Player");
        controlscript = Camera.GetComponent<CameraFollow>();
        breakCollision = false;
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        switch (gameObject.name)
        {
            case "SectionEndCollider":
                if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<Player>().enemiesKilled >= 10 && breakCollision == false)
                {
                    controlscript.SectionEndCollider = gameObject;
                    gameObject.SetActive(false);
                    controlscript.minCameraPos = new Vector3(22.56f, 1.50f, -10f);
                    controlscript.maxCameraPos = new Vector3(28.17f, 5.78f, -10f);
                    breakCollision = true;
                }
                break;

            case "SectionEndCollider2":
                if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<Player>().enemiesKilled >= 15 && breakCollision == false)
                {
                    controlscript.SectionEndCollider = gameObject;
                    gameObject.SetActive(false);
                    controlscript.minCameraPos = new Vector3(33.35f, 1.50f, -10f);
                    controlscript.maxCameraPos = new Vector3(38.38f, 5.80f, -10f);
                    breakCollision = true;
                }
                break;
               
            case "SectionEndCollider3":
                if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<Player>().enemiesKilled >= 20 && breakCollision == false)
                {
                    controlscript.SectionEndCollider = gameObject;
                    gameObject.SetActive(false);
                    controlscript.minCameraPos = new Vector3(43.59f, 1.50f, -10f);
                    controlscript.maxCameraPos = new Vector3(49.01f, 5.80f, -10f);
                    breakCollision = true;
                }
                break;

            case "SectionEndCollider4":
                if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<Player>().enemiesKilled >= 25 && breakCollision == false)
                {
                    controlscript.SectionEndCollider = gameObject;
                    gameObject.SetActive(false);
                    controlscript.minCameraPos = new Vector3(54.21f, 1.50f, -10f);
                    controlscript.maxCameraPos = new Vector3(59.53f, 5.80f, -10f);
                    breakCollision = true;
                }
                break;

            case "SectionEndCollider5":
                if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<Player>().enemiesKilled >= 30 && breakCollision == false)
                {
                    controlscript.SectionEndCollider = gameObject;
                    gameObject.SetActive(false);
                    controlscript.minCameraPos = new Vector3(64.71f, 1.50f, -10f);
                    controlscript.maxCameraPos = new Vector3(73.27f, 5.80f, -10f);
                    breakCollision = true;
                }
                break;

            case "SectionEndCollider6":
                if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<Player>().enemiesKilled >= 37 && breakCollision == false)
                {
                    controlscript.SectionEndCollider = gameObject;
                    gameObject.SetActive(false);
                    controlscript.minCameraPos = new Vector3(78.46f, 1.50f, -10f);
                    controlscript.maxCameraPos = new Vector3(82.24f, 5.80f, -10f);
                    breakCollision = true;
                }
                break;

            case "SectionEndCollider7":
                if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<Player>().enemiesKilled >= 42 && breakCollision == false)
                {
                    controlscript.SectionEndCollider = gameObject;
                    gameObject.SetActive(false);
                    controlscript.minCameraPos = new Vector3(87.42f, 1.50f, -10f);
                    controlscript.maxCameraPos = new Vector3(96.15f, 5.80f, -10f);
                    breakCollision = true;
                }
                break;

            case "SectionEndCollider8":
                if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<Player>().enemiesKilled >= 50 && breakCollision == false)
                {
                    controlscript.SectionEndCollider = gameObject;
                    gameObject.SetActive(false);
                    controlscript.minCameraPos = new Vector3(101.33f, 1.50f, -10f);
                    controlscript.maxCameraPos = new Vector3(109.08f, 5.80f, -10f);
                    breakCollision = true;
                }
                break;

            case "SectionEndCollider9":
                if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<Player>().enemiesKilled >= 55 && breakCollision == false)
                {
                    controlscript.SectionEndCollider = gameObject;
                    gameObject.SetActive(false);
                    controlscript.minCameraPos = new Vector3(114.27f, 1.50f, -10f);
                    controlscript.maxCameraPos = new Vector3(125.23f, 5.80f, -10f);
                    breakCollision = true;
                }
                break;
        }
    }
}
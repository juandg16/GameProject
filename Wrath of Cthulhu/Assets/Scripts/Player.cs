﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {


	//Player1Health
    public float playerHealth;
	public Transform HealthPercentage;
	Player1Health healthscript;

	//Player1Madness
	public float playerMadness;
	public Transform MadnessPercentage;
	Player1Madness madnessscript;

	//Camera
	public float minCameraPosX;
	public float maxCameraPosX;
	public Transform CameraFollow;
	CameraFollow camerascript;


	//Player1Bullet
    public GameObject bullet;
    public float bulletDamage;
    public Transform spawnPoint;
    public bool shotgun;

	//Anim
    private Animator anim;
    private Vector3 input;
    private Rigidbody2D rb2d;
    private bool shootOnce;
	private bool left;

    //Panels
    public GameObject pausePanel;
    public GameObject enterShopUIPanel;
    public GameObject shopPanel;
    public GameObject gamePlayPanel;
    public GameObject scrollBar;
    public GameObject storyPanel;
    public GameObject gameOverPanel;

    //Items
    public bool elixir;
    public bool blink;
    public bool morphine;
    private bool locked;
    public float upgradeDamage;

    //Audio
    public AudioSource[] sounds;
    public GameObject gameAudio;

    //Player variables
    public bool pauseGame;
    public string item;
    public float maxSpeed;
    public float speed;
    public float enemiesKilled = 0f;
	public bool dead;
    public bool markShooting;
    public float minPos;
    public float maxPos;

    public WeaponObject[] weapons;
    public int latestBuy;

    public Transform AmmoCount;
    Ammo ammoScript;
    public GameObject moveForward;
    public Image GoImage;
    public float colliderCount;
    public float damageUpgrade;
    private GameObject StatsUI;
    private GameObject speedUI;
    private GameObject damageUI;
    public float speedCount;
    public bool call;



    // Use this for initialization
    void Start () {

        rb2d = gameObject.GetComponent<Rigidbody2D>(); 
        anim = gameObject.GetComponent<Animator>();
        sounds = gameObject.GetComponents<AudioSource>();
        playerHealth = 100f;
		playerMadness = 0f;
		item = "null";
        pauseGame = false;
		HealthPercentage = GameObject.Find("Player1Health").transform;
		healthscript = HealthPercentage.GetComponent<Player1Health>();
		MadnessPercentage = GameObject.Find("Player1Madness").transform;
		madnessscript = MadnessPercentage.GetComponent<Player1Madness>();
		CameraFollow = GameObject.Find("Main Camera").transform;
		camerascript = CameraFollow.GetComponent<CameraFollow>();
        AmmoCount = GameObject.Find("AmmoCount").transform;
        ammoScript = AmmoCount.GetComponent<Ammo>();
        moveForward = GameObject.Find("MoveForward");
        GoImage = moveForward.GetComponent<Image>();
        StatsUI = GameObject.Find("SpeedAndDamage");
        speedUI = StatsUI.transform.Find("Speed").gameObject;
        damageUI = StatsUI.transform.Find("Damage").gameObject; 
        maxSpeed = .5f;
        speed = 50;
		dead = false;
        shotgun = false;
		latestBuy = 0;
        bulletDamage = 150f;
        damageUpgrade = 0f;
        elixir = false;
        blink = false;
        morphine = false;
        locked = false;
        call = true;
        upgradeDamage = 0f;
        colliderCount = 0f;
        speedCount = 0f;
        StartCoroutine(OpenGamePlayPanel());
    }

    IEnumerator OpenGamePlayPanel()
    {
        yield return new WaitForSecondsRealtime(2);
        gamePlayPanel.SetActive(true);
        storyPanel.SetActive(true);
    }

    IEnumerator BlinkWaitTime()
    {
        yield return new WaitForSecondsRealtime(3);
        anim.SetBool("Blink", false);
        call = true;
    }

    // Update is called once per frame
    void Update()
    {
        markShooting = false;

        if (gamePlayPanel.activeSelf)
        {
            Time.timeScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Return) && pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }

        else if (Input.GetKeyDown(KeyCode.Return) && enterShopUIPanel.activeSelf)
        {
            enterShopUIPanel.SetActive(false);
            shopPanel.SetActive(true);
            Time.timeScale = 0f;
        }

        else if (Input.GetKeyDown(KeyCode.Return) && shopPanel.activeSelf)
        {
            shopPanel.SetActive(false);
            scrollBar.SetActive(false);
            Time.timeScale = 1f;
        }

        else if (Input.GetKeyDown(KeyCode.Return) && gameOverPanel.activeSelf)
        {
            gameOverPanel.SetActive(false);
            Time.timeScale = 1f;
            Application.LoadLevel(0);
        }


        else if (Input.GetKeyDown(KeyCode.P) && !gameOverPanel.activeSelf && !enterShopUIPanel.activeSelf && !shopPanel.activeSelf && !gamePlayPanel.activeSelf)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;

            /*if (pauseGame)
            {
                Time.timeScale = 1f;
                pauseGame = false;
            }
            else
            {
                Time.timeScale = 0f;
                pauseGame = true;
            }*/
        }

        if (Input.GetKey(KeyCode.H))
        {
            playerHealth = 100f;
            playerMadness = 0f;
        }

        else if (dead == false)
        {
            anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x) + Mathf.Abs(rb2d.velocity.y));
            anim.SetBool("Shooting", false);

            if (Input.GetKeyDown(KeyCode.R) && ammoScript.countAmmo != 12f) //reload ammo
            {
                anim.SetBool("Reload", true);
            }

            if (ammoScript.countAmmo == 0 && ammoScript.countAmmo != 12f)
            {
                anim.SetBool("Reload", true);
            }

            if (Input.GetKeyDown(KeyCode.Semicolon) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot_Mark") && shotgun == true && ammoScript.countAmmo >= 3f)
            {
                anim.SetBool("Shooting", true);
                shootOnce = true;
            }

            else if (Input.GetKeyDown(KeyCode.Semicolon) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot_Mark") && shotgun == false && ammoScript.countAmmo > 0f)
            {
                anim.SetBool("Shooting", true);
                shootOnce = true;
            }

            if (Input.GetKeyDown(KeyCode.Quote))
            {
                if (elixir)
                {
                    playerHealth += 50f;
                    playerMadness += 20f;

                    if (playerHealth >= 100f)
                    {
                        playerHealth = 100f;
                    }
                }

                else if (blink)
                {
                    playerMadness += 10f;
                    if (transform.position.x >= .25f && transform.position.x <= 19.63752f + .25248f)
                    {
                        minPos = .25f;
                        maxPos = 19.63752f + .25248f;
                    }

                    else if (transform.position.x >= 19.63752f + .56248f && transform.position.x <= 30.35743f + .25248f)
                    {
                        minPos = 19.63752f + .56248f;
                        maxPos = 30.35743f + .25248f;
                    }

                    else if (transform.position.x >= 30.35743f + .56248f && transform.position.x <= 40.61f + .25248f)
                    {
                        minPos = 30.35743f + .56248f;
                        maxPos = 40.61f + .25248f;
                    }

                    else if (transform.position.x >= 40.61f + .56248f && transform.position.x <= 51.24f + .25248f)
                    {
                        minPos = 40.61f + .56248f;
                        maxPos = 51.24f + .25248f;
                    }

                    else if (transform.position.x >= 51.24f + .56248f && transform.position.x <= 61.75f + .25248f)
                    {
                        minPos = 51.24f + .56248f;
                        maxPos = 61.75f + .25248f;
                    }

                    else if (transform.position.x >= 61.75f + .56248f && transform.position.x <= 75.49f + .25248f)
                    {
                        minPos = 61.75f + .56248f;
                        maxPos = 75.49f + .25248f;
                    }

                    else if (transform.position.x >= 75.49f + .56248f && transform.position.x <= 84.46f + .25248f)
                    {
                        minPos = 75.49f + .56248f;
                        maxPos = 84.46f + .25248f;
                    }

                    else if (transform.position.x >= 84.46f + .56248f && transform.position.x <= 98.37f + .25248f)
                    {
                        minPos = 84.46f + .56248f;
                        maxPos = 98.37f + .25248f;
                    }

                    else if (transform.position.x >= 98.37f + .56248f && transform.position.x <= 111.31f + .25248f)
                    {
                        minPos = 98.37f + .56248f;
                        maxPos = 111.31f + .25248f;
                    }

                    else if (transform.position.x >= 111.31f + .56248f && transform.position.x <= 118.56f)
                    {
                        minPos = 111.31f + .56248f;
                        maxPos = 118.56f;
                    }
                    //float cameraSize = 2.294f;
                    float xPosition = transform.position.x;
                    if (left)
                    {
                        if (xPosition - 2f < minPos)
                            transform.position = new Vector3(minPos, transform.position.y, transform.position.z);
                        else
                            transform.position = new Vector3(xPosition - 2f, transform.position.y, transform.position.z);
                        //transform.Translate(Vector3.left * speed * Time.deltaTime);
                    }
                    else
                    {
                        if (xPosition + 2f > maxPos)
                            transform.position = new Vector3(maxPos, transform.position.y, transform.position.z);
                        else
                            transform.position = new Vector3(xPosition + 2f, transform.position.y, transform.position.z);
                        //transform.Translate(Vector3.right * speed * Time.deltaTime);
                    }

                }
            }


            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot_Mark") && shootOnce && shotgun == true)
            {
                GameObject bullet1 = (GameObject)Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                bullet1.transform.Rotate(0f, 0f, 10f);
                GameObject bullet2 = (GameObject)Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                bullet2.transform.Rotate(0f, 0f, 0f);
                GameObject bullet3 = (GameObject)Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                bullet3.transform.Rotate(0f, 0f, -10f);
                ammoScript.countAmmo -= 3f;
                //Transform newBullet = Instantiate(bullet.transform, spawnPoint.position, Quaternion.identity) as Transform;
                //newBullet.parent = transform;
                markShooting = true;
                shootOnce = false;
            }

            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot_Mark") && shootOnce)
			{
                Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                ammoScript.countAmmo -= 1f;
                //Transform newBullet = Instantiate(bullet.transform, spawnPoint.position, Quaternion.identity) as Transform;
                //newBullet.parent = transform;
                markShooting = true;
                shootOnce = false;
            }

            if (playerHealth <= 0f) {
                anim.SetBool("Dead", true);
				dead = true;          
				//Destroy (transform.gameObject);
			}

			if (playerMadness >= 100f) {
                anim.SetBool("Dead", true);
				dead = true;
                //Destroy (transform.gameObject);
            }

            if (enemiesKilled >= 10 && colliderCount == 0f)
            {
                GoImage.enabled = true;
            }

            else if (enemiesKilled >= 15 && colliderCount == 1f)
            {
                GoImage.enabled = true;
            }

            else if (enemiesKilled >= 21 && colliderCount == 2f)
            {
                GoImage.enabled = true;
            }

            else if (enemiesKilled >= 26 && colliderCount == 3f)
            {
                GoImage.enabled = true;
            }

            else if (enemiesKilled >= 31 && colliderCount == 4f)
            {
                GoImage.enabled = true;
            }

            else if (enemiesKilled >= 38 && colliderCount == 5f)
            {
                GoImage.enabled = true;
            }

            else if (enemiesKilled >= 43 && colliderCount == 6f)
            {
                GoImage.enabled = true;
            }

            else if (enemiesKilled >= 50 && colliderCount == 7f)
            {
                GoImage.enabled = true;
            }

            else if (enemiesKilled >= 55 && colliderCount == 8f)
            {
                GoImage.enabled = true; 
            }

            healthscript.LifePercentage = playerHealth;
			madnessscript.MadnessPercentage = playerMadness;

            if (maxSpeed == .5f)
            {
                speedUI.GetComponent<Text>().text = " " + "Speed : 1x"; 
            }

            else if (speedCount == 0f)
            {
                speedUI.GetComponent<Text>().text = " " + "Speed : 1.3x";
            }

            else if (speedCount == 1f)
            {
                speedUI.GetComponent<Text>().text = " " + "Speed : 1.6x";
            }

            else if (speedCount == 2f)
            {
                speedUI.GetComponent<Text>().text = " " + "Speed : 2x";
            }
            damageUI.GetComponent<Text>().text = " " + "Damage : " + bulletDamage;

            if (anim.GetBool("Blink") == true && call)
            {
                StartCoroutine(BlinkWaitTime());
                call = false;            
            }
        }

        if (dead == true)
        {
            gameAudio.GetComponent<AudioSource>().Stop();
            gameOverPanel.SetActive(true);
        }
    }

    //for physics
    void FixedUpdate()
    {
        Physics2D.gravity = Vector2.zero;
		if (!dead) {
			if (Input.GetKey (KeyCode.A)) {
				rb2d.AddForce (Vector3.left * speed);
                transform.localScale = new Vector3(-2f, 2f, 1f);
                left = true;
            }

			if (Input.GetKey (KeyCode.D)) {
				rb2d.AddForce (Vector3.right * speed);
                transform.localScale = new Vector3(2f, 2f, 1f);
                left = false;
            }

            if (Input.GetKey (KeyCode.W)) {
				rb2d.AddForce (Vector3.up * speed);
			}

			if (Input.GetKey (KeyCode.S)) {
				rb2d.AddForce (Vector3.down * speed);
			}
		

			if (rb2d.velocity.x > maxSpeed) {
				rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
			}

			if (rb2d.velocity.x < -maxSpeed) {
				rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
			}

			if (rb2d.velocity.y > maxSpeed) {
				rb2d.velocity = new Vector2 (rb2d.velocity.x, maxSpeed);
			}

			if (rb2d.velocity.y < -maxSpeed) {
				rb2d.velocity = new Vector2 (rb2d.velocity.x, -maxSpeed);
			}
		}

    }

    void SetHit()
    {
        gameObject.GetComponent<Animator>().SetBool("Hit", false);
    }

    public void SetBlink()
    {
        anim.SetBool("Blink", true);
    }

    void Destroy()
    {
        transform.GetComponent<SpriteRenderer>().enabled = false;
    }

    void playSound()
    {
        sounds[0].Play();
    }

    public void playReloadSound()
    {
        if (!sounds[2].isPlaying)
        {
            sounds[2].Play();
        }
    }

    public void reloadEnd()
    {
        while (ammoScript.countAmmo != 12)
        {
            ammoScript.countAmmo += 1f;
        }
        anim.SetBool("Reload", false);
    }

}
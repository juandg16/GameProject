﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour {

    public GameObject shopPanel;
    public GameObject openShopUI;
    public GameObject scrollBar;
    public GameObject youWinPanel;
    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !youWinPanel.activeSelf)
        {
            OpenShopUI();
        }

    }

    void OpenShopUI()
    {
        openShopUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void OpenShop()
    {
        openShopUI.SetActive(false);
        shopPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        scrollBar.SetActive(false);
        Time.timeScale = 1;
    }

    public void CloseOpenShopUI()
    {
        openShopUI.SetActive(false);
        Time.timeScale = 1;
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(4);
        anim.SetBool("wait", true);
        
    }

    public void setWait()
    {
        anim.SetBool("wait", false);
    }
}

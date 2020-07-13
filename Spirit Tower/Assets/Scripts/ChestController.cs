﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class ChestController : MonoBehaviour
{
    public Animator animator;
    public GameObject chest;
    public bool openChest = false;
    public bool ChestOpened = false; 
    public Text Avisos;
    public GameObject mark;

    public GameObject tempObj;

    [System.Serializable]
    public class DropCurrency 
    {
        public GameObject item;
    }

    public List<DropCurrency> Contenidos = new List<DropCurrency>();
    
    public void sacarObjeto()
    {
        for (int j = 0; j < Contenidos.Count; j++)
        {
            tempObj = Instantiate(Contenidos[j].item, transform.position, Quaternion.identity);
            if (Contenidos[j].item.name == "Heart")
            {
                GameObject player = GameObject.Find("Player1");
                Player playerScript = player.GetComponent<Player>();
                playerScript.health += 1;
            }

            //Mas comportamiento para objetos aqui

        }
    }
        
    void Start()
    {
        animator = GetComponent<Animator>();
        mark.SetActive(false);
    }
   private void Update()
    {
        if (openChest == true & (Input.GetKeyDown(KeyCode.Z) & (ChestOpened == false))){
            Avisos.text = " ";
            animator.SetBool("openChest", true);
            GameObject player = GameObject.Find("Player1");
            Player playerScript = player.GetComponent<Player>();
            playerScript.tesoros += 1;
            sacarObjeto();
            ChestOpened = true; 

        }

        if (ChestOpened == true)
        {
            mark.SetActive(true);
        }

        if(tempObj != null) { 
             tempObj.transform.Translate(transform.up * Time.deltaTime);
            if (tempObj.transform.position.y > 6.0f)
            {
                Destroy(tempObj);
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            openChest = true;
            Avisos.text = "Presiona Z para abrir el cofre";
            if (ChestOpened == true)
            {
                Avisos.text = "Ya has abierto este cofre";
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        Avisos.text = " ";
        openChest = false;
        animator.SetBool("openChest", false);
    }
}






using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddNewDIalogue : MonoBehaviour
{
    private PlayerDialogue player;
    [SerializeField]private bool NewDialogue;
     [SerializeField]private GameObject WhereNewDialogue;
     [SerializeField]private GameObject WhatInstatiate;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerText").GetComponent<PlayerDialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseText()
    {
        player.StartCoroutine(player.ActiveText());
        Destroy(gameObject, 1);
        if(NewDialogue == true)
        {
        Instantiate(WhatInstatiate, WhereNewDialogue.transform);
        }
    }
}

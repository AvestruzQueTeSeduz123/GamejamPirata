using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitonAnim : MonoBehaviour
{
    [SerializeField]private Transform center;
    [SerializeField]private GameObject upBar;
    [SerializeField]private GameObject downBar;
    [SerializeField]private Player player;
    [SerializeField]private GameObject lostCanvas;
    [SerializeField]private GameManager gameManager;
    [SerializeField]private GameObject nextPhase;
    [SerializeField]private bool useNewBuddy = true;
    [SerializeField]private float modifier = 1;

    [SerializeField]private float timeToStart = 1;
    [SerializeField]private Transform upBarCenter;
    [SerializeField]private Transform downBarCenter;

    // Start is called before the first frame update
    void Start()
    {
        upBar.SetActive(false);
        downBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTrasitionAnim()
    {
        upBar.SetActive(true);
        downBar.SetActive(true);
        
        upBar.LeanMoveY(center.position.y, timeToStart).setEaseOutQuart();
        downBar.LeanMoveY(center.position.y, timeToStart).setEaseOutQuart().setOnComplete(() =>
        {
            if(player.lost == true || gameManager.win == true)
        {
            EndTrasitionAnim();  
        }
                  
        });
    }

    public void EndTrasitionAnim()
    {
        upBar.LeanMoveY(upBarCenter.position.y, 1).setEaseOutQuart();
        downBar.LeanMoveY(downBarCenter.position.y, 1).setEaseOutQuart();
        if(player.lost == true)
        {
            lostCanvas.SetActive(true);
            
            // gameObject.SetActive(false);
        } else if(gameManager.win == true)
        {
            nextPhase.SetActive(true);
        }
        
    }
}

using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;
using System.Collections;
using System;

public class ArrowTutorial : MonoBehaviour
{
    public static event Action Shoot;
    Rigidbody2D rb;
    public bool hasHit;
    [SerializeField]private ArrowScrptObj arrowStats;

    private int damage;
    private BowTutorial bow;
    private CinemachineCamera cinemachineCamera;

    
    [SerializeField]private GameObject snowParticle;
    [SerializeField]private GameObject smokeParticle;
    CamControll camControll;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject bowObject = GameObject.FindWithTag("Bow");
        bow = bowObject.GetComponent<BowTutorial>();
        GameObject machineCamera = GameObject.FindWithTag("MachineCamera");
        cinemachineCamera = machineCamera.GetComponent<CinemachineCamera>();
        camControll = GameObject.FindWithTag("GameManager").GetComponent<CamControll>();

        UpdateValues();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hasHit == false)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            cinemachineCamera.Follow = this.transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        hasHit = true;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        bow.isShotting = false;

        camControll.StartCoroutine(camControll.BackCamera());
        Instantiate(snowParticle, gameObject.transform.position, Quaternion.identity);
        Instantiate(smokeParticle, gameObject.transform.position, Quaternion.identity);

        if(col.gameObject.CompareTag("Enemy"))
        {
            TutorialEnemy enemy = col.gameObject.GetComponent<TutorialEnemy>();
                if (enemy != null)
                    {
                        enemy.TakeDamage(damage);
                    }
                    else
                    {
                        Debug.LogWarning("Objeto com tag 'Enemy' não possui o componente 'Enemy'.");
                    }
        }
        


        Destroy(gameObject);
        
    }

    private void UpdateValues()
    {
        damage = arrowStats.damage;
    }
}

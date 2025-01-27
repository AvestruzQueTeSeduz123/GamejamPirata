using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;
using System.Collections;
using System;

public class Arrow : MonoBehaviour
{
    public static event Action Shoot;
    Rigidbody2D rb;
    public bool hasHit;
    [SerializeField]private ArrowScrptObj arrowStats;

    private int damage;
    private Bow bow;
    private CinemachineCamera cinemachineCamera;

    
    [SerializeField]private GameObject snowParticle;
    [SerializeField]private GameObject smokeParticle;
    CamControll camControll;
    private CinemachineImpulseSource impulseSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject bowObject = GameObject.FindWithTag("Bow");
        bow = bowObject.GetComponent<Bow>();
        GameObject machineCamera = GameObject.FindWithTag("MachineCamera");
        cinemachineCamera = machineCamera.GetComponent<CinemachineCamera>();
        camControll = GameObject.FindWithTag("GameManager").GetComponent<CamControll>();
        impulseSource = GetComponent<CinemachineImpulseSource>();

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
        bow.StartCoroutine(bow.ColldownShoot());

        camControll.StartCoroutine(camControll.BackCamera());
        Instantiate(snowParticle, gameObject.transform.position, Quaternion.identity);
        Instantiate(smokeParticle, gameObject.transform.position, Quaternion.identity);

        if(col.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                    {
                        enemy.TakeDamage(damage);
                    }
                    else
                    {
                        Orbs enemyOrbs = col.gameObject.GetComponent<Orbs>();
                            if (enemyOrbs != null)
                            {
                            enemyOrbs.TakeDamage(damage);
                            }
                        Debug.LogWarning("Objeto com tag 'Enemy' não possui o componente 'Enemy'.");
                    }
        }

        if(col.gameObject.CompareTag("Ground"))
        {
            CameraShakeManager.instance.CameraShake(impulseSource);
        }

        if(col.gameObject.CompareTag("Boss"))
        {
            CameraShakeManager.instance.CameraShake(impulseSource);
        }
        Shoot?.Invoke();


        Destroy(gameObject);
        
    }

    private void UpdateValues()
    {
        damage = arrowStats.damage;
    }

    
}

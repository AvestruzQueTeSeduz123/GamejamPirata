using System.Collections;
using UnityEngine;
using System;
using Unity.Cinemachine;


public class Orbs : MonoBehaviour
{

    [SerializeField]private GameObject target;
    public float speedOrb = 80f;
    [SerializeField]private Vector3 direction = Vector3.up;

    [SerializeField]private Renderer enemyRenderer;
    [SerializeField] private Material flashMaterial;
    private Material originalMaterial;


    public float flashDuration = 0.1f;
    public float size; // Duration of the white flash
    private Coroutine flashRoutine;

    [SerializeField]private EnemyStatsScrpObj enemyStats;
    private int life;
    private int maxLife;
    private float speed;
    private float howManyWillWalk;
    private Vector3 targetPosition;
    private bool canWalk;

    private CinemachineImpulseSource impulseSource;

    [SerializeField]private bool isSummoner;
    private int summonTime;
    [SerializeField]private GameObject flashEnemy;
    [SerializeField]private GameManager gameManager;
    [SerializeField]private Player player;
    [SerializeField]private Boss boss;

    private Animator anim;

    public SpriteRenderer spriteRenderer;

    void Start()
    {
            impulseSource = GetComponent<CinemachineImpulseSource>();
            gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
            boss = GameObject.FindWithTag("Boss").GetComponent<Boss>();
            anim  = GetComponent<Animator>();
            originalMaterial = enemyRenderer.material;
            canWalk = false;
            UpdateStats();
    }

    void Update()
    {
        gameObject.transform.RotateAround(target.transform.position, direction, speedOrb * Time.deltaTime);
        anim.SetBool("IsWalking", false);
        


        
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        player.LifeUpdateBig(3);
        if(damage >= 2)
        {
            impulseSource.DefaultVelocity.y = -2;
        } else {
            impulseSource.DefaultVelocity.y = -1;
        }
        
        CameraShakeManager.instance.CameraShake(impulseSource);
        if(life <= 0)
        {
            boss.TakeDamage(5);
            boss.orb -= 1;
            boss.CheckOrb();
            impulseSource.DefaultVelocity.y = -5;
            CameraShakeManager.instance.CameraShake(impulseSource);
            Destroy(gameObject);
            return;
        }

        if(flashRoutine != null)
        {
            StartCoroutine(FlashWhite());
        }
            
        flashRoutine = StartCoroutine(FlashWhite());
        DamageAnim();
        
    }

    private IEnumerator FlashWhite()
    {
        enemyRenderer.material = flashMaterial;
        yield return new WaitForSeconds(flashDuration);
        enemyRenderer.material = originalMaterial;
        flashRoutine = null;
    }

   

    private void UpdateStats()
    {
        maxLife = enemyStats.life;
        life = maxLife;
        speed = enemyStats.speed;
        howManyWillWalk = enemyStats.howManyWillWalk;
    }

    private void DamageAnim()
    {
        gameObject.transform.localScale -= new Vector3(0.3f, 0.3f, 0.3f);

            gameObject.LeanScaleX(size - 0.5f, 0.1f).setEaseOutCirc();
            gameObject.LeanScaleY(size + 0.3f, 0.1f).setEaseOutCirc().setOnComplete(() =>
            {
                gameObject.LeanScaleX(size + 0.3f, 0.1f).setEaseOutCirc();
            gameObject.LeanScaleY(size - 0.5f, 0.1f).setEaseOutCirc().setOnComplete(() =>
            {
                gameObject.LeanScaleX(size, 0.1f).setEaseInQuart();
                gameObject.LeanScaleY(size, 0.1f).setEaseInQuart();
            });
            });
    }

    private void Summon()
    {
        summonTime++;
        Transform summonPoint = null;
            foreach (Transform child in gameObject.GetComponentsInChildren<Transform>())
                {
                    if (child.CompareTag("SpawnEnemy"))
                    {
                        summonPoint = child;
                        break;
                    }
                }
                if(summonTime == 2)
                {
                    Instantiate(flashEnemy, summonPoint.position, Quaternion.identity);
                    gameManager.CheckEnemies();
                    summonTime = 0;
                }
    }
}

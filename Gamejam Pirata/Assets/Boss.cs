using System.Collections;
using UnityEngine;
using System;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;

using System.Collections.Generic;


public class Boss : MonoBehaviour
{
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
    [SerializeField]private GameObject bigEnemy;
    [SerializeField]private GameObject enemy;
    [SerializeField]private GameObject summoner;
    [SerializeField]private GameManager gameManager;

    [SerializeField]private int minNumb = 1;
    [SerializeField]private int maxNumb = 20;

    private int randNumb;
    private Animator anim;


    public int orb = 4;
   [SerializeField] private List<GameObject> orbs = new List<GameObject>();
   [SerializeField]private Sprite[] orbsSprites;
   [SerializeField]private int sprites;

   [SerializeField]private Sprite[] bossSprites;
   private int spritesBoss;
   [SerializeField]private SpriteRenderer spriteRendererBoss;

    void Start()
    {
            impulseSource = GetComponent<CinemachineImpulseSource>();
            gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
            anim  = GetComponent<Animator>();
            originalMaterial = enemyRenderer.material;
            canWalk = false;
            UpdateStats();
            
            Arrow.Shoot += Summon;
    }

    void Update()
    {
        if (canWalk == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            anim.SetBool("IsWalking", true);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                anim.SetBool("IsWalking", false);
                canWalk = false;
            }
        }

        orbs.RemoveAll(orb => orb == null);


        
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        spritesBoss++;
        if(damage >= 2)
        {
            impulseSource.DefaultVelocity.y = -2;
        } else {
            impulseSource.DefaultVelocity.y = -1;
        }

        spriteRendererBoss.sprite = bossSprites[spritesBoss];

        
        CameraShakeManager.instance.CameraShake(impulseSource);
        if(life <= 0)
        {
            Arrow.Shoot -= Summon;
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

    public void Walk()
    {
        if (this == null) return;
        if(isSummoner  == false)
        {
            targetPosition = gameObject.transform.position;
        targetPosition.x -= howManyWillWalk;
        canWalk = true;
        } else {
            Summon();
        }
        
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
        if (SceneManager.GetActiveScene().isLoaded == false)
    {
        Debug.LogWarning("A cena está sendo reiniciada. Ignorando invocação.");
        return;
    }

    if (gameObject == null)
    {
        Debug.LogWarning("O objeto foi destruído antes da invocação.");
        return;
    }

        summonTime++;
        randNumb = UnityEngine.Random.Range(minNumb, maxNumb);
        Transform summonPoint = null;
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>())
                {
                    if (child.CompareTag("SpawnEnemy"))
                    {
                        summonPoint = child;
                        break;
                    }
                }

                if(randNumb == 1)
        {
            Instantiate(enemy, summonPoint.position, Quaternion.identity);
                    gameManager.CheckEnemies();
                    summonTime = 0;
        }

        if(randNumb == 20)
        {
                    summonTime = 0;
        }

        if(summonTime == 2)
        {  

        if(randNumb <= 5)
        {
            Instantiate(bigEnemy, summonPoint.position, Quaternion.identity);
                    gameManager.CheckEnemies();
        }

        if(randNumb > 5 && randNumb <= 10)
        {
            Instantiate(summoner, summonPoint.position, Quaternion.identity);
                    gameManager.CheckEnemies();
        }

        if(randNumb > 10 && randNumb <= 15)
        {
            Instantiate(enemy, summonPoint.position, Quaternion.identity);
                    gameManager.CheckEnemies();
        }

        if(randNumb > 15 && randNumb <= 20)
        {
            Instantiate(flashEnemy, summonPoint.position, Quaternion.identity);
                    gameManager.CheckEnemies();
        }
            summonTime = 0;
                }
    }

    private void OnDestroy()
{
    // Desconecte eventos ou callbacks relacionados ao objeto
    StopAllCoroutines(); // Para todas as Coroutines associadas ao objeto
    Arrow.Shoot -= Summon;
}

    public void CheckOrb()
    {
        if (orb == 3 || orb == 2 || orb == 1) // Condensa as verificações redundantes
{
    sprites++;
    foreach (var orb in orbs)
    {
        if (orb != null) // Verifica se o GameObject ainda existe
        {
            
            Orbs orbScript = orb.GetComponent<Orbs>();
            if (orbScript != null) // Verifica se o componente Orbs está presente
            {
                orbScript.speedOrb += 40f;
                orbScript.spriteRenderer.sprite = orbsSprites[sprites];
            }
        }
        else
        {
            Debug.LogWarning("Um dos objetos no array 'orbs' foi destruído ou está faltando.");
        }
    }
}
    }
}

using System.Collections;
using UnityEngine;
using System;
using Unity.Cinemachine;
public class TutorialEnemy : MonoBehaviour
{
    [SerializeField]private Renderer enemyRenderer;
    [SerializeField] private Material flashMaterial;
    private Material originalMaterial;

    public float flashDuration = 0.1f; // Duration of the white flash
    private Coroutine flashRoutine;
    
    [SerializeField] private PlayerDialogue playerDialogue;
    [SerializeField] private DialogueActivator dialogueActivator;
    [SerializeField] private DialogueObject[] dialogueObjects;
    [SerializeField]private BowTutorial bowTutorial;
    public int currentIndex = 0;

    // [SerializeField]private EnemyStatsScrpObj enemyStats;
    // private int life;
    // private int maxLife;
    // private float speed;
    // private float howManyWillWalk;
    // private Vector3 targetPosition;
    // private bool canWalk;

    private CinemachineImpulseSource impulseSource;


    void Start()
    {
            impulseSource = GetComponent<CinemachineImpulseSource>();
            originalMaterial = enemyRenderer.material;
            // canWalk = false;
            // UpdateStats();
            
            // ArrowTutorial.Shoot += UpdateDialogue;
    }

    void Update()
    {
        // if (canWalk == true)
        // {
        //     transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        //     if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        //     {
        //         canWalk = false;
        //     }
        // }

        
    }

    public void TakeDamage(int damage)
    {
        // life -= damage;
        CameraShakeManager.instance.CameraShake(impulseSource);
        // if(life <= 0)
        // {
        //     Arrow.Shoot -= UpdateDialogue;
        //     Destroy(gameObject);
        //     return;
        // }
        UpdateDialogue();

        if(flashRoutine != null)
        {
            StartCoroutine(FlashWhite());
        }
            
        flashRoutine = StartCoroutine(FlashWhite());
        DamageAnim();
        bowTutorial.unlockedBigShoot = true;
        
    }

    private IEnumerator FlashWhite()
    {
        enemyRenderer.material = flashMaterial;
        yield return new WaitForSeconds(flashDuration);
        enemyRenderer.material = originalMaterial;
        flashRoutine = null;
    }

    // public void Walk()
    // {
    //     if (this == null) return;
        
    //     targetPosition = gameObject.transform.position;
    //     targetPosition.x -= howManyWillWalk;
    //     canWalk = true;
    // }

    // private void UpdateStats()
    // {
    //     maxLife = enemyStats.life;
    //     life = maxLife;
    //     speed = enemyStats.speed;
    //     howManyWillWalk = enemyStats.howManyWillWalk;
    // }

    private void DamageAnim()
    {
        gameObject.transform.localScale -= new Vector3(0.3f, 0.3f, 0.3f);

            gameObject.LeanScaleX(1.7f, 0.1f).setEaseOutCirc();
            gameObject.LeanScaleY(2.7f, 0.1f).setEaseOutCirc().setOnComplete(() =>
            {
                gameObject.LeanScaleX(2.7f, 0.1f).setEaseOutCirc();
            gameObject.LeanScaleY(1.7f, 0.1f).setEaseOutCirc().setOnComplete(() =>
            {
                gameObject.LeanScaleX(2.3f, 0.1f).setEaseInQuart();
                gameObject.LeanScaleY(2.3f, 0.1f).setEaseInQuart();
            });
            });
    }

    void UpdateDialogue()
    {
        if (currentIndex < dialogueObjects.Length)
    {
        dialogueActivator.dialogueObject = dialogueObjects[currentIndex];
        currentIndex++;
    } else if(currentIndex >= dialogueObjects.Length)
    {
        return;
    }
    if(currentIndex == 1)
    {
        bowTutorial.unlockedLightShoot = false;
    }
    
        playerDialogue.StartCoroutine(playerDialogue.ActiveText());
    }

    
}

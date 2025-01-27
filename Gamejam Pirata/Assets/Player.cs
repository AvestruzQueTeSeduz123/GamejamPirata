using UnityEngine;

public class Player : MonoBehaviour
{
    public int life;
    public int maxLife;

    [SerializeField]private FloatingHealthBar healthBar;
    [SerializeField]private GameObject lostCanvas;
    public bool lost = false;
    [SerializeField]private GameObject transition;
    [SerializeField]private CanvasGroup transitionCanvasGroup;

    [SerializeField]private Bow bow;
    
    Vector2  direction;
    [SerializeField]private Sprite[] damageSprites;
    [SerializeField]private GameObject spriteGameoObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        life = maxLife;

        healthBar.UpdateHealthBar(life, maxLife);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) )
        {
            if(life <= 0)
            {
                lost = true;
                        TransitionAnim();
                        bow.canShoot = false;
            }
            LeanTween.rotateZ(gameObject, 45f, 0.1f).setEaseOutCirc();
        }

        if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            LeanTween.rotateZ(gameObject, -25f, 0.1f).setEaseOutCirc().setOnComplete(() =>
            {
                LeanTween.rotateZ(gameObject, 0f, 0.1f).setEaseOutCirc();
            });
        }
    }

    public void LifeUpdate()
    {
        life -= 1;

        healthBar.UpdateHealthBar(life, maxLife);
        AnimAttack();
        UpdateSprite();
        
    }

    public void LifeUpdateBig()
    {
        life -= 2;

        healthBar.UpdateHealthBar(life, maxLife);
        AnimAttackBig();
        UpdateSprite();
        
    }
    public void LifeUpdateBig(int health)
    {
        life += health;
        healthBar.UpdateHealthBar(life, maxLife);
        if(life > 10)
        {
            life = 10;
            UpdateSprite();
        }
        
    }

    
    private void AnimAttack()
    {
        gameObject.LeanScaleX(0.1f, 0.1f).setEaseOutCirc();
        gameObject.LeanScaleY(1f, 0.1f).setEaseOutCirc().setOnComplete(() =>
            {
                gameObject.LeanScaleX(1.5f, 0.1f).setEaseOutCirc();
                gameObject.LeanScaleY(1f, 0.1f).setEaseOutCirc().setOnComplete(() =>
            {
                gameObject.LeanScaleX(1f, 0.1f).setEaseInQuart();
                gameObject.LeanScaleY(1f, 0.1f).setEaseInQuart();
            });
            });
    }

    private void AnimAttackBig()
    {
        gameObject.LeanScaleX(0.1f, 0.1f).setEaseOutCirc();
        gameObject.LeanScaleY(1f, 0.1f).setEaseOutCirc().setOnComplete(() =>
            {
                gameObject.LeanScaleX(2f, 0.1f).setEaseOutCirc();
                gameObject.LeanScaleY(1f, 0.1f).setEaseOutCirc().setOnComplete(() =>
            {
                gameObject.LeanScaleX(1f, 0.1f).setEaseInQuart();
                gameObject.LeanScaleY(1f, 0.1f).setEaseInQuart();
            });
            });
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                    {
                        lost = true;
                        TransitionAnim();
                        bow.canShoot = false;
                    }
        }
        
    }

    private void TransitionAnim()
    {
        transition.SetActive(true);
         LeanTween.alphaCanvas(transitionCanvasGroup, 1f, 0.1f)
                 .setEaseInQuart();
    }

    private void UpdateSprite()
    {
        if(life >= 00 && life <= 10)
        {
            Sprite spriteDamaged = damageSprites[life];
            spriteGameoObject.GetComponent<SpriteRenderer>().sprite = spriteDamaged;
        }
    }
}

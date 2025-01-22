using UnityEngine;

public class Player : MonoBehaviour
{
    public int life;
    public int maxLife;

    [SerializeField]private FloatingHealthBar healthBar;
    [SerializeField]private GameObject lostCanvas;
    public bool lost = false;
    [SerializeField]private TransitonAnim transition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        life = maxLife;

        healthBar.UpdateHealthBar(life, maxLife);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LifeUpdate()
    {
        life -= 1;

        healthBar.UpdateHealthBar(life, maxLife);
        AnimAttack();
        
    }

    public void LifeUpdateBig()
    {
        life -= 3;

        healthBar.UpdateHealthBar(life, maxLife);
        AnimAttack();
        
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                    {
                        lost = true;
                        transition.StartTrasitionAnim();
                    }
        }
        
    }
}

using System.Data.Common;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]private GameObject[] enemies;
    [SerializeField]private GameObject nextPhase;
    public bool win = false;
    [SerializeField]private GameObject transition;
    [SerializeField]private CanvasGroup transitionCanvasGroup;
    [SerializeField]private Bow bow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        bool allEnemiesDestroyed = true;

        foreach (var enemy in enemies)
        {
            if (enemy != null) // Se encontrar pelo menos um inimigo ativo
            {
                allEnemiesDestroyed = false;
                break;
            }
        }

        // Se todos os inimigos foram destruídos, ativa o objeto da próxima fase
        if (allEnemiesDestroyed)
        {
            win = true;
            TransitionAnim();
            bow.canShoot = false;
        }
    }

    private void TransitionAnim()
    {
        transition.SetActive(true);
         LeanTween.alphaCanvas(transitionCanvasGroup, 1f, 0.1f)
                 .setEaseInQuart()
                 .setOnComplete(() =>
                 {
                     // Ativa o objeto 'transition' após a transição
                     transition.SetActive(true);
                 });
    }

    public void CheckEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
}

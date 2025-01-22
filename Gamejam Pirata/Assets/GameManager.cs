using System.Data.Common;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]private GameObject[] enemies;
    [SerializeField]private GameObject nextPhase;
    public bool win = false;
    [SerializeField]private TransitonAnim transition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
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
            transition.StartTrasitionAnim();
        }
    }
}

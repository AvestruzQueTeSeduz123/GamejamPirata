using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneManagerButton : MonoBehaviour
{
    
    [SerializeField]private string nameOfScene;
    private bool isRestarting = false;


    
    public void LoadGameScene()
    {
        if (!isRestarting)
        {
            isRestarting = true; // Evita chamadas duplicadas
            StartCoroutine(RestartCoroutine());
        }
        
        
    }

    private IEnumerator RestartCoroutine()
    {
        // Opcional: Adicione um pequeno atraso ou efeito antes de reiniciar
        yield return new WaitForSeconds(0.5f);

        // Reinicia a cena atual
        LevelManager.Instance.LoadScene(nameOfScene, "CrossFade");
    }
}

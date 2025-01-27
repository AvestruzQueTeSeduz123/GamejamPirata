using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

[System.Serializable]
public class CrossFade : SceneTransition
{
    public CanvasGroup crossFade;
    
    public override IEnumerator AnimateTransitionIn()
{
    // Inicia a animação de alpha
    crossFade.LeanAlpha( 1f, 1f).setOnComplete(() => {

        StartCoroutine(WaitAndContinue()); // Chama a Coroutine para esperar após a animação
    });

    // Aguarda a animação completar
    yield return null;
}

// Coroutine que aguarda 1 segundo após a animação
private IEnumerator WaitAndContinue()
{
    yield return new WaitForSeconds(1f);
    // Coloque o código que deve ser executado após o atraso
}


    public override IEnumerator AnimateTransitionOut()
{
    // Inicia a animação de alpha
    crossFade.LeanAlpha( 0f, 1f).setOnComplete(() => {

        StartCoroutine(WaitAndContinueOut()); // Chama a Coroutine para esperar após a animação
    });

    // Aguarda a animação completar
    yield return null;
}

// Coroutine que aguarda 1 segundo após a animação
private IEnumerator WaitAndContinueOut()
{
    yield return new WaitForSeconds(1f);
    // Coloque o código que deve ser executado após o atraso
}
}

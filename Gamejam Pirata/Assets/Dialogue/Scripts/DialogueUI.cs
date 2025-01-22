using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private GameObject animationPass;
    [SerializeField] private shakeDialogue shake;

    public bool IsOpen { get; private set; }
    // private ResponseHandler responseHandler;
    private TypewriteEffect typewriteEffect;
    [SerializeField]private CanvasGroup canvasGroupDescription;
    [SerializeField]private GameObject characterImage;

    private void Start()
    {
        IsOpen = true;
        typewriteEffect = GetComponent<TypewriteEffect>();
        // responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
        canvasGroupDescription = dialogueBox.GetComponent<CanvasGroup>();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        dialogueBox.SetActive(true);
        animationPass.SetActive(false);
        

        dialogueBox.LeanScaleX( 1f, 0.2f).setEaseOutCirc().setOnComplete(() =>
        {
           characterImage.LeanScaleY( 1f, 0.3f).setEaseOutQuart().setOnComplete(() =>
        {
           StartCoroutine(StepThroughDialogue(dialogueObject));
        });
        });
        canvasGroupDescription.LeanAlpha(1, 0.2f).setEaseInQuart();
        
    }

    // public void AddResponseEvents(ResponseEvent[] responseEvent)
    // {
    //     responseHandler.AddResponseEvents(responseEvent);
    // }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    { 
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            if(i == dialogueObject.Dialogue.Length - 1) break;
            animationPass.SetActive(true);
            shake.start = false;
            yield return null;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1));
        }

        // if (dialogueObject.HasResponses)
        // {
        //     // responseHandler.ShowResponses(dialogueObject.Responses);
        // } else
        // {
        //     CloseDialogueBox();
        // }

            CloseDialogueBox();
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        shake.StartCoroutine(shake.Shaking());
        animationPass.SetActive(false);
        typewriteEffect.Run(dialogue, textLabel);

        while (typewriteEffect.IsRunning)
        {
            yield return null;

            if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                typewriteEffect.Stop();
            }
        }
    }

    public void CloseDialogueBox()
    {
        IsOpen = false;
        characterImage.LeanScaleY( 0f, 0.2f).setEaseOutQuart().setOnComplete(() =>
        {
            dialogueBox.LeanScaleX( 0.32f, 0.3f).setEaseOutCirc();
            canvasGroupDescription.LeanAlpha(0f, 0.15f).setEaseInQuart().setOnComplete(() =>
        {
            dialogueBox.SetActive(false);
            // Ensures that FadeIn can be called next
        });
        
        });
        
        
        textLabel.text = string.Empty;
    }
}

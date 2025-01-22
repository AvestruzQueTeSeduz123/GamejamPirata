using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogue : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;

    public Interectable interactable { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActiveText());
    }

    // Update is called once per frame
    private void Update()
    {
        // if(dialogueUI.IsOpen) return;
        // if(Input.GetKeyDown(KeyCode.E))
        // {
        //         interactable?.Interact(this);
        // }
    }

    public IEnumerator ActiveText()
    {
        yield return new WaitForSeconds(.5f);
        if(dialogueUI.IsOpen) yield break;
        interactable?.Interact(this);
    }
}

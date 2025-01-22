using UnityEngine;

public class DialogueActivator : MonoBehaviour, Interectable
{
    public DialogueObject dialogueObject;

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerText") && other.TryGetComponent(out PlayerDialogue player))
        {
            player.interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerText") && other.TryGetComponent(out PlayerDialogue player))
        {
            if (player.interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.interactable = null;
            }
        }
    }

    public void Interact(PlayerDialogue player)
    {
        player.DialogueUI.ShowDialogue(dialogueObject);
    }
}

using Dialogue;
using UnityEngine;

namespace InteractWithWorld
{
    public class NpcDialogue : Interactable
    {
        [SerializeField] private Dialogue.Dialogue dialogue;
        public override void Interact()
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }
}
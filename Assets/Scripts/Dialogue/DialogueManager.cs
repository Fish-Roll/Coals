using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private Text npcNameText;
        [SerializeField] private Text npcSentenceText;
        [SerializeField] private GameObject playerHUD;
        [SerializeField] private GameObject dialogueHUD;
        private readonly Queue<string> _sentences = new Queue<string>();
        
        public void StartDialogue(Dialogue dialogue)
        {
            Cursor.lockState = CursorLockMode.None;
            playerHUD.SetActive(false);
            dialogueHUD.SetActive(true);
            
            _sentences.Clear();
            foreach (var sentence in dialogue.sentences)
                _sentences.Enqueue(sentence);
            npcNameText.text = dialogue.playerName;
            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (_sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            string sentence = _sentences.Dequeue();
            npcSentenceText.text = sentence;
        }
        private void EndDialogue()
        {
            npcSentenceText.text = "Fuck you";
            dialogueHUD.SetActive(false);
            playerHUD.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}

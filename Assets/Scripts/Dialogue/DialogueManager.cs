using System.Collections;
using System.Collections.Generic;
using QuestSystem.Quests;
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
        [SerializeField] private GameObject giveQuestButton;
        private readonly Queue<string> _sentences = new Queue<string>();
        [SerializeField] private QFindPapers quest;
        public void StartDialogue(Dialogue dialogue)
        {
            Cursor.lockState = CursorLockMode.None;
            playerHUD.SetActive(false);
            dialogueHUD.SetActive(true);
            giveQuestButton.SetActive(false);            
            _sentences.Clear();
            if (quest.isFinished && !quest.isFailed)
            {
                foreach (var sentence in dialogue.sentencesAfterQuest)
                    _sentences.Enqueue(sentence);
            }
            else
            {
                foreach (var sentence in dialogue.sentences)
                    _sentences.Enqueue(sentence);
            }
            npcNameText.text = dialogue.playerName;
            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (_sentences.Count == 1 && !quest.isFinished)
                giveQuestButton.SetActive(true);
            else if (_sentences.Count == 0)
            {
                StartCoroutine(nameof(EndDialogue));
                return;
            }
            string sentence = _sentences.Dequeue();
            npcSentenceText.text = sentence;
        }
        private IEnumerator EndDialogue()
        {
            npcSentenceText.text = "Bye";
            yield return new WaitForSeconds(3f);
            dialogueHUD.SetActive(false);
            playerHUD.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("End");
        }
    }
}

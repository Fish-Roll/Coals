using System.Collections;
using System.Collections.Generic;
using InteractWithWorld;
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
        [SerializeField] private GameObject nextSentenceButton;
        [SerializeField] private Text questName;
        [SerializeField] private string leaveQuest;
        private readonly Queue<string> _sentences = new Queue<string>();
        private readonly Queue<AudioSource> _sounds = new Queue<AudioSource>();
        [SerializeField] private QFindPapers quest;
        public void StartDialogue(Dialogue dialogue)
        {
            Cursor.lockState = CursorLockMode.None;
            playerHUD.SetActive(false);
            dialogueHUD.SetActive(true);
            nextSentenceButton.SetActive(true);
            _sentences.Clear();
            if (quest.isFinished && !quest.isFailed)
            {
                foreach (var sentence in dialogue.sentencesAfterQuest)
                    _sentences.Enqueue(sentence);
                foreach (var sound in dialogue.soundsAfterQuest)
                    _sounds.Enqueue(sound);
            }
            else
            {
                foreach (var sentence in dialogue.sentences)
                    _sentences.Enqueue(sentence);
                foreach (var sound in dialogue.sounds)
                    _sounds.Enqueue(sound);
            }
            npcNameText.text = dialogue.playerName;
            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (_sentences.Count == 0)
            {
                if (!quest.isFinished)
                {
                    quest.Activate();
                }
                StartCoroutine(nameof(EndDialogue));
                return;
            }
            string sentence = _sentences.Dequeue();
            AudioSource source = _sounds.Dequeue();
            npcSentenceText.text = sentence;
            source.Play();
        }
        private IEnumerator EndDialogue()
        {
            nextSentenceButton.SetActive(false);
            npcSentenceText.text = "Bye";
            yield return new WaitForSeconds(1f);
            dialogueHUD.SetActive(false);
            playerHUD.SetActive(true);
            if (quest.isFinished && !quest.isFailed)
            {
                Inventory.Inventory.GetInventory().AddItem(new InventoryItem { id = 2 });
                questName.text = leaveQuest;
            }
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}

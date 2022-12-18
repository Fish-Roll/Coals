using System;
using QuestSystem.Quests;
using UnityEngine;

namespace InteractWithWorld
{
    public class InventoryItem : Interactable
    {
        public int id;
        [SerializeField] private Sprite icon;
        private AudioSource _sound;
        private void Awake()
        {
            _sound = GetComponent<AudioSource>();
        }

        public override void Interact()
        {
            if(id == 1)
                QFindPapers.GetInstance().papers.Remove(gameObject);
            if(Inventory.Inventory.GetInventory().AddItem(this))
                Destroy(gameObject, 1);
            _sound.PlayDelayed(0.2f);
        }
    }
}
using QuestSystem.Quests;
using UnityEngine;

namespace InteractWithWorld
{
    public class InventoryItem : Interactable
    {
        public int id;
        [SerializeField] private Sprite icon;
        public override void Interact()
        {
            if(id == 1)
                QFindPapers.GetInstance().papers.Remove(gameObject);
            if(Inventory.Inventory.GetInventory().AddItem(this))
                Destroy(gameObject, 1);
        }
    }
}
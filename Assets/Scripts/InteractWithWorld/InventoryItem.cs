namespace InteractWithWorld
{
    public class InventoryItem : Interactable
    {
        public override void Interact()
        {
            Destroy(this.gameObject);
        }
    }
}
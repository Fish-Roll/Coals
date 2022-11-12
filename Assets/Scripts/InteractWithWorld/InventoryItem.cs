namespace InteractWithWorld
{
    public class InventoryItem : InteractableObject
    {
        public override void Interact()
        {
            Destroy(this.gameObject);
        }
    }
}
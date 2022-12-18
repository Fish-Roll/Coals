using UnityEngine;
namespace InteractWithWorld
{
    public class PortableItem : Interactable
    {
        [SerializeField] private Transform hand;
        public override void Interact()
        {
            if (hand.childCount == 0)
            {
                gameObject.transform.position = hand.position;
                gameObject.transform.SetParent(hand);
            }
        }
    }
}
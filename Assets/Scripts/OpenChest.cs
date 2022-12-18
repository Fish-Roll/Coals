using System;
using System.Collections;
using InteractWithWorld;
using UnityEngine;

public class OpenChest : Interactable
{
    [SerializeField] private float openAngle;
    [SerializeField] private float timeOpen;
    [SerializeField] private Transform cover;
    public override void Interact()
    {
        StartCoroutine(nameof(Open));
    }

    private IEnumerator Open()
    {
        Quaternion startRotation = cover.rotation;
        float duration = 1f;
        float t = 0;
        
        while (t < 1f)
        {
            t = Mathf.Min(1f, t + Time.deltaTime/duration);
            Vector3 newEulerOffset = Vector3.left * (openAngle * t);      
            cover.rotation = Quaternion.Euler(newEulerOffset) * startRotation;
            yield return null;
        }

        Inventory.Inventory.GetInventory().AddItem(new InventoryItem { id = 3 });
        tag = "Untagged";
    }
}

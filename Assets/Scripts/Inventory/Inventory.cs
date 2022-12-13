using System;
using System.Collections.Generic;
using InteractWithWorld;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private int maxСapacity;
        [SerializeField] private Text potionText;
        [SerializeField] private Text paperText;
        [SerializeField] private Text keyText;
        private string _potionTextTempl = "/5";
        private string _paperTextTempl = "/7";
        private string _keyTextTempl = "/1";
        private static GameObject _gameObject;
        public readonly Dictionary<int, List<InventoryItem>> items = new Dictionary<int, List<InventoryItem>>();

        private void Awake()
        {
            _gameObject = gameObject;
        }

        private Inventory()
        {
            
        }
        
        private static Inventory _inventory;

        public static Inventory GetInventory()
        {
            if (_inventory == null)
                _inventory = _gameObject.GetComponent<Inventory>();
            return _inventory;
        }

        public bool AddItem(InventoryItem item)
        {
            if (items.TryGetValue(item.id, out List<InventoryItem> value))
            {
                value.Add(item);
                switch (item.id)
                {
                    case 1:
                        paperText.text = value.Count + _paperTextTempl;
                        break;
                    case 2:
                        keyText.text = value.Count + _keyTextTempl;
                        break;
                    case 3:
                        potionText.text = value.Count + _potionTextTempl;
                        break;
                }
                return true;
            }

            if (maxСapacity >= items.Count)
            {
                items.Add(item.id, new List<InventoryItem>());
                items.TryGetValue(item.id, out List<InventoryItem> list);
                list.Add(item);
                switch (item.id)
                {
                    case 1:
                        paperText.text = 1 + _paperTextTempl;
                        break;
                    case 2:
                        keyText.text = 1 + _keyTextTempl;
                        break;
                    case 3:
                        potionText.text = 1 + _potionTextTempl;
                        break;
                }

                return true;
            }

            return false;
        }

        public bool RemoveItem(InventoryItem item)
        {
            if (items.TryGetValue(item.id, out List<InventoryItem> value))
            {
                value.RemoveAt(value.Count - 1);
                switch (item.id)
                {
                    case 1:
                        paperText.text = value.Count + _paperTextTempl;
                        break;
                    case 2:
                        keyText.text = value.Count + _keyTextTempl;
                        break;
                    case 3:
                        potionText.text = value.Count + _potionTextTempl;
                        break;
                }
                return true;
            }
            return true;
        }
    }
}

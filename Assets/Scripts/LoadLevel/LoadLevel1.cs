using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel1 : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject key;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var list = other.GetComponent<Inventory.Inventory>().items;
            if (list.Count > 0 && list.TryGetValue(2, out var value))
            {
                //SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.Log("Вы кто такие? Я вас не звал, идите нахуй");
            }
        }
    }

    private void Update()
    {
        if (key == null)
        {
            Debug.Log("Key");
        }
    }
}

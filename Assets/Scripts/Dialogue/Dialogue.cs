using UnityEngine;

namespace Dialogue
{
    [System.Serializable]
    public class Dialogue : MonoBehaviour
    {
        public string playerName;
        
        [TextArea(3,10)] public string[] sentences;
        [TextArea(3, 10)] public string[] sentencesAfterQuest;
    }
}

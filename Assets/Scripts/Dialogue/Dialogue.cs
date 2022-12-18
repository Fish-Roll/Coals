using System;
using UnityEngine;

namespace Dialogue
{
    [System.Serializable]
    public class Dialogue : MonoBehaviour
    {
        public string playerName;
        [TextArea(3,10)] public string[] sentences;
        public AudioSource[] sounds;
        [TextArea(3, 10)] public string[] sentencesAfterQuest;
        public AudioSource[] soundsAfterQuest;
    }
}

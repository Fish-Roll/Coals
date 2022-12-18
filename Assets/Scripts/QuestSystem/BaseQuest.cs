using UnityEngine;

namespace QuestSystem
{
    public abstract class BaseQuest : MonoBehaviour
    {
        [SerializeField] protected string questName;
        public bool isTaken;
        public bool isFinished;
        public bool isFailed;
        public abstract void Activate();

        public abstract void Successful();

        public abstract void Failed();
    }
}
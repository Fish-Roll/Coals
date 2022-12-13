using System;
using UnityEngine;
using System.Collections.Generic;
namespace QuestSystem
{
    public class QuestManager : MonoBehaviour
    {
        public List<BaseQuest> quests;

        private void Awake()
        {
            quests = new List<BaseQuest>();
        }

        private void AddNewQuest(BaseQuest quest)
        {
            if(quest != null)
                quests.Add(quest);
        }
    }
}

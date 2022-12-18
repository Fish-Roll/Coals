using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuestSystem.Quests
{
    public class QFindPapers : BaseQuest
    {
        private static GameObject _gameObject;
        private void Awake()
        {
            _gameObject = gameObject;
        }
        private QFindPapers()
        {
            
        }
        private static QFindPapers _qFindPapers;
        public static QFindPapers GetInstance()
        {
            if (_qFindPapers == null)
                _qFindPapers = _gameObject.GetComponent<QFindPapers>();
            return _qFindPapers;
        }

        
        [SerializeField] private float timer;
        [SerializeField] private Text timerUI;
        [SerializeField] private Text questNameText;
        [SerializeField] public List<GameObject> papers = new List<GameObject>();
        [SerializeField] private List<Transform> spawnPoints;
        [SerializeField] private GameObject paperPrefab;
        [SerializeField] private string goToSkeletonQuest;
        [SerializeField] private Text questFailed;
        [SerializeField] private Canvas failedUI;
        public override void Activate()
        {
            SpawnObjects();
            isTaken = true;
            isFailed = false;
            questNameText.text = questName;
            questNameText.enabled = true;
            timerUI.enabled = true;
            StartCoroutine(nameof(Timer));
        }

        private void SpawnObjects()
        {
            for (int i = papers.Count-1; i >= 0; i--)
                Destroy(papers[i]);
            for (int i = spawnPoints.Count-1; i >= 0; i--)
            {
                var paper = Instantiate(paperPrefab, spawnPoints[i].position, Quaternion.identity);
                papers.Add(paper);
            }
        }
        private IEnumerator Timer()
        {
            float newTimer = timer;
            while (newTimer >= 0)
            {
                if (papers.Count == 0)
                {
                    Successful();
                    yield break;
                }
                int minutes = (int)(newTimer / 60);
                int seconds = (int)(newTimer % 60);
                timerUI.text = $"Time remaining: {minutes} : {seconds}"; 
                newTimer -= Time.deltaTime;
                yield return null;
            }
            Failed();
        }
        public override void Successful()
        {
            isTaken = false;
            isFinished = true;
            isFailed = false;
            timerUI.enabled = false;
            questNameText.text = goToSkeletonQuest;
        }

        public override void Failed()
        {
            isTaken = false;
            isFinished = false;
            isFailed = true;
            timerUI.text = "Quest failed";
            questNameText.text = goToSkeletonQuest;
        }

    }
}
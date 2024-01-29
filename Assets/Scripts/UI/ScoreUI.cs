using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreUI : MonoBehaviour
    {
        public static ScoreUI Singleton { get; private set; }

        public void Awake()
        {
            if (Singleton != null && Singleton != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Singleton = this;
            }
        }

        [SerializeField] private List<TMP_Text> scoresUI;
        [SerializeField] private List<int> scores = new List<int>(4);

        public void Gool(int who, int to)
        {
            scores[who] += 1;
            scoresUI[who].text = scores[who].ToString();

            scores[to] -= 1;
            scoresUI[to].text = scores[to].ToString();
        }
    }
}
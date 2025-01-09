using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ReplayControls
{
    public class GameOverviewItem : MonoBehaviour
    {
        public Color color;
        public string gameName;
        public GameVisibilityControl controller;

        /// <summary>
        /// Sets up the list scroll view items for showing and hiding objects from games
        /// </summary>
        public void SetUp()
        {
            GetComponentInChildren<Image>().color = color;
            GetComponentInChildren<TMP_Text>().text = gameName;
            GetComponentInChildren<Toggle>().onValueChanged.AddListener(controller.HideObjects);
        }
    }
}
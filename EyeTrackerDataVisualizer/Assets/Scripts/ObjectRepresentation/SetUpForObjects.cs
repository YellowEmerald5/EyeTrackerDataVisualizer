using GameEventScripts;
using ReplayControls;
using ScriptableObjects;
using UnityEngine;

namespace ObjectRepresentation
{
    public class SetUpForObjects : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverviewContent;
        [SerializeField] private GameObject ScrollViewItem;
        [SerializeField] private StorageSO storage;
        [SerializeField] private GameObject instanceObject;
        [SerializeField] private GameObject gazeObject;
        [SerializeField] private GameEvent timeValueChanged;

        /// <summary>
        /// Completes the object setup
        /// </summary>
        private void Start()
        {
            var i = 0;
            foreach (var game in storage.GameList)
            {
                var color = new Color(0,0,0,1);
                if (i < storage.Colors.Count)
                {
                    color = storage.Colors[i];
                }
                var parentObject = new GameObject();
                var visibilityControl = parentObject.AddComponent<GameVisibilityControl>();
                var spawner = parentObject.AddComponent<ObjectSpawner>();
                spawner.storage = storage;
                spawner.visibilityControl = visibilityControl;
                spawner.color = color;
                spawner.gazeObject = gazeObject;
                spawner.Parent = parentObject.transform;
                spawner.instanceObject = instanceObject;
                spawner.timeValueChanged = timeValueChanged;
                spawner.Game = game;
                spawner.SpawnObjects();

                var scrollViewItem = Instantiate(ScrollViewItem,gameOverviewContent.transform);
                var overview = scrollViewItem.AddComponent<GameOverviewItem>();
                overview.controller = visibilityControl;
                overview.gameName = game.Name;
                overview.color = color;
                overview.SetUp();
                scrollViewItem.SetActive(true);
                i++;
            }
        }
    }
}
using System.Collections.Generic;
using DataAccessAndPreparation;
using Objects;
using ScriptableObjects;
using UnityEngine;

namespace Lists
{
    public class GameListScript : MonoBehaviour
    {
        [SerializeField] private GameObject scrollViewContent;
        [SerializeField] private GameObject listItem;
        [SerializeField] private StorageSO storage;
        private List<GameObject> _gameObjects = new();

        /// <summary>
        /// Gets games from the chosen sessions and displays them in the scroll view
        /// </summary>
        private void OnEnable()
        {
            ResetScrollViewContent();
            storage.CurrentItemType = MainMenuItemTypes.Game;
            storage.TempGameList = new List<Game>();
            storage.TempGameList = DatabaseHandler.GetGames(storage.SessionList);
            foreach (var item in storage.TempGameList)
            {
                var scrollViewItem = Instantiate(listItem, scrollViewContent.transform, false);
                _gameObjects.Add(scrollViewItem);
                scrollViewItem.SetActive(true);
                var addOrRemove = scrollViewItem.GetComponent<AddOrRemoveCheckedItem>();
                addOrRemove.Game = item;
                var toggleLabel = addOrRemove.Label;
                toggleLabel.text = item.Id + " " + item.Name;
            }

        }

        /// <summary>
        /// Resets the list used and removes existing items
        /// </summary>
        private void ResetScrollViewContent()
        {
            if (_gameObjects == null) return;
            foreach (var item in _gameObjects)
            {
                Destroy(item);
            }
        }

    }
}
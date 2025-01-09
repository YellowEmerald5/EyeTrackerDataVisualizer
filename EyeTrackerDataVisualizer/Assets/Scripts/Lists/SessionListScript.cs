using System.Collections.Generic;
using System.Linq;
using DataAccessAndPreparation;
using Objects;
using ScriptableObjects;
using UnityEngine;

namespace Lists
{
    public class SessionListScript : MonoBehaviour
    {
        [SerializeField] private GameObject scrollViewContent;
        [SerializeField] private GameObject listItem;
        [SerializeField] private StorageSO storage;
        private List<GameObject> _gameObjects;

        /// <summary>
        /// Gets sessions from the chosen users and displays them in the scroll view
        /// </summary>
        private void OnEnable()
        {
            RemoveOldSessions();
            _gameObjects = new List<GameObject>();
            storage.SessionList = new List<Session>();
            storage.CurrentItemType = MainMenuItemTypes.Session;
            storage.TempSessionList = DatabaseHandler.GetSessions(storage.UserList);
            foreach (var item in storage.TempSessionList)
            {
                var scrollViewItem = Instantiate(listItem, scrollViewContent.transform, false);
                _gameObjects.Add(scrollViewItem);
                scrollViewItem.SetActive(true);
                var addOrRemove = scrollViewItem.GetComponent<AddOrRemoveCheckedItem>();
                addOrRemove.Session = item;
                var toggleLabel = addOrRemove.Label;
                toggleLabel.text = "User: " + storage.UserList.FirstOrDefault(u => u.Id == item.UserId)?.Nickname +
                                   " Session: " + item.SessionNumber;
            }
        }

        /// <summary>
        /// Resets the list used and removes existing items
        /// </summary>
        private void RemoveOldSessions()
        {
            if (_gameObjects == null) return;
            foreach (var item in _gameObjects)
            {
                Destroy(item);
            }
        }
    }
}
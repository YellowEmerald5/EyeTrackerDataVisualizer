using System;
using DataAccessAndPreparation;
using Objects;
using ScriptableObjects;
using UnityEngine;

namespace Lists
{
    public class UserListScript : MonoBehaviour
    {
        [SerializeField] private GameObject scrollViewContent;
        [SerializeField] private GameObject listItem;
        [SerializeField] private StorageSO storage;

        /// <summary>
        /// Gets games from the database and displays them in the scroll view
        /// </summary>
        private void Start()
        {
            storage.CurrentItemType = MainMenuItemTypes.User;
            storage.TempUserList = DatabaseHandler.GetUsers();
            foreach (var item in storage.TempUserList)
            {
                var scrollViewItem = Instantiate(listItem, scrollViewContent.transform, false);
                scrollViewItem.SetActive(true);
                var addOrRemove = scrollViewItem.GetComponent<AddOrRemoveCheckedItem>();
                addOrRemove.User = item;
                var toggleLabel = addOrRemove.Label;
                toggleLabel.text = item.Nickname;
            }
        }

        /// <summary>
        /// Sets the item type for correct entries
        /// </summary>
        private void OnEnable()
        {
            storage.CurrentItemType = MainMenuItemTypes.User;
        }
    }
}
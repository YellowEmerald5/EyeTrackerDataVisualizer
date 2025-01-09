using JetBrains.Annotations;
using Objects;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lists
{
    public class AddOrRemoveCheckedItem : MonoBehaviour
    {
        [CanBeNull] public User User;
        [CanBeNull] public Session Session;
        [CanBeNull] public Game Game;
        [SerializeField] public StorageSO Storage;
        [SerializeField] public Text Label;

        /// <summary>
        /// Adds user, session and game to the lists of chosen items based on the current item type in the storage
        /// </summary>
        /// <param name="toggleValue">If a toggle is active or not</param>
        public void AddItemToList(bool toggleValue)
        {
            if (toggleValue)
            {
                switch (Storage.CurrentItemType)
                {
                    case MainMenuItemTypes.User:
                        Storage.UserList.Add(User);
                        break;
                    case MainMenuItemTypes.Session:
                        Storage.SessionList.Add(Session);
                        break;
                    case MainMenuItemTypes.Game:
                        Storage.GameList.Add(Game);
                        break;
                }
                
            }
            else
            {
                switch (Storage.CurrentItemType)
                {
                    case MainMenuItemTypes.User:
                        Storage.UserList.Remove(User);
                        break;
                    case MainMenuItemTypes.Session:
                        Storage.SessionList.Remove(Session);
                        break;
                    case MainMenuItemTypes.Game:
                        Storage.GameList.Remove(Game);
                        break;
                }
            }
            
        }
    }
}
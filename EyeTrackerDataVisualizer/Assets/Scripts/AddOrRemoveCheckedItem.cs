using JetBrains.Annotations;
using Objects;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class AddOrRemoveCheckedItem : MonoBehaviour
    {
        [CanBeNull] public User User;
        [CanBeNull] public Session Session;
        [CanBeNull] public Game Game;
        [SerializeField] public StorageSO Storage;
        [SerializeField] public Text Label;

        public void AddUserToList(bool toggleValue)
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
                Storage.UserList.Remove(User);
            }
            
        }
    }
}
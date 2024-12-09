using System.Collections.Generic;
using DefaultNamespace;
using Objects;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "StorageSO", menuName = "ScriptableObjects/StorageSO", order = 0)]
    public class StorageSO : ScriptableObject
    {
        public List<User> UserList = new();
        public List<Session> SessionList = new();
        public List<Game> GameList = new();
        public MainMenuItemTypes CurrentItemType = MainMenuItemTypes.User;
    }
}
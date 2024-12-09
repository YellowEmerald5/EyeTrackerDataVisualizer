using DefaultNamespace;
using Objects;
using ScriptableObjects;
using UnityEngine;

public class UserListScript : MonoBehaviour
{
    [SerializeField] private GameObject scrollViewContent;
    [SerializeField] private GameObject listItem;
    [SerializeField] private StorageSO storage;
    private void Start()
    {
        
        foreach (var item in storage.UserList)
        {
            var scrollViewItem = Instantiate(listItem, scrollViewContent.transform, false);
            scrollViewItem.SetActive(true);
            var addOrRemove = scrollViewItem.GetComponent<AddOrRemoveCheckedItem>();
            addOrRemove.User = item;
            var toggleLabel = addOrRemove.Label;
            toggleLabel.text = item.Nickname;
        }
    }
        
}
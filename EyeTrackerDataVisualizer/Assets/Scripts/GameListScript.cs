using DefaultNamespace;
using Objects;
using ScriptableObjects;
using UnityEngine;

public class GameListScript : MonoBehaviour
{
    [SerializeField] private GameObject scrollViewContent;
    [SerializeField] private GameObject listItem;
    [SerializeField] private StorageSO storage;
    private void Start()
    {
        
        foreach (var item in storage.GameList)
        {
            var scrollViewItem = Instantiate(listItem, scrollViewContent.transform, false);
            scrollViewItem.SetActive(true);
            var addOrRemove = scrollViewItem.GetComponent<AddOrRemoveCheckedItem>();
            addOrRemove.Game = item;
            var toggleLabel = addOrRemove.Label;
            toggleLabel.text = item.Id + " " + item.Name;
        }
    }
        
}
using ScriptableObjects;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class ResetStorage : MonoBehaviour
{
    public StorageSO storage;
    [FormerlySerializedAs("imageAndVideoStorage")] public ImagesAndVideosStorage imagesAndVideosStorage;
        
    /// <summary>
    /// Sets up a listener to reset the storageSO when play mode is exited in the editor
    /// This will not run in the built application
    /// </summary>
    private void OnEnable()
    {
        EditorApplication.playModeStateChanged += ResetStorages;
    }

    /// <summary>
    /// Resets all values in the StorageSO when play mode is exited.
    /// This is only necessary in the editor due to the behaviour of scriptable objects
    /// </summary>
    /// <param name="state">Represents the current state of the editor</param>
    private void ResetStorages(PlayModeStateChange state)
    {
        if (state != PlayModeStateChange.EnteredEditMode) return;
        storage.ResetStorage();
        imagesAndVideosStorage.ResetStorage();
    }
}
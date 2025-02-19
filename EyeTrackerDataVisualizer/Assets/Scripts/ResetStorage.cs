using ScriptableObjects;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class ResetStorage : MonoBehaviour
{
    public StorageSO storage;
    public GameIdsForVisibility gameIds;
    public ShowHideTimelineTimeframe showHideTimelineTimeframe;
    public ImagesAndVideosStorage imagesAndVideosStorage;
    public TimeframeValuesStorage timeframeValuesStorage;
        
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
        storage.Reset();
        imagesAndVideosStorage.Reset();
        gameIds.Reset();
        showHideTimelineTimeframe.Reset();
        timeframeValuesStorage.Reset();
    }
}
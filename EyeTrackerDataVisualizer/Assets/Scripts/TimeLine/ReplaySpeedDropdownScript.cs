using ScriptableObjects;
using UnityEngine;

namespace TimeLine
{
    public class ReplaySpeedDropdownScript : MonoBehaviour
    {
        [SerializeField] private StorageSO storage;
        
        /// <summary>
        /// Changes the delay for the timeline based on dropdown values
        /// </summary>
        /// <param name="selectedSpeed">Position in dropdown list</param>
        public void ChangeReplaySpeed(int selectedSpeed)
        {
            storage.TimeDelay = selectedSpeed switch
            {
                0 => 0,
                1 => 10,
                2 => 100,
                _ => storage.TimeDelay
            };
            storage.SpeedChanged = true;
        }
    }
}
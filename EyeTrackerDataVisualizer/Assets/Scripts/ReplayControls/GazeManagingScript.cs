using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace ReplayControls
{
    public class GazeManagingScript : MonoBehaviour
    {
        public StorageSO storage;
        public List<Vector3> positions;
        
        /// <summary>
        /// Moves the gaze object to the position for the time stamp
        /// </summary>
        public void MoveGazeObject()
        {
            if (storage.SensorData.Count == 0 || storage.CurrentTimestamp >= positions.Count) return;
            transform.position = positions[(int)storage.CurrentTimestamp];
        }
    }
}
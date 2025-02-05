using System.Linq;
using ScriptableObjects;
using UnityEngine;

namespace ReplayControls
{
    public class GazeManagingScript : MonoBehaviour
    {
        public StorageSO storage;
        //private int i = 0;
        
        /// <summary>
        /// Moves the gaze object to the position for the time stamp
        /// </summary>
        public void MoveGazeObject()
        {
            if (storage.SensorData.Count == 0 || storage.CurrentTimestamp >= storage.SensorData.Count) return;
            var pos = storage.SensorData[(int)storage.CurrentTimestamp];
            if (pos == null) return;
            var worldPos = storage.MainCamera.ScreenToWorldPoint(new Vector3(pos.PosX, pos.PosY, 200));
            transform.position = worldPos;
            //i++;
        }
    }
}
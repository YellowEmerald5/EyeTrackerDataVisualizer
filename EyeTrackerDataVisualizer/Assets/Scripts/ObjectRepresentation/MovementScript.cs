using System.Collections.Generic;
using Objects;
using UnityEngine;

namespace ObjectRepresentation
{
    public class MovementScript : MonoBehaviour
    {
        //The object should be spawned by another script and this script should be added to it
        //Need to make changes for checking if the object should be visible
        private List<Point> _positions;
        private int _currentPosition;
        private Renderer _renderer;
        private ObjectInGame Object;
        //public StorageSO Storage;

        /// <summary>
        /// Prepares the object with the data for replay
        /// </summary>
        /// <param name="obj">Data from the object this one should represent</param>
        public void SetUpObjectRetracing(ObjectInGame obj)
        {
            Object = obj;
            _renderer = GetComponent<MeshRenderer>();
            foreach (var point in obj.Points)
            {
                _positions.Add(point);
            }

            var p = _positions[_currentPosition];
            transform.position = new Vector3(p.PosX, p.PosY, p.PosZ);
            ResizeObject(_currentPosition);
        }

        /// <summary>
        /// Moves the object to the next point in the list of positions
        /// </summary>
        public void MoveObjectForwards()
        {
            _currentPosition++;
            if (_currentPosition < _positions.Count - 1)
            {
                var point = _positions[_currentPosition];
                transform.position = new Vector3(point.PosX, point.PosY, point.PosZ);
                ResizeObject(_currentPosition);
                _renderer.enabled = true;
            }
            else
            {
                _renderer.enabled = false;
            }

        }

        /// <summary>
        /// Moves the object to the previous point in the list of positions
        /// </summary>
        public void MoveObjectBackwards()
        {
            _currentPosition--;
            if (_currentPosition >= 0)
            {
                var point = _positions[_currentPosition];
                transform.position = new Vector3(point.PosX, point.PosY, point.PosZ);
                ResizeObject(_currentPosition);
                _renderer.enabled = true;
            }
            else
            {
                _renderer.enabled = false;
            }
        }

        /// <summary>
        /// (Redundant) Moves the object to a given point in the list of positions
        /// </summary>
        public void MoveObjectToTimestamp()
        {
            /*var timestamp = Storage.timestamp;
            if (_positions.FirstOrDefault(p => p.Time  == timestamp) != null)
            {
                var pos = _positions.FindIndex(i => i.Time == timestamp);
                var point = _positions[pos];
                transform.position = new Vector3(point.PosX,point.PosY,point.PosZ);
                ResizeObject(pos);
                _renderer.enabled = true;
            }
            else
            {
                _renderer.enabled = false;
            }*/
        }


        /// <summary>
        /// Resizes the object to match the aoi sizes
        /// </summary>
        /// <param name="position">Position in the array</param>
        private void ResizeObject(int position)
        {
            if (position < 0 || position > Object.Aoi.Sizes.Count) return;
            var aoisize = Object.Aoi.Sizes[position];
            //Height and width are screenspace based, might not be the correct sizes here
            transform.localScale = new Vector3(aoisize.Height, aoisize.Width, 0);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Objects;
using ReplayControls;
using ScriptableObjects;
using UnityEngine;

namespace ObjectRepresentation
{
    public class ObjectMovementAndResize : MonoBehaviour
    {
        public ObjectInGame Object;
        public StorageSO storage;
        public int startPosition;
        public int endPosition;
        private MeshRenderer _mesh;
        public Color Color;
        //private int i = 0;
        private bool _destroyed;
        public List<Vector3> _pointsInWorld;

        private int timestampDifference;

        /// <summary>
        /// Moves the object into position and resizes it
        /// </summary>
        public void Begin()
        {
            _mesh = gameObject.GetComponent<MeshRenderer>();
            /*var b = new Bounds
            {
                size = new Vector3(Object.Aoi.Sizes[0].Width, Object.Aoi.Sizes[0].Height, 0)
            };
            _mesh.bounds = b;*/
            var material = _mesh.material;
            material.color = Color;
            material.enableInstancing = true;
            timestampDifference = (int) storage.TotalTimestampEntries - startPosition;
            if ((int)storage.CurrentTimestamp == startPosition)
            {
                transform.position = _pointsInWorld[0];
            }
        }

        /// <summary>
        /// Moves the object to the given point based on the timestamp and hides the object if it is destroyed and show destroyed is unchecked
        /// </summary>
        public void MoveObject()
        {
            var currentTimestamp = storage.CurrentTimestamp;
            if (currentTimestamp > startPosition && currentTimestamp < endPosition && _destroyed)
            {
                _destroyed = false;
            }else if (currentTimestamp > endPosition && !_destroyed)
            {
                _destroyed = true;
                HideObject();
                return;
            }
            if (currentTimestamp < startPosition || currentTimestamp > endPosition) return;
            if (currentTimestamp > _pointsInWorld.Count - 1) return;
            transform.position = _pointsInWorld[(int) currentTimestamp-startPosition];
        }
        
        /// <summary>
        /// Enables or disables the object mesh
        /// </summary>

        public void HideObject()
        {
            if (!_destroyed) return;
            _mesh.enabled = storage.ShowDestroyed;
        }
    }
}

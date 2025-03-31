using System.Collections.Generic;
using System.Linq;
using Objects;
using ReplayControls;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

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
        private Image _image;

        private int timestampDifference;

        /// <summary>
        /// Moves the object into position and resizes it
        /// </summary>
        public void Begin()
        {
            gameObject.name = Object.Name;
            if(!storage.twoD){
                _mesh = gameObject.GetComponent<MeshRenderer>();
                /*var b = new Bounds
                {
                    size = new Vector3(Object.Aoi.Sizes[0].Width, Object.Aoi.Sizes[0].Height, 0)
                };
                _mesh.bounds = b;*/
                var material = _mesh.material;
                material.color = Color;
                material.enableInstancing = true;
                
            }
            else
            {
                _image = gameObject.GetComponent<Image>();
                _image.color = Color;
            }
            
            timestampDifference = (int) storage.TotalTimestampEntries - startPosition;

            if ((int)storage.CurrentTimestamp == startPosition)
            {
                if (storage.twoD)
                {
                    transform.localPosition = _pointsInWorld[0];
                }
                else
                {
                    transform.position = _pointsInWorld[0];
                }
                
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
            if (currentTimestamp - startPosition > _pointsInWorld.Count - 1) return;
            if (storage.twoD)
            {
                transform.localPosition = _pointsInWorld[(int)currentTimestamp - startPosition];
            }
            else
            {
                transform.position = _pointsInWorld[(int)currentTimestamp - startPosition];
            }
                
        }
        
        /// <summary>
        /// Enables or disables the object mesh
        /// </summary>

        public void HideObject()
        {
            if (!_destroyed) return;
            if (!storage.twoD)
            {
                _mesh.enabled = storage.ShowDestroyed;
            }
            else
            {
                _image.enabled = storage.ShowDestroyed;
            }
        }
    }
}

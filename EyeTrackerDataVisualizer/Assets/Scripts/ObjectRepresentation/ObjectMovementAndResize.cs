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
        public bool ShowDestroyed;
        private MeshRenderer _mesh;
        public Color Color;
        public GameVisibilityControl visibilityControl;
        private int i = 0;

        /// <summary>
        /// Moves the object into position and resizes it
        /// </summary>
        public void Begin()
        {
            _mesh = gameObject.GetComponent<MeshRenderer>();
            visibilityControl.objectsToHide.Add(_mesh);
            var pos = new Vector3(Object.SpawnPositionX, Object.SpawnPositionY, Object.SpawnPositionZ);
            var worldPos = storage.MainCamera.ScreenToWorldPoint(pos);
            var b = new Bounds();
            b.size = new Vector3(Object.Aoi.Sizes[0].Width, Object.Aoi.Sizes[0].Height, 0);
            _mesh.bounds = b;
            _mesh.material.color = Color;
            transform.position = worldPos;
        }

        /// <summary>
        /// Moves the object to the given point based on the timestamp
        /// </summary>
        public void MoveObject()
        {
            if (!ShowDestroyed)
                _mesh.enabled = !(storage.CurrentTimestamp > Object.Points[^1].Time ||
                                  storage.CurrentTimestamp < Object.Points[0].Time);

            var point = Object.Points.Find(p => p.Time == storage.Timestamps[(int) storage.CurrentTimestamp]);
            if (point == null) return;
            var pos = new Vector3(point.PosX, point.PosY, point.PosZ + 200);
            var worldPos = storage.MainCamera.ScreenToWorldPoint(pos);
            var t = transform;
            t.position = worldPos;
            i++;
            var b = new Bounds
            {
                size = new Vector3(Object.Aoi.Sizes[i].Width, Object.Aoi.Sizes[i].Height, 0)
            };
            _mesh.bounds = b;
        }
    }
}

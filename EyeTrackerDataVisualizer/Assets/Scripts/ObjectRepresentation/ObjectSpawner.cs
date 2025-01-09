using GameEventScripts;
using Objects;
using ReplayControls;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace ObjectRepresentation
{
    public class ObjectSpawner : MonoBehaviour
    {
        public StorageSO storage;
        public GameObject instanceObject;
        public Game Game;
        public GameObject gazeObject;
        public GameEvent timeValueChanged;
        public Transform Parent;
        public Color color;
        public GameVisibilityControl visibilityControl;
        
        /// <summary>
        /// Spawns in and sets up the object representations and the gaze objects
        /// </summary>
        public void SpawnObjects()
        { 
            
            foreach (var ob in Game.Objects)
            {
                var instance = Instantiate(instanceObject,Parent.transform);
                instance.SetActive(true);
                var movementScript = instance.AddComponent<ObjectMovementAndResize>();
                movementScript.Color = color;
                movementScript.ShowDestroyed = storage.ShowDestroyed;
                var eventListener = instance.AddComponent<GameEventListener>();
                var ev = new UnityEvent();
                ev.AddListener(movementScript.MoveObject);
                eventListener.gameEvent = timeValueChanged;
                eventListener.RegisterListener();
                eventListener.response = ev;
                movementScript.storage = storage;
                movementScript.Object = ob;
                movementScript.visibilityControl = visibilityControl;
                movementScript.Begin();
            }

            var gazeObjectInstance = Instantiate(gazeObject,Parent.transform);
            gazeObjectInstance.SetActive(true);
            var script = gazeObjectInstance.AddComponent<GazeManagingScript>();
            var mesh = gazeObjectInstance.GetComponent<MeshRenderer>();
            visibilityControl.objectsToHide.Add(mesh);
            mesh.material.color = color;
            script.storage = storage;
            var gameEventListener = script.AddComponent<GameEventListener>();
            var ent = new UnityEvent();
            ent.AddListener(script.MoveGazeObject);
            gameEventListener.gameEvent = timeValueChanged;
            gameEventListener.RegisterListener();
            gameEventListener.response = ent;
        }
    }
}
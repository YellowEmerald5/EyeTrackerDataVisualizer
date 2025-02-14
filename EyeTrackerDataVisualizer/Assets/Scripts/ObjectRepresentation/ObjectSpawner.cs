using System.Collections.Generic;
using GameEventScripts;
using Objects;
using ReplayControls;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace ObjectRepresentation
{
    public class ObjectSpawner : MonoBehaviour
    {
        public StorageSO storage;
        public GameObject instanceObject;
        public Game Game;
        public GameObject gazeObject;
        public GameEvent timeValueChanged;
        public GameEvent showHideDestroyed;
        public Color color;
        
        /// <summary>
        /// Spawns in and sets up the object representations and the gaze objects
        /// </summary>
        public void SpawnObjects(List<List<Vector3>> objectWorldPositions,List<Vector3> gazeWorldPositions)
        { 
            
            for (var i = 0; i < Game.Objects.Count; i++)
            {
                var obj = Game.Objects[i];
                var instance = Instantiate(instanceObject,gameObject.transform);
                instance.SetActive(true);
                var movementScript = instance.AddComponent<ObjectMovementAndResize>();
                var startAndEnd = storage.StartAndEndPoints[obj.Name];
                movementScript.startPosition = startAndEnd.Item1;
                movementScript.endPosition = startAndEnd.Item2;
                movementScript._pointsInWorld = objectWorldPositions[i];
                movementScript.Color = color;
                
                var eventListener = instance.AddComponent<GameEventListener>();
                var ev = new UnityEvent();
                ev.AddListener(movementScript.MoveObject);
                eventListener.gameEvent = timeValueChanged;
                eventListener.RegisterListener();
                eventListener.response = ev;
                
                movementScript.storage = storage;
                movementScript.Object = obj;
                movementScript.Begin();
                
                var eventListenerHide = instance.AddComponent<GameEventListener>();
                var evHide = new UnityEvent();
                evHide.AddListener(movementScript.HideObject);
                eventListenerHide.gameEvent = showHideDestroyed;
                eventListenerHide.RegisterListener();
                eventListenerHide.response = evHide;
                
            }

            var gazeObjectInstance = Instantiate(gazeObject,gameObject.transform);
            gazeObjectInstance.SetActive(true);
            var script = gazeObjectInstance.AddComponent<GazeManagingScript>();
            var mesh = gazeObjectInstance.GetComponent<MeshRenderer>();
            mesh.material.color = color;
            script.storage = storage;
            script.positions = gazeWorldPositions;
            var gameEventListener = script.AddComponent<GameEventListener>();
            var ent = new UnityEvent();
            ent.AddListener(script.MoveGazeObject);
            gameEventListener.gameEvent = timeValueChanged;
            gameEventListener.RegisterListener();
            gameEventListener.response = ent;
        }
    }
}
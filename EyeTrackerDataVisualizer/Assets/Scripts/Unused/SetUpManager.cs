using System.Collections.Generic;
using GameEventScripts;
using ObjectRepresentation;
using Objects;
using UnityEngine;
using UnityEngine.Events;

namespace Unused
{
    public class SetUpManager : MonoBehaviour
    {
        //Takes a storage scriptable object
        //[SerializeField] private  _storage;
        [SerializeField] private GameObject objectReplacement;
        private ObjectInGame _temp = new("", new Aoi("", 0, new Vector3()), 0, 0, 0, 0, 0);
        
        /// <summary>
        /// Creates a mock objectInGame to display
        /// </summary>
        private void Start()
        {
            var list = new List<ObjectInGame>();
            foreach (var item in list)
            {
                var obj = Instantiate(objectReplacement);
                var movementscript = obj.GetComponent<MovementScript>();
                movementscript.SetUpObjectRetracing(_temp);
                /*AddGameEventListener(obj,new GameEvent(),movementscript.MoveObjectForwards);
                AddGameEventListener(obj,new GameEvent(),movementscript.MoveObjectBackwards);
                AddGameEventListener(obj,new GameEvent(),movementscript.MoveObjectToTimestamp);*/
            }
        }
        
        /// <summary>
        /// Adds a GameEventListener to an object and sets it up
        /// </summary>
        /// <param name="obj">Object the listener should be added to</param>
        /// <param name="ev">GameEvent the listener should listen to</param>
        /// <param name="action">Action to be performed when the GameEvent is invoked</param>
        private static void AddGameEventListener(GameObject obj, GameEvent ev, UnityAction action)
        {
            var listener = obj.AddComponent<GameEventListener>();
            listener.gameEvent = ev;
            var unityEvent = new UnityEvent();
            unityEvent.AddListener(action);
            listener.response = unityEvent;
        }
    }
}
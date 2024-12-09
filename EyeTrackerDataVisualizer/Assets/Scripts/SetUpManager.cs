using System;
using System.Collections.Generic;
using Objects;
using ObjectTracking.GameEventScripts;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class SetUpManager : MonoBehaviour
    {
        //Takes a storage scriptable object
        //[SerializeField] private  _storage;
        [SerializeField] private GameObject objectReplacement;
        private ObjectInGame _temp = new ("",new Aoi("",0,new Vector3()),0,0,0,0,0);
        private void Start()
        {
            var list = new List<ObjectInGame>();
            long timestamp = 0;
            foreach (var item in list)
            {
                var obj = Instantiate(objectReplacement);
                var movementscript = obj.GetComponent<MovementScript>();
                movementscript.SetUpObjectRetracing(_temp);
                AddGameEventListener(obj,new GameEvent(),movementscript.MoveObjectForwards);
                AddGameEventListener(obj,new GameEvent(),movementscript.MoveObjectBackwards);
                AddGameEventListener(obj,new GameEvent(),movementscript.MoveObjectToTimestamp);
            }
            
        }
        
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
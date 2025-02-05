using System.Collections.Generic;
using GameEventScripts;
using Objects;
using ReplayControls;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ObjectRepresentation
{
    public class SetUpForObjects : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverviewContent;
        [SerializeField] private GameObject ScrollViewItem;
        [SerializeField] private StorageSO storage;
        [SerializeField] private GameObject instanceObject;
        [SerializeField] private GameObject gazeObject;
        [SerializeField] private GameEvent timeValueChanged;
        [SerializeField] private GameEvent showHideDestroyed;
        [SerializeField] private GameEvent showHideGame;
        [SerializeField] private GameEvent changedRepresentation;
        [SerializeField] private Shader shader;
        [SerializeField] private GameIdsForVisibility gameIds;
        [SerializeField] private ShowHideTimelineTimeframe showHideTimelineTimeframe;
        [SerializeField] private TimeframeVisibilityController timeframeVisibilityController;
        

        /// <summary>
        /// Completes the object setup
        /// </summary>
        private void Start()
        {
            var parentController = new GameObject
            {
                name = "ObjectController"
            };
            var timelineObjects = new GameObject
            {
                name = "TimelineObjects"
            };
            var timeframeObjects = new GameObject
            {
                name = "TimeframeObjects"
            };

            timelineObjects.transform.parent = parentController.transform;
            timeframeObjects.transform.parent = parentController.transform;

            var timelineVisibilityControl = timelineObjects.AddComponent<GameVisibilityControl>();
            var timeframeVisibilityControl = timeframeObjects.AddComponent<GameVisibilityControl>();

            timelineVisibilityControl.gameIdsStorage = gameIds;
            timeframeVisibilityControl.gameIdsStorage = gameIds;

            timelineVisibilityControl.showHide = showHideTimelineTimeframe;
            timeframeVisibilityControl.showHide = showHideTimelineTimeframe;
            
            var changeRepresentation = timelineObjects.AddComponent<GameEventListener>();
            changeRepresentation.gameEvent = changedRepresentation;
            var ev = new UnityEvent();
            ev.AddListener(timelineVisibilityControl.ShowHideAll);
            changeRepresentation.response = ev;
            changeRepresentation.RegisterListener();
            
            changeRepresentation = timeframeObjects.AddComponent<GameEventListener>();
            changeRepresentation.gameEvent = changedRepresentation;
            ev = new UnityEvent();
            ev.AddListener(timeframeVisibilityControl.ShowHideAll);
            changeRepresentation.response = ev;
            changeRepresentation.RegisterListener();
            
            var i = 0;
            foreach (var game in storage.GameList)
            {
                var color = new Color(0,0,0,1);
                if (i < storage.Colors.Count)
                {
                    color = storage.Colors[i];
                }

                //var listItem = Instantiate(ScrollViewItem, gameOverviewContent.transform);

                var worldPositions = new List<List<Vector3>>();
                foreach (var obj in game.Objects)
                {
                    var pointsInWorldCoords = new List<Vector3>();
                    foreach (var point in obj.Points)
                    {
                        var pos = new Vector3(point.PosX, point.PosY, point.PosZ +300);
                        var worldPos = storage.MainCamera.ScreenToWorldPoint(pos);
                        pointsInWorldCoords.Add(worldPos);
                    }
                    worldPositions.Add(pointsInWorldCoords);
                }
                
                
                CreateTimelineObjects(timelineObjects,timelineVisibilityControl, color, game, worldPositions);
                CreateTimeframeObjects(timeframeObjects,game,worldPositions,timeframeVisibilityControl,color);
                i++;
            }
        }

        private void CreateTimelineObjects(GameObject timelineObjects, GameVisibilityControl timelineVisibilityControl, Color color, Game game, List<List<Vector3>> gameWorldPositions)
        {
            var parentObject = new GameObject
            {
                transform =
                {
                    parent = timelineObjects.transform
                }
            };
            parentObject.name = "TimeLine " + game.Id;
            timelineVisibilityControl.AddGame(game.Id,parentObject);
            timelineVisibilityControl.TimeType = false;
            
            
            
            var spawner = parentObject.AddComponent<ObjectSpawner>();
            spawner.showHideDestroyed = showHideDestroyed;
            spawner.storage = storage;
            spawner.color = color;
            spawner.gazeObject = gazeObject;
            spawner.Parent = parentObject.transform;
            spawner.instanceObject = instanceObject;
            spawner.timeValueChanged = timeValueChanged;
            spawner.Game = game;
            spawner.SpawnObjects(gameWorldPositions);
        }

        private void CreateTimeframeObjects(GameObject timeframeObjects, Game game, List<List<Vector3>> gameWorldPositions, GameVisibilityControl timeframeVisibilityControl,Color color)
        {
            var parentObject = new GameObject
            {
                transform =
                {
                    parent = timeframeObjects.transform
                },
                name = "Timeframe " + game.Id
            };
            parentObject.SetActive(showHideTimelineTimeframe.showTimeframe);
            timeframeVisibilityControl.AddGame(game.Id,parentObject);
            timeframeVisibilityControl.TimeType = true;
            
            var material = new Material(shader)
            {
                enableInstancing = true,
                color = color
            };
            foreach (var obj in gameWorldPositions)
            {
                var pointsParent = new GameObject
                {
                    transform =
                    {
                        parent = parentObject.transform
                    }
                };
                foreach (var point in obj)
                {
                    var newPoint = Instantiate(instanceObject,pointsParent.transform);
                    var spawnedObject = StaticObjectSpawner.SpawnStaticObject(point,material,newPoint);
                    //timeframeVisibilityController.AddPoint(,newPoint);
                }
            }
        }
    }
}
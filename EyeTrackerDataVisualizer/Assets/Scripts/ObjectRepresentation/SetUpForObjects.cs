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
            
            var listener = timelineObjects.AddComponent<GameEventListener>();
            var gameEvent = new UnityEvent();
            gameEvent.AddListener(timelineVisibilityControl.ShowHideGame);
            listener.gameEvent = showHideGame;
            listener.RegisterListener();
            listener.response = gameEvent;
            
            listener = timeframeObjects.AddComponent<GameEventListener>();
            gameEvent = new UnityEvent();
            gameEvent.AddListener(timeframeVisibilityControl.ShowHideGame);
            listener.gameEvent = showHideGame;
            listener.RegisterListener();
            listener.response = gameEvent;
            
            var i = 0;
            for (var j = 0; j < storage.GameList.Count; j++)
            {
                var game = storage.GameList[j];
                var color = new Color(0,0,0,1);
                if (i < storage.Colors.Count)
                {
                    color = storage.Colors[i];
                }

                var listItem = Instantiate(ScrollViewItem, gameOverviewContent.transform);
                var image = listItem.GetComponentInChildren<Image>();
                image.color = color;
                listItem.SetActive(true);
                var gameVisibilityToggle = listItem.AddComponent<GameVisibilityToggle>();
                gameVisibilityToggle.gameId = game.Id;
                gameVisibilityToggle.gameIds = gameIds;
                gameVisibilityToggle.AddGameToDictionary();
                var toggle = listItem.GetComponentInChildren<Toggle>();
                toggle.onValueChanged.AddListener(gameVisibilityToggle.AlterStateInStorage);

                var objectWorldPositions = new List<List<Vector3>>();
                foreach (var obj in game.Objects)
                {
                    var pointsInWorldCoords = new List<Vector3>();
                    foreach (var point in obj.Points)
                    {
                        var pos = new Vector3(point.PosX, point.PosY, point.PosZ +300);
                        var worldPos = storage.MainCamera.ScreenToWorldPoint(pos);
                        pointsInWorldCoords.Add(worldPos);
                    }
                    objectWorldPositions.Add(pointsInWorldCoords);
                }
                
                var gazeWorldPositions = new List<Vector3>();
                foreach (var gazePoint in storage.SensorData[i])
                {
                    var pos = new Vector3(gazePoint.PosX,gazePoint.PosY,200);
                    var worldPos = storage.MainCamera.ScreenToWorldPoint(pos);
                    gazeWorldPositions.Add(worldPos);
                }
                
                CreateTimelineObjects(timelineObjects,timelineVisibilityControl, color, game, objectWorldPositions, gazeWorldPositions);
                CreateTimeframeObjects(timeframeObjects,game,objectWorldPositions,timeframeVisibilityControl,color,gazeWorldPositions, j);
                i++;
            }
        }

        private void CreateTimelineObjects(GameObject timelineObjects, GameVisibilityControl timelineVisibilityControl, Color color, Game game, List<List<Vector3>> gameWorldPositions, List<Vector3> gazeWorldPositions)
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
            spawner.instanceObject = instanceObject;
            spawner.timeValueChanged = timeValueChanged;
            spawner.Game = game;
            spawner.SpawnObjects(gameWorldPositions,gazeWorldPositions);
        }

        private void CreateTimeframeObjects(GameObject timeframeObjects, Game game, List<List<Vector3>> gameWorldPositions, GameVisibilityControl timeframeVisibilityControl,Color color, List<Vector3> gazeWorldPositions, int gameNumber)
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
            for (var i = 0; i<gameWorldPositions.Count;i++)
            {
                var pointsParent = new GameObject
                {
                    transform =
                    {
                        parent = parentObject.transform
                    }
                };
                for (var j = 0; j < gameWorldPositions[i].Count; j++)
                {
                    var newPoint = Instantiate(instanceObject,pointsParent.transform);
                    StaticObjectSetUp.SetUpStaticObjects(gameWorldPositions[i][j],material,newPoint);
                    var startEnd = storage.StartAndEndPoints[game.Objects[i].Name];
                    var positionInList = startEnd.Item1 + j;
                    if(positionInList > startEnd.Item2) continue;
                    timeframeVisibilityController.AddObjectPoint(positionInList,newPoint);
                }
            }

            var timestamp = storage.SensorData[gameNumber][0].Timestamp;
            var tuple = storage.StartEndGazePoints[timestamp];
            var start = tuple.Item1;
            var end = tuple.Item2;
            for (var i = 0; i < gazeWorldPositions.Count; i++)
            {
                var newPoint = Instantiate(gazeObject, parentObject.transform);
                StaticObjectSetUp.SetUpStaticObjects(gazeWorldPositions[i],material,newPoint);
                var positionInList = start + i;
                if(positionInList > end) continue;
                timeframeVisibilityController.AddGazePoint(positionInList,newPoint);
            }

        }
    }
}
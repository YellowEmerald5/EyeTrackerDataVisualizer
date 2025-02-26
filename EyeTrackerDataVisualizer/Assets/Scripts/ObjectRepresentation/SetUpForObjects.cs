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
        /// Sets up game event listeners and prepares the data for game objects and gaze objects
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

            var windowHeight = Screen.height;
            var windowWidth = Screen.width;
            
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

                var gameWindowHeight = game.WindowHeight;
                var gameWindowWidth = game.WindowWidth;
                
                float ratioHeight;
                float ratioWidth;
                
                if (windowHeight <= gameWindowHeight)
                {
                    ratioHeight = windowHeight / gameWindowHeight;
                }
                else
                {
                    ratioHeight = gameWindowHeight / windowHeight;
                }
                
                if (windowWidth <= gameWindowWidth)
                {
                    ratioWidth = windowWidth / gameWindowWidth;
                }
                else
                {
                    ratioWidth = gameWindowWidth / windowWidth;
                }

                var objectWorldPositions = new List<List<Vector3>>();
                foreach (var obj in game.Objects)
                {
                    var pointsInWorldCoords = new List<Vector3>();
                    foreach (var point in obj.Points)
                    {
                        var pos = new Vector3(point.PosX*ratioWidth, point.PosY*ratioHeight, point.PosZ +300);
                        var worldPos = storage.MainCamera.ScreenToWorldPoint(pos);
                        pointsInWorldCoords.Add(worldPos);
                    }
                    objectWorldPositions.Add(pointsInWorldCoords);
                }
                
                var gazeWorldPositions = new List<Vector3>();
                foreach (var gazePoint in storage.SensorData[i])
                {
                    var pos = new Vector3(gazePoint.PosX*ratioWidth,gazePoint.PosY*ratioHeight,200);
                    var worldPos = storage.MainCamera.ScreenToWorldPoint(pos);
                    gazeWorldPositions.Add(worldPos);
                }
                
                CreateTimelineObjects(timelineObjects,timelineVisibilityControl, color, game, objectWorldPositions, gazeWorldPositions);
                CreateTimeframeObjects(timeframeObjects,game,objectWorldPositions,timeframeVisibilityControl,color,gazeWorldPositions, j);
                i++;
            }
        }

        /// <summary>
        /// Prepares the timeline objects, sets up script for hiding them and starts spawning objects
        /// </summary>
        /// <param name="timelineObjects">Parent object for all timeline related objects</param>
        /// <param name="timelineVisibilityControl">Script for controlling the visibility of all timeline objects when changing representation type</param>
        /// <param name="color">The color the objects should have based on the game they belong to</param>
        /// <param name="game">Data from the game containing the objects</param>
        /// <param name="gameWorldPositions">Positions of the objects in game converted to world positions</param>
        /// <param name="gazeWorldPositions">Positions of the gaze data related to the game converted to world positions</param>
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

        /// <summary>
        /// Prepares the timeframe objects, sets up script for hiding them and spawns the static objects
        /// </summary>
        /// <param name="timeframeObjects">Parent object for all timeframe related objects</param>
        /// <param name="game">Data from the game containing the objects</param>
        /// <param name="gameWorldPositions">Positions of the objects in game converted to world positions</param>
        /// <param name="timeframeVisibilityControl">Script for controlling the visibility of all timeframe objects when changing representation type</param>
        /// <param name="color">The color the objects should have based on the game they belong to</param>
        /// <param name="gazeWorldPositions">Positions of the gaze data related to the game converted to world positions</param>
        /// <param name="gameNumber">What game is currently being represented. Used for finding the related gaze points</param>
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

            if (storage.SensorData[gameNumber].Count > 0)
            {
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
}
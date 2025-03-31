using System;
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
        [SerializeField] private GameEvent timeValueChanged;
        [SerializeField] private GameEvent showHideDestroyed;
        [SerializeField] private GameEvent showHideGame;
        [SerializeField] private GameEvent changedRepresentation;
        [SerializeField] private Shader shader;
        [SerializeField] private GameIdsForVisibility gameIds;
        [SerializeField] private ShowHideTimelineTimeframe showHideTimelineTimeframe;
        [SerializeField] private TimeframeVisibilityController timeframeVisibilityController;
        [SerializeField] private GameObject UiObjectsParent;
        [SerializeField] private GameObject imageObject;
        [SerializeField] private GameObject imageGazeObject;

        [SerializeField] private GameIdForVisibilityChange gameIdForVisibility;
        //[SerializeField] private Sprites Sprites;

        private List<Tuple<float,float>> screenRatios = new List<Tuple<float,float>>();
        

        /// <summary>
        /// Sets up game event listeners and prepares the data for game objects and gaze objects
        /// </summary>
        private void Start()
        {
            GameObject timelineObjects;
            GameObject timeframeObjects;

            PrepareScreenRatios();

            if (!storage.twoD)
            {
                var parentController = new GameObject
                {
                    name = "ObjectController"
                };
                timelineObjects = new GameObject
                {
                    name = "TimelineObjects"
                };
                timeframeObjects = new GameObject
                {
                    name = "TimeframeObjects"
                };

                timelineObjects.transform.parent = parentController.transform;
                timeframeObjects.transform.parent = parentController.transform;
            }
            else
            {
                timelineObjects = new GameObject
                {
                    name = "TimelineObjects"
                };
                timeframeObjects = new GameObject
                {
                    name = "TimeframeObjects"
                };

                timelineObjects.transform.parent = UiObjectsParent.transform;
                timeframeObjects.transform.parent = UiObjectsParent.transform;

                timelineObjects.transform.localPosition = new Vector3(0, 0, 0);
                timeframeObjects.transform.localPosition = new Vector3(0, 0, 0);
            }

            var timelineVisibilityControl = timelineObjects.AddComponent<GameVisibilityControl>();
            var timeframeVisibilityControl = timeframeObjects.AddComponent<GameVisibilityControl>();

            timelineVisibilityControl.gameIdToVisibility = gameIds;
            timeframeVisibilityControl.gameIdToVisibility = gameIds;
            timelineVisibilityControl.keyStorage = gameIdForVisibility;
            timeframeVisibilityControl.keyStorage = gameIdForVisibility;

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

            if (!storage.twoD)
            {
                ThreeDSetUp(timelineObjects,timeframeObjects,timelineVisibilityControl,timeframeVisibilityControl);
            }
            else
            {
                TwoDSetUp(timelineObjects, timeframeObjects, timelineVisibilityControl, timeframeVisibilityControl);
            }
        }

        private void PrepareScreenRatios()
        {
            var windowHeight = Screen.height;
            var windowWidth = Screen.width;

            foreach (var game in storage.GameList) {
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
                screenRatios.Add(new Tuple<float,float>(ratioWidth,ratioHeight));
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
        private void CreateTimelineObjects(GameObject obj,GameObject timelineObjects, GameVisibilityControl timelineVisibilityControl, Color color, Game game, List<List<Vector3>> gameWorldPositions, List<Vector3> gazeWorldPositions, Tuple<string,List<Sprite>> gameSpriteTuple)
        {
            var parentObject = new GameObject
            {
                transform =
                {
                    parent = timelineObjects.transform
                }
            };
            if (storage.twoD)
            {
                parentObject.transform.localPosition = new Vector3();
            }
            parentObject.name = "TimeLine " + game.Id;
            timelineVisibilityControl.AddGame(game.Id,parentObject);
            timelineVisibilityControl.TimeType = false;
            
            var spawner = parentObject.AddComponent<ObjectSpawner>();
            spawner.showHideDestroyed = showHideDestroyed;
            spawner.storage = storage;
            spawner.color = color;
            spawner.gazeObject = imageGazeObject;
            spawner.instanceObject = obj;
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
        private void CreateTimeframeObjects(GameObject timeframeObjects, Game game, List<List<Vector3>> gameWorldPositions, GameVisibilityControl timeframeVisibilityControl,Color color, List<Vector3> gazeWorldPositions, int gameNumber, Tuple<string, List<Sprite>> gameSpriteTuple)
        {
            var parentObject = new GameObject
            {
                transform =
                {
                    parent = timeframeObjects.transform
                },
                name = "Timeframe " + game.Id
            };
            if (storage.twoD)
            {
                parentObject.transform.localPosition = new Vector3();
            }

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

                pointsParent.transform.localPosition = new Vector3();

                for (var j = 0; j < gameWorldPositions[i].Count; j++)
                {
                    var newPoint = Instantiate(imageObject, pointsParent.transform);
                    if (!storage.twoD)
                    {
                        newPoint = Instantiate(instanceObject, pointsParent.transform);
                        
                    }
                    
                    StaticObjectSetUp.SetUpStaticObjects(gameWorldPositions[i][j], material, newPoint, storage.twoD);
                    var startEnd = storage.StartAndEndPoints[game.Objects[i].Id];
                    var positionInList = startEnd.Item1 + j;
                    if (positionInList > startEnd.Item2) continue;
                    timeframeVisibilityController.AddObjectPoint(positionInList, newPoint);
                    
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
                    var newPoint = Instantiate(imageGazeObject, UiObjectsParent.transform);
                    StaticObjectSetUp.SetUpStaticGazeObjects(gazeWorldPositions[i],material,newPoint, storage.twoD);
                    var positionInList = start + i;
                    if(positionInList > end) continue;
                    timeframeVisibilityController.AddGazePoint(positionInList,newPoint);
                }
            }

        }

        private void ThreeDSetUp(GameObject timelineObjects, GameObject timeframeObjects, GameVisibilityControl timelineVisibilityControl, GameVisibilityControl timeframeVisibilityControl)
        {
            
            var i = 0;
            for (var j = 0; j < storage.GameList.Count; j++)
            {
                var game = storage.GameList[j];
                var color = new Color(0,0,0,1);
                if (i < storage.Colors.Count)
                {
                    color = storage.Colors[i];
                }
                
                SetUpListItem(game,color);

                var objectWorldPositions = new List<List<Vector3>>();
                foreach (var obj in game.Objects)
                {
                    var pointsInWorldCoords = new List<Vector3>();
                    foreach (var point in obj.Points)
                    {
                        var pos = new Vector3(point.PosX * screenRatios[j].Item1, point.PosY* screenRatios[j].Item2, point.PosZ + 400);
                        var worldPos = storage.MainCamera.ScreenToWorldPoint(pos);
                        pointsInWorldCoords.Add(worldPos);
                    }
                    objectWorldPositions.Add(pointsInWorldCoords);
                }

                var gazeWorldPositions = SetUpGazePoints(i, screenRatios[j].Item1, screenRatios[j].Item2);

                CreateTimelineObjects(instanceObject,timelineObjects,timelineVisibilityControl, color, game, objectWorldPositions, gazeWorldPositions,null);
                CreateTimeframeObjects(timeframeObjects,game,objectWorldPositions,timeframeVisibilityControl,color,gazeWorldPositions, j,null);
                i++;
            }
        }

        private void TwoDSetUp(GameObject timelineObjects, GameObject timeframeObjects, GameVisibilityControl timelineVisibilityControl, GameVisibilityControl timeframeVisibilityControl)
        {
            
            var i = 0;
            for (var j = 0; j < storage.GameList.Count; j++)
            {
                var game = storage.GameList[j];
                var color = new Color(0,0,0,1);
                if (i < storage.Colors.Count)
                {
                    color = storage.Colors[i];
                }

                SetUpListItem(game,color);

                var objectWorldPositions = new List<List<Vector3>>();
                foreach (var obj in game.Objects)
                {
                    var pointsInWorldCoords = new List<Vector3>();
                    foreach (var point in obj.Points)
                    {
                        var pos = new Vector3(point.PosX* screenRatios[j].Item1, point.PosY* screenRatios[j].Item2, point.PosZ + 400);
                        var worldPos = storage.MainCamera.ScreenToWorldPoint(pos);
                        pointsInWorldCoords.Add(worldPos);
                    }
                    objectWorldPositions.Add(pointsInWorldCoords);
                }
                //var sprites = Sprites.GetSpritesFromGameId(game.Name);
                var gazeWorldPositions = SetUpGazePoints(i, screenRatios[j].Item1, screenRatios[j].Item2);

                CreateTimelineObjects(imageObject, timelineObjects, timelineVisibilityControl, color, game, objectWorldPositions, gazeWorldPositions, null); //new Tuple<string,List<Sprite>>(game.Name,sprites));
                CreateTimeframeObjects(timeframeObjects, game, objectWorldPositions, timeframeVisibilityControl, color, gazeWorldPositions, j, null);//new Tuple<string, List<Sprite>>(game.Name, sprites));
                i++;
            }
        }

        private List<Vector3> SetUpGazePoints(int gamePosition,float ratioWidth,float ratioHeight)
        {
            var gazeWorldPositions = new List<Vector3>();
            foreach (var gazePoint in storage.SensorData[gamePosition])
            {
                var pos = new Vector3(gazePoint.PosX * ratioWidth, gazePoint.PosY * ratioHeight, 400);
                var worldPos = storage.MainCamera.ScreenToWorldPoint(pos);
                gazeWorldPositions.Add(worldPos);
            }
            return gazeWorldPositions;
        }

        private void SetUpListItem(Game game,Color color)
        {
            var listItem = Instantiate(ScrollViewItem, gameOverviewContent.transform);
            var image = listItem.GetComponentInChildren<Image>();
            image.color = color;
            listItem.SetActive(true);
            var gameVisibilityToggle = listItem.AddComponent<GameVisibilityToggle>();
            gameVisibilityToggle.gameIds = gameIds;
            gameVisibilityToggle.game = game;
            gameVisibilityToggle.valueChanged = showHideGame;
            gameVisibilityToggle.AddGameToDictionary();
            gameVisibilityToggle.BeginSetUp();
            var toggle = listItem.GetComponentInChildren<Toggle>();
            toggle.onValueChanged.AddListener(gameVisibilityToggle.AlterStateInStorage);
        }
    }
}
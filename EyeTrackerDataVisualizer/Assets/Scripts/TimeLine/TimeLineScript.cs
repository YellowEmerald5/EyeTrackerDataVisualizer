using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TimeLine
{
    public class TimeLineScript : MonoBehaviour
    {
        public bool play = true;
        [SerializeField] public Slider slider;
        [SerializeField] private StorageSO storage;
        [SerializeField] public UnityEvent valueChanged;
        private int i = 0;
        private int previousSpeed;
    
        /// <summary>
        /// Sets the max value of the slider and gets the main camera used for object placements;
        /// </summary>
        void Start()
        {
            storage.MainCamera = Camera.main;
            slider.maxValue = storage.TotalTimestampEntries;
        }

        /// <summary>
        /// Increments the sliders value and updates the timestamp in the storage.
        /// </summary>
        private void ProgressTimeline()
        {
            print(play);
            if (!play || slider.value+1 >= slider.maxValue) return;
            slider.value++;
            storage.CurrentTimestamp = slider.value;
        }

        /// <summary>
        /// Pauses and restarts the progression of the slider
        /// </summary>
        public void ChangePlayState()
        {
            play = !play;
        }

        /// <summary>
        /// Sets the current timestamp to the value of the slider and informs the game objects of the change
        /// </summary>
        /// <param name="value">Value of the slider</param>
        public void ChangedValue(float value)
        {
            storage.CurrentTimestamp = value;
            valueChanged.Invoke();
        }

        /// <summary>
        /// Updates every frame and progresses the timeline. Delayed by the the chosen replay speed
        /// </summary>
        private void FixedUpdate()
        {
            if (storage.SpeedChanged)
            {
                i = 0;
                storage.SpeedChanged = false;
            }
            if (i > storage.TimeDelay) return;
            if (i == storage.TimeDelay)
            {
                ProgressTimeline();
                i = 0;
            }
            if (i < storage.TimeDelay && storage.TimeDelay > 0)
            {
                i++;
            }

        }
    }
}

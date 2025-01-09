using UnityEngine;

namespace Unused
{
    public class ReactiveScript : MonoBehaviour
    {
        /// <summary>
        /// Prints a message in the console when invoked. Used to test if GameEvents are activating as they should
        /// </summary>
        public void GameEventInvoked()
        {
            print("Game event was invoked and received");
        }
    }
}
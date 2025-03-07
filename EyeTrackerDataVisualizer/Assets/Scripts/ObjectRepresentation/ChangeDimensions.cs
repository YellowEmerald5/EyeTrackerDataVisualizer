using ScriptableObjects;
using UnityEngine;

namespace ObjectRepresentation
{
    public class ChangeDimensions : MonoBehaviour
    {
        [SerializeField] private StorageSO storage;
        [SerializeField] private bool twoDimensional;

        public void SetDimension()
        {
            storage.twoD = twoDimensional;
        }
    }
}
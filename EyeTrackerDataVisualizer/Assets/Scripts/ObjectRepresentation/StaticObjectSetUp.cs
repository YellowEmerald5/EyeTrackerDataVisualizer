using UnityEngine;

namespace ObjectRepresentation
{
    public static class StaticObjectSetUp
    {
        
        /// <summary>
        /// Sets the material and position of an object
        /// </summary>
        /// <param name="position">The position the object should be placed at</param>
        /// <param name="material">The material the object should have</param>
        /// <param name="obj">The object to make changes to</param>
        public static void SetUpStaticObjects(Vector3 position, Material material, GameObject obj)
        {
            obj.GetComponent<MeshRenderer>().material = material;
            obj.transform.position = position;
        }
    }
}
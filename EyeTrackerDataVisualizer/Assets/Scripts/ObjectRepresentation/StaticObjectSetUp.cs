using Microsoft.EntityFrameworkCore.Query;
using UnityEngine;
using UnityEngine.UI;

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
        public static void SetUpStaticObjects(Vector3 position, Material material, GameObject obj, bool twoD)
        {
            if (!twoD)
            {
                obj.GetComponent<MeshRenderer>().material = material;
                obj.transform.position = position;
            }
            else
            {
                obj.GetComponent<Image>().color = material.color;
                obj.transform.localPosition = position;
            }
            
        }

        public static void SetUpStaticGazeObjects(Vector3 position, Material material, GameObject obj, bool twoD)
        {
            obj.GetComponent<Image>().color = material.color;
            if (twoD)
            {
                obj.transform.localPosition = position;
            }
            else
            {
                obj.transform.position = position;
            }
            
        }
    }
}
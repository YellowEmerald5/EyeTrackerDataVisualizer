using UnityEngine;

namespace ObjectRepresentation
{
    public static class StaticObjectSetUp
    {
        public static void SetUpStaticObjects(Vector3 position, Material material, GameObject obj)
        {
            obj.GetComponent<MeshRenderer>().material = material;
            obj.transform.position = position;
        }
    }
}
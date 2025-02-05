using UnityEngine;

namespace ObjectRepresentation
{
    public static class StaticObjectSpawner
    {
        public static GameObject SpawnStaticObject(Vector3 position, Material material, GameObject obj)
        {
            obj.GetComponent<MeshRenderer>().material = material;
            obj.transform.position = position;
            return obj;
        }
    }
}
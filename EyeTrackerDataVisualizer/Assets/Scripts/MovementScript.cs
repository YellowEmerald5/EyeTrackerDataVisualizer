using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Objects;

public class MovementScript : MonoBehaviour
{
    //The object should be spawned by another script and this script should be added to it
    //Need to make changes for checking if the object should be visible
    private List<Vector3> _positions;
    private List<long> _pointIDs;
    private int _currentPosition;
    private Renderer _renderer;
    private ObjectInGame Object;

    /// <summary>
    /// Takes the object data and prepares it for replay
    /// </summary>
    public void SetUpObjectRetracing(ObjectInGame obj)
    {
        Object = obj;
        _renderer = GetComponent<MeshRenderer>();
        foreach (var point in obj.Points)
        {
            var vector = new Vector3(point.PosX,point.PosY,point.PosZ);
            _pointIDs.Add(point.Id);
            _positions.Add(vector);
        }

        transform.position = _positions[_currentPosition];
        ResizeObject();
    }

    public void MoveObjectForwards()
    {
        _currentPosition++;
        if (_currentPosition < _positions.Count-1)
        {
            transform.position = _positions[_currentPosition];
            ResizeObject();
            _renderer.enabled = true;
        }
        else
        {
            _renderer.enabled = false;
        }
        
    }
    
    public void MoveObjectBackwards()
    {
        _currentPosition--;
        if (_currentPosition >= 0)
        {
            transform.position = _positions[_currentPosition];
            ResizeObject();
            _renderer.enabled = true;
        }
        else
        {
            _renderer.enabled = false;
        }
        
    }

    public void MoveObjectToTimestamp(long timestamp)
    {
        if (_pointIDs.Contains(timestamp))
        {
            var pos = _pointIDs.FindIndex(i => i == timestamp);
            transform.position = _positions[pos];
            ResizeObject();
            _renderer.enabled = true;
        }
        else
        {
            _renderer.enabled = false;
        }
    }

    private void ResizeObject()
    {
        var timestamp = _pointIDs[_currentPosition];
        //Change when structure and id type in objecttracking is known
        //var aoisize = Object.Aois.AoiSizes.Where(a => a.Id == timestamp);
        
    }
}

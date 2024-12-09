using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using Objects;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    //The object should be spawned by another script and this script should be added to it
    //Need to make changes for checking if the object should be visible
    private List<Point> _positions;
    private int _currentPosition;
    private Renderer _renderer;
    private ObjectInGame Object;
    //public StorageSO Storage;

    /// <summary>
    /// Takes the object data and prepares it for replay
    /// </summary>
    public void SetUpObjectRetracing(ObjectInGame obj)
    {
        Object = obj;
        _renderer = GetComponent<MeshRenderer>();
        foreach (var point in obj.Points)
        {
            _positions.Add(point);
        }

        var p = _positions[_currentPosition];
        transform.position = new Vector3(p.PosX,p.PosY,p.PosZ);
        ResizeObject(_currentPosition);
    }

    public void MoveObjectForwards()
    {
        _currentPosition++;
        if (_currentPosition < _positions.Count-1)
        {
            var point = _positions[_currentPosition];
            transform.position = new Vector3(point.PosX,point.PosY,point.PosZ);
            ResizeObject(_currentPosition);
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
            var point = _positions[_currentPosition];
            transform.position = new Vector3(point.PosX,point.PosY,point.PosZ);
            ResizeObject(_currentPosition);
            _renderer.enabled = true;
        }
        else
        {
            _renderer.enabled = false;
        }
    }

    public void MoveObjectToTimestamp()
    {
        /*var timestamp = Storage.timestamp;
        if (_positions.FirstOrDefault(p => p.Time  == timestamp) != null)
        {
            var pos = _positions.FindIndex(i => i.Time == timestamp);
            var point = _positions[pos];
            transform.position = new Vector3(point.PosX,point.PosY,point.PosZ);
            ResizeObject(pos);
            _renderer.enabled = true;
        }
        else
        {
            _renderer.enabled = false;
        }*/
    }

    private void ResizeObject(int position)
    {
        if (position < 0 || position > Object.Aoi.Sizes.Count) return;
        var aoisize = Object.Aoi.Sizes[position];
        //Height and width are screenspace based, might not be the correct sizes here
        transform.localScale = new Vector3(aoisize.Height,aoisize.Width,0);
    }
}

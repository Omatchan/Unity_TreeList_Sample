using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private Stack<MonoBehaviour> objects = new Stack<MonoBehaviour>();
    private MonoBehaviour prefab;

    public ObjectPool(MonoBehaviour prefab)
    {
        this.prefab = prefab;
    }

    public T GetObject<T>() where T : MonoBehaviour
    {
        if (objects.Count == 0)
        {
            var newObj = Object.Instantiate(prefab);
            newObj.gameObject.SetActive(true);
            objects.Push(newObj);
        }
        return objects.Pop() as T;
    }

    public void ReturnObject(MonoBehaviour obj)
    {
        obj.gameObject.SetActive(false);
        objects.Push(obj);
    }
}

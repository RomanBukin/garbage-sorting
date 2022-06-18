using System;
using System.Collections.Generic;

public class CrossSceneContainer
{
    private readonly Dictionary<Type, object> _data = new Dictionary<Type, object>(); 

    public void Put(object obj)
    {
        _data[obj.GetType()] = obj;
    }

    public T Get<T>()
    {
        return (T) _data[typeof(T)];
    }

    public void Remove<T>()
    {
        _data.Remove(typeof(T));
    }

    public T Pop<T>()
    {
        var obj = Get<T>();
        Remove<T>();
        return obj;
    }
}
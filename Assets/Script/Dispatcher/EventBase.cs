using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class EventBase<T, P, X> where T : new() where P : class
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }

    private readonly ConcurrentDictionary<X, List<Action<P>>> dic = new ConcurrentDictionary<X, List<Action<P>>>();

    public void AddEventListener(X key, Action<P> handle)
    {
        dic.AddOrUpdate(key,
            k => new List<Action<P>> { handle },
            (k, actions) =>
            {
                actions.Add(handle);
                return actions;
            });
    }

    public void RemoveEventListener(X key, Action<P> handle)
    {
        if (dic.TryGetValue(key, out List<Action<P>> actions))
        {
            actions.Remove(handle);
            if (actions.Count == 0)
            {
                dic.TryRemove(key, out _);
            }
        }
    }

    public void Dispatch(X key, P p)
    {
        if (dic.TryGetValue(key, out List<Action<P>> actions))
        {
            foreach (var action in actions)
            {
                action?.Invoke(p);
            }
        }
    }

    public void Dispatch(X key)
    {
        Dispatch(key, null);
    }
}

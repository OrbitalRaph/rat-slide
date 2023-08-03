using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver, IEnumerable<KeyValuePair<TKey, TValue>>
{
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();

    public TValue this[TKey key]
    {
        get
        {
            int index = keys.IndexOf(key);
            if (index >= 0 && index < values.Count)
            {
                return values[index];
            }
            else
            {
                Debug.LogError($"Key '{key}' not found in the SerializableDictionary.");
                return default;
            }
        }
        set
        {
            int index = keys.IndexOf(key);
            if (index >= 0 && index < values.Count)
            {
                values[index] = value;
            }
            else
            {
                Debug.LogError($"Key '{key}' not found in the SerializableDictionary.");
            }
        }
    }

    public int Count => keys.Count;

    public void Add(TKey key, TValue value)
    {
        if (!keys.Contains(key))
        {
            keys.Add(key);
            values.Add(value);
        }
        else
        {
            Debug.LogError($"Key '{key}' already exists in the SerializableDictionary.");
        }
    }

    public bool Remove(TKey key)
    {
        int index = keys.IndexOf(key);
        if (index >= 0 && index < values.Count)
        {
            keys.RemoveAt(index);
            values.RemoveAt(index);
            return true;
        }
        else
        {
            Debug.LogWarning($"Key '{key}' not found in the SerializableDictionary.");
            return false;
        }
    }

    public bool ContainsKey(TKey key)
    {
        return keys.Contains(key);
    }

    public void Clear()
    {
        keys.Clear();
        values.Clear();
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        for (int i = 0; i < keys.Count; i++)
        {
            yield return new KeyValuePair<TKey, TValue>(keys[i], values[i]);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void OnBeforeSerialize()
    { 
    }

    public void OnAfterDeserialize()
    {
        if (keys.Count != values.Count)
        {
            Debug.LogWarning("The number of keys and values in the SerializableDictionary do not match.");
        }
    }
}
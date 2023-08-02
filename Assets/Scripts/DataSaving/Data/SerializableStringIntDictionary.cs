using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SerializableStringIntDictionary : IEnumerable<KeyValuePair<string, int>>
{
    public List<string> keys = new List<string>();
    public List<int> values = new List<int>();

    public int this[string key]
    {
        get
        {
            if (keys.Contains(key))
            {
                return values[keys.IndexOf(key)];
            }
            else
            {
                return 0;
            }
        }
        set
        {
            if (keys.Contains(key))
            {
                values[keys.IndexOf(key)] = value;
            }
            else
            {
                keys.Add(key);
                values.Add(value);
            }
        }
    }

    public IEnumerator<KeyValuePair<string, int>> GetEnumerator()
    {
        for (int i = 0; i < keys.Count; i++)
        {
            yield return new KeyValuePair<string, int>(keys[i], values[i]);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Clear()
    {
        keys.Clear();
        values.Clear();
    }

    public bool ContainsKey(string key)
    {
        return keys.Contains(key);
    }
}


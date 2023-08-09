using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Dictionnaire sérialisable.
/// Permet de sauvegarder un dictionnaire dans un fichier.
/// Permet de modifier les valeurs du dictionnaire dans l'inspecteur.
/// </summary>
[System.Serializable]
public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver, IEnumerable<KeyValuePair<TKey, TValue>>
{
    [SerializeField] private List<TKey> keys = new();
    [SerializeField] private List<TValue> values = new();

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
                Debug.LogError($"La clé '{key}' n'a pas été trouvée dans le SerializableDictionary.");
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
                Debug.LogError($"La clé '{key}' n'a pas été trouvée dans le SerializableDictionary.");
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
            Debug.LogError($"La clé '{key}' existe déjà dans le SerializableDictionary.");
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
            Debug.LogWarning($"La clé'{key}' n'a pas été trouvée dans le SerializableDictionary.");
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
            Debug.LogWarning("Attention, le nombre de clés et de valeurs n'est pas le même dans le SerializableDictionary. Les données seront perdues.");
        }
    }
}
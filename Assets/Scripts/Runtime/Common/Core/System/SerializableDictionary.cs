using UnityEngine;
using System;
using System.Collections.Generic;

// Since Unity doesn't know how to serialize a Dictionary, we will reconstruct it from saved Lists.
// Reference: http://docs.unity3d.com/ScriptReference/ISerializationCallbackReceiver.OnBeforeSerialize.html
// Usage: To create a serializable dictionary, put the following in your class:    
//      [Serializable]
//      public class MyDictionary : SerializableDictionary<MyKeyType, MyValueType> { }
[Serializable]
public abstract class SerializableDictionary<Tkey, TValue> : ISerializationCallbackReceiver
{
	[SerializeField]
	private List<Tkey> m_keys = new List<Tkey>();
	[SerializeField]
	private List<TValue> m_values = new List<TValue>();

	private Dictionary<Tkey, TValue> m_dictionary = new Dictionary<Tkey, TValue>();

	public Dictionary<Tkey, TValue> Dictionary
	{
		get
		{
			return m_dictionary;
		}
	}

	public void OnBeforeSerialize()
	{
		m_keys.Clear();
		m_values.Clear();
		foreach (var kvp in m_dictionary)
		{
			m_keys.Add(kvp.Key);
			m_values.Add(kvp.Value);
		}
	}

	public void OnAfterDeserialize()
	{
		m_dictionary = new Dictionary<Tkey, TValue>();
		for (int i = 0; i != Math.Min(m_keys.Count, m_values.Count); i++)
		{
			m_dictionary.Add(m_keys[i], m_values[i]);
		}
	}
}
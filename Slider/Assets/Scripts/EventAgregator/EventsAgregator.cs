using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.EventAgregators
{
	public class EventsAgregator : IEventsAgregator
	{
		public Dictionary<Type, Delegate> Subscribers => delegates;
		
		private readonly Dictionary<Type, Delegate> delegates = new Dictionary<Type, Delegate>();

		public void AddListener<T>(Action<T> action)
		{
			if (!delegates.ContainsKey(typeof(T)))
				delegates.Add(typeof(T), null);
			delegates[typeof(T)] = Delegate.Combine(delegates[typeof(T)], action);
		}

		public void RemoveListener<T>(Action<T> action)
		{
			if (delegates.ContainsKey(typeof(T)))
				delegates[typeof(T)] = Delegate.Remove(delegates[typeof(T)], action);
		}

		public void Clear()
		{
			Subscribers.Clear();
		}

		public void Invoke<T>(T obj)
		{
			if (!delegates.ContainsKey(typeof(T)))
			{
				RegisterType<T>();
			}

			var handler = (Action<T>)delegates[typeof(T)];
			if (handler != null) handler.Invoke(obj);
		}

		public void RegisterType<T>()
		{
			if (!delegates.ContainsKey(typeof(T))) delegates[typeof(T)] = null;
		}
	}

}
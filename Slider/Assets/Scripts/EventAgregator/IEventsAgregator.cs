using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.EventAgregators
{
	public interface IEventsAgregator
	{
		Dictionary<Type, Delegate> Subscribers { get; }

		void RegisterType<T>();

		void Invoke<T>(T obj);

		void AddListener<T>(Action<T> action);

		void RemoveListener<T>(Action<T> action);
	}
}
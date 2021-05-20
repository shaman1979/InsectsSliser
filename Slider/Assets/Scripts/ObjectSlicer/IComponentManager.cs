using UnityEngine;

namespace BzKovSoft.ObjectSlicer
{
	/// <summary>
	/// Manage components for sliced objects
	/// </summary>
	public interface IComponentManager
	{
		bool Success { get; }

		/// <summary>
		/// Asynchronous call
		/// </summary>
		void OnSlicedWorkerThread(SliceTryItem[] items);
	}
}

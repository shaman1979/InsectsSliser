using LightDev;
using UnityEngine;

namespace BzKovSoft.ObjectSlicerSamples
{
	[RequireComponent(typeof(MeshCollider), typeof(Rigidbody))]
	public class Knife : MonoBehaviour
	{
		public int SliceID { get; private set; }

		[SerializeField]
		private Vector3 _origin = Vector3.down;

		[SerializeField]
		private Vector3 _direction = Vector3.up;

		private Vector3 _prevPos;
		private Vector3 _pos;
		public Vector3 BladeDirection => transform.rotation * _direction.normalized;
		public Vector3 MoveDirection => (_pos - _prevPos).normalized;

		public Vector3 Origin
		{
			get
			{
				Vector3 localShifted = transform.InverseTransformPoint(transform.position) + _origin;
				return transform.TransformPoint(localShifted);
			}
		}

		public void BeginNewSlice()
		{
			SliceID = Random.Range(int.MinValue, int.MaxValue);
		}

        private void OnEnable()
        {
			Events.PointerUp += BeginNewSlice;
        }

        private void OnDisable()
        {
			Events.PointerUp -= BeginNewSlice;
		}

        private void Update()
		{
			_prevPos = _pos;
			_pos = transform.position;
		}
	}
}

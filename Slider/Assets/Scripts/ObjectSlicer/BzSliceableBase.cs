﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Threading;
using BzKovSoft.ObjectSlicer;
using BzKovSoft.ObjectSlicer.EventHandlers;
using UnityEngine.Profiling;
using LightDev;

namespace BzKovSoft.ObjectSlicer
{
    /// <summary>
    /// Base class for sliceable object
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class BzSliceableBase : MonoBehaviour, IBzSliceableAsync
    {
        [HideInInspector] [SerializeField] int _sliceId;
        [HideInInspector] [SerializeField] float _lastSliceTime = float.MinValue;


#pragma warning disable 0649
        /// <summary>
        /// Material that will be applied after slicing
        /// </summary>
        [SerializeField] private Material _defaultSliceMaterial;

        [SerializeField] private bool _asynchronously = false;
        [SerializeField]
        /// <summary>
        /// If your code do not use SliceId, it can relay on delay between last slice and new.
        /// If real delay is less than this value, slice will be ignored
        /// </summary>
        private float _delayBetweenSlices = 1f;
#pragma warning restore 0649

        Queue<SliceTry> _sliceTrys;

        public Material DefaultSliceMaterial
        {
            get { return _defaultSliceMaterial; }
            set { _defaultSliceMaterial = value; }
        }

        public bool Asynchronously
        {
            get { return _asynchronously; }
            set => _asynchronously = value;
        }

        private void OnEnable()
        {
            _sliceTrys = new Queue<SliceTry>();
        }

        /// <summary>
        /// Start slicing process
        /// </summary>
        /// <param name="addData">You can pass any object. You will </param>
        /// <returns>Returns true if pre-slice conditions was succeeded and task was added to the queue</returns>
        private IEnumerator StartSlice(BzSliceTryData sliceTryData, Action<BzSliceTryResult> callBack)
        {
            var renderers = GetRenderers(gameObject);
            yield return null;
            
            var items = new SliceTryItem[renderers.Length];

            for (var i = 0; i < renderers.Length; i++)
            {
                var renderer = renderers[i];

                var adapterAndMesh = GetAdapterAndMesh(renderer.GetComponent<MeshFilter>());

                if (adapterAndMesh == null)
                    continue;

                var mesh = adapterAndMesh.mesh;
                var adapter = adapterAndMesh.adapter;

                var configuration = renderer.gameObject.GetComponent<BzSliceConfiguration>();
                var meshDissector = new BzMeshDataDissector(mesh, sliceTryData.plane, renderer.sharedMaterials, adapter,
                    configuration) {DefaultSliceMaterial = _defaultSliceMaterial};

                var sliceTryItem = new SliceTryItem {meshRenderer = renderer, meshDissector = meshDissector};
                items[i] = sliceTryItem;
            }
            
            var sliceTry = new SliceTry {items = items, callBack = callBack, sliceData = sliceTryData};
            StartWorker(WorkForWorker, sliceTry);
            _sliceTrys.Enqueue(sliceTry);
        }

        protected abstract AdapterAndMesh GetAdapterAndMesh(MeshFilter meshFilter);

        /// <summary>
        /// You need to override this to use your thead pool
        /// </summary>
        /// <param name="method">method that you need to call</param>
        /// <param name="obj">object that you need to pass to method</param>
        protected virtual void StartWorker(Action<object> method, object obj)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(method), obj);
        }

        void WorkForWorker(object obj)
        {
            try
            {
                var sliceTry = (SliceTry) obj;
                Work(sliceTry);
                sliceTry.Finished = true;
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }
        }

        void Work(SliceTry sliceTry)
        {
            var somethingOnNeg = false;
            var somethingOnPos = false;
            for (var i = 0; i < sliceTry.items.Length; i++)
            {
                var sliceTryItem = sliceTry.items[i];

                if (sliceTryItem == null)
                    continue;

                var meshDissector = sliceTryItem.meshDissector;
                sliceTryItem.SliceResult = meshDissector.Slice();

                if (sliceTryItem.SliceResult == SliceResult.Neg |
                    sliceTryItem.SliceResult == SliceResult.Duplicate |
                    sliceTryItem.SliceResult == SliceResult.Sliced)
                {
                    somethingOnNeg = true;
                }

                if (sliceTryItem.SliceResult == SliceResult.Pos |
                    sliceTryItem.SliceResult == SliceResult.Duplicate |
                    sliceTryItem.SliceResult == SliceResult.Sliced)
                {
                    somethingOnPos = true;
                }
            }

            sliceTry.sliced = somethingOnNeg & somethingOnPos;
        }

        void Update()
        {
            Profiler.BeginSample("GetFinishedTask");
            var sliceTry = GetFinishedTask();
            Profiler.EndSample();

            if (sliceTry == null)
                return;

            Profiler.BeginSample("SliceTryFinished");
            SliceTryFinished(sliceTry);
            Profiler.EndSample();
        }

        private void SliceTryFinished(SliceTry sliceTry)
        {
            BzSliceTryResult result = null;
            if (sliceTry.sliced)
            {
                Profiler.BeginSample("ApplyChanges");
                result = ApplyChanges(sliceTry);
                Profiler.EndSample();
            }

            if (result == null)
            {
                result = new BzSliceTryResult(false, sliceTry.sliceData.addData);
            }
            else
            {
                Profiler.BeginSample("InvokeEvents");
                InvokeEvents(result.outObjectNeg, result.outObjectPos);
                Profiler.EndSample();
            }

            Profiler.BeginSample("OnSliceFinished");
            OnSliceFinished(result);
            Profiler.EndSample();

            if (sliceTry.callBack != null)
            {
                Profiler.BeginSample("CallBackMethod");
                sliceTry.callBack(result);
                Profiler.EndSample();
            }
        }

        private void InvokeEvents(GameObject resultNeg, GameObject resultPos)
        {
            var events = resultNeg.GetComponents<IBzObjectSlicedEvent>();
            for (var i = 0; i < events.Length; i++)
                events[i].ObjectSliced(gameObject, resultNeg, resultPos);
        }

        private BzSliceTryResult ApplyChanges(SliceTry sliceTry)
        {
            // duplicate object
            GameObject resultObjNeg, resultObjPos;
            GetNewObjects(out resultObjNeg, out resultObjPos);
            
            var renderersNeg = GetRenderers(resultObjNeg);
            var renderersPos = GetRenderers(resultObjPos);

            if (renderersNeg.Length != renderersPos.Length |
                renderersNeg.Length != sliceTry.items.Length)
            {
                // if something wrong happaned with object, and during slicing it was changed
                // reject this slice try
                return null;
            }

            var result = new BzSliceTryResult(true, sliceTry.sliceData.addData);
            result.meshItems = new BzMeshSliceResult[sliceTry.items.Length];

            for (var i = 0; i < sliceTry.items.Length; i++)
            {
                var sliceTryItem = sliceTry.items[i];
                if (sliceTryItem == null)
                    continue;

                var rendererNeg = renderersNeg[i];
                var rendererPos = renderersPos[i];

                if (sliceTryItem.SliceResult == SliceResult.Sliced)
                {
                    sliceTryItem.meshDissector.RebuildNegMesh(rendererNeg);
                    sliceTryItem.meshDissector.RebuildPosMesh(rendererPos);

                    var itemResult = GetItemResult(sliceTryItem, rendererNeg, rendererPos);
                    result.meshItems[i] = itemResult;
                }
            }

            result.outObjectNeg = resultObjNeg;
            result.outObjectPos = resultObjPos;

            Events.SliceResult.Call(result);
            gameObject.SetActive(false);
            return result;
        }

        protected virtual void GetNewObjects(out GameObject resultObjNeg, out GameObject resultObjPos)
        {
            resultObjNeg = Instantiate(this.gameObject, this.gameObject.transform.parent);
            resultObjPos = Instantiate(this.gameObject, this.gameObject.transform.parent);

            resultObjPos.name = resultObjNeg.name + "_pos";
            resultObjNeg.name = resultObjNeg.name + "_neg";
        }

        private static void DeleteRenderer(Renderer renderer)
        {
            GameObject.Destroy(renderer);
            var mf = renderer.gameObject.GetComponent<MeshFilter>();
            if (mf != null)
            {
                GameObject.Destroy(mf);
            }
        }

        protected abstract BzSliceTryData PrepareData(Plane plane);

        protected abstract void OnSliceFinished(BzSliceTryResult result);

        // ReSharper disable Unity.PerformanceAnalysis
        public void Slice(Plane plane, int sliceId, Action<BzSliceTryResult> callBack)
        {
            if (this == null) // if this component was destroied
                return;

            var currentSliceTime = Time.time;

            // we should prevent slicing same object:
            // - if _delayBetweenSlices was not exceeded
            // - with the same sliceId
            if ((sliceId == 0 & _lastSliceTime + _delayBetweenSlices > currentSliceTime) |
                (sliceId != 0 & _sliceId == sliceId))
            {
                return;
            }

            // exit if it have LazyActionRunner
            if (GetComponent<LazyActionRunner>() != null)
                return;

            _lastSliceTime = currentSliceTime;
            _sliceId = sliceId;

            if (_defaultSliceMaterial == null)
                throw new InvalidOperationException("DefaultSliceMaterial == null");

            var data = PrepareData(plane);
            if (data == null)
            {
                if (callBack != null)
                    callBack(null);
                return;
            }

            if (!data.componentManager.Success)
            {
                if (callBack != null)
                    callBack(new BzSliceTryResult(false, data.addData));
                return;
            }

           StartCoroutine( StartSlice(data, callBack));
        }

        private SliceTry GetFinishedTask()
        {
            if (_sliceTrys.Count == 0)
                return null;

            var sliceTry = _sliceTrys.Peek();

            if (sliceTry == null || !sliceTry.Finished)
                return null;

            return _sliceTrys.Dequeue();
        }

        private static BzMeshSliceResult GetItemResult(SliceTryItem sliceTryItem, Renderer rendererNeg,
            Renderer rendererPos)
        {
            var itemResult = new BzMeshSliceResult {rendererNeg = rendererNeg, rendererPos = rendererPos};

            var sliceEdgeNegResult = new BzSliceEdgeResult[sliceTryItem.meshDissector.CapsNeg.Count];
            for (var i = 0; i < sliceEdgeNegResult.Length; i++)
            {
                var edgeResult = new BzSliceEdgeResult();
                sliceEdgeNegResult[i] = edgeResult;
                edgeResult.capsData = sliceTryItem.meshDissector.CapsNeg[i];
            }

            itemResult.sliceEdgesNeg = sliceEdgeNegResult;

            var sliceEdgePosResult = new BzSliceEdgeResult[sliceTryItem.meshDissector.CapsPos.Count];
            for (var i = 0; i < sliceEdgePosResult.Length; i++)
            {
                var edgeResult = new BzSliceEdgeResult();
                sliceEdgePosResult[i] = edgeResult;
                edgeResult.capsData = sliceTryItem.meshDissector.CapsPos[i];
            }

            itemResult.sliceEdgesPos = sliceEdgePosResult;

            return itemResult;
        }

        private Renderer[] GetRenderers(GameObject gameObject)
        {
            return gameObject.GetComponentsInChildren<Renderer>();
        }

        public override string ToString()
        {
            // prevent from accessing the name in debuge mode.
            return GetType().Name;
        }

        protected class AdapterAndMesh
        {
            public IBzSliceAddapter adapter;
            public Mesh mesh;
        }
    }
}
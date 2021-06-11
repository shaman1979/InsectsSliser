using LightDev.Core;
using Slicer.EventAgregators;
using Tools;
using Zenject;

namespace LightDev.UI
{
    /// <summary>
    /// Attach this script to Canvas GameObject in order to control CanvasElements.
    /// </summary>
    public class CanvasManager : Base
    {
        [Inject] 
        private IEventsAgregator eventsAgregator;
        
        protected CanvasElement[] canvasElements;

        protected virtual void Awake()
        {
            canvasElements = GetComponentsInChildren<CanvasElement>(true);
            foreach (CanvasElement element in canvasElements)
            {
                element.gameObject.Activate();
                element.Subscribe(eventsAgregator);
                element.gameObject.Deactivate();
            }
        }

        protected virtual void OnDestroy()
        {
            foreach (CanvasElement element in canvasElements)
            {
                element.Unsubscribe();
            }
        }
    }
}

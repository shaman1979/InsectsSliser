using BzKovSoft.ObjectSlicer;
using LightDev.Core;

namespace LightDev
{
    public partial class Events
    {
        public static Event<int, int> SuccessfulSlice;
        public static Event<BzSliceTryResult, int, int> SliceResult;
    }
}

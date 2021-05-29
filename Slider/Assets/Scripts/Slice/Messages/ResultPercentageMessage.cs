namespace Slicer.Slice.Messages
{
    public class ResultPercentageMessage
    {
        public ResultPercentageMessage(int left, int right)
        {
            Left = left;
            Right = right;
        }

        public int Right { get;}
        public int Left { get; }
    }
}
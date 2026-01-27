namespace PetrushevskiApps.WhosGame.Scripts.InfiniteScrollListService
{
    public struct ListRow
    {
        public int Index { get; }
        public float Position { get; }

        public ListRow(int index, float position)
        {
            Index = index;
            Position = position;
        }
    }
}
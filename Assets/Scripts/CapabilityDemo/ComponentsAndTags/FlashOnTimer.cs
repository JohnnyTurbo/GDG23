using Unity.Entities;

namespace TMG.GDG23
{
    public struct FlashOnTimer : IComponentData
    {
        public float RegularToggleInterval;
        public float RapidToggleInterval;
        public float RapidToggleThreshold;
        public float NextToggleTime;
        public bool IsRed;
    }
}
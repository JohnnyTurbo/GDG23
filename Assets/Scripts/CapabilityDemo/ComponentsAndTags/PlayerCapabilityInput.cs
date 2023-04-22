using Unity.Entities;

namespace TMG.GDG23
{
    public struct PlayerCapabilityInput : IComponentData
    {
        public bool Current;
        public bool Previous;

        public bool Begin => Current && !Previous;
        public bool Held => Current && Previous;
        public bool End => !Current && Previous;
    }
}
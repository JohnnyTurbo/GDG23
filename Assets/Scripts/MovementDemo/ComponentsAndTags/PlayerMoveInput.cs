using Unity.Entities;
using Unity.Mathematics;

namespace TMG.GDG23
{
    public struct PlayerMoveInput : IComponentData
    {
        public float3 Current;
        public float3 Previous;
        public float3 LastTrueInput;
        
        public bool Begin => !Current.Equals(Previous) && math.lengthsq(Current) > 0.1f;
        public bool Held => Current.Equals(Previous) && math.lengthsq(Current) > 0.1f;
        public bool End => !Current.Equals(Previous) && math.lengthsq(Current) < 0.1f;
    }
}
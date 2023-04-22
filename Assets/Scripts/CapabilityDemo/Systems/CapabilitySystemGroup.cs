using Unity.Entities;
using Unity.Transforms;

namespace TMG.GDG23
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    [UpdateAfter(typeof(MovementSystemGroup))]
    public partial class CapabilitySystemGroup : ComponentSystemGroup
    {
    }
}
using Unity.Entities;
using Unity.Transforms;

namespace TMG.GDG23
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    [UpdateAfter(typeof(GetPlayerMoveInputSystem))]
    public partial class MovementSystemGroup : ComponentSystemGroup
    {
    }
}
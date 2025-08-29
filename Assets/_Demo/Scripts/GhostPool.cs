using System.Runtime.CompilerServices;

public static class GhostPool
{
    static readonly Pool<Ghost> Pool = new Pool<Ghost>(() => new Ghost(), 16);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ghost Get()
    {
        return Pool.Get();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Return(Ghost ghost)
    {
        Pool.Return(ghost);
    }
}

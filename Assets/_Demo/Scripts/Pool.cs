using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class Pool<T>
{
    readonly Stack<T> items = new Stack<T>();

    readonly Func<T> itemGenerator;

    public Pool(Func<T> itemGenerator, int initialCapacity)
    {
        this.itemGenerator = itemGenerator;

        for (int i = 0; i < initialCapacity; i++)
        {
            items.Push(itemGenerator());
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Get() => items.Count > 0 ? items.Pop() : itemGenerator();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Return(T item) => items.Push(item);
}

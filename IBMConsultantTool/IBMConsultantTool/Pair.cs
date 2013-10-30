using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public sealed class Pair<TKey, TValue>
{
    private readonly TKey key;
    private readonly IDictionary<TKey, TValue> data;
    public Pair(TKey key, IDictionary<TKey, TValue> data)
    {
        this.key = key;
        this.data = data;
    }
    public TKey Key { get { return key; } }
    public TValue Value
    {
        get
        {
            TValue value;
            data.TryGetValue(key, out value);
            return value;
        }
        set { data[key] = value; }
    }
}

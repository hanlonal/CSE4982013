﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

public class DictionaryBindingList<TKey, TValue>
    : BindingList<Pair<TKey, TValue>>
{
    private readonly IDictionary<TKey, TValue> data;
    public DictionaryBindingList(IDictionary<TKey, TValue> data)
    {
        this.data = data;
        Reset();
    }
    public void Reset()
    {
        bool oldRaise = RaiseListChangedEvents;
        RaiseListChangedEvents = false;
        try
        {
            Clear();
            foreach (TKey key in data.Keys)
            {
                Add(new Pair<TKey, TValue>(key, data));
            }
        }
        finally
        {
            RaiseListChangedEvents = oldRaise;
            ResetBindings();
        }
    }

}
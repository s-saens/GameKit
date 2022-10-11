using System;

public class Data<T>
{
    private T v;
    public Action<T> onChange;
    public bool blockChangeEvent = false;
    
    public virtual T value
    {
        get
        {
            return this.v;
        }
        set
        {
            this.v = value;
            if(!blockChangeEvent) this.onChange?.Invoke(value);
        }
    }

    public Data() {}
    public Data(T val)
    {
        this.v = val;
    }
    
    public void LockEvent()
    {
        blockChangeEvent = true;
    }

    public void UnlockEvent()
    {
        blockChangeEvent = false;
    }
}
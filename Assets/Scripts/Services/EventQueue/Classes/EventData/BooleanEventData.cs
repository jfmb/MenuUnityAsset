using System;

namespace Services.EventQueue
{
    public class BooleanEventData: EventArgs
    {
        public bool Value { get; }

        public BooleanEventData(bool value)
        {
            Value = value;
        }
    }
}
using System;

namespace Services.EventQueue.Classes.EventData
{
    public class IntegerEventData: EventArgs
    {
        public int Value { get; }

        public IntegerEventData(int value)
        {
            Value = value;
        }
    }
}
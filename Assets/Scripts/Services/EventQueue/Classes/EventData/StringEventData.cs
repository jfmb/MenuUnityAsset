using System;

namespace Services.EventQueue.Events.ScriptableObjects
{
    public class StringEventData : EventArgs
    {
        public string Value { get; }

        public StringEventData(string value)
        {
            Value = value;
        }
    }
}
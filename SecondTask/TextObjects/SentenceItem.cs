using System;

namespace SecondTask.TextObjects
{
    public abstract class SentenceItem
    {
        protected string Value { get; set; }

        public int Length => Value.Length;

        public SentenceItem(string value)
        {
            Value = value;
        }

        public string ValueToLower => Value.ToLower();

        public override sealed string ToString() => Value;

        public override int GetHashCode()
        {
            return ValueToLower.GetHashCode();
        }

        public override bool Equals(object other)
        {
            return other is SentenceItem && Value.Equals(((SentenceItem)other).Value, StringComparison.OrdinalIgnoreCase);
        }

    }
}

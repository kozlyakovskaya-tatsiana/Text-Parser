using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override sealed string ToString() => Value;

        public override int GetHashCode()
        {
            return Value.ToLower().GetHashCode();
        }

        public override bool Equals(object other)
        {
            return other is SentenceItem && Value.Equals(((SentenceItem)other).Value,StringComparison.OrdinalIgnoreCase);
        }
       
    }
}

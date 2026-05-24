using System;

namespace BasicTools
{
    public struct BasicRange : IEquatable<BasicRange>
    {
        public int Min { get; private set; }
        public int Max { get; private set; }

        public BasicRange(int min, int max)
            : this()
        {
            if (min > max)
            {
                throw new ArgumentException("BasicRange: Min must be less than or equal to Max");
            }
            Min = min;
            Max = max;
        }

        public override bool Equals(object obj) => obj is BasicRange other && this.Equals(other);

        public bool Equals(BasicRange p) => Min == p.Min && Max == p.Max;

        public override int GetHashCode() => (Min, Max).GetHashCode();

        public static bool operator ==(BasicRange lhs, BasicRange rhs) => lhs.Equals(rhs);

        public static bool operator !=(BasicRange lhs, BasicRange rhs) => !(lhs == rhs);
    }
}
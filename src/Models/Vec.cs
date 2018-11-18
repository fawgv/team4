namespace thegame.Models
{
    public class Vec
    {
        public Vec(int x, int y)
        {
            X = x;
            Y = y;
        }

        public readonly int X, Y;

        public static Vec operator +(Vec vec1, Vec vec2)
        {
            return new Vec(vec1.X + vec2.X, vec1.Y + vec2.Y);
        }

        protected bool Equals(Vec other) => X == other.X && Y == other.Y;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Vec) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return X * 397 ^ Y;
            }
        }
    }
}
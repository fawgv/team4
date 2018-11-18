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
    }
}
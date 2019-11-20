namespace Map
{
    public class Pair<T, K>
    {
        public Pair(T first, K second)
        {
            First = first;
            Second = second;
        }

        public T First { get; set; }
        public K Second { get; set; }

        public bool equal(T t, K k)
        {
            return First.Equals(t) && Second.Equals(k);
        }
    }
}
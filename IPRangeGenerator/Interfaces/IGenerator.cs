namespace IPRangeGenerator.Interfaces
{
    internal interface IGenerator<T>
    {

        T? MinValue { get; init; }

        T? MaxValue { get; init; }

        T Generate();

        T GenerateInRange(bool isInclusive = false);

        IEnumerable<T> GenerateEnumerable(int count);

        IEnumerable<T> GenerateEnumerableInRange(int count, bool isInclusive = false);
    }
}

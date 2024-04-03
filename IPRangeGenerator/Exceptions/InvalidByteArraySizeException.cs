namespace IPRangeGenerator.Exceptions
{
    internal class InvalidByteArraySizeException : Exception
    {
        public int ExpectedSize { get; }

        public int ActualSize { get; }

        public InvalidByteArraySizeException(int expectedSize, int actualSize)
        {
            ExpectedSize = expectedSize;
            ActualSize = actualSize;
        }

        public override string Message => $"Неправильный размер массива байт. Ожидался размер {ExpectedSize}, фактический размер {ActualSize}.";
    }
}

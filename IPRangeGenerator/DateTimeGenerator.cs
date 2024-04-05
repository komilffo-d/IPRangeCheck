using IPRangeGenerator.Base;
using IPRangeGenerator.Interfaces;
using System.Globalization;

namespace IPRangeGenerator
{
    public class DateTimeGenerator : BaseGenerator, IGenerator<DateTime>
    {

        public override Random _random { get; init; }
        public DateTime MinValue { get; init; } = new DateTime(1900, 1, 1, 0, 0, 0);
        public DateTime MaxValue { get; init; } = DateTime.Now;


        public DateTimeGenerator()
        {


        }

        public DateTimeGenerator(DateTime minDateTime, DateTime maxDateTime) : base()
        {
            if (minDateTime > maxDateTime)
                throw new InvalidDataException("Минимальная дата больше максимальной");
            MinValue = minDateTime;
            MaxValue = maxDateTime;
        }

        public DateTimeGenerator(in string? minDateTime, in string? maxDateTime) : base()
        {
            if (!DateTime.TryParseExact(minDateTime, "d.M.yyyy H:m:s", null, DateTimeStyles.None, out DateTime tempMinValue))
                throw new InvalidDataException("Неправильный формат даты");

            if (!DateTime.TryParseExact(maxDateTime, "d.M.yyyy H:m:s", null, DateTimeStyles.None, out DateTime tempMaxValue))
                throw new InvalidDataException("Неправильный формат даты");

            if (tempMinValue > tempMaxValue)
                throw new InvalidDataException("Минимальная дата больше максимальной");
            MinValue = tempMinValue;
            MaxValue = tempMaxValue;
        }
        public DateTime Generate()
        {
            long totalSeconds = (long)(DateTime.MaxValue - DateTime.MinValue).TotalSeconds;

            long randomLong = (long)Math.Floor(_random.NextDouble() * totalSeconds);

            return DateTime.MinValue.AddSeconds(randomLong);
        }


        public DateTime GenerateInRange(bool isInclusive = true)
        {
            DateTime tempMinDateTime = this.MinValue;
            DateTime tempMaxDateTime = this.MaxValue;
            if (!isInclusive)
                tempMinDateTime=tempMinDateTime.AddSeconds(TimeSpan.FromMinutes(1).TotalSeconds);

            return tempMinDateTime.AddSeconds(RandomNext(minValue: tempMinDateTime, maxValue: tempMaxDateTime,isInclusive));
        }

        public IEnumerable<DateTime> GenerateEnumerable(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return Generate();
            }
        }

        public IEnumerable<DateTime> GenerateEnumerableInRange(int count, bool isInclusive = true)
        {
            for (int i = 0; i < count; i++)
            {
                yield return GenerateInRange(isInclusive);
            }
        }
        private protected long RandomNext(DateTime minValue, DateTime maxValue, bool isInclusive = true)
        {
            long totalSeconds = (long)(maxValue - minValue).TotalSeconds;
            long totalSecondsInMinute = default(long);
            if (!isInclusive)
            {
                TimeSpan secondsInDay = TimeSpan.FromMinutes(1);
                totalSecondsInMinute = (long)secondsInDay.TotalSeconds;
            }
            totalSeconds -= totalSecondsInMinute;
            long randomSeconds = (long)Math.Round(_random.NextDouble() * (totalSeconds + double.Epsilon));
            return randomSeconds;
        }
    }
}

namespace Api.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime bob)
        {
            var today =DateTime.UtcNow;

            var age =today.Year-bob.Year;

            if (bob > today.AddYears(-age)) age--;

            return age;
        }
    }
}
namespace SOA_Layered_Arch.BusinessLayer
{
    public static class RatingCalculator
    {
        public static decimal CalculateAverageRating(IEnumerable<Rating>
       ratings)
        {
            if (ratings == null || !ratings.Any())
                return 0;
            return ratings.Average(r => r.Value);
        }
    }
}

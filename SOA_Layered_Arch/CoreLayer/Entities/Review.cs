namespace SOA_Layered_Arch.CoreLayer.Entities
{
   
        public class Review
        {
            public int Id { get; set; }
            public int MovieId { get; set; }
            public int UserId { get; set; }
            public string ReviewText { get; set; }
            public DateTime ReviewDate { get; set; }
        }
}

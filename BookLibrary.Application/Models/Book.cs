namespace BookLibrary.Application.Models
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public ushort Year { get; set; }
        public string Blurb { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
    }
}
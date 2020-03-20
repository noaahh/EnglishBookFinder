namespace EnglishBookFinder
{
    public class Book
    {
        public Book(string name, string url, BookSource source)
        {
            Name = name;
            Url = url;
            Source = source;
        }

        public BookSource Source { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int TotalWords { get; set; }
    }
}
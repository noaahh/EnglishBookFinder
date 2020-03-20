namespace EnglishBookFinder
{
    public class BookSource
    {
        public BookSource(string friendlyName, string url)
        {
            FriendlyName = friendlyName;
            Url = url;
        }

        public string FriendlyName { get; set; }

        public string Url { get; set; }
    }
}
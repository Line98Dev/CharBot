namespace CharBot.Models
{
    internal class Link
    {
        public Link(string name, string url)
        {
            Name = name;
            Url = url;
        }

        internal string Name { get; }
        internal string Url { get; }

        public new string ToString()
        {
            return "[" + Name + "](" + Url + ")";
        }
    }
}

namespace SuperHeroiAPI
{
    public class HeroiResponse
    {
        public List<SuperHeroi> SuperHerois { get; set; } = new List<SuperHeroi>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}

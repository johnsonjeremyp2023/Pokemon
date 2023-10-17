namespace Pokemon.Models
{
    public class PokemonList
    {
        public List<PokemonListItem> Items { get; set; }
        public int Limit { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int Count { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public bool ShowFavorites { get; set; }
    }

    public class PokemonListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool IsFavorite { get; set; }

    }

    public class PokemonDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Abilities { get; set; }
        public string Type { get; set; }
        public string Forms { get; set; }
        public string Species { get; set; }
        public List<Stat> Stats { get; set; }
        public bool ShowFavorite { get; set; }
        public bool IsFavorite { get; set; }
    }

    public class Stat
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}

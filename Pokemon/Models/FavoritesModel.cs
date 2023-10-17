using System.ComponentModel.DataAnnotations;

namespace Pokemon.Models
{
    public class FavoritesModel
    {
        public string UserId { get; set; }
        public int PokemonId { get; set; }
    }

    public class FavoritesList
    {
        public List<FavoritesListItem> Items { get; set; }
    }

    public class FavoritesListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Type { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Pokemon.Areas.Identity.Data;
using Pokemon.Models;

namespace Pokemon.Controllers
{
    public class PokemonController : Controller
    {
        private readonly ILogger<PokemonController> _logger;
        private readonly PokemonDbContext _context;
        private UserManager<UsersModel> _userManager;

        public PokemonController(ILogger<PokemonController> logger, PokemonDbContext context, UserManager<UsersModel> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int limit = 20, int page = 1)
        {
            PokemonList model = new PokemonList();

            int offset = (page - 1) * limit;

            var getPokemon = GetPokemon(limit, offset);

            await Task.WhenAll(getPokemon);

            model = getPokemon.Result;
            model.Limit = limit;
            model.Page = page;
            model.TotalPages = (int)Math.Ceiling(model.Count / (float)model.Limit);
            model.ShowFavorites = User.Identity.IsAuthenticated;

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            PokemonDetails model = new PokemonDetails();
            model.Id = id;

            using (HttpClient client = new HttpClient())
            {
                string url = "https://pokeapi.co/api/v2/pokemon/" + id;
                HttpResponseMessage responseMessage = await client.GetAsync(url);
                HttpContent content = responseMessage.Content;
                var data = await content.ReadAsStringAsync();
                JObject dataObject = JObject.Parse(data);

                model.Name = dataObject["name"].ToString();
                model.ImageUrl = dataObject["sprites"]["front_default"].ToString();
                List<string> abilities = new List<string>();
                var ab = dataObject["abilities"].ToList();
                foreach (var ability in ab)
                {
                    abilities.Add(ability["ability"]["name"].ToString());
                }
                model.Abilities = String.Join(", ", abilities);

                List<string> t = new List<string>();
                var types = dataObject["types"].ToList();
                foreach (var type in types)
                {
                    t.Add(type["type"]["name"].ToString());
                }
                model.Type = String.Join(", ", t);

                List<string> f = new List<string>();
                var forms = dataObject["forms"].ToList();
                foreach (var form in forms)
                {
                    f.Add(form["name"].ToString());
                }
                model.Forms = String.Join(", ", f);

                List<Stat> stats = new List<Stat>();
                var statistics = dataObject["stats"].ToList();
                foreach (var stat in statistics)
                {
                    Stat newStat = new Stat();
                    newStat.Name = stat["stat"]["name"].ToString();
                    newStat.Value = Convert.ToInt32(stat["base_stat"]);
                    stats.Add(newStat);
                }
                model.Stats = stats;

                model.Species = dataObject["species"]["name"].ToString();

                string userId = _userManager.GetUserId(this.User);
                if (!string.IsNullOrEmpty(userId))
                {
                    model.IsFavorite = _context.Favorites.Where(u => u.UserId == userId && u.PokemonId == model.Id).Count() > 0;
                    model.ShowFavorite = User.Identity.IsAuthenticated;
                }
            }


            return View(model);
        }

        public JsonResult AddToFavorites(int id)
        {
            if (id <= 0)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "Invalid id sent: " + id
                });
            }
            if (!User.Identity.IsAuthenticated)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "User is not logged in."
                });
            }

            string userId = _userManager.GetUserId(this.User);
            if (_context.Favorites.Where(u => u.UserId == userId && u.PokemonId == id).Count() > 0)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "Pokemon " + id + " already added to favorites."
                });
            }

            FavoritesModel fav = new FavoritesModel();
            fav.UserId = userId;
            fav.PokemonId = id;
            _context.Favorites.Add(fav);
            _context.SaveChanges();

            return new JsonResult(new
            {
                success = true,
                message = "Added to Favorites!"
            });
        }

        public JsonResult RemoveFromFavorites(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "User is not logged in."
                });
            }

            string userId = _userManager.GetUserId(this.User);
            var favorites = _context.Favorites.Where(u => u.UserId == userId && u.PokemonId == id);
            if (favorites.Count() <= 0)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "Pokemon " + id + " is not a favorite."
                });
            }

            foreach (var fav in favorites)
            {
                _context.Favorites.Remove(fav);
            }
            _context.SaveChanges();

            return new JsonResult(new
            {
                success = true,
                message = "Removed from favorites."
            });
        }

        private async Task<PokemonList> GetPokemon(int limit = 20, int offset = 0)
        {
            PokemonList model = new PokemonList();
            List<PokemonListItem> list = new List<PokemonListItem>();
            string url = "https://pokeapi.co/api/v2/pokemon/?limit=" + limit + "&offset=" + offset;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url);
                HttpContent content = responseMessage.Content;
                var data = await content.ReadAsStringAsync();
                JObject dataObject = JObject.Parse(data);
                var results = dataObject["results"];
                foreach (var result in results)
                {
                    PokemonListItem item = new PokemonListItem();
                    HttpResponseMessage itemResponseMessage = await client.GetAsync(result["url"].ToString());
                    HttpContent itemContent = itemResponseMessage.Content;
                    var itemData = await itemContent.ReadAsStringAsync();
                    JObject itemObject = JObject.Parse(itemData);
                    item.Id = Convert.ToInt32(itemObject["id"]);
                    item.Name = itemObject["name"].ToString();
                    item.ImageUrl = itemObject["sprites"]["front_default"].ToString();
                    string userId = _userManager.GetUserId(this.User);
                    if (!string.IsNullOrEmpty(userId))
                    {
                        item.IsFavorite = _context.Favorites.Where(u => u.UserId == userId && u.PokemonId == item.Id).Count() > 0;
                    }
                    list.Add(item);
                }
                model.Count = Convert.ToInt32(dataObject["count"]);
                model.HasNext = !string.IsNullOrEmpty(dataObject["next"].ToString());
                model.HasPrevious = !string.IsNullOrEmpty(dataObject["previous"].ToString());
            }
            model.Items = list;

            return model;
        }
    }
}

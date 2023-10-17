using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Pokemon.Models;
using System.Collections.Generic;

namespace Pokemon.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly ILogger<FavoritesController> _logger;
        private readonly PokemonDbContext _context;
        private UserManager<UsersModel> _userManager;

        public FavoritesController(ILogger<FavoritesController> logger, PokemonDbContext context, UserManager<UsersModel> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            FavoritesList model = new FavoritesList();

            var getFavorites = GetFavorites();

            await Task.WhenAll(getFavorites);

            model = getFavorites.Result;

            return View(model);
        }

        private async Task<FavoritesList> GetFavorites()
        {
            FavoritesList model = new FavoritesList();
            List<FavoritesListItem> list = new List<FavoritesListItem>();

            string userId = _userManager.GetUserId(this.User);
            var favoriteIds = _context.Favorites.Where(u => u.UserId == userId).Select(u => u.PokemonId).ToList();

            using (HttpClient client = new HttpClient())
            {
                foreach (var id in favoriteIds)
                {
                    string url = "https://pokeapi.co/api/v2/pokemon/" + id;
                    HttpResponseMessage responseMessage = await client.GetAsync(url);
                    HttpContent content = responseMessage.Content;
                    var data = await content.ReadAsStringAsync();
                    JObject dataObject = JObject.Parse(data);

                    FavoritesListItem item = new FavoritesListItem();
                    item.Id = id;
                    item.Name = dataObject["name"].ToString();
                    item.ImageUrl = dataObject["sprites"]["front_default"].ToString();

                    var types = dataObject["types"].ToList();
                    List<string> t = new List<string>();
                    foreach (var type in types)
                    {
                        t.Add(type["type"]["name"].ToString());
                    }
                    item.Type = String.Join(", ", t);

                    list.Add(item);
                }
            }

            model.Items = list;

            return model;
        }
    }
}

# Pokemon

Pokemon project for Envision Horizons Logic & Problem Solving Test.

## Installation

Clone the project from git and it should work out of the box. There are a few nuget packages that are required so you might need to restore nuget packages when  you pull the project. Here's the list of all nuget packages for this project if you need to install them manually for some reason: 

* Microsoft.AspNetCore.Identity.EntityFrameworkCore
* Microsoft.AspNetCore.Identity.UI
* Microsoft.EntityFrameworkCore.Sqlite
* Microsoft.EntityFrameworkCore.SqlServer
* Microsoft.EntityFrameworkCore.Tools
* Microsoft.VisualStudio.Azure.Containers.Tools.Targets
* Microsoft.VisualStudio.Web.CodeGeneration.Design

The database (pokemon.db) for this project uses SQLite, so in order to look at what's in the database, you can either [download the command line tools](https://www.sqlite.org/download.html) or install [DB Browser for SQlite](https://sqlitebrowser.org/dl/).

## Frameworks

The only two frameworks used in this project are Entity Framework and .NET Core's Identity framework. Both of which should be included with the nuget packages listed above.

## Data

All data for this project is retrieved from the [PokeApi](https://pokeapi.co/docs/v2).

## Pages

### Home Page

The home page is a very simple page that tells you to login if you're not already, and has links to the pokemon list page. It also has a link to the signup page if you're not logged in. If you are logged in, it has a link to the favorites page. 

### Login/Register

The login and register pages are taken care of through the Identity framework. It's mostly just the default functionality of the framework.

### Pokemon List

The Pokemon List page is where users can view all of the pokemon available from the PokeApi. There is custom pagination at the bottom of the list. By default, 20 pokemon appear on the page at once. This can be changed by adding a "limit" query string parameter to the url and setting that to however many pokemon you want to appear at once. That limit value will then be passed to the PokeApi to get the paginated data.

Logged in users can also click the heart that appears in the top left of each pokemon to add/remove it from their favorites list. This is done with ajax calls so the page doesn't need to reload when users do this.

For each pokemon there is a link to the details page to view more information about that pokemon.

### Pokemon Details

The Pokemon details page is where all users can see more details about whatever pokemon they selected. They can also click on the heart to add/remove the pokemon from their favorites.

### Favorites

The Favorites page is where logged in users can look at all of their favorited pokemon. It's very similar to the pokemon list page, and when users click on the heart next to the pokemon, that pokemon will be removed from their favorites list. They will need to go back to the list page if they want to add it back. This page is only available for logged in users, and if a user who isn't logged in tries to access this page, they will be redirected to the login page.

## Database

For the database, there's only one table besides the ones created by the Identity framework, the Favorites table. This table only has two columns, UserId and PokemonId. The UserId is the ID of a user taken from the AspNetUsers table from the Identity framework, and the PokemonId is taken from the PokeAPI. This table is added to whenever a user adds a pokemon to their favorites list, with the user's ID and the pokemon's ID. And records are deleted whenever a user removes a pokemon from their favorites list.
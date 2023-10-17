//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Pokemon.Models;

//namespace Pokemon.Controllers
//{
//    public class UsersController : Controller
//    {
//        private readonly PokemonDbContext _context;

//        public UsersController(PokemonDbContext context)
//        {
//            _context = context;
//        }

//        // GET: Users
//        public async Task<IActionResult> Index()
//        {
//              return _context.Users != null ? 
//                          View(await _context.Users.ToListAsync()) :
//                          Problem("Entity set 'PokemonDbContext.Users'  is null.");
//        }

//        // GET: Users/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null || _context.Users == null)
//            {
//                return NotFound();
//            }

//            var usersModel = await _context.Users
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (usersModel == null)
//            {
//                return NotFound();
//            }

//            return View(usersModel);
//        }

//        // GET: Users/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Users/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,Username,Email,Salt,HashedPassword")] UsersModel usersModel)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(usersModel);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(usersModel);
//        }

//        // GET: Users/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null || _context.Users == null)
//            {
//                return NotFound();
//            }

//            var usersModel = await _context.Users.FindAsync(id);
//            if (usersModel == null)
//            {
//                return NotFound();
//            }
//            return View(usersModel);
//        }

//        // POST: Users/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Email,Salt,HashedPassword")] UsersModel usersModel)
//        {
//            if (id != usersModel.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(usersModel);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!UsersModelExists(usersModel.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(usersModel);
//        }

//        // GET: Users/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null || _context.Users == null)
//            {
//                return NotFound();
//            }

//            var usersModel = await _context.Users
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (usersModel == null)
//            {
//                return NotFound();
//            }

//            return View(usersModel);
//        }

//        // POST: Users/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            if (_context.Users == null)
//            {
//                return Problem("Entity set 'PokemonDbContext.Users'  is null.");
//            }
//            var usersModel = await _context.Users.FindAsync(id);
//            if (usersModel != null)
//            {
//                _context.Users.Remove(usersModel);
//            }
            
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool UsersModelExists(int id)
//        {
//          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
//        }
//    }
//}

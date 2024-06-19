using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Visitor_Management.Data;
using Visitor_Management.Models;

namespace Visitor_Management.Controllers
{
    public class VisitorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VisitorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.Visitor != null ? 
                          View(await _context.Visitor.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Visitor'  is null.");
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Visitor == null)
            {
                return NotFound();
            }

            var visitor = await _context.Visitor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visitor == null)
            {
                return NotFound();
            }

            return View(visitor);
        }

     
        [HttpGet]
        public IActionResult Create()
        {
            var visitor = new Visitor
            {
                Email = "example@example.com",
                Name = "John Doe",
                Arrivaltime = DateTime.Now,
                departuretime = DateTime.Now.AddHours(1)
            };
            Tempitems tempItems=new Tempitems();
           List<Tempitems> tempItemsList = new List<Tempitems> { tempItems };
            var model = new
            {
                Visitor = visitor,
             TempItems = tempItemsList
            };
            return View( model);
        }
        
        [HttpPost]
        public IActionResult Create(Visitor visitor)
        {
           

            visitor.departuretime = null;
            if (!ModelState.IsValid)
            {
                return View(visitor);
            }

            
            _context.Visitor.Add(visitor);
            _context.SaveChanges();

       
            var items = _context.TempItem.ToList();
            int sr = 1;
            foreach (var item in items)
            {
                var newItem = new Item
                {
                    Id = visitor.Id,
                    Item_Name = item.Item_Name,
                    Qty = item.Qty,
                   SrNO = sr++
                };
                _context.Items.Add(newItem);
                _context.SaveChanges();
                Create(visitor);
            } 

          
            _context.TempItem.RemoveRange(items);
            _context.SaveChanges();
           
            return RedirectToAction(nameof(Index));
        }
       
        private int currentSrNo = 1;
             public IActionResult AddItem( String ItemName, int qty)
             {
                 var newItem = new Tempitems();
                 newItem.SrNO = currentSrNo++;
                 newItem.Qty = qty; 
                 newItem.Item_Name = ItemName;

                 _context.TempItem.Add(newItem);
                 _context.SaveChanges();

            return PartialView("_ItemList", _context.TempItem.ToList());

            
        }
        [HttpPost]
        public IActionResult DeleteItem(int id)
        {
            var item = _context.TempItem.Find(id);
            if (item != null)
            {
                _context.TempItem.Remove(item);
                _context.SaveChanges();
                return Content("success");
            }
            return Content("error");
        }


        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Visitor == null)
            {
                return NotFound();
            }

            var visitor = await _context.Visitor.FindAsync(id);
            if (visitor == null)
            {
                return NotFound();
            }
            return View(visitor);
        }



        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Name,Arrivaltime,departuretime")] Visitor visitor)
        {
            if (id != visitor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visitor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitorExists(visitor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(visitor);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Visitor == null)
            {
                return NotFound();
            }

            var visitor = await _context.Visitor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visitor == null)
            {
                return NotFound();
            }

            return View(visitor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Visitor == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Visitor'  is null.");
            }
            var visitor = await _context.Visitor.FindAsync(id);
            if (visitor != null)
            {
                _context.Visitor.Remove(visitor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitorExists(int id)
        {
          return (_context.Visitor?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

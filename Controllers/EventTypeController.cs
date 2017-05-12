using EventPlanner2.Data;
using EventPlanner2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner2.Controllers
{
    public class EventTypeController : Controller
    {
        private ApplicationDbContext db_context;

        public int Id { get; private set; }

        public EventTypeController(ApplicationDbContext context)
        {
            db_context = context;
        }

        public IActionResult Index()
        {

            var eTypes = db_context.EventType.Select(e => e);

            return View(eTypes.ToList());
        }

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(EventType eventtype)
        //{
        //    db_context.Add(eventtype);
        //    //  if (ModelState.IsValid)
        //    //{
        //     await db_context.SaveChangesAsync();
        //      return RedirectToAction("Index");
        //    //}
        //   // return RedirectToAction("Index");
        //}


        [HttpGet]
        public IActionResult Edit(int id)
        {
            EventType eType = db_context.EventType.Where(e => e.Id == Id).SingleOrDefault();
            return View(eType);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditPost(int? Id)
        {
            EventType eType = db_context.EventType.Where(e => e.Id == Id).SingleOrDefault();
            if (await TryUpdateModelAsync<EventType>(eType, "", e => e.Label))
            {
                try
                {
                    await db_context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException /*ex*/)
                {
                    //Log the error (uncomment ex variable name and write a log)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " + "see your system administrator.");
                }

            }
            return View(eType);
        }


        [HttpGet]
        public IActionResult Create(int id)
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreatePost(EventType eventtype)
        {
            if (TryValidateModel(eventtype))
            {
                try
                {
                    db_context.Add(eventtype);
                    await db_context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException /*ex*/)
                {
                    //Log the error (uncomment ex variable name and write a log)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " + "see your system administrator.");
                }

            }

            return View(eventtype);
        }
    }
}
    
    
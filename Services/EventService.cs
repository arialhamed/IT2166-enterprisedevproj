using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using enterprisedevproj.Models;
using Microsoft.EntityFrameworkCore;

namespace enterprisedevproj.Services
{
    public class EventService
    {
        private readonly Models.EnterpriseDevProjDbContext _context;
        public EventService(Models.EnterpriseDevProjDbContext context)
        {
            _context = context;
        }
        public List<Event> GetAllEvents()
        {
            List<Event> AllEvents = new List<Event>();
            AllEvents = _context.Events.ToList(); //ERROR HERE
            AllEvents = AllEvents.OrderBy(e => e.StartTime).ToList();
            return AllEvents;
        }
        public Event GetEventById(string id)
        {
            Event events = _context.Events.Where(e => e.Id == id).FirstOrDefault();
            return events;
        }
        public List<Event> GetEventsForSearch(string inValue)
        {
            List<Event> SomeEvents = new List<Event>();
            SomeEvents = _context.Events.Where(e => e.Description.Contains(inValue)).ToList();
            return SomeEvents;
        }
        public bool EventExists(string id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
        public bool AddEvent(Event newEvent)
        {
            if (EventExists(newEvent.Id))
            {
                return false;
            }
            _context.Add(newEvent);
            _context.SaveChanges();
            return true;
        }
        public bool UpdateEvent(Event theEvent)
        {
            bool updated = true;
            //_context.Attach(theEvent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.Attach(theEvent).State = EntityState.Modified;

            //POTENTIAL ERROR HERE
            try
            {
                _context.SaveChanges();
                updated = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(theEvent.Id))
                {
                    updated = false;
                }
                else
                {
                    throw;
                }
            }
            return updated;
        }
        public bool DeleteEvent(Event theEvent)
        {
            if (!EventExists(theEvent.Id))
            {
                return false;
            }
            _context.Attach(theEvent);
            _context.Remove(theEvent);
            _context.SaveChanges();
            return true;
        }
    }
}

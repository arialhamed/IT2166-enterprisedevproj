using enterprisedevproj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace enterprisedevproj.Services
{
    public class ParticipantService
    {
        private readonly Models.EnterpriseDevProjDbContext _context;
        private readonly EventService _svcEvent;
        public ParticipantService(Models.EnterpriseDevProjDbContext context, EventService serviceEvent)
        {
            _context = context;
            _svcEvent = serviceEvent;
        }
        public List<EventParticipant> GetAllParticipants()
        {
            List<EventParticipant> AllParticipants = new List<EventParticipant>();
            AllParticipants = _context.EventParticipants.ToList(); //ERROR HERE
            return AllParticipants;
        }
        public List<EventParticipant> GetAllParticipantsByEventId(string id)
        {
            List<EventParticipant> AllParticipants = new List<EventParticipant>();
            foreach (var e in _context.EventParticipants.ToList())
            {
                if (e.EventId == id)
                {
                    AllParticipants.Add(e);
                }
            }
            return AllParticipants;
        }
        public bool ParticipantExists(int id)
        {
            return _context.EventParticipants.Any(e => e.Id == id);
        }
        public bool AddParticipant(EventParticipant newParticipant)
        {
            if (ParticipantExists(newParticipant.Id))
            {
                return false;
            }
            _context.Add(newParticipant);
            _context.SaveChanges();
            return true;
        }
        public bool DeleteParticipant(EventParticipant theParticipant)
        {
            if (!ParticipantExists(theParticipant.Id))
            {
                return false;
            }
            _context.Attach(theParticipant);
            _context.Remove(theParticipant);
            _context.SaveChanges();
            return true;
        }
    }
}
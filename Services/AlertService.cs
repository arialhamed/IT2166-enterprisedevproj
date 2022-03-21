using enterprisedevproj.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace enterprisedevproj.Services
{
    public class AlertService
    {
        private readonly Models.EnterpriseDevProjDbContext _context;
        public AlertService(Models.EnterpriseDevProjDbContext context)
        {
            _context = context;
        }
        public List<Alert> GetAllAlerts()
        {
            List<Alert> AllAlerts = new List<Alert>();
            AllAlerts = _context.Alerts.ToList();
            AllAlerts = AllAlerts.OrderBy(e => e.AlertTime).ToList();
            return AllAlerts;
        }
        public Alert GetAlertById (string id)
        {
            Alert alert = _context.Alerts.Where(e => e.Id == id).FirstOrDefault();
            return alert;
        }
        public List<Alert> GetAlertsForSearch (string inValue)
        {
            List<Alert> SomeAlerts = new List<Alert>();
            SomeAlerts = _context
                .Alerts
                .Where(e => e.Description.Contains(inValue))
                .ToList();
            return SomeAlerts;
        }
        public List<Alert> GetActiveAlerts()
        {
            List<Alert> SomeAlerts = new List<Alert>();
            foreach (Alert a in GetAllAlerts())
            {
                if (!a.Description.EndsWith("RESOLVED"))
                {
                    SomeAlerts.Add(a);
                }
            }
            return SomeAlerts;
        }
        public List<Alert> GetResolvedAlerts()
        {
            List<Alert> SomeAlerts = new List<Alert>();
            foreach (Alert a in GetAllAlerts())
            {
                if (a.Description.EndsWith("RESOLVED"))
                {
                    SomeAlerts.Add(a);
                }
            }
            return SomeAlerts;
        }
        public bool AlertExists(string id)
        {
            return _context.Alerts.Any(e => e.Id == id);
        }
        public bool AddAlert(Alert newAlert)
        {
            if (AlertExists(newAlert.Id))
            {
                return false;
            }
            _context.Add(newAlert);
            _context.SaveChanges();
            return true;
        }
        public bool UpdateAlert(Alert theAlert)
        {
            bool updated = true;
            _context.Attach(theAlert).State = EntityState.Modified;

            //POTENTIAL ERROR HERE
            try
            {
                _context.SaveChanges();
                updated = true;
            } catch (DbUpdateConcurrencyException)
            {
                if (!AlertExists(theAlert.Id))
                {
                    updated = false;
                } else
                {
                    throw;
                }
            }
            return updated;
        }
        public bool DeleteAlert(Alert theAlert)
        {
            if (!AlertExists(theAlert.Id))
            {
                return false;
            }
            /*_context.Attach(theAlert);
            _context.Remove(theAlert);
            _context.SaveChanges();*/

            // by recommendation
            //theAlert.Id += "_RESOLVED";
            theAlert.Description += "_RESOLVED";
            
            return UpdateAlert(theAlert);
        }
    }
}

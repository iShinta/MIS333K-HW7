using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ho_MinhTri_HW7.Models;

namespace Ho_MinhTri_HW7.Controllers
{
    public class EventsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Events
        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            //Add to ViewBag
            ViewBag.AllCommittees = GetAllCommittees();
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID,EventTitle,EventDate,EventLocation,MembersOnly")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventID,EventTitle,EventDate,EventLocation,MembersOnly")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // SelectList Committees
        //COMMITTEE ALREADY CHOSEN 
        public SelectList GetAllCommittees(Event @event)
        {
            //Populate list of committees
            var query = from c in db.Committees
                        orderby c.CommitteeName
                        select c;

            //create list and execute query
            List<Committee> allCommittees = query.ToList();

            //convert to select list
            SelectList allCommitteesList = new SelectList(allCommittees, "CommitteeID", "CommitteeName", @event.SponsoringCommittee.CommitteeID);

            return allCommitteesList;
        }

        // SelectList Committees
        public SelectList GetAllCommittees()
        {
            //Populate list of committees
            var query = from c in db.Committees
                        orderby c.CommitteeName
                        select c;

            //create list and execute query
            List<Committee> allCommittees = query.ToList();

            //convert to select list
            SelectList allCommitteesList = new SelectList(allCommittees, "CommitteeID", "CommitteeName");

            return allCommitteesList;
        }

        // SelectList Committees
        public MultiSelectList GetAllMembers(Event @event)
        {
            //Populate list of members
            var query = from m in db.Users
                        orderby m.Email
                        select m;

            //create list and execute query
            List<AppUser> allMembers = query.ToList();

            //Create list of selected members
            List<String> SelectedMembers = new List<String>();

            //Loop through list of members and add MemberId
            foreach (AppUser m in @event.Members)
            {
                SelectedMembers.Add(m.Id);
            }

            MultiSelectList allMemberList = new MultiSelectList(allMembers, "Id", "Email", SelectedMembers);

            return allMemberList;
        }
    }
}

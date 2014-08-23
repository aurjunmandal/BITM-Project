using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Controllers
{
    public class AllocateClassRoomController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        // GET: /AllocateClassRoom/
        public ActionResult Index()
        {
            var allocateclassrooms = db.AllocateClassRooms.Include(a => a.Course).Include(a => a.Day).Include(a => a.Department).Include(a => a.Room);
            return View(allocateclassrooms.ToList());
        }

        // GET: /AllocateClassRoom/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllocateClassRoom allocateclassroom = db.AllocateClassRooms.Find(id);
            if (allocateclassroom == null)
            {
                return HttpNotFound();
            }
            return View(allocateclassroom);
        }

        // GET: /AllocateClassRoom/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code");
            ViewBag.DayId = new SelectList(db.Days, "DayId", "TimeDay");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNo");
            return View();
        }

        // POST: /AllocateClassRoom/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AllocateClassRoomId,DepartmentId,CourseId,RoomId,DayId,StartTime,EndTime")] AllocateClassRoom allocateclassroom)
        {
            if (ModelState.IsValid)
            {
                Room room = db.Rooms.Find(allocateclassroom.RoomId);
                Course course = db.Courses.Find(allocateclassroom.CourseId);
                Day day = db.Days.Find(allocateclassroom.DayId);

                if (allocateclassroom.StartTime.Length == 4)
                    allocateclassroom.StartTime = "0" + allocateclassroom.StartTime;
                if (allocateclassroom.EndTime.Length == 4)
                    allocateclassroom.EndTime = "0" + allocateclassroom.EndTime;

                
                int givenStartTime = ConvertTimeFromStringToInt(allocateclassroom.StartTime);
                int givenEndTime = ConvertTimeFromStringToInt(allocateclassroom.EndTime);

                //check if start time >= Endtime

                List<AllocateClassRoom> rooOverLapList = new List<AllocateClassRoom>();

                var DayRoomAllocationList = db.AllocateClassRooms.Include(c => c.Department).Include(d => d.Course).Include(r => r.Room).Include(r => r.Day).Where(r => r.RoomId == allocateclassroom.RoomId && r.DayId == allocateclassroom.DayId).ToList();

                foreach (var aDayroomAllocation in DayRoomAllocationList)
                {
                    int DbFrom = ConvertTimeFromStringToInt(aDayroomAllocation.StartTime);
                    int DbEnd = ConvertTimeFromStringToInt(aDayroomAllocation.EndTime);

                    if (
                            ((DbFrom <= givenStartTime) && (givenStartTime < DbEnd))
                            || ((DbFrom < givenEndTime) && (givenEndTime <= DbEnd))
                            || ((givenStartTime <= DbFrom) && (DbEnd <= givenEndTime))
                        )
                    {
                        rooOverLapList.Add(aDayroomAllocation);
                    }
                }
                if (rooOverLapList.Count == 0)
                {
                    db.AllocateClassRooms.Add(allocateclassroom);
                    db.SaveChanges();
                    TempData["AlloacteMessage"] = "Room : " + room.RoomNo + "Allocated "
                                                  + " for course : " + course.Code + " From " +
                                                  allocateclassroom.StartTime
                                                  + " to " + allocateclassroom.EndTime + " on " + day.TimeDay;
                    return RedirectToAction("Index");
                }
                else
                {
                    string message = "Room : " + room.RoomNo + " Overlapped With : ";
                    foreach (var anOverlappedRoom in rooOverLapList)
                    {
                        int dbFrom = ConvertTimeFromStringToInt(anOverlappedRoom.StartTime);
                        int dbEnd = ConvertTimeFromStringToInt(anOverlappedRoom.EndTime);

                        string overlapStart, overlapEnd;

                        if ((dbFrom <= givenStartTime) && (givenStartTime < dbEnd))
                            overlapStart = allocateclassroom.StartTime;
                        else
                            overlapStart = anOverlappedRoom.StartTime;

                        if ((dbFrom < givenEndTime) && (givenEndTime <= dbEnd))
                            overlapEnd = allocateclassroom.EndTime;
                        else
                            overlapEnd = anOverlappedRoom.EndTime;
                        message += " Course : " + anOverlappedRoom.Course.Code + " Start Time : "
                                   + anOverlappedRoom.StartTime + " End Time : "
                                   + anOverlappedRoom.EndTime + " Overlapped from : ";
                        message += overlapStart + " To " + overlapEnd;
                    }

                    TempData["Message"] = message + " on " + day.TimeDay;

                    ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.DepartmentId == course.DepartmentId),
                        "CourseId", "Code", allocateclassroom.CourseId);
                    ViewBag.DayId = new SelectList(db.Days, "DayId", "TimeDay", allocateclassroom.DayId);
                    ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code",
                        allocateclassroom.DepartmentId);
                    ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNo", allocateclassroom.RoomId);

                    return View(allocateclassroom);

                }




                //db.AllocateClassRooms.Add(allocateclassroom);
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", allocateclassroom.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "DayId", "TimeDay", allocateclassroom.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", allocateclassroom.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNo", allocateclassroom.RoomId);
            return View(allocateclassroom);
        }

        private int ConvertTimeFromStringToInt(string stringTime)
        {
            string hour = stringTime[0].ToString() + stringTime[1];
            string minute = stringTime[3].ToString() + stringTime[4];
            int time = (Convert.ToInt32(hour) * 60);
            time += Convert.ToInt32(minute);
            return time;
        }

        // GET: /AllocateClassRoom/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllocateClassRoom allocateclassroom = db.AllocateClassRooms.Find(id);
            if (allocateclassroom == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", allocateclassroom.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "DayId", "TimeDay", allocateclassroom.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", allocateclassroom.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNo", allocateclassroom.RoomId);
            return View(allocateclassroom);
        }

        // POST: /AllocateClassRoom/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AllocateClassRoomId,DepartmentId,CourseId,RoomId,DayId,StartTime,EndTime")] AllocateClassRoom allocateclassroom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allocateclassroom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", allocateclassroom.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "DayId", "TimeDay", allocateclassroom.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", allocateclassroom.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNo", allocateclassroom.RoomId);
            return View(allocateclassroom);
        }

        // GET: /AllocateClassRoom/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllocateClassRoom allocateclassroom = db.AllocateClassRooms.Find(id);
            if (allocateclassroom == null)
            {
                return HttpNotFound();
            }
            return View(allocateclassroom);
        }

        // POST: /AllocateClassRoom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AllocateClassRoom allocateclassroom = db.AllocateClassRooms.Find(id);
            db.AllocateClassRooms.Remove(allocateclassroom);
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
    }
}

using BAIS3230Project.TechnicalServices;
using BAIS3230Project.ViewModel;

namespace BAIS3230Project.Domain
{
    public class BCS
    {
        // Events 
        public bool CreateEvent(Event newEvent)
        {
            bool Confirmation;

            Events EventManager = new();

            Confirmation = EventManager.AddEvent(newEvent);

            return Confirmation;
        }
        public Event FindEventByID(int eventID)
        {
            Event activeEvent;

            Events eventManager = new();

            activeEvent = eventManager.GetEventById(eventID);

            return activeEvent;
        }

        public List<Event> FindAllEvents()
        {
            List<Event> eventsList;

            Events eventManager = new();  
            eventsList = eventManager.GetAllEvents();  
            return eventsList;
        }


        public bool ModifyEvent(Event activeEvent)
        {
            bool Confirmation;

            Events EventManager = new();

            Confirmation = EventManager.UpdateEvent(activeEvent);

            return Confirmation;

        }
        public bool RemoveEvent(int eventID)
        {
            bool Confirmation;

            Events EventManager = new();
            Confirmation = EventManager.DeleteEvent(eventID);

            return Confirmation;
        }

        public List<Event> FindEventsByDate(DateTime date)
        {
            List<Event> eventsOnDate;

            Events EventManager = new();

            eventsOnDate = EventManager.GetEventsByDate(date);

            return eventsOnDate;
        }
        public bool IsTeeTimeBlocked(DateTime date, TimeSpan requestedStartTime, TimeSpan requestedEndTime)
        {
            // Get all events for the selected date
            List<Event> eventsOnDate = FindEventsByDate(date);

            // Check each event's start and end time to see if it overlaps with the requested tee time
            foreach (var ev in eventsOnDate)
            {
                // Check if the event's times overlap with the requested tee time
                if (requestedStartTime < ev.EndTime && requestedEndTime > ev.StartTime)
                {
                    // If there's an overlap, block the requested tee time
                    return true;
                }
            }

            // If no overlaps were found, the tee time is available
            return false;
        }


        // is date blocked by an event

        public bool IsDateBlockedByEvent(DateTime date)
        {
            Events EventManager = new();
            return EventManager.IsDateBlockedByEvent(date);
        }

        // Applications

        public bool CreateApplication(Application newApplication)
        {
            bool Confirmation;

            Applications ApplicationManager = new();

            Confirmation =ApplicationManager.AddApplication(newApplication);

            return Confirmation;
        }

        public Application FindApplicationByID(int applicationID)
        {
            Application activeApplication;

            Applications ApplicationManager = new();

            activeApplication = ApplicationManager.GetApplicationById(applicationID);

            return activeApplication;
        }

        public List<Application> FindAllApplications()
        {
            List<Application> applicationList;

            Applications ApplicationManager = new();
            applicationList = ApplicationManager.GetAllApplications();
            return applicationList;
        }


        public bool ModifyApplication(Application activeApplication)
        {
            bool Confirmation;

            Applications ApplicationManager = new();

            Confirmation = ApplicationManager.UpdateApplication(activeApplication);

            return Confirmation;

        }
        public bool RemoveApplication(int applicationID)
        {
            bool Confirmation;

            Applications ApplicationManager = new();

            Confirmation = ApplicationManager.DeleteApplication(applicationID);

            return Confirmation;
        }



        // StandingTeeTime
        public bool CreateStandingTeeTime(StandingTeeTime newStandingTeeTime)
        {
            bool Confirmation;

            StandingTeeTimes StandingTeeTimeManager = new();

            Confirmation = StandingTeeTimeManager.BookStandingTeeTime(newStandingTeeTime);

            return Confirmation;
        }
        public StandingTeeTime FindStandingTeeTimebyID(int standingTeeTimeID)
        {
            StandingTeeTime activeStandingTeeTime;

            StandingTeeTimes StandingTeeTimeManager = new();

            activeStandingTeeTime = StandingTeeTimeManager.GetStandingTeeTimeById(standingTeeTimeID);

            return activeStandingTeeTime;
        }

        public List<StandingTeeTime> FindAllStandingTeeTimes()
        {
            List<StandingTeeTime> standingTeeTimeList;

            StandingTeeTimes StandingTeeTimeManager = new();
            standingTeeTimeList= StandingTeeTimeManager.GetAllStandingTeeTimes();
            return standingTeeTimeList;
        }


        public bool ModifyStandingTeeTime(StandingTeeTime activeStandingTeeTime)
        {
            bool Confirmation;

            StandingTeeTimes StandingTeeTimeManager = new();

            Confirmation = StandingTeeTimeManager.UpdateStandingTeeTime(activeStandingTeeTime);

            return Confirmation;

        }
        public bool RemoveStandingTeeTime(int standingTeeTimeID)
        {
            bool Confirmation;

            StandingTeeTimes StandingTeeTimeManager = new();
            Confirmation = StandingTeeTimeManager.DeleteStandingTeeTime(standingTeeTimeID);

            return Confirmation;
        }

        // reoccuring standing tee time method
        public bool ReoccurringStandingTeeTime(StandingTeeTime newStandingTeeTime)
        {
            bool Confirmation;

            StandingTeeTimes StandingTeeTimeManager = new();

            Confirmation = StandingTeeTimeManager.CreateReoccurringStandingTeeTime(newStandingTeeTime);

            return Confirmation;
        }

        // TeeTimes

        public bool CreateTeeTime(TeeTime newTeeTime)
        {
            bool Confirmation;

            TeeTimes TeeTimeManager = new();

            Confirmation = TeeTimeManager.BookTeeTime(newTeeTime);

            return Confirmation;
        }
        public TeeTime FindTeeTimebyID(int teeTimeID)
        {
            TeeTime activeTeeTime;

            TeeTimes TeeTimeManager = new();

            activeTeeTime = TeeTimeManager.GetTeeTimeById(teeTimeID);

            return activeTeeTime;
        }

        public List<TeeTime> FindAllTeeTimes()
        {
            List<TeeTime> teeTimeList;

            TeeTimes TeeTimeManager = new();
            teeTimeList = TeeTimeManager.GetAllTeeTimes();
            return teeTimeList;
        }


        public bool ModifyTeeTime(TeeTime activeTeeTime)
        {
            bool Confirmation;

            TeeTimes TeeTimeManager = new();

            Confirmation = TeeTimeManager.UpdateTeeTime(activeTeeTime);

            return Confirmation;

        }
        public bool RemoveTeeTime(int teeTimeID)
        {
            bool Confirmation;

            TeeTimes TeeTimeManager = new();
            Confirmation = TeeTimeManager.DeleteTeeTime(teeTimeID);

            return Confirmation;
        }

        // Get tee times by email (for myreservations)
        public List<TeeTime> FindTeeTimebyEmail(string email)
        {
            List<TeeTime> activeTeeTime;

            TeeTimes TeeTimeManager = new();

            activeTeeTime = TeeTimeManager.GetTeeTimesByEmail(email);

            return activeTeeTime;
        }

        // Member
        public bool CreateMember(Member newMember)
        {
            bool Confirmation;

            Members MemberManager = new();

            Confirmation = MemberManager.AddMember(newMember);

            return Confirmation;
        }

        public List<Member> FindAllMembers()
        {
            List<Member> memberList;

            Members MemberManager = new();
            memberList= MemberManager.GetAllMembers();
            return memberList;
        }

        public Member FindMemberByEmail(string email)
        {
            Member currentMember;

            Members MemberManager = new();
            currentMember = MemberManager.GetMemberByEmail(email);

            return currentMember;
        }
        public string FindMemberAccountStatus(int memberId)
        {
            Members MemberManager = new();
            // Call the repository to get the account status from the stored procedure
            string accountStatus = MemberManager.GetMemberAccountStatus(memberId);

            return accountStatus;
        }

        public Member FindMemberByID(int id)
        {
            Member currentMember;

            Members MemberManager = new();
            currentMember = MemberManager.GetMemberByID(id);

            return currentMember;
        }

        // Employee
        public List<Employee> FindAllEmployees()
        {
            List<Employee> employeeList;

            Employees EmployeeManager = new();
            employeeList= EmployeeManager.GetAllEmployees();
            return employeeList;
        }

        // TeeSheet

        public List<TeeSheet> GetTeeSheetByDate(DateTime teesheetDate)
        {
            List<TeeSheet> teesheet;

            TeeSheets TeesheetManager = new();

            teesheet = TeesheetManager.GetTeeSheetByDate(teesheetDate);
            return teesheet;
        }
        // HELPER METHODS FOR MEMBERSHIPS
        // 3 o fthese- if logged in as bronze, show this, if logged in as gold, show this
        
        public static List<TimeSpan> GetAllowedTimesForDay(DateTime date, string role)
        {
            var allowed = new List<TimeSpan>();
            var isWeekend = date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;

            var current = new TimeSpan(7, 0, 0); // Start at 7:00 AM
            var end = new TimeSpan(20, 0, 0);    // End at 8:00 PM
            bool addSeven = true; // Flag to alternate between 7 and 8 minutes

            while (current <= end)
            {
                if (IsAllowed(role, date.DayOfWeek, current, isWeekend))
                {
                    allowed.Add(current);
                }

                current = current.Add(TimeSpan.FromMinutes(addSeven ? 7 : 8));
                addSeven = !addSeven; // Toggle between 7 and 8 minutes
            }

            return allowed;
        }

        public static bool IsAllowed(string role, DayOfWeek day, TimeSpan time, bool isWeekend)
        {
            // admin or staff permissions
            if (role == "Admin" || role == "Clerk" || role == "ProShop")
            {
                return true;  
            }

            if (role == "Gold")
                return true;

            if (role == "Silver")
            {
                if (isWeekend)
                {
                    return time >= TimeSpan.FromHours(11); // Silver after 11 AM on weekends
                }
                else
                {
                    // Before 3 PM or after 5:30 PM
                    return (time < TimeSpan.FromHours(15)) || (time > TimeSpan.FromHours(17.5));
                }
            }

            if (role == "Bronze")
            {
                if (isWeekend)
                {
                    return time >= TimeSpan.FromHours(13); // Bronze after 1 PM on weekends
                }
                return time < TimeSpan.FromHours(15) || time > TimeSpan.FromHours(18); // Before 3 PM or after 6 PM for weekdays
            }

            return false;
        }

    }
}

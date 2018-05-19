import { Component, OnInit, TemplateRef,ViewChild } from '@angular/core';
import {
  startOfDay,
  endOfDay,
  subDays,
  addDays,
  endOfMonth,
  isSameDay,
  isSameMonth,
  addHours
} from 'date-fns';
import {
  CalendarEvent,
  CalendarEventAction,
  CalendarEventTimesChangedEvent,
  DAYS_OF_WEEK,
} from 'angular-calendar';

const colors: any = {
  red: {
    primary: '#ad2121',
    secondary: '#FAE3E3'
  },
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF'
  },
  yellow: {
    primary: '#e3bc08',
    secondary: '#FDF1BA'
  }
};
import { TimeSpentService, TimeSpent } from '../timespent.service';
import { LoginService, AuthenticatedUser } from '../login.service';
import {Router} from "@angular/router";
@Component({
  selector: 'app-user-completed-tasks',
  templateUrl: './user-completed-tasks.component.html',
  styleUrls: ['./user-completed-tasks.component.scss'],
  providers: [TimeSpentService]
})
export class UserCompletedTasksComponent implements OnInit {

  viewDate: Date = new Date();
  activeDayIsOpen: boolean = true;
  view = 'month';
  weekStartsOn: number = DAYS_OF_WEEK.MONDAY;

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
        this.viewDate = date;
      }
    }
  }

  events: CalendarEvent[] = [
  ];

  modalData: {
    action: string;
    event: CalendarEvent;
  };

  constructor(
    private login: LoginService,
    private timeSpentService: TimeSpentService,
    private router: Router) {

      this.login.userSubscription.subscribe(result => {
        if (result !== null && result !== undefined) {
          this.getEvents(this.viewDate);
        }
      });
  
   }

   handleEvent(event: any): void {
    this.router.navigate(['/problem', event.problemId]);
  }

   getEvents(date:Date) {
    var userId = this.login.getAuthenticatedUser().id;
    this.timeSpentService.getUserTimeSpentPeriod(userId,date.toISOString()).subscribe(result=>{
      var events = new Array<CalendarEvent>();
      console.log(result);
      for (let entry of result) {
        var event = {
          start: new Date(entry.dateRecorded),
          title: `Problema: ${entry.problemName}. Atlikti darbai: ${entry.description}. Laikas: ${entry.hoursSpent} valandos`,
          problemId: entry.problemId
        };
        events.push(event);
    }
    this.events = events;
    });
   }

   changedMonth() {
     this.activeDayIsOpen =false;
     this.getEvents(this.viewDate); 
   }

  ngOnInit() {
  }

}
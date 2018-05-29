import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HttpModule } from '@angular/http';
import {HttpClientModule, HttpClient} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';

import { AppComponent } from './app.component';
import { LoginComponent } from './login-component/login.component';

import { LoginService } from './login.service';
import { UrlService } from './url.service';
import { CalendarModule } from 'angular-calendar';
import {CdkTableModule} from '@angular/cdk/table';
import {
  MatAutocompleteModule,
  MatButtonModule,
  MatButtonToggleModule,
  MatCardModule,
  MatCheckboxModule,
  MatChipsModule,
  MatDatepickerModule,
  MatDialogModule,
  MatDividerModule,
  MatExpansionModule,
  MatGridListModule,
  MatIconModule,
  MatInputModule,
  MatListModule,
  MatMenuModule,
  MatNativeDateModule,
  MatPaginatorModule,
  MatProgressBarModule,
  MatProgressSpinnerModule,
  MatRadioModule,
  MatRippleModule,
  MatSelectModule,
  MatSidenavModule,
  MatSliderModule,
  MatSlideToggleModule,
  MatSnackBarModule,
  MatSortModule,
  MatStepperModule,
  MatTableModule,
  MatTabsModule,
  MatToolbarModule,
  MatTooltipModule,
} from '@angular/material';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { FlexLayoutModule } from '@angular/flex-layout';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ProblemListComponent } from './problem-list/problem-list.component';
import { ProblemListCompactComponent } from './problem-list-compact/problem-list-compact.component';
import { CreateProblemComponent } from './create-problem/create-problem.component';
import { ViewProblemComponent } from './view-problem/view-problem.component';
import { ChartsModule } from 'ng2-charts';
import { UsersListComponent } from './users-list/users-list.component';
import { InternetUserListComponent } from './internet-user-list/internet-user-list.component';
import { CreateProblemDialogComponent } from './create-problem-dialog/create-problem-dialog.component';
import { InternetUserInformationComponent } from './internet-user-information/internet-user-information.component';
import { UserCompletedTasksComponent } from './user-completed-tasks/user-completed-tasks.component';
import { ViewSystemUserComponent } from './view-system-user/view-system-user.component';
import { ReportsComponentComponent } from './reports-component/reports-component.component';
import { CreateInterentuserDialogComponent } from './create-interentuser-dialog/create-interentuser-dialog.component';
import { CreateSystemuserDialogComponent } from './create-systemuser-dialog/create-systemuser-dialog.component';

const appRoutes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'login' },
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'problemList', component: ProblemListComponent },
  { path: 'usersList', component: UsersListComponent },
  { path: 'internetUsersList', component: InternetUserListComponent },
  { path: 'createProblem', component: CreateProblemComponent },
  { path: 'userTasks', component: UserCompletedTasksComponent },
  { path: 'reports', component: ReportsComponentComponent },
  { path: 'problem/:id', component: ViewProblemComponent },
  { path: 'internetUser/:id', component: InternetUserInformationComponent },
  { path: 'systemUser/:id', component: ViewSystemUserComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DashboardComponent,
    ProblemListComponent,
    CreateProblemComponent,
    ViewProblemComponent,
    UsersListComponent,
    InternetUserListComponent,
    CreateProblemDialogComponent,
    InternetUserInformationComponent,
    UserCompletedTasksComponent,
    ViewSystemUserComponent,
    ReportsComponentComponent,
    ProblemListCompactComponent,
    CreateInterentuserDialogComponent,
    CreateSystemuserDialogComponent
  ],
  imports: [
    ChartsModule,
    HttpClientModule,
    FlexLayoutModule,
    RouterModule.forRoot(appRoutes, { enableTracing: true }),
    BrowserModule,
    HttpModule,
    FormsModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    CdkTableModule,
    MatAutocompleteModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatStepperModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
    CalendarModule.forRoot() 
  ],
  providers: [UrlService, LoginService],
  entryComponents: [
    CreateProblemDialogComponent,
    CreateInterentuserDialogComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

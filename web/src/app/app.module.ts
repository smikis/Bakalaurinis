import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HttpModule } from '@angular/http';
import {HttpClientModule, HttpClient} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';

import { AppComponent } from './app.component';
import { LoginComponent } from './login-component/login.component';

// Angular Material Components
import {MatInputModule} from '@angular/material/input';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatRadioModule} from '@angular/material/radio';
import {MatSelectModule} from '@angular/material/select';
import {MatSliderModule} from '@angular/material/slider';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatMenuModule} from '@angular/material/menu';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatListModule} from '@angular/material/list';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatCardModule} from '@angular/material/card';
import {MatStepperModule} from '@angular/material/stepper';
import {MatTabsModule} from '@angular/material/tabs';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import {MatChipsModule} from '@angular/material/chips';
import {MatIconModule} from '@angular/material/icon';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {MatDialogModule} from '@angular/material/dialog';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatTableModule} from '@angular/material/table';
import {MatSortModule} from '@angular/material/sort';
import {MatPaginatorModule} from '@angular/material/paginator';

import { LoginService } from './login.service';
import { UrlService } from './url.service';
import { CalendarModule } from 'angular-calendar';
import {CdkTableModule} from '@angular/cdk/table';
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
import { MyProblemListComponent } from './my-problem-list/my-problem-list.component';
import { DeviceListComponent } from './device-list/device-list.component';
import { UpdateInternetuserDialogComponent } from './update-internetuser-dialog/update-internetuser-dialog.component';
import { UpdateUserDialogComponent } from './update-user-dialog/update-user-dialog.component';
import { CreateDeviceDialogComponent } from './create-device-dialog/create-device-dialog.component';

const appRoutes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'login' },
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'problemList', component: ProblemListComponent },
  { path: 'myProblemList', component: MyProblemListComponent },
  { path: 'usersList', component: UsersListComponent },
  { path: 'internetUsersList', component: InternetUserListComponent },
  { path: 'createProblem', component: CreateProblemComponent },
  { path: 'userTasks', component: UserCompletedTasksComponent },
  { path: 'reports', component: ReportsComponentComponent },
  { path: 'devices', component: DeviceListComponent },
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
    CreateSystemuserDialogComponent,
    MyProblemListComponent,
    DeviceListComponent,
    UpdateInternetuserDialogComponent,
    UpdateUserDialogComponent,
    CreateDeviceDialogComponent
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
    CalendarModule.forRoot(),
    BrowserModule,
    BrowserAnimationsModule,
    MatInputModule,
    MatAutocompleteModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatRadioModule,
    MatSelectModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatMenuModule,
    MatSidenavModule,
    MatToolbarModule,
    MatListModule,
    MatGridListModule,
    MatCardModule,
    MatStepperModule,
    MatTabsModule,
    MatExpansionModule,
    MatButtonToggleModule,
    MatChipsModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatProgressBarModule,
    MatDialogModule,
    MatTooltipModule,
    MatSnackBarModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
  ],
  providers: [UrlService, LoginService],
  entryComponents: [
    CreateProblemDialogComponent,
    CreateInterentuserDialogComponent,
    CreateSystemuserDialogComponent,
    UpdateInternetuserDialogComponent,
    UpdateUserDialogComponent,
    CreateDeviceDialogComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

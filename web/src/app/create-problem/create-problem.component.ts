import { Component, OnInit } from '@angular/core';
import { Tag} from '../tag.service';
import {MatChipInputEvent} from '@angular/material';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/startWith';
import { Subscription } from 'rxjs/Rx';
import 'rxjs/add/operator/toPromise';
import {map, startWith} from 'rxjs/operators';
import {InternetUserService, InternetUser} from '../internet.user.service';
import {UserService, User} from '../user.service';
import {ENTER, COMMA} from '@angular/cdk/keycodes';
import {ProblemService, Problem, CreateProblem} from '../problem.service';
@Component({
  selector: 'app-create-problem',
  templateUrl: './create-problem.component.html',
  styleUrls: ['./create-problem.component.css'],
  providers: [InternetUserService,UserService, ProblemService]
})
export class CreateProblemComponent implements OnInit {
  automaticallyAssign = false;

  myform: FormGroup;

  internetUserCtrl: FormControl;
  filteredInternetUsers: Subscription;
  internetUsers: InternetUser[];
  selectedInternetUser: InternetUser;

  userCtrl: FormControl;
  filteredUsers: Subscription;
  users: User[];
  selectedUser: User;

  tags = new Array<string>();
  visible: boolean = true;
  selectable: boolean = true;
  removable: boolean = true;
  addOnBlur: boolean = true;
  separatorKeysCodes = [ENTER, COMMA];

  constructor(private internetUserService: InternetUserService, private userService: UserService, private problemService: ProblemService) {     
    this.internetUserCtrl = new FormControl();
    this.filteredInternetUsers = this.internetUserCtrl.valueChanges
    .startWith(null)
    .debounceTime(400)
       .do(val => {
         internetUserService.searchInternetUsers(val)
         .toPromise()
         .then(res => {
           this.internetUsers = res
         })
       })
       .subscribe();

       this.userCtrl = new FormControl();
       this.filteredUsers = this.userCtrl.valueChanges
       .startWith(null)
       .debounceTime(400)
          .do(val => {
            userService.searchUsers(val)
            .toPromise()
            .then(res => {
              this.users = res
            })
          })
          .subscribe();

    }

      changedInternetUser(user:InternetUser) {
        this.myform.controls['location'].setValue(user.location);
        this.selectedInternetUser = user;
      }

      changedUser(user:User) {
        this.selectedUser = user;
      }
      
  ngOnInit() {
    this.myform = new FormGroup({
      name: new FormControl('', Validators.required),
      status: new FormControl('1', Validators.required),
      category: new FormControl('1', Validators.required),
      description: new FormControl('', Validators.required),
      location: new FormControl('',Validators.required)
  });
  }

  submit() {
    var problem = new CreateProblem();
    problem.assignedUser = this.selectedUser? this.selectedUser.id: null;
    problem.internetUserId = this.selectedInternetUser? this.selectedInternetUser.id: null;
    problem.category = +this.myform.controls['category'].value;
    problem.description = this.myform.controls['description'].value;
    problem.location = this.myform.controls['location'].value;
    problem.name = this.myform.controls['name'].value;
    problem.statusId = +this.myform.controls['status'].value;
    problem.tags = this.tags;
    console.log(problem);
    this.problemService.createProblem(problem).subscribe(result=> {
      console.log(result);
    })
  }

  add(event: MatChipInputEvent): void {
    let input = event.input;
    let value = event.value;

    if ((value || '').trim()) {
      this.tags.push(value);
    }

    // Reset the input value
    if (input) {
      input.value = '';
    }
  }

  remove(tag: string): void {
    let index = this.tags.indexOf(tag);

    if (index >= 0) {    
      this.tags.splice(index, 1);
    }
  }

}

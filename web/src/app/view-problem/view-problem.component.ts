import { Component, OnInit, } from '@angular/core';
import { MatTableDataSource, MatChipInputEvent } from '@angular/material';
import { ENTER, COMMA } from '@angular/cdk/keycodes';
import { ActivatedRoute, Router } from '@angular/router';
import { ProblemService, Problem } from '../problem.service';
import { InternetUserService, InternetUser } from '../internet.user.service';
import { CommentService, Comment } from '../comment.service';
import { LoginService, AuthenticatedUser } from '../login.service';
import { ValidatorService } from '../cutom-material-table/validator.service';
import { FormsModule, ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { TimeSpentTableDataSource } from '../cutom-material-table/timespent-table-data-source';
import { TimeSpentService, TimeSpent, CreateTimeSpent } from '../timespent.service';
import { TagService, Tag } from '../tag.service';
import { UserService, User } from '../user.service';
import { Subscription } from 'rxjs/Rx';

export class TimeSpentValidatorService implements ValidatorService {
  getRowValidator(): FormGroup {
    return new FormGroup({
      'hoursSpent': new FormControl(null, Validators.required),
      'description': new FormControl(null, Validators.required),
      'firstName': new FormControl(null),
      'created': new FormControl(null)
    });
  }
}

@Component({
  selector: 'app-view-problem',
  templateUrl: './view-problem.component.html',
  styleUrls: ['./view-problem.component.css'],
  providers: [ProblemService, InternetUserService, CommentService, TimeSpentService, TagService, UserService,
    { provide: ValidatorService, useClass: TimeSpentValidatorService }]
})
export class ViewProblemComponent implements OnInit {
  problemId: number;
  problem: Problem;
  status: string;
  description: string;
  internetUser: InternetUser;
  editAssignedUser = false;
  editStatus = false;
  editDescription = false;

  comments: Array<Comment>;
  commentText: string = null;

  timeSpent: TimeSpent[];
  displayedColumns = ['firstName', 'hoursSpent', 'created', 'description', 'actionsColumn'];
  dataSource: TimeSpentTableDataSource;

  tags: Tag[];

  users: User[];
  userCtrl: FormControl;
  filteredUsers: Subscription;
  selectedUser: User;

  constructor(private route: ActivatedRoute,
    private problemService: ProblemService,
    private commentService: CommentService,
    private internetUserService: InternetUserService,
    private router: Router,
    private login: LoginService,
    private sourcesValidator: ValidatorService,
    private tagService: TagService,
    private userService: UserService,
    private timeSpentService: TimeSpentService) {

    this.userCtrl = new FormControl({ value: '', disabled: true });
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

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.problemId = params['id'];


      //Get problem data
      this.problemService.getProblem(this.problemId).subscribe(data => {
        this.problem = data;
        this.status = data.statusId.toString();
        if (this.problem.internetUserId !== undefined) {
          this.internetUserService.getInternetUser(this.problem.internetUserId).subscribe(data => {
            this.internetUser = data;
          });
        }

      },
        error => {
          this.redirecOnError();
        });

      //Get problem comments
      this.commentService.getProblemComments(this.problemId).subscribe(commentData => {
        this.comments = commentData;
      });

      //Get problem time spent
      this.timeSpentService.getProblemTimeSpent(this.problemId).subscribe(data => {
        this.timeSpent = data;
        this.dataSource = new TimeSpentTableDataSource(data, this.timeSpentService, this.login, this.problemId, TimeSpent, this.sourcesValidator)
      });

      //Get tags
      this.tagService.getProblemTags(this.problemId).subscribe(data => {
        this.tags = data;
      });
    },
      error => {
        this.redirecOnError();
      }
    );
  }

  redirecOnError() {
    console.log("Error getting param value");
    this.router.navigate['/dashboard'];
  }

  sendComment() {
    var firstName = this.login.getAuthenticatedUser().firstName;
    var lastName = this.login.getAuthenticatedUser().lastName;
    var userId = this.login.getAuthenticatedUser().id;
    var comment = new Comment();
    comment.text = this.commentText;
    comment.firstName = firstName;
    comment.lastName = lastName;

    console.log(this.commentText);
    this.commentService.createComment(this.commentText, userId, this.problemId).subscribe(result => {
      this.commentText = null;
      this.comments.push(comment);
    });

  }

  confirmUserEdit() {
    this.problemService.updateProblemAssignedUser(this.problemId, this.selectedUser.id).subscribe(result => {
      console.log(result);
    });
  }


  setStatus() {
    this.problemService.updateProblemStatus(this.problemId, this.status).subscribe(result => {
      console.log(result);
    });
    console.log(this.status);
  }

  setDescription() {
    this.problemService.updateProblemDescription(this.problemId, this.description).subscribe(result => {
      console.log(result);
    });
    console.log(this.description);
  }

  closeProblem() {
    this.status = '3';
    this.setStatus();
  }

  openProblem() {
    this.status = '2';
    this.setStatus();
  }

  visible: boolean = true;
  selectable: boolean = true;
  removable: boolean = true;
  addOnBlur: boolean = true;

  // Enter, comma
  separatorKeysCodes = [ENTER, COMMA];

  add(event: MatChipInputEvent): void {
    let input = event.input;
    let value = event.value;

    if ((value || '').trim()) {

      this.tagService.createProblemTag(value, this.problemId).subscribe(result => {
        this.tags.push(result);
      });
    }

    // Reset the input value
    if (input) {
      input.value = '';
    }
  }

  remove(tag: Tag): void {
    let index = this.tags.indexOf(tag);

    if (index >= 0) {
      this.tagService.delete(tag.id).subscribe(result => {
      });
      this.tags.splice(index, 1);
    }
  }

}


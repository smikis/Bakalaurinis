import { Component, OnInit, } from '@angular/core';
import {MatTableDataSource, MatChipInputEvent} from '@angular/material';
import {ENTER, COMMA} from '@angular/cdk/keycodes';
import {ActivatedRoute, Router} from '@angular/router';
import {ProblemService, Problem} from '../problem.service';
import {InternetUserService, InternetUser} from '../internet.user.service';
import {CommentService, Comment} from '../comment.service';
import { LoginService, AuthenticatedUser } from '../login.service';

@Component({
  selector: 'app-view-problem',
  templateUrl: './view-problem.component.html',
  styleUrls: ['./view-problem.component.css'],
  providers:[ProblemService,InternetUserService, CommentService]
})
export class ViewProblemComponent implements OnInit {
  problemId : number;
  problem: Problem;
  internetUser: InternetUser;
  editAssignedUser = false;
  editInternetUser = false;
  editStatus = false;

  comments: Array<Comment>;
  commentText: string = null;

  displayedColumns = ['position', 'name', 'weight', 'symbol'];
  dataSource = new MatTableDataSource<Element>(ELEMENT_DATA);
  constructor(private route:ActivatedRoute, private problemService: ProblemService, 
    private commentService: CommentService, private internetUserService: InternetUserService,  private router: Router,private login: LoginService) { }

  ngOnInit() {
    this.route.params.subscribe( params => {
      this.problemId = params['id'];

        this.problemService.getProblem(this.problemId).subscribe(data=> {
            this.problem = data;
            
            if(this.problem.internetUserId!== undefined) {
              this.internetUserService.getInternetUser(this.problem.internetUserId).subscribe(data=> {
                this.internetUser = data;
              });
            }

        },
      error=> {
        this.redirecOnError();
      });

      this.commentService.getProblemComments(this.problemId).subscribe(commentData=> {
          this.comments = commentData;
          console.log(this.comments);
      });

    },
    error=> {
      this.redirecOnError();
    } 
  );
  }

  redirecOnError(){
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
    this.commentService.createComment(this.commentText,userId, this.problemId).subscribe(result=> {
      this.commentText = null;
      this.comments.push(comment);
    });

  }

  visible: boolean = true;
  selectable: boolean = true;
  removable: boolean = true;
  addOnBlur: boolean = true;

  // Enter, comma
  separatorKeysCodes = [ENTER, COMMA];


  fruits = [
    { name: 'Lemon' },
    { name: 'Lime' },
    { name: 'Apple' },
  ];


  add(event: MatChipInputEvent): void {
    let input = event.input;
    let value = event.value;

    // Add our fruit
    if ((value || '').trim()) {
      this.fruits.push({ name: value.trim() });
    }

    // Reset the input value
    if (input) {
      input.value = '';
    }
  }

  remove(fruit: any): void {
    let index = this.fruits.indexOf(fruit);

    if (index >= 0) {
      this.fruits.splice(index, 1);
    }
  }

}

export interface Element {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

const ELEMENT_DATA: Element[] = [
  {position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H'},
  {position: 2, name: 'Helium', weight: 4.0026, symbol: 'He'},
  {position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li'},
  {position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be'}
];

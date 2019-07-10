import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
users: any;
  constructor(private Http: HttpClient) { }

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    this.Http.get('http://localhost:5000/api/users/').subscribe(response => {
      this.users = response;
    }, error => {
      console.log(error);
    });
  }

}

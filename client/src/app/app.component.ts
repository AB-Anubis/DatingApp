import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = "The Dating App";
  users: any;

  constructor(private accountService: AccountService) {}

  ngOnInit() {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const value = localStorage.getItem('user');
    if (typeof value === 'string') {
      const user: User = JSON.parse(value);
      if (user) {
        this.accountService.setCurrentUser(user);
      }
    }
  }


}

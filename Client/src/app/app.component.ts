import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgFor } from '@angular/common';
import { NavComponent } from "./nav/nav.component";
import { HomeComponent } from "./home/home.component";
import { AccountServiceService } from './_Services/account-service.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgFor, NavComponent, HomeComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'Client';
  myaccountservice=inject(AccountServiceService)
  ngOnInit(): void {
  this.setCurrentUser()
  }


  setCurrentUser() {

    const userstring=localStorage.getItem('user');
    if(!userstring) return;
    const user=JSON.parse(userstring);
    this.myaccountservice.setcurrentuser(user);


  }







}

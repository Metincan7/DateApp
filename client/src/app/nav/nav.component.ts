import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model:any={};
  logeedIn=false;
  constructor(private accountService:AccountService) {}
  ngOnInit():void{

  }
  login()
  {
    this.accountService.login(this.model).subscribe({
      next:response =>{
        console.log(response);
        this.logeedIn=true;
      },
      error:error =>console.log(error)
    })
  }
  logout()
  {
    this.logeedIn=false;
  }

}

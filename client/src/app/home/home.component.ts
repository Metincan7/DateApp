import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { every } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit{
registerMode=false;
constructor(private http:HttpClient ) { }
users :any;

ngOnInit(): void {
  this.getUsers();
  
}
registerToggle()
  {
      this.registerMode=!this.registerMode;

  }
  getUsers(){
    this.http.get('https://localhost:7193/api/user').subscribe(
      {
        next:response =>this.users=response,
        error:error=>console.log(error),
        complete:() => console.log('Process has been success.')
      }
    )
  }
  cancelRegisterModel(event:boolean){
    this.registerMode=event;
  }
}

import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit
{
  constructor(private http:HttpClient)
  {}
  ngOnInit(): void {
    this.http.get('https://localhost:7193/api/user').subscribe(
      {
        next:response =>this.users=response,
        error:error=>console.log(error),
        complete:() => console.log('Process has been success.')
      }
    )
  }
  title = 'client';
  users :any;
}

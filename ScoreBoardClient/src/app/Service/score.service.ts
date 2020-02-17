import { Injectable, EventEmitter } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { ScoreViewModel } from '../Model/score-view-model';
import {HttpClientModule, HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ScoreService {

  private hubConnection: signalR.HubConnection;
  recivedSignal = new EventEmitter<ScoreViewModel>();

  constructor(private http: HttpClient) { 
    this.buildConnection();
    this.startConnection();
  }
  public buildConnection =() =>{
    this.hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:44310/signalHub")
    .build();
  };

  public startConnection=() =>{
    this.hubConnection
    .start()
    .then(() => {
      console.log("Connection started!");
      this.registerSignalEvent();
    })
    .catch(err => {
      console.log("shit happen..."+ err);
      //retry
      setTimeout(function(){this.startConnection();},3000);
    });
  };

  private registerSignalEvent(){
    this.hubConnection.on("SignalMessageRecieved",(data: ScoreViewModel) =>{
      this.recivedSignal.emit(data);
      console.log(data)
    })
  }

  public Create(userModel : ScoreViewModel) : Observable<ScoreViewModel>
  {
    return this.http.post<ScoreViewModel>("https://localhost:44310/ScoreBoard",userModel);
  }
}
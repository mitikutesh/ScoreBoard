import { Injectable, EventEmitter } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { ScoreViewModel } from '../Model/score-view-model';


@Injectable({
  providedIn: 'root'
})
export class ScoreService {

  private hubConnection: signalR.HubConnection;
  recivedSignal = new EventEmitter<ScoreViewModel>();

  constructor() { 
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
    })
  }
}
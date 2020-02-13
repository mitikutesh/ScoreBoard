import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { ScoreModel } from '../model/score';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  public data: ScoreModel[];
  public bradcastedData: ScoreModel[];

  constructor() { }

  private hubConnection: signalR.HubConnection

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/score')
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  public addTransferChartDataListener = () => {
    this.hubConnection.on('transferdata', (data) => {
      this.data = data;
      console.log(data);
    });
  }
  public broadcastChartData = () => {
    this.hubConnection.invoke('broadcasttdata', this.data)
      .catch(err => console.error(err));
  }

  public addBroadcastChartDataListener = () => {
    this.hubConnection.on('broadcastdata', (data) => {
      this.bradcastedData = data;
    })
  }
}


import { Component, OnInit } from '@angular/core';
import { ScoreViewModel } from './Model/score-view-model';
import { ScoreService } from './Service/score.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'ScoreBoardClient';
  scoreList: ScoreViewModel[]=[];
  
  constructor(private scoreService  : ScoreService){}

  ngOnInit(){
    this.scoreService.recivedSignal.subscribe((score: ScoreViewModel) => {
      this.scoreList.push(score);
      console.log(score);
    })
  }
 

}

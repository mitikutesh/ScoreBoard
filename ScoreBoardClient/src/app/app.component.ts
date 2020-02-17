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
  // scoreList: ScoreViewModel[]=[];
  topscore : ScoreViewModel=null;
  userScore= new ScoreViewModel('TEst',2,'tete');
 
  constructor(private scoreService  : ScoreService){}

  ngOnInit(){
    this.scoreService.recivedSignal.subscribe((score: ScoreViewModel) => {
      // this.scoreList.push(score);
      this.topscore = score;
      // console.log(score);
    })

  }



  onSubmit() {

   
    this.scoreService.Create(this.userScore).subscribe(data => {
      console.log(this.userScore);
    });

   
  }
}

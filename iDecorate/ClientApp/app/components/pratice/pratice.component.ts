import { Component, OnInit, Inject, Renderer } from '@angular/core';
import { Topic } from '../models/Topic';
import { Word } from '../models/Word';
import { WordExpresions } from '../models/WordExpresions';
import { WordExpressionsModel } from '../models/WordExpressionsModel';
import { Http } from "@angular/http";
import { FormGroup, FormBuilder, FormControl, Validators } from "@angular/forms";

@Component({
  selector: 'pratice',
  templateUrl: './pratice.component.html',
  styleUrls: ['./pratice.component.css']
})
export class PraticeComponent implements OnInit {

  public endRequest: boolean = false;
  public modelTopic: Topic = new Topic();
  public topics: Array<Topic> = new Array<Topic>();
  public wordExpresions: Array<WordExpressionsModel> = new Array<WordExpressionsModel>();
  public praticeWords: Array<WordExpressionsModel> = new Array<WordExpressionsModel>();
  public toImprove: Array<WordExpressionsModel> = new Array<WordExpressionsModel>();
  public chosen: WordExpressionsModel = new WordExpressionsModel();
  public isPratice: boolean = false;
  public isDone: boolean = false;
  public isChecking: boolean = false;
  public correctInTheFirstTime: boolean = true;
  public correct: boolean = false;
  public valueDisplay: string = '';
  public valueDisplayMessage: string;
  public _correctAnswers: number = 0;
  public _wrongAnswers: number = 0;
  public _total: number = 0;
  private _baseUrl: string = '';
  private _http: Http;
  formPratice: FormGroup;
  formTopic: FormGroup;

  constructor(http: Http, @Inject('BASE_URL') baseUrl: string, private formBuilder: FormBuilder, private renderer: Renderer) {
    this._http = http;
    this._baseUrl = baseUrl;
    this.getTopics();
  }

  getTopics() {
    this.wordExpresions = [];
    this.topics = [];
    this.endRequest = false;
    this._http.get(this._baseUrl + 'api/Topic').subscribe(result => {

      let resultObject = result.json() as Array<Topic>;

      resultObject.map(elementTopic => {
        let topic = new Topic();
        topic.words = new Array<Word>();
        topic.id = elementTopic.id;
        topic.description = elementTopic.description;
        elementTopic.words.map(elementWord => {
          let word = new Word();
          word.id = elementWord.id;
          word.description = elementWord.description;
          word.meaning = elementWord.meaning;
          topic.words.push(word);
        });
        if(elementTopic.words.length > 0) this.topics.push(topic);
      });

      this.topics.forEach(topic => {
        topic.words.forEach(word => {
          let wordExpresion = new WordExpressionsModel();
          wordExpresion.topic_id = topic.id;
          wordExpresion.word_id = word.id;
          wordExpresion.topic_description = topic.description;
          wordExpresion.word_description = word.description;
          wordExpresion.word_meaning = word.meaning;
          this.wordExpresions.push(wordExpresion);
        });
      });

      this.topics.forEach(topic => {
        topic.words.forEach(word => {
          let wordExpresion = new WordExpressionsModel();
          wordExpresion.topic_id = topic.id;
          wordExpresion.word_id = word.id;
          wordExpresion.topic_description = topic.description;
          wordExpresion.word_description = word.meaning;
          wordExpresion.word_meaning = word.description;
          this.wordExpresions.push(wordExpresion);
        });
      });

      this.endRequest = true;

      this.onResetTopic();
    }, error => {
      console.error(error);
    });
  }

  ngOnInit() {
    this.buildFormTopic();
  }

  onSubmitTopic() {

    this.praticeWords = this.wordExpresions.filter(t => { return t.topic_id == this.formTopic.value.id; });

    if (this.praticeWords.length > 0) {

      this.isPratice = true;

      this._total = this.praticeWords.length;

      this.chosenWord();
    }
  }

  onResetTopic() {
    this.modelTopic = new Topic();
    this.buildFormTopic();
  }

  buildFormTopic() {
    this.formTopic = this.formBuilder.group({
      id: ['selected', Validators.required],
      description: [''],
    });

    this.formPratice = this.formBuilder.group({
      valueText: ['']
    });
  }

  donePratice() {
    this.isPratice = false;
    this.praticeWords = [];
    this.isChecking = false;
    this.valueDisplayMessage = '';
    this.isDone = false;
    this._correctAnswers = 0;
    this._wrongAnswers = 0;
  }

  checkPratice() {

    this.correct = this.formPratice.value.valueText.toUpperCase() === this.chosen.word_meaning.toUpperCase();

    if (this.correct) {
      this.isChecking = true;
      if (this.correctInTheFirstTime) {
        this._correctAnswers++;
      }
      this.correctInTheFirstTime = true;
      this.valueDisplayMessage = 'Correct! (' + this.chosen.word_meaning + ')';
      setTimeout(() => {
        if (this.praticeWords.length > 0) {
          this.chosenWord();
          this.isChecking = false;
          this.valueDisplayMessage = '';
          this.isDone = false;
        }
        else {
          this.isDone = true;
          this.valueDisplayMessage = '';
        }
        this.formPratice = this.formBuilder.group({
          valueText: ['']
        });
      }, 2000);
    } else {
      if (!this.toImprove.some((item) => { return item.word_description == this.chosen.word_description })) {
        this.toImprove.push(this.chosen);
        this._wrongAnswers++;
      }
      this.correctInTheFirstTime = false;
      this.valueDisplayMessage = "Wrong, try again!";
      this.formPratice = this.formBuilder.group({
        valueText: ['']
      });
    }
  }

  chosenWord() {
    var x = Math.floor((Math.random() * this.praticeWords.length) + 1) - 1;
    this.chosen = this.praticeWords[x];
    this.valueDisplay = this.chosen.word_description;
    this.praticeWords.splice(x, 1);
  }
}
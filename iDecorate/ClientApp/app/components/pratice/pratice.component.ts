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
  public modelWord: WordExpressionsModel = new WordExpressionsModel();
  public modelTopic: Topic = new Topic();
  public topics: Array<Topic> = new Array<Topic>();
  public words: Array<Word> = new Array<Word>();
  public wordExpresions: Array<WordExpressionsModel> = new Array<WordExpressionsModel>();
  public onEditionWord: boolean = false;
  public onEditionTopic: boolean = false;
  private _baseUrl: string = '';
  private _http: Http;
  formWordExpression: FormGroup;
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
        this.topics.push(topic);
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

    let topic = this.topics.find(t =>{ return t.id == this.formTopic.value.id; });

    if(topic)
    topic.words.forEach((element) => {
      this.words.push(element);
    });

    // this.topics.forEach((element) => {
    //   this.topicsCopy.push([element[1], element[0]]);
    // });

    console.error(this.words);
  }

  onResetTopic() {
    this.modelTopic = new Topic();
    this.onEditionTopic = false;
    this.buildFormTopic();
  }

  buildFormTopic() {
    this.formTopic = this.formBuilder.group({
      id: ['selected', Validators.required],
      description: [''],
    });
  }

  private prepareRequestTopic(): Topic {
    const formModel = this.formTopic.value;

    const saveTopic: Topic = {
      id: this.modelTopic.id,
      description: formModel.description,
      words: this.modelTopic.words
    };
    return saveTopic;
  }
}
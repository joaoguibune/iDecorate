import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Http, Headers, RequestOptions, RequestOptionsArgs } from '@angular/http';
import { Topic } from '../models/Topic';
import { Word } from '../models/Word';
import { WordExpresions } from '../models/WordExpresions';
import { WordExpressionsModel } from '../models/WordExpressionsModel';
import { Observable } from 'rxjs/Observable';

@Component({
    selector: 'word-expressions',
    templateUrl: './word-expressions.component.html',
    styleUrls: ['./word-expressions.component.css']
})
export class WordExpressionsComponent implements OnInit {
    public endRequest: boolean = false;
    public model: WordExpressionsModel = new WordExpressionsModel();
    public topics: Array<Topic> = new Array<Topic>();
    public wordExpresions: Array<WordExpresions> = new Array<WordExpresions>();
    private _baseUrl: string = '';
    private _http: Http;
    formWordExpression: FormGroup;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        this._baseUrl = baseUrl;
        this._http = http;
        this.getTopics();
    }

    getTopics() {
        this._http.get(this._baseUrl + 'api/Topic').subscribe(result => {

            let resultObject = result.json() as Array<Topic>;

            this.topics = [];
            this.wordExpresions = [];

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

            this.topics.forEach(topic => {
                topic.words.forEach(word => {
                    let wordExpresion = new WordExpresions();
                    wordExpresion.topic = topic.description;
                    wordExpresion.word = word.description;
                    wordExpresion.meaning = word.meaning;
                    this.wordExpresions.push(wordExpresion);
                });
            });

            this.endRequest = true;
        }, error => {
            console.error(error);
        });
    }

    ngOnInit() {
        this.formWordExpression = new FormGroup({
            'topic_id': new FormControl(this.model.topic_id, [
                Validators.required,

            ]),
            'description': new FormControl(this.model.description, [
                Validators.required
            ]),
            'meaning': new FormControl(this.model.meaning, [
                Validators.required
            ])
        });
        this.formWordExpression.setValue({ topic_id: 'selected', description: '', meaning: '' });
    }

    onSubmit() {
        this._http.post(this._baseUrl + "api/Word", this.model).subscribe(result => {
            this.onReset();
            this.model = new WordExpressionsModel();
            this.getTopics();
        }, error => {
            console.error(error);
        });
    }

    onReset() {
        this.formWordExpression.reset({});
    }
}
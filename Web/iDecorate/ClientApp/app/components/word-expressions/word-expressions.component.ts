import { Component, OnInit, Inject, Renderer } from '@angular/core';
import { Topic } from '../models/Topic';
import { Word } from '../models/Word';
import { WordExpresions } from '../models/WordExpresions';
import { WordExpressionsModel } from '../models/WordExpressionsModel';
import { Http } from "@angular/http";
import { FormGroup, FormBuilder, FormControl, Validators } from "@angular/forms";

@Component({
    selector: 'word-expressions',
    templateUrl: './word-expressions.component.html',
    styleUrls: ['./word-expressions.component.css']
})
export class WordExpressionsComponent implements OnInit {

    public endRequest: boolean = false;
    public modelWord: WordExpressionsModel = new WordExpressionsModel();
    public modelTopic: Topic = new Topic();
    public topics: Array<Topic> = new Array<Topic>();
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

            this.endRequest = true;

            this.onResetWord();
            this.onResetTopic();
        }, error => {
            console.error(error);
        });
    }

    ngOnInit() {
        this.buildFormWord();
        this.buildFormTopic();
    }

    onSubmitWord() {

        this.modelWord = this.prepareRequestWord();

        if (this.onEditionWord) {
            this._http.put(this._baseUrl + "api/Word", this.modelWord).subscribe(result => {
                this.getTopics();
            }, error => {
                console.error(error);
            });
        } else {
            this._http.post(this._baseUrl + "api/Word", this.modelWord).subscribe(result => {
                this.getTopics();
            }, error => {
                console.error(error);
            });
        }
    }

    onSubmitTopic() {

        this.modelTopic = this.prepareRequestTopic();

        if (this.onEditionTopic) {
            this._http.put(this._baseUrl + "api/Topic", this.modelTopic).subscribe(result => {
                this.getTopics();
            }, error => {
                console.error(error);
            });
        } else {
            this._http.post(this._baseUrl + "api/Topic", this.modelTopic).subscribe(result => {
                this.getTopics();
            }, error => {
                console.error(error);
            });
        }
    }

    onResetWord() {
        this.modelWord = new WordExpressionsModel();
        this.onEditionWord = false;
        this.buildFormWord();
    }

    onResetTopic() {
        this.modelTopic = new Topic();
        this.onEditionTopic = false;
        this.buildFormTopic();
    }

    buildFormWord() {
        this.formWordExpression = this.formBuilder.group({
            topic_id: ['selected', Validators.required],
            description: ['', Validators.required],
            meaning: ['', Validators.required]
        });
    }

    buildFormTopic() {
        this.formTopic = this.formBuilder.group({
            id: [''],
            description: ['', Validators.required],
        });
    }

    private prepareRequestWord(): WordExpressionsModel {
        const formModel = this.formWordExpression.value;

        const saveWord: WordExpressionsModel = {
            word_id: this.modelWord.word_id,
            topic_id: formModel.topic_id,
            word_description: formModel.description,
            topic_description: this.modelWord.topic_description,
            word_meaning: formModel.meaning
        };
        return saveWord;
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

    onEditWord(word: WordExpressionsModel) {
        this.onEditionWord = true;
        this.modelWord = word;
        this.formWordExpression = this.formBuilder.group({
            topic_id: [word.topic_id, Validators.required],
            description: [word.word_description, Validators.required],
            meaning: [word.word_meaning, Validators.required]
        });
    }

    onEditTopic(topic: Topic) {
        this.onEditionTopic = true;
        this.modelTopic = topic;
        this.formTopic = this.formBuilder.group({
            id: [topic.id],
            description: [topic.description, Validators.required],
            words: [topic.words]
        });
    }

    onDeleteWord(word: WordExpressionsModel) {
        this.endRequest = false;
        this._http.delete(this._baseUrl + "api/Word/" + word.word_id).subscribe(result => {
            this.getTopics();
        }, error => {
            console.error(error);
        });
    }

    onDeleteTopic(topic: Topic) {
        this.endRequest = false;
        this._http.delete(this._baseUrl + "api/Topic/" + topic.id).subscribe(result => {
            this.getTopics();
        }, error => {
            console.error(error);
        });
    }
}
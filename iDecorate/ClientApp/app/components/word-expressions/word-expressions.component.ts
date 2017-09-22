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
    public model: WordExpressionsModel = new WordExpressionsModel();
    public topics: Array<Topic> = new Array<Topic>();
    public wordExpresions: Array<WordExpressionsModel> = new Array<WordExpressionsModel>();
    public onEdition: boolean = false;
    private _baseUrl: string = '';
    private _http: Http;
    formWordExpression: FormGroup;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string, private formBuilder: FormBuilder, private renderer: Renderer) {
        this._http = http;
        this._baseUrl = baseUrl;
        this.getTopics();
    }

    getTopics() {
        this.endRequest = false;
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

            this.onReset();
        }, error => {
            console.error(error);
        });
    }

    ngOnInit() {
        this.buildForm();
    }

    onSubmit() {

        this.model = this.prepareRequestWord();

        this.endRequest = false;
        
        if (this.onEdition) {
            this._http.put(this._baseUrl + "api/Word", this.model).subscribe(result => {
                this.endRequest = true;
                this.getTopics();
            }, error => {
                this.endRequest = true;
                console.error(error);
            });
        } else {
            this._http.post(this._baseUrl + "api/Word", this.model).subscribe(result => {
                this.endRequest = true;
                this.getTopics();
            }, error => {
                this.endRequest = true;
                console.error(error);
            });
        }
    }

    onReset() {
        this.model = new WordExpressionsModel();
        this.onEdition = false;
        this.buildForm();
    }

    buildForm() {
        this.formWordExpression = this.formBuilder.group({
            topic_id: ['selected', Validators.required],
            description: ['', Validators.required],
            meaning: ['', Validators.required]
        });
    }

    private prepareRequestWord(): WordExpressionsModel {
        const formModel = this.formWordExpression.value;

        const saveWord: WordExpressionsModel = {
            word_id: this.model.word_id,
            topic_id: formModel.topic_id,
            word_description: formModel.description,
            topic_description: this.model.topic_description,
            word_meaning: formModel.meaning
        };
        return saveWord;
    }

    onEdit(word: WordExpressionsModel) {
        this.onEdition = true;
        this.model = word;
        this.formWordExpression = this.formBuilder.group({
            topic_id: [word.topic_id, Validators.required],
            description: [word.word_description, Validators.required],
            meaning: [word.word_meaning, Validators.required]
        });
    }

    onDelete(word: WordExpressionsModel){
        this.endRequest = false;
        this._http.delete(this._baseUrl + "api/Word/" + word.word_id).subscribe(result => {
            this.endRequest = true;
            this.getTopics();
        }, error => {
            this.endRequest = true;
            console.error(error);
        });
    }
}

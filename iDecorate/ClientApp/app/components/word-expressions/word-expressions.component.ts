import { Component, OnInit, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Topic } from '../models/Topic';
import { Word } from '../models/Word';
import { WordExpresions } from '../models/WordExpresions';
import { WordExpressionsModel } from '../models/WordExpressionsModel';

@Component({
    selector: 'word-expressions',
    templateUrl: './word-expressions.component.html',
    styleUrls: ['./word-expressions.component.css']
})
export class WordExpressionsComponent implements OnInit {
    public model: WordExpressionsModel = new WordExpressionsModel();
    public topics: Array<Topic> = new Array<Topic>();
    public wordExpresions: Array<WordExpresions> = new Array<WordExpresions>();
    public id_topic_selected: string;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/Topic').subscribe(result => {

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
                    let wordExpresion = new WordExpresions();
                    wordExpresion.topic = topic.description;
                    wordExpresion.word = word.description;
                    wordExpresion.meaning = word.meaning;
                    this.wordExpresions.push(wordExpresion);
                });
            });
        }, error => {
            console.error(error);
        });
    }

    ngOnInit() {

    }

    onSubmit() {
        this.model.topic_id = this.id_topic_selected;
        console.log(this.model);
    }
}
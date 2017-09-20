import { Component, OnInit, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'word-expressions',
    templateUrl: './word-expressions.component.html',
    styleUrls: ['./word-expressions.component.css']
})
export class WordExpressionsComponent implements OnInit {
    public wordExpresions: WordExpresions[];
    public model: Model;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
            this.wordExpresions = result.json() as WordExpresions[];
        }, error => console.error(error));
    }

    ngOnInit() {
        
    }

    onSubmit() {
        console.log("Form Submitted!");
    }
}

interface WordExpresions {
    dateFormatted: string;
    wordExpression: number;
    meanning: number;
}


class Model {
    wordExpression: string
    meanning: string;
}

import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { LoadingBarService } from '@ngx-loading-bar/core';

@Component({
    selector: 'teams',
    templateUrl: './teams.component.html'
})

export class TeamsComponent {
    public isLoading: Boolean = false;
    public dataSource: Team[];
    public model: Team = new Team();
    public operationTitle: String;

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string, private loadingBar: LoadingBarService) {
        this.loadGrid();
    }

    public register() {
        this.operationTitle = "Cadastrar time";
        this.model = new Team();
    }

    public edit(team: Team) {
        this.operationTitle = "Editar time";
        this.model = team;
    }

    public loadGrid() {
        this.loadingBar.start();
        this.isLoading = true;

        this.http.get(this.baseUrl + 'teams').subscribe(result => {
            this.dataSource = result.json() as Team[];

            this.isLoading = false;
            this.loadingBar.complete();
        }, error => {
            console.error(error);
            this.isLoading = false;
            this.loadingBar.complete();
        })
    }

    public save() {
        this.loadingBar.start();
        this.isLoading = true;

        this.http.post(this.baseUrl + 'teams', this.model).subscribe(response => {
            console.log(response);

            if (!this.model.id) {
                this.register();
            }

            this.loadGrid();

            this.isLoading = false;
            this.loadingBar.complete();
        }, error => {
            console.error(error);
            this.isLoading = false;
            this.loadingBar.complete();
        })
    }

    public delete(team: Team) {
        this.isLoading = true;
        this.loadingBar.start();

        this.http.delete(this.baseUrl + 'teams/' + team.id).subscribe(response => {
            console.log(response);

            this.loadGrid();
            this.isLoading = false;
            this.loadingBar.complete();
        }, error => {
            console.error(error);
            this.isLoading = false;
            this.loadingBar.complete();
        })
    }
}

class Team {
    id: string;
    name: string;
}

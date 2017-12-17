import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { LoadingBarService } from '@ngx-loading-bar/core';
import * as bootstrap from 'bootstrap';
import * as $ from 'jquery';

@Component({
    selector: 'tournaments',
    templateUrl: './tournaments.component.html',
    styleUrls: ['./tournaments.component.css']
})
export class TournamentsComponent {
    public isLoading: Boolean = false;
    public dataSource: Tournament[];
    public availableTeams: Team[];
    public selectedTeams: Team[] = [];
    public model: Tournament = new Tournament();
    public tournamentResult: TournamentResult = new TournamentResult();
    public operationTitle: String;
    public resultsTitle: String;

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string, private loadingBar: LoadingBarService) {
        this.loadGrid();
    }

    public register() {
        this.operationTitle = "Cadastrar torneio";
        this.model = new Tournament();
    }

    public playMatch(tournament: Tournament) {
        this.loadingBar.start();
        this.isLoading = true;

        if (tournament.stage == "WAITING") {
            this.playQuarterFinals(tournament);
        } else if (tournament.stage == "QUARTER_FINALS") {
            this.playSemiFinals(tournament);
        } else if (tournament.stage == "SEMI_FINALS") {
            this.playFinal(tournament);
        } else if (tournament.stage == "FINAL") {
            this.getWinner(tournament);
        }

        this.availableTeams = [];
        this.selectedTeams = [];
    }

    public getTeam(availableTeams: Team[]) {
        var team = null;

        while (team == null) {
            var sorted = availableTeams[Math.floor(Math.random() * availableTeams.length)];

            if (!this.selectedTeams.find(x => x.id == sorted.id)) {
                team = sorted;
                this.selectedTeams.push(sorted)
            }
        }

        return team;
    }

    public results(tournament: Tournament) {
        this.loadingBar.start();
        this.isLoading = true;

        this.model = tournament;
        this.resultsTitle = this.model.name;

        this.http.get(this.baseUrl + 'tournament-results/' + tournament.id).subscribe(result => {
            this.tournamentResult = result.json() as TournamentResult;

            $('#modalResults').modal('toggle');

        }, error => {
            console.error(error);
        }, () => {
            this.isLoading = false;
            this.loadingBar.complete();
        })

        return true;
    }

    public edit(tournament: Tournament) {
        this.operationTitle = "Editar torneio";
        this.model = tournament;
    }

    public loadGrid() {
        this.loadingBar.start();
        this.isLoading = true;

        this.http.get(this.baseUrl + 'tournaments').subscribe(result => {
            this.dataSource = result.json() as Tournament[];

        }, error => {
            console.error(error);
        }, () => {
            this.isLoading = false;
            this.loadingBar.complete();
        })
    }

    public save() {
        this.loadingBar.start();
        this.isLoading = true;

        if (!this.model.id) {
            this.model.stage = "WAITING";
        }

        this.http.post(this.baseUrl + 'tournaments', this.model).subscribe(response => {
            console.log(response);

            if (!this.model.id) {
                this.register();
            }

            this.loadGrid();
        }, error => {
            console.error(error);
        }, () => {
            this.isLoading = false;
            this.loadingBar.complete();
        })
    }

    public delete(tournament: Tournament) {
        this.isLoading = true;
        this.loadingBar.start();

        this.http.delete(this.baseUrl + 'tournaments/' + tournament.id).subscribe(response => {
            console.log(response);

            this.loadGrid();
        }, error => {
            console.error(error);
        }, () => {
            this.isLoading = false;
            this.loadingBar.complete();
        })
    }

    private playQuarterFinals(tournament: Tournament){
        this.http.get(this.baseUrl + 'teams').subscribe(result => {
            this.availableTeams = result.json() as Team[];

            if (this.availableTeams.length < 8) {
                window.alert("Para iniciar um torneio ao menos 8 times precisam ser cadastrados!");
            } else {

                var quarterFinals = new TournamentQuarterFinalsMatch();

                quarterFinals.keyOne.push(this.getTeam(this.availableTeams));
                quarterFinals.keyOne.push(this.getTeam(this.availableTeams));

                quarterFinals.keyTwo.push(this.getTeam(this.availableTeams));
                quarterFinals.keyTwo.push(this.getTeam(this.availableTeams));

                quarterFinals.keyTree.push(this.getTeam(this.availableTeams));
                quarterFinals.keyTree.push(this.getTeam(this.availableTeams))

                quarterFinals.keyFour.push(this.getTeam(this.availableTeams));
                quarterFinals.keyFour.push(this.getTeam(this.availableTeams));

                this.http.patch(this.baseUrl + 'tournament-results/' + tournament.id + '/quarter-finals', quarterFinals).subscribe(response => {
                    console.log(response);

                    this.loadGrid();
                }, error => {
                    console.error(error);
                })
            }
        }, error => {
            console.error(error);
        }, () => {
            this.isLoading = false;
            this.loadingBar.complete();
        })
    }

    private playSemiFinals(tournament: Tournament){
        this.http.get(this.baseUrl + 'tournament-results/' + tournament.id + '/quarter-finals').subscribe(result => {
            var quarterFinals = result.json() as TournamentQuarterFinalsMatch;
            this.availableTeams = quarterFinals.keyOne.concat(quarterFinals.keyTwo).concat(quarterFinals.keyTree).concat(quarterFinals.keyFour);
            var semiFinals = new TournamentSemiFinalsMatch();

            semiFinals.keyOne.push(this.getTeam(quarterFinals.keyOne));
            semiFinals.keyOne.push(this.getTeam(quarterFinals.keyTwo));

            semiFinals.keyTwo.push(this.getTeam(quarterFinals.keyTree));
            semiFinals.keyTwo.push(this.getTeam(quarterFinals.keyFour));

            this.http.patch(this.baseUrl + 'tournament-results/' + tournament.id + '/semi-finals', semiFinals).subscribe(response => {
                console.log(response);

                if (response.status == 400) {
                    alert(response.toString());
                }

                this.loadGrid();
            }, error => {
                console.error(error);
            })
        }, error => {
            console.error(error);
        }, () => {
            this.isLoading = false;
            this.loadingBar.complete();
        })
    }

    private playFinal(tournament: Tournament){
        this.http.get(this.baseUrl + 'tournament-results/' + tournament.id + '/semi-finals').subscribe(result => {
            var semiFinals = result.json() as TournamentSemiFinalsMatch;
            this.availableTeams = semiFinals.keyOne.concat(semiFinals.keyTwo);
            var final = new TournamentFinalMatch();

            final.teamA = this.getTeam(semiFinals.keyOne);
            final.teamB = this.getTeam(semiFinals.keyTwo);

            this.http.patch(this.baseUrl + 'tournament-results/' + tournament.id + '/final', final).subscribe(response => {
                console.log(response);

                if (response.status == 400) {
                    alert(response.toString());
                }

                this.loadGrid();
            }, error => {
                console.error(error);
            })
        }, error => {
            console.error(error);
        }, () => {
            this.isLoading = false;
            this.loadingBar.complete();
        })
    }

    private getWinner(tournament: Tournament){
        this.http.get(this.baseUrl + 'tournament-results/' + tournament.id + '/final').subscribe(result => {
            var final = result.json() as TournamentFinalMatch;

            this.availableTeams.push(final.teamA);
            this.availableTeams.push(final.teamB);

            this.http.patch(this.baseUrl + 'tournament-results/' + tournament.id + '/winner', this.getTeam(this.availableTeams)).subscribe(response => {
                console.log(response);

                if (response.status == 400) {
                    alert(response.toString());
                }

                this.loadGrid();
            }, error => {
                console.error(error);
            })
        }, error => {
            console.error(error);
        }, () => {
            this.isLoading = false;
            this.loadingBar.complete();
        })
    }
}

class Team {
    id: string;
    name: string;
}

class Tournament {
    id: string;
    name: string;
    stage: string;
}

class TournamentResult {
    tournament: string;
    quarterFinals: TournamentQuarterFinalsMatch = new TournamentQuarterFinalsMatch();
    semiFinals: TournamentSemiFinalsMatch = new TournamentSemiFinalsMatch();
    final: TournamentFinalMatch = new TournamentFinalMatch();
    winner: Team = new Team();
}

class TournamentQuarterFinalsMatch {
    keyOne: Team[] = [];
    keyTwo: Team[] = [];
    keyTree: Team[] = [];
    keyFour: Team[] = [];
}

class TournamentSemiFinalsMatch {
    keyOne: Team[] = [];
    keyTwo: Team[] = [];
}

class TournamentFinalMatch {
    teamA: Team = new Team();
    teamB: Team = new Team();
}
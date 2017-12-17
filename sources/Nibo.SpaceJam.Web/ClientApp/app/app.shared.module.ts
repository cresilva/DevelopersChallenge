import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { LoadingBarModule } from '@ngx-loading-bar/core';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { TeamsComponent } from './components/teams/teams.component';
import { TournamentsComponent } from './components/tournaments/tournaments.component';
import { TournamentStageFormatter } from './formatters/tournament.stage';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        TeamsComponent,
        TournamentsComponent,
        TournamentStageFormatter
    ],
    imports: [
        LoadingBarModule.forRoot(),
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'tournaments', pathMatch: 'full' },
            { path: 'tournaments', component: TournamentsComponent },
            { path: 'teams', component: TeamsComponent },
            { path: '**', redirectTo: 'tournaments' }
        ])
    ]
})
export class AppModuleShared {
}

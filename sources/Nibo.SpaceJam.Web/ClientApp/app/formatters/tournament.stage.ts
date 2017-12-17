import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'tournamentStage'
})
export class TournamentStageFormatter implements PipeTransform {
    transform(value: string) {
        if (value == "WAITING") {
            return "Não iniciado";
        }

        if (value == "QUARTER_FINALS") {
            return "Quartas de final";
        }

        if (value == "SEMI_FINALS") {
            return "Semi final";
        }

        if (value == "FINAL") {
            return "Final";
        }

        if (value == "CLOSED") {
            return "Encerrado";
        }
    }
}
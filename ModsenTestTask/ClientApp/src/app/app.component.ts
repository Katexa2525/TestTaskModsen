import { Component } from '@angular/core';

@Component({
    selector: 'app',
    template: `<label>������� ���:</label>
                 <input [(ngModel)]="name" placeholder="name">
                 <h2>����� ���������� {{name}}!</h2>`
})
export class AppComponent {
    name = '';
}
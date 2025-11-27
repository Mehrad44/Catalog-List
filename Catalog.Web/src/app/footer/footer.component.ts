import { Component } from '@angular/core';
import { AppModule } from '../app.module';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports:[AppModule],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss'
})
export class FooterComponent {

}

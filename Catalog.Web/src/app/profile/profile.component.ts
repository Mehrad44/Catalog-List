import { Component } from '@angular/core';
import { AppModule } from '../app.module';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports:[AppModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent {

}

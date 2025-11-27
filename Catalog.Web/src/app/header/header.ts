import { Component } from '@angular/core';
import {MatToolbarModule} from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { AppModule } from '../app.module';


@Component({
  selector: 'app-header',
  imports: [AppModule , RouterLink],
  templateUrl: './header.html',
  styleUrl: './header.scss'
})
export class Header {

}

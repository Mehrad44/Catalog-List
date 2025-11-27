import { Component, EventEmitter, Output } from '@angular/core';
import { UserService } from '../services/user.service';
import { Router, RouterLink } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { LoginComponent } from '../login/login.component';
import { RegisterComponent } from '../register/register.component';
import { AppModule } from '../app.module';

@Component({
  selector: 'app-header',
  standalone: true,
  imports:[AppModule, RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  @Output() sidenavToggle = new EventEmitter();

  constructor(
    public userService: UserService,
    private router: Router,
    private dialog: MatDialog) { }

  logout() {
    this.userService.logout();
    this.router.navigate(['/']);
  }

  login() {
    this.dialog.open(LoginComponent, {
      width: '25%'
    }).afterClosed().subscribe((result: boolean) => {
      if (result) {
        this.router.navigate(['/product-list']);
      }
    });
  }

  register() {
    this.dialog.open(RegisterComponent, {
      width: '25%'
    }).afterClosed().subscribe((result: boolean) => {
      if (result) {
        this.router.navigate(['/product-list']);
      }
    });
  }

  toggle() {
    this.sidenavToggle.emit();
  }

}

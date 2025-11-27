import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { ToastrService } from 'ngx-toastr';
import { UserToken } from '../models/user-token.model';
import { UserLogin } from '../models/user-login.model';
import { MatDialogRef } from '@angular/material/dialog';
import { AppModule } from '../app.module';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [AppModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  userForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private toastr: ToastrService,
    private dialogRef: MatDialogRef<LoginComponent>) {
    this.userForm = fb.group({
      userName: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [Validators.required, Validators.minLength(3)]],
    })
  }

  login() {
    if (this.userForm.invalid) {
      return;
    }

    var user = this.userForm.value as UserLogin;
    this.userService.login(user).subscribe({
      next: (userToken: UserToken) => {

        const helper = new JwtHelperService();
        const decodedToken= helper.decodeToken(userToken.accessToken);
        
        // localStorage.setItem('userName', decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"]);
        localStorage.setItem('userName', decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"][0]);
        localStorage.setItem('token', JSON.stringify(userToken));

        this.toastr.success('Login Completed', 'Login');

        this.dialogRef.close(true);
      },
      error: (error: any) => {
        this.toastr.error('Login Failed', 'Login');
      }
    });
  }

  get userName(): FormControl {
    return this.userForm.get('userName') as FormControl;
  }

  get password(): FormControl {
    return this.userForm.get('password') as FormControl;
  }

  cancel() {
    console.log('canel');
    this.dialogRef.close(false);
  }
}

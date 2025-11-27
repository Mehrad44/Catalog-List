import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { UserRegister } from '../models/user-register.model';
import { ToastrService } from 'ngx-toastr';
import { UserToken } from '../models/user-token.model';
import { Router } from '@angular/router';
import { MatDialogRef } from '@angular/material/dialog';
import { AppModule } from '../app.module';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-register',
  standalone: true,
  imports:[AppModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  userForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private toastr: ToastrService,
    private dialogRef: MatDialogRef<RegisterComponent>,
    private router: Router) {
    this.userForm = fb.group({
      userName: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [Validators.required, Validators.minLength(3)]],
    })
  }

  register() {
    if (this.userForm.invalid) {
      return;
    }

    var user = this.userForm.value as UserRegister;
    this.userService.register(user).subscribe({
      next: (userToken: UserToken) => {
        this.toastr.success('Register Completed', 'Register');

        const helper = new JwtHelperService();
        const decodedToken= helper.decodeToken(userToken.accessToken);
        
        localStorage.setItem('userName', decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"]);
        localStorage.setItem('token', JSON.stringify(userToken));

        this.dialogRef.close(true);
      },
      error: (error: any) => {
        this.toastr.error('Register Failed', 'Register');
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

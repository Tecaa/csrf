import { Component, ViewChild } from '@angular/core';
import { FormsModule, NgForm, ReactiveFormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment.dev';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule],
})
export class LoginComponent {
  private apiUrl = environment.apiUrl;
  
  @ViewChild('loginForm') loginForm!: NgForm;
  constructor(private http: HttpClient, private router: Router) {}

  
  onSubmit() {
    if (this.loginForm.valid) {
      const username = this.loginForm.value.username;
      const password = this.loginForm.value.password;
      // Handle the login logic here
      console.log('Username:', username);
      console.log('Password:', password);
      
      // Make an HTTP call for login
      this.http.post(`${this.apiUrl}/auth/login`, { username, password }, {withCredentials: true}).subscribe(response => {
        console.log('Login successful:', response);
        // Redirect to /transactions if login is successful
        this.router.navigate(['/transactions']);
      }, error => {
        console.error('Login failed:', error);
      });
    }
  }
}

import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { LoginRequest, LoginResponse } from './login.model';
import axios from 'axios';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup | undefined;

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      Email: ['', [Validators.required, Validators.email]],
      Password: ['', Validators.required]
    });
  }

  async login() {
    if (this.loginForm.invalid) {
      alert('Please ensure the form is filled out correctly.');
      return;
    }

    try {
      const response = await axios.post('http://localhost:5263/api/auth/login', this.loginForm.value);
      if (response.data && response.data.Token) {
        console.log("Token received:", response.data.Token);
        // Store the token in a service or a cookie/localStorage for further requests.
        alert('Login successful');
      } else {
        alert('Unexpected response from the server.');
      }
    } catch (error) {
      console.error('Login error:', error);
      alert('Failed to login. Please check your credentials and try again.');
    }
  }
}

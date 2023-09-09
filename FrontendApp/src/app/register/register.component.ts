import { Component } from '@angular/core';
import axios from 'axios';
import { RegisterRequest, RegisterResponse } from './register.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
    model: RegisterRequest = {
        Email: '',
        Password: '',
        FirstName: '',
        LastName: '',
        Address: '',
        City: '',
        State: '',
        PostalCode: '',
        Country: ''
    };
    
    errors: string | null = null;  // <-- Here's the 'errors' property definition

    async handleSubmit(event: Event) {
      event.preventDefault();  // This prevents the default form submission behavior.
      await this.register();
    }

    async register() {
      try {
          const response = await axios.post<RegisterResponse>('http://localhost:5263/api/auth/register', this.model);
          if (response.data && response.data.message) {
              alert(response.data.message);
          }
      } catch (error) {
          console.error('Registration error:', error);
          this.errors = 'Failed to register. Please try again.';  // <-- Assigning error message to 'errors' property
      }
    }
}

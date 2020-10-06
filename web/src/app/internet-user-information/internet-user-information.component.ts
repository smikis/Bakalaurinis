/// <reference types="@types/googlemaps" />
import { Component, OnInit, ViewChild } from '@angular/core';
import { InternetUserService, InternetUser } from '../internet.user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { PingService, Ping, PingInformation } from '../ping.service';

@Component({
  selector: 'app-internet-user-information',
  templateUrl: './internet-user-information.component.html',
  styleUrls: ['./internet-user-information.component.scss'],
  providers: [InternetUserService, PingService]
})
export class InternetUserInformationComponent implements OnInit {
  internetUserId: number;
  internetUser: InternetUser;

  isLoadingResults = true;
  resultsLength = 0;
  displayedColumns = ['ipAddress', 'time', 'recorded', 'status'];
  pingResults = new Array<Ping>();
  dataSource = new MatTableDataSource();
  pingInformation: PingInformation;

  @ViewChild('gmap') gmapElement: any;
  map: google.maps.Map;
  geocoder: google.maps.Geocoder;

  // lineChart
  public lineChartData: Array<any> = [0];
  public lineChartLabels: Array<any> = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10'];
  public lineChartOptions: any = {
    responsive: true,
    scales: {
      yAxes: [{
        ticks: {
          beginAtZero: true
        }
      }]
    }

  };
  public lineChartColors: Array<any> = [
    { // grey
      backgroundColor: 'rgba(148,159,177,0.2)',
      borderColor: 'rgba(148,159,177,1)',
      pointBackgroundColor: 'rgba(148,159,177,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    }
  ];
  public lineChartLegend = false;
  public lineChartType = 'line';

  constructor(private route: ActivatedRoute,
    private internetUserService: InternetUserService,
    private pingService: PingService) { }
  ngOnInit() {
    this.geocoder = new google.maps.Geocoder();

    this.route.params.subscribe(params => {
      this.internetUserId = params['id'];
      this.internetUserService.getInternetUser(this.internetUserId).subscribe(result => {
        this.internetUser = result;

        // Show map
        this.geocoder.geocode({ 'address': this.internetUser.location }, res => {
          const location = res[0].geometry.location;
          console.log(location);
          this.initializeMap(location, `${this.internetUser.firstName} ${this.internetUser.lastName}`);
        });
      });

      // Get ping infromation
      this.pingService.getInternetUserPings(this.internetUserId).subscribe(result => {
        this.pingResults = result;
        this.dataSource.data = result;
        this.isLoadingResults = false;
        const chartData = result.map(x => x.time);
        this.lineChartData = [
          {data: chartData, label: 'Atsako laikas'}
        ];
      });

      this.pingService.getInternetUserPingInformation(this.internetUserId).subscribe(result => {
        this.pingInformation = result;
      });

    });
  }

  initializeMap(location: google.maps.LatLng, name: string) {
    const mapProp = {
      zoom: 15,
      mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    this.map = new google.maps.Map(this.gmapElement.nativeElement, mapProp);

    this.map.panTo(location);

    const marker = new google.maps.Marker({
      position: location,
      map: this.map,
      title: name
    });
  }

}

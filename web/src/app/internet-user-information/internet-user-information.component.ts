import { Component, OnInit, ViewChild } from '@angular/core';
import { InternetUserService, InternetUser } from '../internet.user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material';
import { PingService, Ping, PingInformation } from '../ping.service';
import { } from '@types/googlemaps';

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

  // Shared chart options
  globalChartOptions: any = {
    responsive: true,
    legend: {
      display: false,
      position: 'bottom'
    }
  };

  // Bar
  barChartLabels: string[] = ['1', '2', '3', '4', '5', '6', '7'];
  barChartType = 'bar';
  barChartLegend = true;
  barChartData: any[] = [{
    data: [6, 5, 8, 8, 5, 5, 4],
    label: 'Series A',
    borderWidth: 0
  }, {
    data: [5, 4, 4, 2, 6, 2, 5],
    label: 'Series B',
    borderWidth: 0
  }];
  barChartOptions: any = Object.assign({
    scaleShowVerticalLines: false,
    tooltips: {
      mode: 'index',
      intersect: false
    },
    responsive: true,
    scales: {
      xAxes: [{
        gridLines: {
          color: 'rgba(0,0,0,0.02)',
          defaultFontColor: 'rgba(0,0,0,0.02)',
          zeroLineColor: 'rgba(0,0,0,0.02)'
        },
        stacked: true,
        ticks: {
          beginAtZero: true
        }
      }],
      yAxes: [{
        gridLines: {
          color: 'rgba(0,0,0,0.02)',
          defaultFontColor: 'rgba(0,0,0,0.02)',
          zeroLineColor: 'rgba(0,0,0,0.02)'
        },
        stacked: true
      }]
    }
  }, this.globalChartOptions);

  constructor(private route: ActivatedRoute,
    private internetUserService: InternetUserService,
    private pingService: PingService) { }
  ngOnInit() {
    this.geocoder = new google.maps.Geocoder();

    this.route.params.subscribe(params => {
      this.internetUserId = params['id'];
      this.internetUserService.getInternetUser(this.internetUserId).subscribe(result => {
        this.internetUser = result;

        //Show map
        this.geocoder.geocode({ 'address': this.internetUser.location }, result => {
          var location = result[0].geometry.location;
          console.log(location);
          this.initializeMap(location, `${this.internetUser.firstName} ${this.internetUser.lastName}`);
        })
      });

      //Get ping infromation
      this.pingService.getInternetUserPings(this.internetUserId).subscribe(result=> {
        this.pingResults = result;
        this.dataSource.data = result;
        this.isLoadingResults = false;
      });

      this.pingService.getInternetUserPingInformation(this.internetUserId).subscribe(result=> {
        this.pingInformation = result;
      });

    });
  }

  initializeMap(location: google.maps.LatLng, name: string) {
    var mapProp = {
      zoom: 15,
      mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    this.map = new google.maps.Map(this.gmapElement.nativeElement, mapProp);

    this.map.panTo(location);

    var marker = new google.maps.Marker({
      position: location,
      map: this.map,
      title: name
    });
  }

}

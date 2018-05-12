import { Component, OnInit,ViewChild  } from '@angular/core';
import {InternetUserService, InternetUser} from '../internet.user.service';
import {ActivatedRoute, Router} from '@angular/router';
import { } from '@types/googlemaps';

@Component({
  selector: 'app-internet-user-information',
  templateUrl: './internet-user-information.component.html',
  styleUrls: ['./internet-user-information.component.scss'],
  providers:[InternetUserService]
})
export class InternetUserInformationComponent implements OnInit {
  internetUserId : number;
  internetUser: InternetUser;

  @ViewChild('gmap') gmapElement: any;
  map: google.maps.Map;
  geocoder: google.maps.Geocoder;
  constructor(private route:ActivatedRoute, 
    private internetUserService: InternetUserService) { }

  ngOnInit() {   
    this.geocoder = new google.maps.Geocoder();

    this.route.params.subscribe( params => {
      this.internetUserId = params['id'];
        this.internetUserService.getInternetUser(this.internetUserId).subscribe(result=> {
          this.internetUser = result;

          //Show map
          this.geocoder.geocode({'address': this.internetUser.location}, result=> {
            var location = result[0].geometry.location;
            console.log(location);
            this.initializeMap(location,`${this.internetUser.firstName} ${this.internetUser.lastName}`);
          })        
        })
    });
  }

  initializeMap(location:google.maps.LatLng, name: string) {
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

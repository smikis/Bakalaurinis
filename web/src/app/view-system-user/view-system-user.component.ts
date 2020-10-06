/// <reference types="@types/googlemaps" />
import { Component, OnInit, ViewChild } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {UserService, UserExtended} from '../user.service';

@Component({
  selector: 'app-view-system-user',
  templateUrl: './view-system-user.component.html',
  styleUrls: ['./view-system-user.component.scss'],
  providers: [UserService]
})
export class ViewSystemUserComponent implements OnInit {
  userId: string;
  user: UserExtended;

  @ViewChild('gmap') gmapElement: any;
  map: google.maps.Map;
  marker: google.maps.Marker;

  constructor(private route:ActivatedRoute, private userService: UserService) { }

  ngOnInit() {
    var mapProp = {
      zoom: 15,
      mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    this.map = new google.maps.Map(this.gmapElement.nativeElement, mapProp);

    this.route.params.subscribe( params => {
      this.userId = params['id'];
      console.log(this.userId);

      this.userService.getUser(this.userId).subscribe(data=> {
        this.user = data;
      });

      this.refreshLocationMarker();

    });
  }

  refreshLocationMarker() {
    this.userService.getUserLocation(this.userId).subscribe(result=> {
      var location = new google.maps.LatLng(result.lat, result.lng);
      this.map.panTo(location);

        if(!this.marker) {
          this.marker = new google.maps.Marker({
            position: location,
            map: this.map,
            title: this.userId
          });
        }
        else {
          this.marker.setPosition(location);
        }
    });
  }

}

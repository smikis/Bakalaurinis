import { Component, OnInit } from '@angular/core';
import {InternetUserService, InternetUser} from '../internet.user.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Subscription } from 'rxjs/Rx';
import { DeviceService, Device } from '../device.service';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-create-device-dialog',
  templateUrl: './create-device-dialog.component.html',
  styleUrls: ['./create-device-dialog.component.scss'],
  providers: [InternetUserService,DeviceService]
})
export class CreateDeviceDialogComponent implements OnInit {

  internetUserCtrl: FormControl;
  filteredInternetUsers: Subscription;
  internetUsers: InternetUser[];
  selectedInternetUser: InternetUser;

  myform: FormGroup;

  constructor(private internetUserService: InternetUserService
    ,private deviceService: DeviceService
    ,public dialogRef: MatDialogRef<CreateDeviceDialogComponent>) { 
    this.internetUserCtrl = new FormControl();
    this.filteredInternetUsers = this.internetUserCtrl.valueChanges
    .startWith(null)
    .debounceTime(400)
       .do(val => {
         internetUserService.searchInternetUsers(val)
         .toPromise()
         .then(res => {
           this.internetUsers = res
         })
       })
       .subscribe();

     

  }

  changedInternetUser(user:InternetUser) {
    this.selectedInternetUser = user;
  }

  ngOnInit() {
    this.myform = new FormGroup({
      name: new FormControl('', Validators.required),
      manufacturer: new FormControl('', Validators.required),
      macAddress: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required)
  });
  }

  submit() {
      var device = new Device();
      device.name = this.myform.controls['name'].value;
      device.manufacturer = this.myform.controls['manufacturer'].value;
      device.macAddress = this.myform.controls['macAddress'].value;
      device.description = this.myform.controls['description'].value;
      device.internetUserId = this.selectedInternetUser? this.selectedInternetUser.id: null;

      console.log(device);
      this.deviceService.createDevice(device).subscribe(result=> {
        this.dialogRef.close('Success');
      })
  }

}

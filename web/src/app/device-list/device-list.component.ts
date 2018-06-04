import { Component, OnInit, ViewChild } from '@angular/core';
import { DeviceService, Device } from '../device.service';
import { MatTableDataSource,MatPaginator } from '@angular/material';
import { MatDialog, MatDialogRef } from '@angular/material';
import { CreateDeviceDialogComponent } from '../create-device-dialog/create-device-dialog.component';
@Component({
  selector: 'app-device-list',
  templateUrl: './device-list.component.html',
  styleUrls: ['./device-list.component.scss'],
  providers: [DeviceService]
})
export class DeviceListComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;

  deviceListDataSource = new MatTableDataSource();
  deviceListdisplayedColumns = ['name', 'manufacturer', 'macAddress', 'description', 'internetUserId', 'warrantyExpiration'];
  
  constructor(private deviceService: DeviceService,private dialog: MatDialog) {
   
  }

  ngOnInit() {
    this.deviceService.getDevices().subscribe(result=> {
      this.deviceListDataSource.data = result; 
      this.deviceListDataSource.paginator = this.paginator;    
    });
  }


  createDevice() {
    let createproblemdialogRef = this.dialog.open(CreateDeviceDialogComponent, {
      width: '30%',
    });
    createproblemdialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.deviceService.getDevices().subscribe(result=> {
          this.deviceListDataSource.data = result;  
        });
      }
    });
  }


  

}

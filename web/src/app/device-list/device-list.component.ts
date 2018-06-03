import { Component, OnInit, ViewChild } from '@angular/core';
import { DeviceService, Device } from '../device.service';
import { MatTableDataSource,MatPaginator } from '@angular/material';
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
  
  constructor(private deviceService: DeviceService) {
   
  }

  ngOnInit() {
    this.deviceService.getDevices().subscribe(result=> {
      this.deviceListDataSource.data = result; 
      this.deviceListDataSource.paginator = this.paginator;    
    });
  }

}

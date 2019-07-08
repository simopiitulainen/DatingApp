import { Component, OnInit } from '@angular/core';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Photo } from 'src/app/_models/photo';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/user';
import { AdminService } from 'src/app/_services/admin.service';
import { TouchSequence } from 'selenium-webdriver';
import { markParentViewsForCheckProjectedViews } from '@angular/core/src/view/util';

@Component({
  selector: 'app-photo-management',
  templateUrl: './photo-management.component.html',
  styleUrls: ['./photo-management.component.css']
})
export class PhotoManagementComponent implements OnInit {
  photos: Photo[];

  constructor(private alertify: AlertifyService, private adminService: AdminService) { }

  ngOnInit() {
    this.loadPhotos();
  }


  loadPhotos() {
    this.adminService.getUnapprovedPhotos()
    .subscribe((photos) => {
      this.photos = photos;
    }
    , error => {
      this.alertify.error(error);
    });
  }

  rejectPhoto(id: number) {
    this.adminService.rejectPhoto(id).subscribe(() => {
      this.alertify.success('Rejected photo');
      this.loadPhotos();
    }, error => {
      this.alertify.error(error);
    });

  }

  approvePhoto(id: number) {
    this.adminService.approvePhoto(id).subscribe(() => {
      this.alertify.success('Approved photo');
      this.loadPhotos();
    }, error => {
      this.alertify.error(error);
    });
  }




}

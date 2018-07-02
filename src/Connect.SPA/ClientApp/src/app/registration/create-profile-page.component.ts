import { Component } from "@angular/core";
import { Subject } from "rxjs";

@Component({
  templateUrl: "./create-profile-page.component.html",
  styleUrls: ["./create-profile-page.component.css"],
  selector: "app-create-profile-page"
})
export class CreateProfilePageComponent { 

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}

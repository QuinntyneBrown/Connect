import { Component } from "@angular/core";
import { Subject } from "rxjs";

@Component({
  templateUrl: "./select-profile-type-page.component.html",
  styleUrls: ["./select-profile-type-page.component.css"],
  selector: "app-select-profile-type-page"
})
export class SelectProfileTypePageComponent { 

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}

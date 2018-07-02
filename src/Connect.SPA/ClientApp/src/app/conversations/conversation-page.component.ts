import { Component } from "@angular/core";
import { Subject } from "rxjs";

@Component({
  templateUrl: "./conversation-page.component.html",
  styleUrls: ["./conversation-page.component.css"],
  selector: "app-conversation-page"
})
export class ConversationPageComponent { 

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}

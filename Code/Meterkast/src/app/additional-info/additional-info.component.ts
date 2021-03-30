import { Component, OnInit } from '@angular/core';
import { ApicommsService } from '../apicomms.service';
import { Datagram } from '../entities/Datagram';

@Component({
  selector: 'app-additional-info',
  templateUrl: './additional-info.component.html',
  styleUrls: ['./additional-info.component.css']
})
export class AdditionalInfoComponent implements OnInit {

  constructor(private api: ApicommsService) { }
  datagram: Datagram;
  gaugeType = "semi";
  gaugeValue = 0;
  gaugeLabel = "Power Usage";
  gaugeAppendText = "kWh";

  gaugeType1 = "semi";
  gaugeValue1 = 0;
  gaugeLabel1 = " Total Gas Used";
  gaugeAppendText1 = "m*3";

  gaugeType2 = "semi";
  gaugeValue2= 0;
  gaugeLabel2 = "Total Usage";
  gaugeAppendText2 = "kWh";

  gaugeType3 = "semi";
  gaugeValue3= 0;
  gaugeLabel3 = "High return";
  gaugeAppendText3 = "kWh";
  ngOnInit(): void {
     this.getDatagrams();
     setInterval(() => { this.getDatagrams(); }, 10000);
  }

  getDatagrams() {
    console.log("received data");
    this.api.getSingleDatagram().subscribe(datagram => {
      this.datagram = datagram;
      this.gaugeValue = datagram.currentUsage;
      this.gaugeValue1 = datagram.gasUsage;
      this.gaugeValue2 = datagram.totalHigh;
      this.gaugeValue3 = datagram.returnHigh;
    });
  }
}

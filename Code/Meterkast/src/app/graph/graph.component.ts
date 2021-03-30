import { Component, OnInit } from '@angular/core';
import { EChartsOption } from 'echarts';
import { ApicommsService } from '../apicomms.service';
import { Datagram } from '../entities/Datagram';

@Component({
  selector: 'app-graph',
  templateUrl: './graph.component.html',
  styleUrls: ['./graph.component.css']
})
export class GraphComponent implements OnInit {

  constructor(private api : ApicommsService) { }
  datagrams: Datagram[] = [];
  timestamps: string[] = [];
  values: number[] = [];
 chartOption : EChartsOption;
  ngOnInit(): void {
  this.getDatagrams();
  setInterval(() => { this.getDatagrams(); }, 10000);
  }



  getDatagrams(){
    this.timestamps = [];
    this.values = [];
    this.api.getxDatagrams(100).subscribe(datagrams => {
      this.datagrams = datagrams;
      this.datagrams.forEach(element => {
        this.timestamps.push(element.timeStamp);
        this.values.push(element.currentUsage);
        
      });
      console.log(this.timestamps)
      console.log(this.values)
        this.chartOption = {
          color: ['#006faf'],
          tooltip: {
            trigger: 'axis',
            axisPointer: {
              type: 'shadow',
            },
          },
        xAxis: {
          type: 'category',
          name: 'date',
          data: this.timestamps.reverse(),
          axisTick: {
            alignWithLabel: true,
          },
        },
        yAxis: {
          type: 'value',
        },
        series: [
          {
            data: this.values.reverse(),
            type: 'line',
            name: 'Usage in kWh',
            smooth: true
          },
        ],
      };
    })
  }

}

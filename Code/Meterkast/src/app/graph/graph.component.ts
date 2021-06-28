import { Component, OnInit } from '@angular/core';
import { EChartsOption } from 'echarts';
import { ApicommsService } from '../apicomms.service';
import { Datagram } from '../entities/Datagram';
import { Temprature } from '../entities/Temprature';

@Component({
  selector: 'app-graph',
  templateUrl: './graph.component.html',
  styleUrls: ['./graph.component.css']
})
export class GraphComponent implements OnInit {

  constructor(private api : ApicommsService) { }
  datagrams: Datagram[] = [];
  tempratures: Temprature[] = [];
  datagramTimestamps: string[] = [];
  tempratureTimestamps:string[]= [];
  datagramValues: number[] = [];
  tempratureValues:number[] = [];
 chartOption : EChartsOption;
 tempOptions : EChartsOption;
  ngOnInit(): void {
  this.getDatagrams();
  this.getTempratures();
  setInterval(() => { 
    this.getDatagrams(); 
    this.getTempratures();
  }, 20000);
  }


  getTempratures()
  {
    this.tempratureTimestamps = [];
    this.tempratureValues = [];
    this.api.getxTempratures(100).subscribe(tempratures => {
      this.tempratures = tempratures;
      this.tempratures.sort((n1,n2) => {
      if (n1.timeStamp > n2.timeStamp)
      {
        return -1;
      }
      if (n1.timeStamp < n2.timeStamp)
      {
        return 1;
      }
      return 0;
    });
    this.tempratures.forEach(element => {
      this.tempratureTimestamps.push(element.timeStamp);
      this.tempratureValues.push(Number(element.value));
    });
    console.log(this.tempratureTimestamps);
    console.log(this.tempratureValues);
    this.tempOptions = {
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
      data: this.tempratureTimestamps.reverse(),
      axisTick: {
        alignWithLabel: true,
      },
    },
    yAxis: {
      type: 'value',
    },
    series: [
      {
        data: this.tempratureValues.reverse(),
        type: 'line',
        name: 'Temprature in C',
        smooth: true
      },
    ],
  };
  })
}

  getDatagrams(){
    this.datagramTimestamps = [];
    this.datagramValues = [];
    this.api.getxDatagrams(100).subscribe(datagrams => {
      this.datagrams = datagrams;
      this.datagrams.sort((n1,n2) => {
        if (n1.timeStamp > n2.timeStamp)
        {
          return -1;
        }
        if (n1.timeStamp < n2.timeStamp)
        {
          return 1;
        }
        return 0;
      });
      this.datagrams.forEach(element => {
        this.datagramTimestamps.push(element.timeStamp);
        this.datagramValues.push(element.currentUsage);
        
      });
      console.log(this.datagramTimestamps)
      console.log(this.datagramValues)
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
          data: this.datagramTimestamps.reverse(),
          axisTick: {
            alignWithLabel: true,
          },
        },
        yAxis: {
          type: 'value',
        },
        series: [
          {
            data: this.datagramValues.reverse(),
            type: 'line',
            name: 'Usage in kWh',
            smooth: true
          },
        ],
      };
    })
  }

}

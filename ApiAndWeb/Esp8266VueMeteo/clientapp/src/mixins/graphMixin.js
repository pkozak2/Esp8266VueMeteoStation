export const defaultDataSet = {
  datasets: []
};

const defaultChartOptions = {
  spanGaps: false,
  aspectRatio: 2,
  responsive: true,
  maintainAspectRatio: false,
  tooltips: {
    mode: "index",
    intersect: false
  },
  legend: {
    labels: {
      fontColor: "white"
    }
  },
  annotation: {
    annotations: []
  },
  scales: {
    xAxes: [
      {
        display: true,
        type: "time",
        time: {
          displayFormats: {
            millisecond: "HH:mm:ss.SSS",
            second: "HH:mm:ss",
            minute: "HH:mm",
            hour: "HH"
          },
          tooltipFormat: "DD-MM HH:mm"
        },
        ticks: { fontColor: "white" }
      }
    ],
    yAxes: [
      {
        ticks: { fontColor: "white" }
      }
    ]
  },
  elements: {
    point: {
      radius: 0
    }
  },
  scaleLabel: {
    display: false
  }
};

const defaultPMoptions = {
  pm25: {
    value: 25,
    text: "PM₂₅ limit",
    color: "purple"
  },
  pm10: {
    value: 50,
    text: "PM₁₀ limit",
    color: "orange"
  }
};

function generatePMgraphOptions(options) {
  var localChartOptions = Object.assign({}, defaultChartOptions);

  var localOptions = { ...defaultPMoptions, ...options };

  localChartOptions.scales.yAxes = [
    {
      display: true,
      scaleLabel: {
        display: false
      },
      ticks: {
        fontColor: "white",
        suggestedMin: 0,
        suggestedMax: 55
      }
    }
  ];
  localChartOptions.annotation.annotations.push({
    type: "line",
    mode: "horizontal",
    scaleID: "y-axis-0",
    value: localOptions.pm25.value,
    borderColor: localOptions.pm25.color,
    borderWidth: 1,
    label: {
      content: localOptions.pm25.text,
      enabled: true,
      position: "left",
      backgroundColor: "rgba(0,0,0,0.3)"
    }
  });

  localChartOptions.annotation.annotations.push({
    type: "line",
    mode: "horizontal",
    scaleID: "y-axis-0",
    value: localOptions.pm10.value,
    borderColor: localOptions.pm10.color,
    borderWidth: 1,
    label: {
      content: localOptions.pm10.text,
      enabled: true,
      position: "left",
      backgroundColor: "rgba(0,0,0,0.3)"
    }
  });

  return localChartOptions;
}

export default {
  methods: {
    generateChartOption(graphType, options) {
      switch (graphType) {
        case "pm":
          return generatePMgraphOptions(options);
        default:
          return defaultChartOptions;
      }
    }
  }
};

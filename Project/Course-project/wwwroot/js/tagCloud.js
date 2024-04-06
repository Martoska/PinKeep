

// Themes begin
am4core.useTheme(am4themes_moonrisekingdom);
am4core.useTheme(am4themes_animated);
// Themes end

var chart = am4core.create("chartdiv", am4plugins_wordCloud.WordCloud);
chart.fontFamily = "Courier New";
var series = chart.series.push(new am4plugins_wordCloud.WordCloudSeries());
series.randomness = 0.1;
series.rotationThreshold = 0.5;

series.data = [{
    "tag": "2963bc98-3883-4f31-bb84-76a8c40af842",
    "count": "1765836"
}, {
    "tag": "java",
    "count": "1517355"
}, {
    "tag": "c#",
    "count": "1287629"
}, {
    "tag": "php",
    "count": "1263946"
}, {
    "tag": "android",
    "count": "1174721"
}, {
    "tag": "python",
    "count": "1116769"
}, {
    "tag": "jquery",
    "count": "944983"
}, {
    "tag": "html",
    "count": "805679"
}, {
    "tag": "c++",
    "count": "606051"
}, {
    "tag": "ios",
    "count": "591410"
}, {
    "tag": "css",
    "count": "574684"
}, {
    "tag": "mysql",
    "count": "550916"
}, {
    "tag": "sql",
    "count": "479892"
}, {
    "tag": "asp.net",
    "count": "343092"
}, {
    "tag": "ruby-on-rails",
    "count": "303311"
}, {
    "tag": "c",
    "count": "296963"
}, {
    "tag": "arrays",
    "count": "288445"
}, {
    "tag": "objective-c",
    "count": "286823"
}, {
    "tag": ".net",
    "count": "280079"
}, {
    "tag": "r",
    "count": "277144"
}, {
    "tag": "node.js",
    "count": "263451"
}, {
    "tag": "angularjs",
    "count": "257159"
}, {
    "tag": "json",
    "count": "255661"
}, {
    "tag": "sql-server",
    "count": "253824"
}, {
    "tag": "swift",
    "count": "222387"
}, {
    "tag": "iphone",
    "count": "219827"
}, {
    "tag": "regex",
    "count": "203121"
}, {
    "tag": "ruby",
    "count": "202547"
}, {
    "tag": "ajax",
    "count": "196727"
}, {
    "tag": "django",
    "count": "191174"
}, {
    "tag": "excel",
    "count": "188787"
}, {
    "tag": "xml",
    "count": "180742"
}, {
    "tag": "asp.net-mvc",
    "count": "178291"
}, {
    "tag": "linux",
    "count": "173278"
}, {
    "tag": "angular",
    "count": "154447"
}, {
    "tag": "database",
    "count": "153581"
}, {
    "tag": "wpf",
    "count": "147538"
}, {
    "tag": "spring",
    "count": "147456"
}, {
    "tag": "wordpress",
    "count": "145801"
}, {
    "tag": "python-3.x",
    "count": "145685"
}, {
    "tag": "vba",
    "count": "139940"
}, {
    "tag": "string",
    "count": "136649"
}, {
    "tag": "xcode",
    "count": "130591"
}, {
    "tag": "windows",
    "count": "127680"
}, {
    "tag": "reactjs",
    "count": "125021"
}, {
    "tag": "vb.net",
    "count": "122559"
}, {
    "tag": "html5",
    "count": "118810"
}, {
    "tag": "eclipse",
    "count": "115802"
}, {
    "tag": "multithreading",
    "count": "113719"
}, {
    "tag": "mongodb",
    "count": "110348"
}, {
    "tag": "laravel",
    "count": "109340"
}, {
    "tag": "bash",
    "count": "108797"
}, {
    "tag": "git",
    "count": "108075"
}, {
    "tag": "oracle",
    "count": "106936"
}, {
    "tag": "pandas",
    "count": "96225"
}, {
    "tag": "postgresql",
    "count": "96027"
}, {
    "tag": "twitter-bootstrap",
    "count": "94348"
}];




series.dataFields.word = "tag";
series.dataFields.value = "count";

series.heatRules.push({
    "target": series.labels.template,
    "property": "fill",
    "min": am4core.color("#0000CC"),
    "max": am4core.color("#CC00CC"),
    "dataField": "value"
});

series.labels.template.url = "https://localhost:7047/Items/TagSearch/{word}";
series.labels.template.urlTarget = "_blank";
series.labels.template.tooltipText = "{word}: {value}";

var hoverState = series.labels.template.states.create("hover");
hoverState.properties.fill = am4core.color("#FF0000");

var subtitle = chart.titles.create();
subtitle.text = "(click to open)";

var title = chart.titles.create();
title.text = "Most Popular Tags @ StackOverflow";
title.fontSize = 20;
title.fontWeight = "800";
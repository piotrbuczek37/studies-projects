function Get(yourUrl){
    var Httpreq = new XMLHttpRequest();
    Httpreq.open("GET",yourUrl,false);
    Httpreq.send(null);
    return Httpreq.responseText;          
}

var myObjJSON = "";
function getData(){
  var urlAdress = 'https://www.reddit.com/r/funny.json';
  var json_obj = JSON.parse(Get(urlAdress));
  console.log(json_obj);
  myObjJSON = {"posts":[{"title":json_obj.data.children[0].data.title, "upvotes": json_obj.data.children[0].data.ups, "score": json_obj.data.children[0].data.score, "num_comments": json_obj.data.children[0].data.num_comments, "created": json_obj.data.children[0].data.created}],"count": json_obj.data.children.length};
  for(let i=1;i<json_obj.data.children.length;i++){
      myObjJSON.posts.push({"title":json_obj.data.children[i].data.title, "upvotes": json_obj.data.children[i].data.ups, "score": json_obj.data.children[i].data.score, "num_comments": json_obj.data.children[i].data.num_comments, "created": json_obj.data.children[i].data.created});
  }
}

function showJSON(){
  var JSONstructure = "";
  var i = "";
  JSONstructure += "<h2>Posts from r/funny: </h2>";
  for (i in myObjJSON.posts) {
    JSONstructure += "<h3>Title: " + myObjJSON.posts[i].title + "<br>Upvotes: " + myObjJSON.posts[i].upvotes + "<br>Score: " + myObjJSON.posts[i].score + "<br>Number of comments: " + myObjJSON.posts[i].num_comments + "<br>Created at: " + new Date(myObjJSON.posts[i].created*1000).toGMTString() + "</h3>";
  }
  JSONstructure += "<h2>Number of posts: " + myObjJSON.count + "</h2>";
  document.getElementById("results").innerHTML = JSONstructure;
}

function sortBy(value) {
  sortByValue(myObjJSON.posts, value);
  var i = "";
  var sortedObject = "";
  sortedObject += "<h2>Posts from r/funny sorted by " + value + ": </h2>";
  for (i in myObjJSON.posts) {
    sortedObject += "<h3>Title: " + myObjJSON.posts[i].title + "<br>Upvotes: " + myObjJSON.posts[i].upvotes + "<br>Score: " + myObjJSON.posts[i].score + "<br>Number of comments: " + myObjJSON.posts[i].num_comments + "<br>Created at: " + new Date(myObjJSON.posts[i].created*1000).toGMTString() + "</h3>";
  }
  sortedObject += "<h2>Number of posts: " + myObjJSON.count + "</h2>";
  document.getElementById("results").innerHTML = sortedObject;
}

function sortByValue(array, key){
  return array.sort(function(a,b){
        var x = a[key]; var y = b[key];
        let comparison = 0;
        if(x<y) comparison = -1;
        if(x>y) comparison = 1;
        return comparison;
  });
}

var ratio = "";
function bestRatio(){
  ratio = new Array(myObjJSON.posts.length);
  var bestRatioPosition = 0;
  var temp = 0;
  for(let i=0;i<myObjJSON.posts.length;i++){
    ratio[i] = myObjJSON.posts[i].upvotes/myObjJSON.posts[i].num_comments;
    if(temp<ratio[i]){
      bestRatioPosition = i;
      temp = ratio[i];    
    }
    else if(temp==ratio[i]){
      if(myObjJSON.posts[bestRatioPosition].created>myObjJSON.posts[i].created){ 
        continue;
      }
      else{
        bestRatioPosition = i;
        temp = ratio[i];
      }
    }
  }
  var bestRatioObject = "<h3>Title: " + myObjJSON.posts[bestRatioPosition].title + "<br></h3>";
  document.getElementById("results").innerHTML = bestRatioObject;
}

function viewLastDay(){
  var i = "";
  var lastDayPosts = "";
  var dateDayAgo = (Date.now()/1000)-86400;
  for(i in myObjJSON.posts){
    if(myObjJSON.posts[i].created<=dateDayAgo&&myObjJSON.posts[i].created>=(dateDayAgo-86400)){
      lastDayPosts += "<h3>Title: " + myObjJSON.posts[i].title + "<br>Upvotes: " + myObjJSON.posts[i].upvotes + "<br>Score: " + myObjJSON.posts[i].score + "<br>Number of comments: " + myObjJSON.posts[i].num_comments + "<br>Created at: " + new Date(myObjJSON.posts[i].created*1000).toGMTString() + "</h3>";
    }
  }
  document.getElementById("results").innerHTML = lastDayPosts;
}
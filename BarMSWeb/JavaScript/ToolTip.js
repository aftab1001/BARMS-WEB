// JScript File

// JScript File


        
var cX = 0; var cY = 0; var rX = 0; var rY = 0;
function UpdateCursorPosition(e){ cX = e.pageX; cY = e.pageY;}
function UpdateCursorPositionDocAll(e){ cX = event.clientX; cY = event.clientY;}
if(document.all) { document.onmousemove = UpdateCursorPositionDocAll; }
else { document.onmousemove = UpdateCursorPosition; }
function AssignPosition(d) {

    if (self.pageYOffset) {
        
        rX = self.pageXOffset;
       
        rY = self.pageYOffset;
    }
    else if (document.documentElement && document.documentElement.scrollTop) {
        rX = document.documentElement.scrollLeft;
        rY = document.documentElement.scrollTop;
    }
    else if (document.body) {
        rX = document.body.scrollLeft;
        rY = document.body.scrollTop;

    }
    if (document.all) {
        cX += rX;
        cY += rY;
    }

    
    
    
    d.style.left = (cX + 10) + "px";
    d.style.top = (cY + 10) + "px";
   
//    var wh = screen.width - (cX);
//   
//    if (wh < 300) {
//        if (wh < 151) {
//            d.style.left = (cX - (wh+150)) + "px";
//        }
//        else {
//            d.style.left = (cX - wh) + "px";
//        }
//        
//    }
    
   // alert(screen.width);
}
var clearTheTime;
function HideContent(d) {
if(d.length < 1) { return; }
clearTimeout(clearTheTime);
document.getElementById(d).style.display = "none";
}
function ShowContent1(d) {
//alert("div is " + d);
//alert("Div Message " + msg);
var dd = document.getElementById(d);
//alert("Found div is " + dd);
if(dd.style.display == "none")
    {
    //clearTheTime=setTimeout("ShowContent('" + d + "','" + msg +"','"+ path + "')",1000)};
    clearTheTime=setTimeout("ShowContent('" + d + "')",100)
    }
else
{
dd.style.display = "none";
}
}
function ShowContent(d) { 
if(d.length < 1) { return; }
var dd = document.getElementById(d);
//alert(dd);
AssignPosition(dd);
dd.style.display = "block";
//alert(msg);
//dd.innerHTML= msg;
//alert(dd.innerHTML);
//var divimg= document.getElementById("imgdiv");
//divimg.src=path;
}
function ReverseContentDisplay(d) {
if(d.length < 1) { return; }
var dd = document.getElementById(d);
AssignPosition(dd);
if(dd.style.display == "none") { dd.style.display = "block"; }
else { dd.style.display = "none"; }
}
//-->
   
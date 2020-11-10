// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


    var map;
    
    var x = document.getElementById("location");
    var current;
    var latPos, longPos;
    
getLocation();
    
    
function getLocation() {
  if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
  } else {
        x.innerHTML = "Geolocation is not supported by this browser.";
  }
}


function showPosition(position) {

        latPos = position.coords.latitude;
    longPos = position.coords.longitude;
  
    x.innerHTML = "Latitude: " + latPos +
  "<br>Longitude: " + longPos;
  
    current = new google.maps.LatLng(latPos, longPos)
  
   
  var marker = new google.maps.Marker({position: current, map: map});
      
   
        map.setCenter(current);
       
}
      
function initMap() {

    map = new google.maps.Map(
        document.getElementById("map"), { zoom: 7, center: { lat: -25.344, lng: 131.036 } });
}



google.maps.event.addDomListener(window, 'load', initMap);
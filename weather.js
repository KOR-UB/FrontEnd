const weatherMain = document.querySelectorAll(".weather_main");
const weatherIcon = document.querySelectorAll(".weather_icon");
const weatherText = document.querySelectorAll(".weather_text");

const API_KEY = "64bb4412769a73988b668782663bd5f7";
const COORDS = `coords`;

const week = ["일", "월", "화", "수", "목", "금", "토"];
const date = new Date();
const today = date.getDay();
const $city = document.querySelector('.js_city');

function getCity(lat, lng) {
  fetch(`https://api.openweathermap.org/data/2.5/weather?lat=${lat}&lon=${lng}&appid=${API_KEY}&units=metric`)
    .then(function (response) {
      return response.json();
    }).then(function (json) {
      console.dir(json);
      const temperature = json.main.temp;
      const place = json.name;
      $city.textContent = `${temperature}°C / ${place}`;
    });
}

function getWeather(lat, lng) {
  fetch(`https://api.openweathermap.org/data/2.5/onecall?lat=${lat}&lon=${lng}&appid=${API_KEY}&units=metric&lang=kr`)
    .then(function (response) {
      return response.json();
    }).then(function (json) {
      console.dir(json);
      weatherMain.forEach((main, i) => {
        const j = i > 0 ? (i * 8) : i;
        switch (json.daily[i].weather[0].main) {
          case "Clear":
            weatherIcon[i].className = "fa fa-sun";
            json.daily[i].weather[0].main = "맑음";
            break;
          case "Rain":
            weatherIcon[i].className = "fa fa-cloud-rain";
            json.daily[i].weather[0].main = "비옴";
            break;
          case "Clouds":
            weatherIcon[i].className = "fa fa-cloud";
            json.daily[i].weather[0].main = "구름";
            break;
          case "Mist":
          case "Smoke":
          case "Haze":
          case "Dust":
          case "Fog":
            console.log();
          default:
            weatherIcon[i].className = "fa fa-wind";
            json.daily[i].weather[0].main = "바람";
        }
        main.textContent = `날씨 ${json.daily[i].weather[0].main} /  ${json.daily[i].temp.min}°C / ${json.daily[i].temp.max}°C`;
        weatherText[i].textContent = week[(today + i) === 7 ? 0 : (today + i) === 8 ?  1 : (today + i) === 9 ? 2 : (today + i) === 10 ? 3 : today + i];
        weatherText[0].textContent = "ToDay";
      });
    });
}

function saveCoords(coordsObj) {
  localStorage.setItem(COORDS, JSON.stringify(coordsObj));
}

function handleGeoSucces(position) {
  const latitude = position.coords.latitude;
  const longitude = position.coords.longitude;
  const coordsObj = {
    latitude,
    longitude
  };
  saveCoords(coordsObj);
  getWeather(latitude, longitude);
  getCity(latitude, longitude);
}

function handleGeoError() {
  console.log("Cant acces geo location");
}

function askForCoords() {
  navigator.geolocation.getCurrentPosition(handleGeoSucces, handleGeoError);
}

function loadCoords() {
  const loadedCords = localStorage.getItem(COORDS);
  if (loadedCords === null) {
    askForCoords();
  } else {
    const parsedCoords = JSON.parse(loadedCords);
    getWeather(parsedCoords.latitude, parsedCoords.longitude);
    getCity(parsedCoords.latitude, parsedCoords.longitude);
  }
}

function init() {
  loadCoords();
}
init();
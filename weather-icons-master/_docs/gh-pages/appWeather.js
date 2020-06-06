const weatherMain = document.querySelectorAll(".weather_main");
const weatherIcon = document.querySelectorAll(".weather_icon");
const weatherText = document.querySelectorAll(".weather_text");

const API_KEY = "64bb4412769a73988b668782663bd5f7";
const COORDS = `coords`;

const week = ["일", "월", "화", "수", "목", "금", "토"];
const date = new Date();
const today = date.getDay();
const $temp = document.querySelector('.js_temp');
const $city = document.querySelector('.js_city');
const $clock = document.querySelector('.js_clock');
const $cityNowBtn = document.querySelector(".city_now");


function getKoreaCityList() {
  fetch('cityKR.list.json').then(function(response){
    return response.json();
  }).then(function(cityList){
    // console.dir(cityList);
    // console.log(cityList.filter(list => list.name === "서울"));
    const $select = document.querySelector(".city_select")
    $select.addEventListener("change", (e) => {
      const cityObj = cityList.filter(city => city.name === e.target.value);
      const { coord } = cityObj[0];
      const { lat, lon } = coord;
      getWeather(lat, lon);
      getCity(lat, lon);
    })
  });
}
function getCity(lat, lng) {
  fetch(`https://api.openweathermap.org/data/2.5/weather?lat=${lat}&lon=${lng}&appid=${API_KEY}&units=metric`)
    .then(function (response) {
      return response.json();
    }).then(function (city) {
      console.dir(city);
      $city.textContent = city.name;
    });
  setInterval(() => {
    const date = new Date();
    const h = date.getHours();
    const m = date.getMinutes();
    const s = date.getSeconds();
    if (m === 0 && s === 0) {
      getWeather(lat, lng);
    }
    $clock.textContent = `${h < 10 ? `0${h}` : h}:${m < 10 ? `0${m}` : m}:${s < 10 ? `0${s}` : s}`;
  }, 1000);
}

function getWeather(lat, lng) {
  fetch(`https://api.openweathermap.org/data/2.5/onecall?lat=${lat}&lon=${lng}&appid=${API_KEY}&units=metric&lang=kr`)
    .then(function (response) {
      return response.json();
    }).then(function (weatherJson) {
      console.dir(weatherJson);
      const { daily, hourly } = weatherJson;
      weatherMain.forEach((main, i) => {
        switch (daily[i].weather[0].main) {
          case "Thunderstorm":
            weatherIcon[i].className = 'wi wi-lightning';
            break;
          case "Rain":
          case "Drizzle":
            weatherIcon[i].className = 'wi wi-raindrops';
            break;
          case "Snow":
            weatherIcon[i].className = 'wi wi-snowflake-cold';
            break;
          case "Mist":
          case "Smoke":
          case "Haze":
          case "Dust":
          case "Fog":
          case "Sand":
          case "Dust":
          case "Ash":
          case "Squall":
          case "Tornado":
            weatherIcon[i].className = 'wi wi-fog';
            break;
          case "Clear":
            weatherIcon[i].className = 'wi wi-day-sunny';
            break;
          case "Clouds":
            weatherIcon[i].className = "wi wi-cloudy";
            break;
          default:
            weatherIcon[i].className = 'wi wi-refresh';
            // weather.daily[i].weather[0].main = "";
        }
        main.textContent = `${daily[i].weather[0].description} /  ${daily[i].temp.min}°C / ${daily[i].temp.max}°C`;
        weatherText[i].textContent = week[(today + i) === 7 ? 0 : (today + i) === 8 ? 1 : (today + i) === 9 ? 2 : (today + i) === 10 ? 3 : today + i];
        weatherText[0].textContent = "ToDay";
        const temperature = hourly[0].temp;
        $temp.textContent = `${temperature}°C`;
      });
    });
}
function handleGeoSuccess(position) {
  const latitude = position.coords.latitude;
  const longitude = position.coords.longitude;
  getCity(latitude, longitude);
  getWeather(latitude, longitude);
}

function handleGeoError() {
  console.log("Cant acces geo location");
}

function askForCoords() {
  navigator.geolocation.getCurrentPosition(handleGeoSuccess, handleGeoError);
}
$cityNowBtn.addEventListener("click", (e) => {
  askForCoords();
});
function init() {
  askForCoords();
  getKoreaCityList();
}
init();
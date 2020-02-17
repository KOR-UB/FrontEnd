//#region Clock
const clock = document.querySelector(".js-clock"),
clockText = clock.querySelector("h1");

function ClockCheck()
{
    const date = new Date();
    const h = date.getHours();
    const m = date.getMinutes();
    const s = date.getSeconds();
    clockText.innerText = `${h < 10 ? `0${h}` : h}:${m < 10 ? `0${m}` : m}:${s < 10 ? `0${s}` : s}`
}
function Clockinit()
{
    ClockCheck();
    setInterval(ClockCheck, 1000);
}
Clockinit();
//#endregion
//#region Greetings
const form = document.querySelector(".js-form"),
input = form.querySelector("input"),
greetings = document.querySelector(".js-greetings");

const USER_LS = "currentUser",
SHOWING_CN = "showing";

function loadName()
{
    const currentUser = localStorage.getItem(USER_LS)
    if(currentUser === null)
    {
        askForName();
    }
    else
    {
        paintGreeting(currentUser);
    }
}
function askForName()
{   
    form.classList.add(SHOWING_CN);
    form.addEventListener("submit", handleSubmit);
}
function handleSubmit(event)
{
    event.preventDefault();
    currentUser = input.value;
    paintGreeting(currentUser);
    saveName(currentUser);
}
function saveName(text)
{
    localStorage.setItem(USER_LS, text);
}
function paintGreeting(text)
{
    form.classList.remove(SHOWING_CN);
    greetings.classList.add(SHOWING_CN);
    greetings.innerText = `안녕하세요 ${text}`
}
loadName();
//#endregion
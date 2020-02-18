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
    const currentUser = localStorage.getItem(USER_LS);
    if(currentUser === null)
    {
        askForName();
    }
    else
    {
        paintGreeting(currentUser);
    }
}
function saveName(text)
{
    localStorage.setItem(USER_LS, text)
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
function paintGreeting(text)
{
    form.classList.remove(SHOWING_CN);
    greetings.classList.add(SHOWING_CN);
    greetings.innerText = `안녕하세요 ${text}`
}

loadName();

//#endregion
//#region toDoList
const toDoForm = document.querySelector(".js-toDoForm"),
toDoInput = toDoForm.querySelector("input"),
toDoList = document.querySelector(".js-toDoList");

const TODOS_LS = "toDos";
let toDos = [];

function saveToDos()
{
    localStorage.setItem(TODOS_LS, JSON.stringify(toDos));
}
function deleteToDo(event)
{
    const btn = event.target;
    const li = btn.parentNode;
    toDoList.removeChild(li);
    const cleanToDos = toDos.filter(function(toDo)
    {
       return toDo.id !== parseInt(li.id);
    });
    toDos = cleanToDos;
    saveToDos();
}
function paintToDo(text)
{
    const li = document.createElement("li");
    const span = document.createElement("span");
    const delBtn = document.createElement("button");
    const newId = toDos.length +1;
    delBtn.innerText = "X";
    span.innerText = text;
    delBtn.addEventListener("click",deleteToDo);
    li.appendChild(span);
    li.appendChild(delBtn);
    li.id = newId
    toDoList.appendChild(li);
    const toDoObj ={
        text : text,
        id : newId
    };
    toDos.push(toDoObj);
    saveToDos();
}
function submitHandle(event)
{
    event.preventDefault();
    const currentvalue = toDoInput.value;
    paintToDo(currentvalue);
    toDoInput.value = "";
}
function toDoInit()
{
    loadToDos();
    toDoForm.addEventListener("submit", submitHandle)
}
function loadToDos()
{
    const loadedToDos = localStorage.getItem(TODOS_LS);
    if(loadedToDos !== null)
    {
        const parsedToDos = JSON.parse(loadedToDos);
        parsedToDos.forEach(function(toDo){
            paintToDo(toDo.text)
        });
    }
}
toDoInit();
//#endregion
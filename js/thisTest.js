const form = document.querySelector(".js-form");
const input = form.firstElementChild;
const body = document.querySelector("body");
const container = document.querySelector(".container");
const left = container.querySelector(".left"),
center = container.querySelector(".center"),
right = container.querySelector(".right");
const todo_form = document.querySelector(".todo-form");
const todo_ul = document.querySelector('.todo-ul');
const todoinput = todo_form.querySelector("input");

todo_form.addEventListener("submit", todolist)

function todolist(e)
{
    if(todoinput.value == "")
    {
        e.preventDefault();
        return;
    }
    e.preventDefault();
    const li = document.createElement("li");
    li.innerText = todoinput.value
    todoinput.value = "";
    todo_ul.appendChild(li);
    li.scrollIntoView();
}




const randbglist = ["red", "blue", "black", "skyblue"];
const objlist = [];

function NEWObject(name)
{
    this.name = name;
    this.isWindow = function()
    {
        return this === window;
    }
}
function randbg(randNum)
{
    const createDiv = document.createElement("div");
    createDiv.className = "box";
    if(randNum > 0 && randNum <= 20)
    {
        // body.style.background = randbglist[0];
        createDiv.style.background = randbglist[0];
        body.appendChild(createDiv);
    }
    else if(randNum > 20 && randNum <= 40)
    {
        // body.style.background = randbglist[1];
        createDiv.style.background = randbglist[1];
        left.appendChild(createDiv);
        
    }
    else if(randNum > 40 && randNum <= 60)
    {
        // body.style.background = randbglist[2];
        createDiv.style.background = randbglist[2];
        center.appendChild(createDiv);
    }
    else if(randNum > 60 && randNum <= 80)
    {
        // body.style.background = randbglist[3];
        createDiv.style.background = randbglist[3];
        right.appendChild(createDiv);
    }
    else if(randNum > 80 && randNum <= 100)
    {
        // body.style.background = randbglist[4];
        createDiv.style.background = randbglist[0];
        body.appendChild(createDiv);
    }   
}
// const newobj = new NEWObject("ub", "black");
// objlist.push = newobj;


form.addEventListener("submit", paintinput);

function paintinput(e)
{    
    if(input.value == "")
    {
        e.preventDefault();
        return;
    }
    e.preventDefault();
    const randNum =  Math.floor(Math.random() * (101 - 1) + 1);
    console.log(randNum + typeof(randNum));
    randbg(randNum);
    const newobj = new NEWObject(input.value);
    input.value = "";
    objlist.push = newobj;
}

// console.log(objlist);

// console.log(input);
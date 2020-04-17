const form = document.querySelector(".js-form");
const input = form.firstElementChild;
const body = document.querySelector("body");


const objlist = [];


function NEWObject(name, color)
{
    this.name = name;
    this.color = color;
    this.isWindow = function()
    {
        return this === window;
    }
}
// const newobj = new NEWObject("ub", "black");
// objlist.push = newobj;
const randbg = ["red", "blue", "black", "skyblue"]

form.addEventListener("submit", paintinput);

function paintinput(e)
{    
    e.preventDefault();
    const newobj = new NEWObject(input.value, "red");
    input.value = "";
    objlist.push = newobj;
    console.log(newobj);
}





// console.log(objlist);

// console.log(input);
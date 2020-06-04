const TODOS_LS = "toDos";
let todos = [];

class ToDoList {
  constructor(domNode) {
    this.listNode = domNode;
    // this.id = _idGenerator() - 1;
    this.itemNode = null;
    this.inputNode = null;
    this.labelNode = null;
    this.iconNode = null;
    this.timeNode = null;
  }
  init() {
    // this._domNodeGenerator("테스트");
    this._bindEvents();
    const loadedToDos = localStorage.getItem(TODOS_LS);
    if(loadedToDos !== null) {
      const parsedToDos = JSON.parse(loadedToDos);
      todos = parsedToDos;
      parsedToDos.forEach(todo => {
          this._domNodeGenerator(todo);
      });
    }
  }
  _bindEvents() {
    const { listNode } = this;
    listNode.addEventListener("change", (e) => {
      if (!e.target.matches("li > input.checkbox")) return;
      todos = todos.map(todo => todo = todo.id === parseInt(e.target.parentNode.id) ? {...todo, complete: !todo.complete}: todo);
      console.log(todos);
      localStorage.setItem(TODOS_LS, JSON.stringify(todos));
      _completeText();
    });
    listNode.addEventListener("click", (e) => {
      if (!e.target.matches("li > i")) return;
      todos = todos.filter(todo => todo.id !== parseInt(e.target.parentNode.id));
      console.log(todos);
      localStorage.setItem(TODOS_LS, JSON.stringify(todos));
      e.target.parentNode.classList.toggle("bye");
      setTimeout(() => {
        $todos.removeChild(e.target.parentNode);
      },1000)
      _completeText();
    });

  }
  _domNodeGenerator(content) {
    console.log(content);
    const { listNode } = this;

    this.itemNode = document.createElement("li");
    this.inputNode = document.createElement("input");
    this.labelNode = document.createElement("label");
    this.iconNode = document.createElement("i");
    this.timeNode = document.createElement("time");

    this.itemNode.id = content.id;
    this.itemNode.className = "todo-item";
    this.itemNode.style = `--i: ${document.querySelectorAll(".todo-item").length};`

    this.inputNode.id = `ck-${content.id}`;
    this.inputNode.className = "checkbox";
    this.inputNode.setAttribute("type", "checkbox");
    this.inputNode.checked = content.complete;

    this.labelNode.setAttribute("for", `ck-${content.id}`);
    this.labelNode.textContent = content.content;

    this.iconNode.className = "remove-todo far fa-times-circle";

    this.timeNode.setAttribute("datetime", `${content.year}-${content.month < 10 ? `0${content.month}` : content.month}-${content.day < 10 ? `0${content.day}` : content.day}-${content.h < 10 ? `0${content.h}` : content.h}:${content.m < 10 ? `0${content.m}` : content.m}:${content.s < 10 ? `0${content.s}` : content.s}`);
    this.timeNode.textContent = `${content.year} / ${content.month < 10 ? `0${content.month}` : content.month} / ${content.day < 10 ? `0${content.day}` : content.day} / ${content.h < 10 ? `0${content.h}` : content.h}:${content.m < 10 ? `0${content.m}` : content.m}:${content.s < 10 ? `0${content.s}` : content.s}`;

    this.itemNode.appendChild(this.inputNode);
    this.itemNode.appendChild(this.labelNode);
    this.itemNode.appendChild(this.iconNode);
    this.itemNode.appendChild(this.timeNode);
    listNode.appendChild(this.itemNode);
    _completeText();
    this.itemNode.scrollIntoView();
  }
}
class InputToDo {
  constructor(domNode) {
    this.inputToDoNode = domNode;
  }
  init() {
    this._bindEvents();
  }
  static keyCode = {
    enter: 13
  }
  _bindEvents() {
    const { inputToDoNode } = this;
    inputToDoNode.addEventListener("keyup", this.inputHandler.bind(this));
  }
  _idGenerator = () => {
    let ID = todos.map(todo => todo.id);
    return ID.length ? Math.max(...ID) + 1 : 1;
  }
  inputHandler(e) {
    const { enter } = InputToDo.keyCode;
    if (e.keyCode !== enter || e.target.value.trim() === "") return;
    const date = new Date(),
    year = date.getFullYear(),
    month = date.getMonth() + 1,
    day = date.getDay(),
    h = date.getHours(),
    m = date.getMinutes(),
    s = date.getSeconds();
    const toDoObj = {
      id: this._idGenerator(),
      content: e.target.value.trim(),
      complete: false,
      year,
      month,
      day,
      h,
      m,
      s
    };
    todos = [...todos, toDoObj];
    localStorage.setItem(TODOS_LS, JSON.stringify(todos));
    console.log(todos)
    toDoList._domNodeGenerator(toDoObj);
    e.target.value = '';
  }
}
class Footer {
  constructor(domNode) {
    this.footerNode = domNode;
    this.completeInput = null;
    this.clearBtn = null;
    this.completeTodos = null;
    this.activeTodos = null;
    // this.completeNum = 0;
    // this.activeNum = 0;
  }
  init() {
    const { footerNode } = this;
    this.completeInput = footerNode.querySelector(".checkbox");
    this.clearBtn = footerNode.querySelector(".btn");
    this.completeTodos = this.clearBtn.querySelector(".completed-todos");
    this.activeTodos = footerNode.querySelector(".active-todos");
    
    this._bindEvents();
    _completeText();
  }
  _bindEvents() {
    const { completeInput, clearBtn } = this;
    completeInput.addEventListener("change", (e) => {
      e.target.checked ? todos = todos.map(todo => todo = {...todo, complete : true}) : todos = todos.map( todo => todo = {...todo, complete : false});  
      console.log(todos);
      document.querySelectorAll(".todos li input.checkbox").forEach(item => {
        item.checked = item.checked = e.target.checked;
      });
      localStorage.setItem(TODOS_LS, JSON.stringify(todos));
      _completeText();
    });
    clearBtn.addEventListener("click", (e) => {
      const checkboxAll = document.querySelectorAll(".todos li input.checkbox")
      checkboxAll.forEach(item => item.checked ? item.parentNode.classList.toggle("bye") : !item.checked);
      completeInput.checked = false;
      todos = todos.filter(todo => !todo.complete);
      localStorage.setItem(TODOS_LS, JSON.stringify(todos));
      console.log(todos);
      setTimeout(() => {
        checkboxAll.forEach(item => item.checked ? $todos.removeChild(item.parentNode) : !item.checked);
        _completeText();
      }, 1000)
    });
  }
}
const completeTodos = document.querySelector(".completed-todos");
const activeTodos = document.querySelector(".active-todos");
let completeNum = 0;
let activeNum = 0;
_completeText = () => {
  completeNum = todos.filter(todo => todo.complete === true).length;
  completeTodos.textContent = completeNum;
  activeNum = todos.length - completeNum;
  activeTodos.textContent = activeNum;
}
const $todos = document.querySelector(".todos");
const $input_todo = document.querySelector(".input-todo")
const $footer = document.querySelector("footer");
const toDoList = new ToDoList($todos);
const inputToDo = new InputToDo($input_todo);
const _Footer = new Footer($footer);
window.onload = () => {
  inputToDo.init();
  toDoList.init();
  _Footer.init();
}
// const $complete_todos = document.querySelector(".completed-todos");
// const $active_todos = document.querySelector(".active-todos");
// _idGenerator = () => {
//   let ID = todos.map(todo => todo.id);
//   return ID.length ? Math.max(...ID) + 1 : 1;
// }
// const $input_todo = document.querySelector(".input-todo");
// $input_todo.addEventListener("keyup", function (e) {
//   if (e.keyCode !== 13 || e.target.value.trim() === "") return;
//   const toDoObj = {
//     id: _idGenerator(),
//     content: e.target.value.trim(),
//     complete: false
//   };
//   todos = [...todos, toDoObj];
//   console.log(todos);

//   const toDoList = new ToDoList($todos);
//   toDoList._domNodeGenerator(e.target.value);
//   e.target.value = "";
// });
// const $btn = document.querySelector(".btn");
// $btn.addEventListener("click", function(e) {
//   $complete_all.checked = false;
//   todos = todos.filter(todo => !todo.complete);
//   document.querySelectorAll(".todos li input.checkbox").forEach(item => item.checked ? $todos.removeChild(item.parentNode): !item.checked);
//   todos.sort( (a, b) => a.id - b.id);
// });
// const $complete_all = document.getElementById("ck-complete-all");
// $complete_all.addEventListener("change", function(e) {
//   e.target.checked ? todos = todos.map( todo => todo = {...todo, complete : true}) : todos = todos.map( todo => todo = {...todo, complete : false});
//   document.querySelectorAll(".todos li input.checkbox").forEach(item => {
//     item.checked = item.checked ? false : true;
//   });
//   $active_todos.textContent = todos.filter(todo => todo.complete === false).length;
//   $complete_todos.textContent = todos.filter(todo => todo.complete === true).length;
//   console.log(todos)
// });

// const $todos = document.querySelector(".todos");
// $todos.addEventListener("change", function(e) {
//   if (!e.target.matches("li > input.checkbox")) return;
//     $complete_all.checked = false;
//   todos = todos.map(todo => todo = todo.id === parseInt(e.target.parentNode.id) ? {...todo, complete: !todo.complete} : todo);
//   console.log(todos)
//   $active_todos.textContent = todos.filter(todo => todo.complete === false).length;
//   $complete_todos.textContent = todos.filter(todo => todo.complete === true).length;
// });
// window.onload = () => {
//   const toDoList = new ToDoList($todos);
//   // toDoList.init();
// }
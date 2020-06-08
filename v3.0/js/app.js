// State
let todos = [];
const TODOS_LS = "toDos";

const $body = document.querySelector("body");

class Body {
  constructor(domNode) {
    this.bodyNode = domNode;
    this.inputNode = null;
    this.navNode = null;
    this.todosNode = null;
    this.footerNode = document.querySelector("footer");
    this.completeAllNode = null;
    this.completeTodosNode = null;
    this.clearCompletedNode = null;
    this.activeTodosNode = null;
  }
  static keyCode = {
    enter: 13
  }
  init() {
    this._domNodeSettings();
    this._bindEvents();
    const loadedToDos = localStorage.getItem(TODOS_LS);
    if(loadedToDos !== null) {
      const parsedToDos = JSON.parse(loadedToDos);
      todos = parsedToDos;
      parsedToDos.forEach(todo => {
        this.paintTodo(todo);
      });
    }
    this.SaveTodos();
  }
  _domNodeSettings() {
    const { bodyNode, footerNode } = this;
    this.inputNode = bodyNode.querySelector(".input-todo");
    this.navNode = bodyNode.querySelector(".nav");
    this.todosNode = bodyNode.querySelector(".todos");
    this.completeAllNode = footerNode.querySelector(".checkbox");
    this.clearCompleteNode = footerNode.querySelector(".btn");
    this.completeTodosNode = footerNode.querySelector(".completed-todos");
    this.activeTodosNode = footerNode.querySelector(".active-todos");
  }
  _bindEvents() {
    const { inputNode, navNode, todosNode, completeAllNode, clearCompleteNode } = this;
    inputNode.addEventListener("keyup", this.handleKeyUp.bind(this));
    navNode.addEventListener("click", this.handleToggle.bind(this));
    todosNode.addEventListener("change", this.handleCheck.bind(this));
    todosNode.addEventListener("click", this.handleDeleteTodo.bind(this));
    completeAllNode.addEventListener("change", this.handleCheckAll.bind(this));
    clearCompleteNode.addEventListener("click", this.handleClearTodos.bind(this));
  }
  handleKeyUp(e) {
    const { enter } = Body.keyCode;
    if (e.keyCode !== enter || e.target.value.trim() === "") return;
    e.target.value = e.target.value.trim();
    const date = new Date(),
    year = date.getFullYear(),
    month = date.getMonth() + 1,
    day = date.getDay(),
    h = date.getHours(),
    m = date.getMinutes(),
    s = date.getSeconds();
    const toDoObj = {
      id: this._idGenerator(),
      content: e.target.value,
      complete: false,
      year,
      month,
      day,
      h,
      m,
      s
    }
    todos = [...todos, toDoObj];
    this.SaveTodos();
    console.log(todos);
    e.target.value = ""
    this.paintTodo(toDoObj);
  }
  _idGenerator() {
    return todos.length ? Math.max( ...todos.map(todo => todo.id)) + 1 : 1;
  }
  paintTodo(todo) {
    const { todosNode } = this;
    const { id, content, complete, year, month, day, h, m, s } = todo;
    const $li = document.createElement("li"),
    $input = document.createElement("input"),
    $label = document.createElement("label"),
    $i = document.createElement("i"),
    $time = document.createElement("time");

    $li.id = id;
    $li.className = "todo-item";
    $li.style = `--i: ${document.querySelectorAll(".todo-item").length}`;

    $input.id = `ck-${id}`;
    $input.className = "checkbox";
    $input.setAttribute("type", "checkbox");
    $input.checked = complete;
    
    $label.setAttribute("for", `ck-${id}`);
    $label.textContent = content;

    $i.className = "remove-todo far fa-times-circle";

    $time.setAttribute("datetime", `${year}-${month < 10 ? `0${month}` : month}-${day < 10 ? `0${day}` : day}-${h < 10 ? `0${h}` : h}:${m < 10 ? `0${m}` : m}:${s < 10 ? `0${s}` : s}`);
    $time.textContent = `${year} / ${month < 10 ? `0${month}` : month} / ${day < 10 ? `0${day}` : day} / ${h < 10 ? `0${h}` : h}:${m < 10 ? `0${m}` : m}:${s < 10 ? `0${s}` : s}`;

    $li.appendChild($input);
    $li.appendChild($label);
    $li.appendChild($i);
    $li.appendChild($time);
    todosNode.appendChild($li);
  }
  handleToggle(e) {
    const { navNode, todosNode, completeAllNode } = this;
    if(!e.target.matches(".nav > li")) return
    // if(!e.target.matches(".nav > li:not(.active)"));
    // [...navNode.children].forEach(item => {
    //   item.classList.toggle("active", item === target);
    // });
    const $active = navNode.querySelector(".active");
    if($active === e.target) return;
    $active.classList.remove("active");
    e.target.classList.add("active");
    todosNode.querySelectorAll("li").forEach(item => todosNode.removeChild(item));
    switch(e.target.id) {
      case "all":
        todos.forEach(todo => {
          this.paintTodo(todo);
        })
        break;
      case "active":
        const activeTodos = todos.filter(todo => !todo.complete)
        activeTodos.forEach(todo => {
          this.paintTodo(todo);
        });
        completeAllNode.checked = false;
        break;
      case "completed":
        const completeTodos = todos.filter(todo => todo.complete);
        completeTodos.forEach(todo => {
          this.paintTodo(todo);
        });
        completeAllNode.checked = true;
        break;
    }
  }
  handleCheck(e) {
    const { completeAllNode } = this;
    if (!e.target.matches("li > input.checkbox")) return;
    todos = todos.map(todo => todo = todo.id === parseInt(e.target.parentNode.id) ? {...todo, complete: !todo.complete}: todo);
    completeAllNode.checked = todos.every(todo => todo.complete) ? true : false;
    this.SaveTodos();
    console.log(todos);
    this.changeToggle();
  }
  handleDeleteTodo(e) {
    const { todosNode } = this;
    if (!e.target.matches("li > i")) return;
    todos = todos.filter(todo => todo.id !== parseInt(e.target.parentNode.id));
    console.log(todos);
    this.SaveTodos();
    e.target.parentNode.classList.toggle("bye");
    setTimeout(() => {
      todosNode.removeChild(e.target.parentNode);
    },1000)
  }
  handleCheckAll(e) {
    e.target.checked ? todos = todos.map(todo => todo = {...todo, complete : true}) : todos = todos.map( todo => todo = {...todo, complete : false});
    document.querySelectorAll(".todos li input.checkbox").forEach(item => {
      item.checked = item.checked = e.target.checked;
    });
    this.SaveTodos();
    console.log(todos);
    this.changeToggle();
  }
  handleClearTodos(e) {
    const { todosNode, completeAllNode } = this;
    const checkboxAll = todosNode.querySelectorAll("li input.checkbox")
    console.log(checkboxAll);
    checkboxAll.forEach(item => item.checked ? item.parentNode.classList.toggle("bye") : !item.checked);
    completeAllNode.checked = false;
    todos = todos.filter(todo => !todo.complete);
    this.SaveTodos();
    console.log(todos);
    setTimeout(() => {
      checkboxAll.forEach(item => item.checked ? todosNode.removeChild(item.parentNode) : !item.checked);
    }, 1000)
  }
  SaveTodos() {
    const { completeTodosNode, activeTodosNode } = this;
    localStorage.setItem(TODOS_LS, JSON.stringify(todos));
    completeTodosNode.textContent = todos.filter(todo => todo.complete ).length;
    activeTodosNode.textContent = todos.filter(todo => !todo.complete).length;
  }
  changeToggle() {
    const { navNode, todosNode } = this;
    const $active = navNode.querySelector(".active");
    const $all = navNode.firstElementChild;
    if($active.id === $all.id) return;
    $active.classList.remove("active");
    $all.classList.add("active");
    todosNode.querySelectorAll("li").forEach(item => todosNode.removeChild(item));
    todos.forEach(todo => {
      this.paintTodo(todo);
    });
  }
}
window.onload = () => {
  const _Body = new Body($body);
  _Body.init();
}
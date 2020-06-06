// State
let todos = [];

const $body = document.querySelector("body");

class Body {
  constructor(domNode) {
    this.bodyNode = domNode;
    this.inputNode = null;
    this.navNode = null;
    this.todosNode = null;
    this.footerNode = document.querySelector("footer");
    this.completeAll = null;
    this.completedTodos = null;
    this.clearCompleted = null;
    this.activeTodos = null;
  }
  static keyCode = {
    enter: 13
  }
  init() {
    this._domNodeSettings();
    this._bindEvents();
  }
  _domNodeSettings() {
    const { bodyNode, footerNode } = this;
    this.inputNode = bodyNode.querySelector(".input-todo");
    this.navNode = bodyNode.querySelector(".nav");
    this.todosNode = bodyNode.querySelector(".todos");
    this.completeAll = footerNode.querySelector(".checkbox");
    this.completedTodos = footerNode.querySelector(".completed-todos");
    this.clearCompleted = footerNode.querySelector(".btn");
    this.activeTodos = footerNode.querySelector(".active-todos");
  }
  _bindEvents() {
    const { inputNode, navNode, todosNode, completeAll, completedTodos, clearCompleted, activeTodos } = this;
    inputNode.addEventListener("keyup", this.handleKeyUp.bind(this));
    navNode.addEventListener("click", this.handleToggle.bind(this));
    todosNode.addEventListener("change", this.handleCheck.bind(this));
    completeAll.addEventListener("change", this.handleCheckAll.bind(this));
  }
  handleKeyUp(e) {
    const { enter } = Body.keyCode;
    if(e.keyCode !== enter || e.target.value.trim() === "") return;
    e.target.value = e.target.value.trim();
    console.log(e.target.value);
    const toDoObj = {
      id: this._idGenerator(),
      content: e.target.value,
      complete: false,
    }
    todos = [...todos, toDoObj];
    console.log(todos);
    e.target.value = ""
    this.paintTodo(toDoObj);
  }
  _idGenerator() {
    return todos.length ? Math.max( ...todos.map(todo => todo.id)) + 1 : 1;
  }
  paintTodo(todo) {
    const { todosNode } = this;
    const { id, content, completed } = todo;
    const $li = document.createElement("li"),
    $input = document.createElement("input"),
    $label = document.createElement("label"),
    $i = document.createElement("i");

    $li.id = id;
    $li.className = "todo-item";

    $input.id = `ck-${id}`;
    $input.className = "checkbox";
    $input.setAttribute("type", "checkbox");
    $input.checked = completed;
    
    $label.setAttribute("for", `ck-${id}`);
    $label.textContent = content;

    $i.className = "remove-todo far fa-times-circle";

    $li.appendChild($input);
    $li.appendChild($label);
    $li.appendChild($i);
    todosNode.appendChild($li);
  }
  handleToggle(e) {
    const { navNode } = this;
    if(!e.target.matches(".nav > li")) return
    const $active = navNode.querySelector(".active");
    if($active === e.target) return;
    $active.classList.remove("active");
    e.target.classList.add("active");
  }
  handleCheck(e) {
    const { completeAll } = this;
    if (!e.target.matches("li > input.checkbox")) return;
    todos = todos.map(todo => todo = todo.id === parseInt(e.target.parentNode.id) ? {...todo, complete: !todo.complete}: todo);
    completeAll.checked = todos.every(todo => todo.complete) ? true : false;
    console.log(todos); 
  }
  handleCheckAll(e) {
    e.target.checked ? todos = todos.map(todo => todo = {...todo, complete : true}) : todos = todos.map( todo => todo = {...todo, complete : false});
    document.querySelectorAll(".todos li input.checkbox").forEach(item => {
      item.checked = item.checked = e.target.checked;
    });
    console.log(todos);
  }
  
}
window.onload = (e) => {
  const _Body = new Body($body);
  _Body.init();
}
// document.querySelector().addEventListener;

//필터써서 자기 id보다 높은 녀석들 id를 1씩 뺀다;
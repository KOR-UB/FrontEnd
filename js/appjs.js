/* <li id="myId" class="todo-item">
<input id="ck-myId" class="checkbox" type="checkbox">
<label for="ck-myId">HTML</label>
<i class="remove-todo far fa-times-circle"></i>
</li> */
class ToDoList {
  constructor(domNode) {
    this.iistNode = domNode;
    this.id = 0;
    this.itemNode = null;
    this.inputNode = null;
    this.labelNode = null;
    this.iconNode = null;
  }
  init() {
    this._domNodeGenerator();
  }
  _domNodeGenerator(content) {
    const { listNode, id } = this;
    id++;
    this.itemNode = document.createElement("li");
    this.inputNode = document.createElement("input");
    this.labelNode = document.createElement("label");
    this.iconNode = document.createElement('i');

    this.itemNode.id = id;
    this.itemNode.className = "todo-item";

    this.inputNode.id = `ck-${id}`;
    this.inputNode.className ="checkbox";
    this.inputNode.setAttribute("type","checkbox");

    this.labelNode.setAttribute("for",`ck-${id}`);
    this.labelNode.textContent = content;

    this.iconNode.className('remove-todo far fa-times-circle');
  }
}
const $todos = document.querySelector(".todos");
window.onload = () => {
  const toDoList = new ToDoList($todos);
  toDoList.init();
} 
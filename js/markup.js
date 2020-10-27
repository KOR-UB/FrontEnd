class Markup {
  constructor(Data) {
    this.head = Data.head;
    this.body = Data.body;
    this.$body = document.body;
    this.$head = document.head;
    if (this.head) this.headInit(this.head);
    if (this.body) this.bodyInit(this.body);
  }
  headInit(head) {
    const { $head } = this;
    console.log($head);
  }
  bodyInit(body) {
    const { $body } = this;
    switch (body) {
      case "A":
        console.log($body);
        break;
      default:
        break;
    }
  }
}
new Markup({ head: "a", body: "A" });

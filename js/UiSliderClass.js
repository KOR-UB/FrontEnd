class UiSlider {
  constructor(domNode) {
    this.sliderNode = domNode;
    this.min = 0;
    this.now = 0;
    this.max = 100;
    this.unit = "%";
    this.railWidth = 0;
    this.thumbWidth = 0;
    this.thumbHeight = 0;
    this.railNode = null;
    this.thumbNode = null;
    this.valueNode = null;
  }
  init() {
    this._findDomNodeAndSettings();
    this._bindEvents();
  }
  _findDomNodeAndSettings() {
    const { sliderNode } = this;
    this.railNode = sliderNode.querySelector(".UiSlider__rail");
    this.thumbNode = sliderNode.querySelector(".UiSlider__thumb");
    this.valueNode = sliderNode.querySelector(".UiSlider__value");

    if (this.thumbNode.tabIndex !== 0) {
      this.thumbNode.tabIndex = 0
    }
    if (this.thumbNode.getAttribute("role") !== "slider") {
      this.thumbNode.setAttribute("role", "slider");
    }
    let valueMin = parseInt(this.thumbNode.getAttribute("aria-valuemin"), 10);
    this.min = valueMin ? valueMin : this.min;
    let valueMax = parseInt(this.thumbNode.getAttribute("aria-valuemax"), 10);
    this.max = valueMax ? valueMax : this.max;
    console.log(this.max);
    let valueNow = parseInt(this.thumbNode.getAttribute("aria-valuenow"), 10);
    this.now = valueNow ? valueNow : this.now;
    if(this.valueNode) {
      this.valueNode.textContent = this.now + this.unit;
    }
    else {
      this.thumbNode.setAttribute("aria-valuetext", this.now + this.unit);
    }
  }
  _bindEvents() {
    const {thumbNode, railNode} = this;
    thumbNode.addEventListener("keydown", handleThumbkeyDown);
    thumbNode.addEventListener("pointerdown", handlePointerDown);
    thumbNode.addEventListener("focus", handleThumbFocus);
    thumbNode.addEventListener("blue", handleThumbBlur);
  }
  handleThumbkeyDown() {

  }
  handlePointerDown()
  {

  }
  handleThumbFocus() {

  }
  handleThumbBlur() {
    
  }
}
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
  static keyCode = {
    left: 37,
    up: 38,
    right: 39,
    bottom: 40,
    home: 36,
    end: 35,
    pageUp: 33,
    pageDown: 34,
  }
  init() {
    this._findDomNodeAndSettings();
    this._bindEvents();
    this.moveSliderTo(this.now)
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
    let valueNow = parseInt(this.thumbNode.getAttribute("aria-valuenow"), 10);
    this.now = valueNow ? valueNow : this.now;
    if(this.valueNode) {
      this.valueNode.textContent = this.now + this.unit;
    }
    else {
      this.thumbNode.setAttribute("aria-valuetext", this.now + this.unit);
    }
    this.railWidth = this.railNode.getBoundingClientRect().width;
    this.thumbWidth = this.thumbNode.getBoundingClientRect().width;
    this.thumbHeight = this.thumbNode.getBoundingClientRect().height;
  }
  _bindEvents() {
    const {thumbNode, railNode} = this;
    thumbNode.addEventListener("keydown", this.handleThumbkeyDown.bind(this));
    thumbNode.addEventListener("focus", this.handleThumbFocus.bind(this));
    thumbNode.addEventListener("blue", this.handleThumbBlur.bind(this));
    [
      thumbNode,
      railNode
    ].forEach((node) => node.addEventListener("pointerdown", this.handlePointerDown.bind(this)));
  }
  moveSliderTo(value) {
    const { min, max, unit, thumbNode, valueNode, railWidth } = this;
    value = value < min ? min : value > max ? max : value;

    valueNode ? valueNode.textContent = value + unit : thumbNode.setAttribute("aria-valuetext", value + unit);
    
    const thumbPosition = Math.round(railWidth * value / max - min) - this.thumbWidth / 2;
    thumbNode.style.left = thumbPosition + "px";
    this.now = value;
  }
  handleThumbkeyDown(e) {
    const { left, up, right, bottom, home, end, pageUp, pageDown } = UiSlider.keyCode;

    let { now, min, max } = this;
    let isPressed = false;
    switch (e.keyCode) {
      case left:
      case bottom:
        isPressed = true;
        this.moveSliderTo(now - 1);
        break;
      case right:
      case up:
        isPressed = true;
        this.moveSliderTo(now + 1);
        break;
      case home:
        isPressed = true;
        this.moveSliderTo(min);
        break;
      case end:
        isPressed = true;
        this.moveSliderTo(max);
        break;
      case pageUp:
        isPressed = true;
        this.moveSliderTo(now + 10);
        break;
      case pageDown:
        isPressed = true;
        this.moveSliderTo(now - 10);
        break;
    }
  }
  handlePointerDown(e) {
    const { railNode, max, min, railWidth } = this;

    const handleMove = (e) => {
      const pointerPosition = e.pageX - railNode.getBoundingClientRect().x;
      const pointerValue = Math.round(pointerPosition / railWidth * (max - min));
      this.moveSliderTo(pointerValue);
    }
    const handleUp = () => {
      document.removeEventListener("pointermove", handleMove);
      document.removeEventListener("pointerup", handleUp);
    }
    const pointerPosition = e.pageX - railNode.getBoundingClientRect().x;
    const pointerValue = Math.round(pointerPosition / railWidth * (max - min));
    this.moveSliderTo(pointerValue);
    document.addEventListener("pointermove", handleMove);
    document.addEventListener("pointerup", handleUp);
  }
  handleThumbFocus() {
    const { thumbNode, railNode } = this;
    thumbNode.classList.add('focus');
    railNode.classList.add('focus');
  }
  handleThumbBlur() {
    const { thumbNode, railNode } = this;
    thumbNode.classList.remove('focus');
    railNode.classList.remove('focus');
  }
}
// const person = {
//     name: 'Lee',
//     foo(callback) {
//       // ①
//       setTimeout(callback.bind(this), 1000);
//     }
//   };

//   person.foo(function () {
//     this.name = "kim";
//     console.log(person)
//     console.log(`Hi! my name is ${this.name}.`); // ② Hi! my name is .
//     // window.name은 브라우저 창의 이름을 나타내는 프로퍼티이며 기본값은 ''이다
//     // 만약 Node.js 환경에서 실행하면 undefined가 출력된다.
//   });
// class Person {
//   constructor(name){
//      this.name = name;
//   }
//   sayHi() {
//     console.log(`Hi My name is ${this.name}`);
//   }
//   static sayHello() {
//     console.log("Hello");
//   }
// }

// const me = new Person("Youn");
// console.log(Person.prototype );
// me.sayHi();
// Person.sayHello();

// console.dir(Person)
// const person = class {};

// const person = class MyClass {};
// 수퍼 클래스
class Base {
  constructor(name) {
    this.name = name;
  }

  sayHi() {
    return `Hi! ${this.name}`;
  }
  sayHi2 = function () {
    test = function () {
      console.log("테스트");
    }
    return `Hi! ${this.name}`;
  }
}

// 서브 클래스
class Derived extends Base {
  sayHi() {
    // super.sayHi는 수퍼 클래스의 프로토타입 메소드를 가리킨다.
    return `${super.sayHi2}. how are you doing?`;
  }
}

const derived = new Derived('Lee');
console.log(derived.test); // Hi! Lee. how are you doing?
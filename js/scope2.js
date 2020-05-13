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
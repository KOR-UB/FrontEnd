// const o = {
//     foo: 1
// };

// o.bar = 2;

// Object.defineProperty(o, 'baz', {
//     value: 3,
//     writable: true,
//     configurable: true,
//     enumerable: true
// });

// console.log(Object.getOwnPropertyDescriptor(Object.prototype, '__proto__'));

// console.log(o);

// function binarySearch(array, target) {
//     let result = 0;
//     let length = array.length;
//     for (let i = 0; i < length; i++) {
//         result = result - Math.floor(length * 0.5);
//         if (target >= result)
//         {
//             if(target === array[i]) {
//                 return i;
//             }
//         }
//     }
//     return -1;
// }

//   console.log(binarySearch([1, 2, 3, 4, 5, 6], 1)); // 0
//   console.log(binarySearch([1, 2, 3, 4, 5, 6], 3)); // 2
//   console.log(binarySearch([1, 2, 3, 4, 5, 6], 5)); // 4
//   console.log(binarySearch([1, 2, 3, 4, 5, 6], 6)); // 5
//   console.log(binarySearch([1, 2, 3, 4, 5, 6], -1)); // -1
//   console.log(binarySearch([1, 2, 3, 4, 5, 6], 0)); // -1
//   console.log(binarySearch([1, 2, 3, 4, 5, 6], 7)); // -1
function binarySearch(array, target) {
  let i = 0;
  const length = array.length;
  const start = array[0];
  const end = array[length - 1];
  const mid = Math.floor(end * 0.5);
  // console.log(start);
  // console.log(end);
  // console.log(mid);
//   검색 대상 값이 중간값보다 작은 경우 중간값보다 작은 쪽(왼쪽)을 검색 범위로 한정한다.
// 검색 대상 값이 중간값보다 큰 경우 중간값보다 큰 쪽(오른쪽)을 검색 범위로 한정한다.
// 검색 대상 값을 검색할 때까지 이와 같은 처러를 반복한다.
  
  while (target < mid) {
    i++;
    if(target === array[i]) {
      return i;
    }
    else return -1;
  }
  while (target > mid) {
    i++;
    if(target === array[i]) {
      return i;
    }
    else return -1;
  }
  
}

console.log(binarySearch([1, 2, 3, 4, 5, 6], 1)); // 0
console.log(binarySearch([1, 2, 3, 4, 5, 6], 3)); // 2
console.log(binarySearch([1, 2, 3, 4, 5, 6], 5)); // 4
console.log(binarySearch([1, 2, 3, 4, 5, 6], 6)); // 5
console.log(binarySearch([1, 2, 3, 4, 5, 6], -1)); // -1
console.log(binarySearch([1, 2, 3, 4, 5, 6], 0)); // -1
console.log(binarySearch([1, 2, 3, 4, 5, 6], 7)); // -1
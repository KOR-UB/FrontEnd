// const onTwoThree = [1,2,3];

const randText = document.querySelector(".randText");
const randbtn = document.querySelector(".randbtn");
const body = document.querySelector("body");
        
randbtn.addEventListener("click", randomText());

function randomText()
{
    const randNum =  Math.floor(Math.random() * (101 - 1) + 1);
    randText.innerText =  randNum;
    if(randNum >= 0 && randNum <= 20)
    {
        // body.style.backgroundColor = "skyblue";
    }
    else if(randNum > 20 && randNum <= 40)
    {
        // body.style.backgroundColor = "#f0f30b";
    }
    else if(randNum > 40 && randNum <= 60)
    {
        // body.style.backgroundColor = "#ff00ff";
    }
    else if(randNum > 60 && randNum <= 80)
    {
        // body.style.backgroundColor = "#17fadd";
    }
    else if(randNum > 80 && randNum <= 99)
    {
        // body.style.backgroundColor = "#daf17b";
    }
    else
    {
        body.style.backgroundColor = "#000"
        randText.innerText = "축하합니다" + randNum;
        clearInterval(test);
        const main = document.querySelector(".main");
        main.classList.toggle("active");
    }
}
var test = setInterval(randomText, 100)
// const result = onTwoThree.map((v) =>
// {
//     console.log(v);
//     test(v, v*2)
//     return v;
// });
function test(a, b)
{
    console.log(a+b);
    const result = a+b;
    if(result === 9)
    {
        plus(result);
    }
}
function plus(num)
{
    console.log(`넘버! ${num}`)
    const a = (Math.random() * 100).toFixed(2); //toFixed 소수점 
    console.log(a);
}

// var z = "1";

// if(x == z)
// {
//     console.log("x와 z는 동등합니다")
// }
// if(x === z)
// {
//     console.log("서로 일치합니다")
// }
// if(x != y)
// {
//     console.log("x와 y는 불동등합니다.");
// }
// if(x != z)
// {
//     console.log("x와 z는 불동등합니다.");
// }
// if(x !== z)
// {
//     console.log("x와 z 불일치합니다.");
// }
// var x = 1;
// var y = 2;
// if(x === 1 || y === 3)
// {
//     console.log("둘중 하나라도 트루라면 트루")
// }
// if (x === 1 && y === 3)
// {
//     console.log("둘중 하나라도 false라면 false");
// }
// if(x == !null)  
// {
//     console.log("x가 널이 아니라면 실행")
// }

// function repeat(n, f) 
// {
//     for (let i = 0; i < n; i++)
//     {
//         f(i, 2, 3);
//     }
// }
// repeat(3, console.log);
// repeat(3, function (i)
// {
//     console.log(i);
// });

let change_count = 0;
function ChangeValue()
{
    change_count ++;
    switch(change_count)
    {
        case 1:
        console.log(change_count);
        break;
        case 2:
        console.log(change_count);
        break;
        case 3:
        console.log(change_count);
        break;
        default:
        change_count = 0;
        clearInterval(change);
    }
}
const change = setInterval(ChangeValue, 1000)
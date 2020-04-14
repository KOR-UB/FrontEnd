const onTwoThree = [1,2,3];

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
const result = onTwoThree.map((v) =>
{
    console.log(v);
    test(v, v*2)
    return v;
});
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
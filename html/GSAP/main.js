let chatDatas = [];
function getJson(fetchStr, lis) {
  return fetch(fetchStr, { lis })
    .then(res => res.json())
    .then(jsonData => jsonData)
    .catch(err => console.error(err));
}
const $input = document.querySelector("input");
const $chatContent = document.querySelector(".chat_content");
$input.addEventListener("keyup", e => {
  if (e.keyCode !== 13) return;
  console.log($input.value);
  $input.value = $input.value.trim();
  pushDM($input.value);
  $input.value = "";
})

function idGenerator() {
  return chatDatas.length ? Math.max( ...chatDatas.map(data => data.id)) + 1 : 1;
}
async function pushDM(value) {
  const $p = document.createElement("p");
  $p.textContent = value;
  $chatContent.appendChild($p);
  const payload = {
    id: idGenerator(),
    content: value
  }
  const _channel = await getJson("./channel.json", {
    method: "POST",
    headers: { 'content-Type': 'application/json' },
    body: JSON.stringify(payload)
  });
  chatDatas = _channel;
}
async function init() {
  const _channel = await getJson("./channel.json");
  chatDatas = _channel;
  console.log(_channel);
  _channel.forEach(item => {
    pushDM(item.content)
  })

}
init();
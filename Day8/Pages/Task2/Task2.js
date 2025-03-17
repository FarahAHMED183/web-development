const divText = document.querySelector('#div1');
const span = divText.childNodes[1].textContent.trim(); 
const text = divText.childNodes[4].textContent.trim();

console.log(span + text);

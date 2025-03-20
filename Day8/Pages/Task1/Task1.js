function createElements() {
    const num = document.querySelector('#num').value;
    const content = document.querySelector('#content').value;
    const elementType = document.querySelector('#elementType').value;
    const div = document.querySelector('#div3');

    
    div.innerHTML = '';
    div.style = "width:90%; display: grid; gap:20px; grid-template-columns: auto auto auto; text-align: center;";

    for (let i = 1; i <= num; i++) {
        const element = document.createElement(elementType);
        element.textContent = content || "Test";
        element.setAttribute("class", "box");
        element.setAttribute("id", `id-${i}`);
        element.style = "display:inline-block; background-color: #ff5722; padding:5px 10%; border-radius: 10px; padding:10px 0; font-family: sans-serif; color:white; text-align:center;";
        div.appendChild(element);
    }
}

//  styles to inputs, select, and button
document.querySelectorAll("input, select").forEach(input => {
    input.style = "width: 50%; display: block; background-color: #eee; margin-bottom: 20px; padding: 10px 0; border-radius: 10px; border-color: #dfdfdf;";
});

document.querySelector("button").style = "width: 50%; display: block; background-color: seagreen; margin-bottom: 20px; padding: 10px 0; border: none; border-radius: 10px; font-family: sans-serif; color: white; font-weight: bold;";

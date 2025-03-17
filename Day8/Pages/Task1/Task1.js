function createElements() {
    const num = document.querySelector('#num').value;
    const content = document.querySelector('#content').value;
   
    const elementType = document.querySelector('#elementType').value;
    const div = document.querySelector('#div3');
    
    div.innerHTML = ''; 
    for (let i = 1; i <= num; i++) {
        const element = document.createElement(elementType);
        element.textContent = content || "Test";
        div.appendChild(element);
    }
}

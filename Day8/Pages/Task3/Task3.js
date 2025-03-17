
document.getElementById('numInput').oninput = function() {
    const USD = this.value; 
    const EGP = (USD * 15.6).toFixed(2); 
    document.getElementById('result').textContent = `{${USD}} USD Dollar = {${EGP}} Egyptian Pound`; 
}

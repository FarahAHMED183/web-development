const images = document.querySelectorAll('img');
console.log(images);

images.forEach(img => {
    if (img.alt === "One" || img.alt=== "Three") {
        img.setAttribute('alt','Old');
    } 
    else {
       img.setAttribute('alt','Elzero New');
    }
});
console.log(images);

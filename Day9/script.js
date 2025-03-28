// Task 1
let myFriends=[
    {title:"Osama",age:39,available:true,skills:["HTML","CSS"]},
    {title:"Ahmed",age:25,available:false,skills:["Python","Django"]},
    {title:"Sayed",age:33,available:true,skills:["PHP","Laravel"]},
]
let chosen=3;
const {title,age,available,skills:[,sec]}=myFriends[chosen-1];
console.log(title);
console.log(age);
console.log(`${available ? "Available" : "Not Available"}`);
console.log(sec);


//Task 2
let my_Friends = new Set(["Osama", "Ahmed", "Sayed", "Sayed", "Mahmoud", "Osama"]);
let sortedFriends = [...my_Friends].sort();
console.log(sortedFriends);




// Task 3

let myInfo={
    username:"Osama",
    role:"Admin",
    country:"Egypt"
}

// for of >> returns entries(key, value)
for (let [key, value] of Object.entries(myInfo)) {
    console.log(`{${key} -> ${value}}`);
}



// Task 4

let arr1=["Ahmed","Sameh","Sayed"];
let arr2=["Mohamed","Gamal","Amir"];
let arr3=["Haytham","Shady","Mahmoud"];

let [c,,]=arr1;
let [,a,b]=arr3;
console.log(`My Best Friends: ${a}, ${b}, ${c}`);


// Task 5

let theNumber=100020003000;
let result = Number(theNumber.toString().replace(/0/g, ""));
console.log(result);


// Task 6
let chars=["A","B","C",20,"D","E",10,15,6];
let letters = chars.filter(item => typeof item === "string");

console.log(...letters.slice(0,4),...letters.slice(0,5));

// Task 7

let date1 = "25/10/1982";
let date2 = "25 - 10 - 1982";
let date3 = "25 10 1982";
let date4 = "25 10 82";

let re4 = /(\d{2})\s*[\/-\s]\s*(\d{2})\s*[\/-\s]\s*(\d{2,4})/; 

console.log(date1.match(re4)); 
console.log(date2.match(re4)); 
console.log(date3.match(re4)); 
console.log(date4.match(re4)); 




// Task 8

let url1 = 'elzero.org';
let url2 = 'http://elzero.org';
let url3 = 'https://elzero.org';
let url4 = 'https://www.elzero.org';
let url5 = 'https://www.elzero.org:8080/articles.php?id=100&cat=topics';

let re = /(https?:\/\/)?(www\.)?([\w]+\.\w+)(:\d+)?(\/.*)?/;

console.log(url1.match(re));
console.log(url2.match(re));
console.log(url3.match(re));
console.log(url4.match(re));
console.log(url5.match(re));






























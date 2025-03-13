/*first task*/
let mix = [1, 2, 3, "E", 4, "l", "z", "e", "r", 5, "o"];
let letters = mix.filter(item => typeof item === 'string').join('');
console.log(letters);


/*second task*/
let word = "EElllzzzzzzeroo";
let no_duplicate = [...new Set(word)].join('');
console.log(no_duplicate);


/*third task*/
function totalVotes(arr)
{
    let votedOnly = arr.filter(voter => voter.voted === true).length;
    console.log(votedOnly);

}

var voters=[
    {name:'Bob',age:30,voted:true},
    {name:'Jake',age:32,voted:true},
    {name:'Kate',age:25,voted:false},
    {name:'Sam',age:20,voted:false},
    {name:'Phil',age:21,voted:true},
    {name:'Ed',age:55,voted:true},
    {name:'Tami',age:54,voted:true},
    {name:'Mary',age:31,voted:false},
    {name:'Becky',age:43,voted:false},
    {name:'Joey',age:41,voted:true},
    {name:'Jeff',age:30,voted:true},
    {name:'Zack',age:19,voted:false}
];

console.log(totalVotes(voters));
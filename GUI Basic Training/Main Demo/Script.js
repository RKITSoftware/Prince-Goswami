// import User from "./User";
import User from "./User.js";

// decalre global variables and set its values
let user;

//Get the p tag and cite tag elements.
const pTag = $('#quote');
const citeTag = $('#label');

//show only on copy to clipboard
$('#alert-msg').hide();
pTag.hide();

//check if user is logged in 
function loadUSer() {
  //check for local user
  if (localStorage.getItem('user') !== null) {
    user = JSON.parse(localStorage.getItem('user'));
  }
  //check for session user
  else if (sessionStorage.getItem('user') !== null) {
    user = JSON.parse(sessionStorage.getItem('user'));
  }
  // console.log(user);
  return user;
}

//loadQuote on ptag and citetag
let loadQuote = (quote, author) => {
  pTag.hide(() => {
      //hide old pTag, change its content
    // console.log(quote);
    pTag.text(quote);
    citeTag.text(author);
  });
  pTag.show(1000);
};

//on logout button clicked
$("#logOut").click(() => {
  User.logOut(user.storageType);
  //replace username with login button
  $('.userName').html(`<a href="Login.html" title="Login" class="btn btn-outline-light">Login</a>`);
});

//on document load
$(document).ready(async () => {
  //check if user is logged in
  let user = loadUSer();

  //if user is logged in set username
  if (user !== undefined) {
    $('.userName').text("hii, " + user.userName);
  }
  //else set username to login button
  else {
    $('.userName').html(`<a href="Login.html" title="Login" class="btn btn-outline-light">Login</a>`);
    $("a.dropdown-item").hide();
  }

  //generate random quote on first load
  const url = 'https://quotes15.p.rapidapi.com/quotes/random/';
  const options = {
    method: 'GET',
    headers: {
      'X-RapidAPI-Key': 'fb9a25b181msh5c88c6d0aa36a4bp128766jsn26c140c6f064',
      'X-RapidAPI-Host': 'quotes15.p.rapidapi.com'
    }
  };
  fetchData(url, options)
    .then((data) => {
      let quote = data.content;
      let author = data.originator.name;
      loadQuote(quote, author);
    });
});

//Fetch data from server and parse it as JSON.
async function fetchData(url, options) {
  try {
    //fetch data from server and return it after JSON parsing
    const response = await fetch(url, options);
    const result = JSON.parse(await response.text());
    return result;
  } catch (error) {
    console.error(error);
    throw error;
  }
}

//generate random quote
const genRandom = async () => {
  //url for random quote generator
  ///source : randomQuotes
  const url = 'https://quotes15.p.rapidapi.com/quotes/random/';
  const options = {
    method: 'GET',
    headers: {
      'X-RapidAPI-Key': 'fb9a25b181msh5c88c6d0aa36a4bp128766jsn26c140c6f064',
      'X-RapidAPI-Host': 'quotes15.p.rapidapi.com'
    }
  };
  //call fetchData function 
  fetchData(url, options)
    .then((data) => {
      let quote = data.content;
      let author = data.originator.name;
      loadQuote(quote, author);
    });
}

//generate quote of the day
let genQTD =   () => {
  //source : quotes-motivational-quotes
  const url = 'https://quotes-inspirational-quotes-motivational-quotes.p.rapidapi.com/quote?token=ipworld.info';
  const options = {
    method: 'GET',
    headers: {
      'X-RapidAPI-Key': 'fb9a25b181msh5c88c6d0aa36a4bp128766jsn26c140c6f064',
      'X-RapidAPI-Host': 'quotes-inspirational-quotes-motivational-quotes.p.rapidapi.com'
    }
  };

  fetchData(url, options)
    .then(
      (data) => {
        pTag.hide(() => {
          let quote = data.text;
          let author = data.author;
          loadQuote(quote, author);
        });
      })
    .catch((error) => {
      console.error("Error occurred:", error);
    });
}

//generate quote by keyword
let genSBK = () => {

  let topic = $('#keyword').val();
  //check if topic is listed in topic list if not change it to all topics
  topic = topicList.includes(topic.toLowerCase()) ? topic.toLowerCase() : 'all';

  //source famous-quotes4
  const url = `https://famous-quotes4.p.rapidapi.com/random?category=${topic ? topic : 'all'}&count=1`;
  const options = {
    method: 'GET',
    headers: {
      'X-RapidAPI-Key': 'fb9a25b181msh5c88c6d0aa36a4bp128766jsn26c140c6f064',
      'X-RapidAPI-Host': 'famous-quotes4.p.rapidapi.com'
    }
  };

  fetchData(url, options)
    .then((result) => {
      result.map((data) => {
        let quote = data.text;
        let author = data.author;
        loadQuote(quote, author);
      });
    })
    .catch((error) => {
      console.error("Error occurred:", error);
    });
}

//define onclick handlers
$('#QOTD').click(genQTD);
$('#GRQ').click(genRandom)
$('#SBK').click(genSBK);


//share quote on whatsapp
$('#other-share').click(() => {
  const shareData = {
    text :  `${pTag.text()} : ${citeTag.text()}`
  }
  navigator.share(shareData)
    .then(() => {
      console.log('Shared successfully');
    })
    .catch((error) => {
      console.error('Share failed:', error);
    });
})

//save data to clipboard
$('#clipboard').click(() => {

  
  // Get the text to copy.
  const textToCopy = pTag.text() + ' ' + citeTag.text();
  $('#clipboard').hide();
  $('#alert-msg').text('copied to clipboard!!');
  // Copy the text to the clipboard.
  navigator.clipboard.writeText(textToCopy);
  $('#alert-msg').fadeIn();
  $('#alert-msg').fadeOut();
  $('#clipboard').show();
  // navigator.clipboard.readText()
  // .then(text => {
  //   console.log('Pasted content: ', text);
  // })
  // .catch(err => {
  //   console.error('Failed to read clipboard contents: ', err);
  // });
});


//topiclist
let topicList = [
  "age",
  "alone",
  "amazing",
  "anger",
  "anniversary",
  "architecture",
  "art",
  "attitude",
  "beauty",
  "best",
  "birthday",
  "business",
  "car",
  "change",
  "communication",
  "computers",
  "cool",
  "courage",
  "dad",
  "dating",
  "death",
  "design",
  "diet",
  "dreams",
  "education",
  "environmental",
  "equality",
  "experience",
  "failure",
  "faith",
  "family",
  "famous",
  "fear",
  "finance",
  "fitness",
  "food",
  "forgiveness",
  "freedom",
  "friendship",
  "funny",
  "future",
  "gardening",
  "god",
  "good",
  "government",
  "graduation",
  "great",
  "happiness",
  "health",
  "history",
  "home",
  "hope",
  "humor",
  "imagination",
  "inspirational",
  "intelligence",
  "jealousy",
  "knowledge",
  "leadership",
  "learning",
  "legal",
  "life",
  "love",
  "marriage",
  "medical",
  "men",
  "mom",
  "money",
  "morning",
  "motivational",
  "movies",
  "movingon",
  "music",
  "nature",
  "parenting",
  "patience",
  "patriotism",
  "peace",
  "pet",
  "poetry",
  "politics",
  "positive",
  "power",
  "relationship",
  "religion",
  "respect",
  "romantic",
  "sad",
  "science",
  "smile",
  "society",
  "sports",
  "strength",
  "success",
  "sympathy",
  "teacher",
  "technology",
  "teen",
  "thankful",
  "time",
  "travel",
  "trust",
  "truth",
  "war",
  "wedding",
  "wisdom",
  "women",
  "work",
  "christmas",
  "easter",
  "fathersday",
  "memorialday",
  "mothersday",
  "newyears",
  "saintpatricksday",
  "thanksgiving",
  "valentinesday"
]